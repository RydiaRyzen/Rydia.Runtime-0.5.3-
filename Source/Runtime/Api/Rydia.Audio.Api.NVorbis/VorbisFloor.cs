/****************************************************************************
 * NVorbis                                                                  *
 * Copyright (C) 2012, Andrew Ward <afward@gmail.com>                       *
 *                                                                          *
 * See COPYING for license terms (Ms-PL).                                   *
 *                                                                          *
 ***************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Rydia.Audio.Api.NVorbis
{
    abstract class VorbisFloor
    {
        internal static VorbisFloor Init(VorbisStreamDecoder vorbis, DataPacket packet)
        {
			int type = (int)packet.ReadBits(16);

            VorbisFloor floor = null;
            switch (type)
            {
                case 0: floor = new Floor0(vorbis); break;
                case 1: floor = new Floor1(vorbis); break;
            }
            if (floor == null) throw new InvalidDataException();

            floor.Init(packet);
            return floor;
        }

        VorbisStreamDecoder _vorbis;

        protected VorbisFloor(VorbisStreamDecoder vorbis)
        {
			this._vorbis = vorbis;
        }

        abstract protected void Init(DataPacket packet);

        abstract internal PacketData UnpackPacket(DataPacket packet, int blockSize, int channel);

        abstract internal void Apply(PacketData packetData, float[] residue);

        abstract internal class PacketData
        {
            internal int BlockSize;
            abstract protected bool HasEnergy { get; }
            internal bool ForceEnergy { get; set; }
            internal bool ForceNoEnergy { get; set; }

			internal bool ExecuteChannel
			{
				get
				{
					// if we have energy or are forcing energy, return !ForceNoEnergy, else false
					return (ForceEnergy | HasEnergy) & !ForceNoEnergy;
				}
			}
		}

        class Floor0 : VorbisFloor
        {
            internal Floor0(VorbisStreamDecoder vorbis) : base(vorbis) { }

            int _order, _rate, _bark_map_size, _ampBits, _ampOfs, _ampDiv;
            VorbisCodebook[] _books;
            int _bookBits;
            Dictionary<int, float[]> _wMap;
            Dictionary<int, int[]> _barkMaps;

            protected override void Init(DataPacket packet)
            {
				// this is pretty well stolen directly from libvorbis...  BSD license
				this._order = (int)packet.ReadBits(8);
				this._rate = (int)packet.ReadBits(16);
				this._bark_map_size = (int)packet.ReadBits(16);
				this._ampBits = (int)packet.ReadBits(6);
				this._ampOfs = (int)packet.ReadBits(8);
				this._books = new VorbisCodebook[(int)packet.ReadBits(4) + 1];

                if (this._order < 1 || this._rate < 1 || this._bark_map_size < 1 || this._books.Length == 0) throw new InvalidDataException();

				this._ampDiv = (1 << this._ampBits) - 1;

                for (int i = 0; i < this._books.Length; i++)
                {
					int num = (int)packet.ReadBits(8);
                    if (num < 0 || num >= this._vorbis.Books.Length) throw new InvalidDataException();
					VorbisCodebook book = this._vorbis.Books[num];

                    if (book.MapType == 0 || book.Dimensions < 1) throw new InvalidDataException();

					this._books[i] = book;
                }
				this._bookBits = Utils.ilog(this._books.Length);

				this._barkMaps = new Dictionary<int, int[]>();
				this._barkMaps[this._vorbis.Block0Size] = SynthesizeBarkCurve(this._vorbis.Block0Size / 2);
				this._barkMaps[this._vorbis.Block1Size] = SynthesizeBarkCurve(this._vorbis.Block1Size / 2);

				this._wMap = new Dictionary<int, float[]>();
				this._wMap[this._vorbis.Block0Size] = SynthesizeWDelMap(this._vorbis.Block0Size / 2);
				this._wMap[this._vorbis.Block1Size] = SynthesizeWDelMap(this._vorbis.Block1Size / 2);

				this._reusablePacketData = new PacketData0[this._vorbis._channels];
                for (int i = 0; i < this._reusablePacketData.Length; i++)
                {
					this._reusablePacketData[i] = new PacketData0() { Coeff = new float[this._order + 1] };
                }
            }

            int[] SynthesizeBarkCurve(int n)
            {
				float scale = this._bark_map_size / toBARK(this._rate / 2);

				int[] map = new int[n + 1];

                for (int i = 0; i < n - 1; i++)
                {
                    map[i] = Math.Min(this._bark_map_size - 1, (int)Math.Floor(toBARK((this._rate / 2f) / n * i) * scale));
                }
                map[n] = -1;
                return map;
            }

            static float toBARK(double lsp)
            {
                return (float)(13.1 * Math.Atan(0.00074 * lsp) + 2.24 * Math.Atan(0.0000000185 * lsp * lsp) + .0001 * lsp);
            }

            float[] SynthesizeWDelMap(int n)
            {
				float wdel = (float)(Math.PI / this._bark_map_size);

				float[] map = new float[n];
                for (int i = 0; i < n; i++)
                {
                    map[i] = 2f * (float)Math.Cos(wdel * i);
                }
                return map;
            }

            class PacketData0 : PacketData
            {
				protected override bool HasEnergy
				{
					get
					{
						return this.Amp > 0f;
					}
				}

				internal float[] Coeff;
                internal float Amp;
            }

            PacketData0[] _reusablePacketData;

            internal override PacketData UnpackPacket(DataPacket packet, int blockSize, int channel)
            {
				PacketData0 data = this._reusablePacketData[channel];
                data.BlockSize = blockSize;
                data.ForceEnergy = false;
                data.ForceNoEnergy = false;

                data.Amp = packet.ReadBits(this._ampBits);
                if (data.Amp > 0f)
                {
                    // this is pretty well stolen directly from libvorbis...  BSD license
                    Array.Clear(data.Coeff, 0, data.Coeff.Length);

                    data.Amp = (float)(data.Amp / this._ampDiv * this._ampOfs);

					uint bookNum = (uint)packet.ReadBits(this._bookBits);
                    if (bookNum >= this._books.Length)
                    {
                        // we ran out of data or the packet is corrupt...  0 the floor and return
                        data.Amp = 0;
                        return data;
                    }
					VorbisCodebook book = this._books[bookNum];

                    // first, the book decode...
                    for (int i = 0; i < this._order; )
                    {
						int entry = book.DecodeScalar(packet);
                        if (entry == -1)
                        {
                            // we ran out of data or the packet is corrupt...  0 the floor and return
                            data.Amp = 0;
                            return data;
                        }
                        for (int j = 0; i < this._order && j < book.Dimensions; j++, i++)
                        {
                            data.Coeff[i] = book[entry, j];
                        }
                    }

					// then, the "averaging"
					float last = 0f;
                    for (int j = 0; j < this._order; )
                    {
                        for (int k = 0; j < this._order && k < book.Dimensions; j++, k++)
                        {
                            data.Coeff[j] += last;
                        }
                        last = data.Coeff[j - 1];
                    }
                }
                return data;
            }

            internal override void Apply(PacketData packetData, float[] residue)
            {
				PacketData0 data = packetData as PacketData0;
                if (data == null) throw new ArgumentException("Incorrect packet data!");

				int n = data.BlockSize / 2;

                if (data.Amp > 0f)
                {
					// this is pretty well stolen directly from libvorbis...  BSD license
					int[] barkMap = this._barkMaps[data.BlockSize];
					float[] wMap = this._wMap[data.BlockSize];

                    int i = 0;
                    for (i = 0; i < this._order; i++)
                    {
                        data.Coeff[i] = 2f * (float)Math.Cos(data.Coeff[i]);
                    }

                    i = 0;
                    while (i < n)
                    {
                        int j;
						int k = barkMap[i];
						float p = .5f;
						float q = .5f;
						float w = wMap[k];
                        for (j = 1; j < this._order; j += 2)
                        {
                            q *= w - data.Coeff[j - 1];
                            p *= w - data.Coeff[j];
                        }
                        if (j == this._order)
                        {
                            // odd order filter; slightly assymetric
                            q *= w - data.Coeff[j - 1];
                            p *= p * (4f - w * w);
                            q *= q;
                        }
                        else
                        {
                            // even order filter; still symetric
                            p *= p * (2f - w);
                            q *= q * (2f + w);
                        }

                        // calc the dB of this bark section
                        q = data.Amp / (float)Math.Sqrt(p + q) - this._ampOfs;

                        // now convert to a linear sample multiplier
                        q = (float)Math.Exp(q * 0.11512925f);

                        residue[i] *= q;

                        while (barkMap[++i] == k) residue[i] *= q;
                    }
                }
                else
                {
                    Array.Clear(residue, 0, n);
                }
            }
        }

        class Floor1 : VorbisFloor
        {
            internal Floor1(VorbisStreamDecoder vorbis) : base(vorbis) { }

            int[] _partitionClass, _classDimensions, _classSubclasses, _xList, _classMasterBookIndex, _hNeigh, _lNeigh, _sortIdx;
            int _multiplier, _range, _yBits;
            VorbisCodebook[] _classMasterbooks;
            VorbisCodebook[][] _subclassBooks;
            int[][] _subclassBookIndex;

            static int[] _rangeLookup = { 256, 128, 86, 64 };
            static int[] _yBitsLookup = { 8, 7, 7, 6 };

            protected override void Init(DataPacket packet)
            {
				this._partitionClass = new int[(int)packet.ReadBits(5)];
                for (int i = 0; i < this._partitionClass.Length; i++)
                {
					this._partitionClass[i] = (int)packet.ReadBits(4);
                }

				int maximum_class = this._partitionClass.Max();
				this._classDimensions = new int[maximum_class + 1];
				this._classSubclasses = new int[maximum_class + 1];
				this._classMasterbooks = new VorbisCodebook[maximum_class + 1];
				this._classMasterBookIndex = new int[maximum_class + 1];
				this._subclassBooks = new VorbisCodebook[maximum_class + 1][];
				this._subclassBookIndex = new int[maximum_class + 1][];
                for (int i = 0; i <= maximum_class; i++)
                {
					this._classDimensions[i] = (int)packet.ReadBits(3) + 1;
					this._classSubclasses[i] = (int)packet.ReadBits(2);
                    if (this._classSubclasses[i] > 0)
                    {
						this._classMasterBookIndex[i] = (int)packet.ReadBits(8);
						this._classMasterbooks[i] = this._vorbis.Books[this._classMasterBookIndex[i]];
                    }

					this._subclassBooks[i] = new VorbisCodebook[1 << this._classSubclasses[i]];
					this._subclassBookIndex[i] = new int[this._subclassBooks[i].Length];
                    for (int j = 0; j < this._subclassBooks[i].Length; j++)
                    {
						int bookNum = (int)packet.ReadBits(8) - 1;
                        if (bookNum >= 0) this._subclassBooks[i][j] = this._vorbis.Books[bookNum];
						this._subclassBookIndex[i][j] = bookNum;
                    }
                }

				this._multiplier = (int)packet.ReadBits(2);

				this._range = _rangeLookup[this._multiplier];
				this._yBits = _yBitsLookup[this._multiplier];

                ++this._multiplier;

				int rangeBits = (int)packet.ReadBits(4);

				List<int> xList = new List<int>();
                xList.Add(0);
                xList.Add(1 << rangeBits);
                
                for (int i = 0; i < this._partitionClass.Length; i++)
                {
					int classNum = this._partitionClass[i];
                    for (int j = 0; j < this._classDimensions[classNum]; j++)
                    {
                        xList.Add((int)packet.ReadBits(rangeBits));
                    }
                }
				this._xList = xList.ToArray();

				// precalc the low and high neighbors (and init the sort table)
				this._lNeigh = new int[xList.Count];
				this._hNeigh = new int[xList.Count];
				this._sortIdx = new int[xList.Count];
				this._sortIdx[0] = 0;
				this._sortIdx[1] = 1;
                for (int i = 2; i < this._lNeigh.Length; i++)
                {
					this._lNeigh[i] = 0;
					this._hNeigh[i] = 1;
					this._sortIdx[i] = i;
                    for (int j = 2; j < i; j++)
                    {
						int temp = this._xList[j];
                        if (temp < this._xList[i])
                        {
                            if (temp > this._xList[this._lNeigh[i]]) this._lNeigh[i] = j;
                        }
                        else
                        {
                            if (temp < this._xList[this._hNeigh[i]]) this._hNeigh[i] = j;
                        }
                    }
                }

                // precalc the sort table
                for (int i = 0; i < this._sortIdx.Length - 1; i++)
                {
                    for (int j = i + 1; j < this._sortIdx.Length; j++)
                    {
                        if (this._xList[i] == this._xList[j]) throw new InvalidDataException();

                        if (this._xList[this._sortIdx[i]] > this._xList[this._sortIdx[j]])
                        {
							// swap the sort indexes
							int temp = this._sortIdx[i];
							this._sortIdx[i] = this._sortIdx[j];
							this._sortIdx[j] = temp;
                        }
                    }
                }

				// pre-create our packet data instances
				this._reusablePacketData = new PacketData1[this._vorbis._channels];
                for (int i = 0; i < this._reusablePacketData.Length; i++)
                {
					this._reusablePacketData[i] = new PacketData1();
                }
            }

            class PacketData1 : PacketData
            {
				protected override bool HasEnergy
				{
					get
					{
						return this.PostCount > 0;
					}
				}

				public int[] Posts = new int[64];
                public int PostCount;
            }

            PacketData1[] _reusablePacketData;

            internal override PacketData UnpackPacket(DataPacket packet, int blockSize, int channel)
            {
				PacketData1 data = this._reusablePacketData[channel];
                data.BlockSize = blockSize;
                data.ForceEnergy = false;
                data.ForceNoEnergy = false;
                data.PostCount = 0;
                Array.Clear(data.Posts, 0, 64);

                // hoist ReadPosts to here since that's all we're doing...
                if (packet.ReadBit())
                {
					int postCount = 2;
                    data.Posts[0] = (int)packet.ReadBits(this._yBits);
                    data.Posts[1] = (int)packet.ReadBits(this._yBits);

                    for (int i = 0; i < this._partitionClass.Length; i++)
                    {
						int clsNum = this._partitionClass[i];
						int cdim = this._classDimensions[clsNum];
						int cbits = this._classSubclasses[clsNum];
						int csub = (1 << cbits) - 1;
						uint cval = 0U;
                        if (cbits > 0)
                        {
                            if ((cval = (uint)this._classMasterbooks[clsNum].DecodeScalar(packet)) == uint.MaxValue)
                            {
                                // we read a bad value...  bail
                                postCount = 0;
                                break;
                            }
                        }
                        for (int j = 0; j < cdim; j++)
                        {
							VorbisCodebook book = this._subclassBooks[clsNum][cval & csub];
                            cval >>= cbits;
                            if (book != null)
                            {
                                if ((data.Posts[postCount] = book.DecodeScalar(packet)) == -1)
                                {
                                    // we read a bad value... bail
                                    postCount = 0;
                                    i = this._partitionClass.Length;
                                    break;
                                }
                            }
                            ++postCount;
                        }
                    }

                    data.PostCount = postCount;
                }

                return data;
            }

            internal override void Apply(PacketData packetData, float[] residue)
            {
				PacketData1 data = packetData as PacketData1;
                if (data == null) throw new ArgumentException("Incorrect packet data!", "packetData");

				int n = data.BlockSize / 2;

                if (data.PostCount > 0)
                {
					bool[] stepFlags = UnwrapPosts(data);

					int lx = 0;
					int ly = data.Posts[0] * this._multiplier;
                    for (int i = 1; i < data.PostCount; i++)
                    {
						int idx = this._sortIdx[i];

                        if (stepFlags[idx])
                        {
							int hx = this._xList[idx];
							int hy = data.Posts[idx] * this._multiplier;
                            if (lx < n) RenderLineMulti(lx, ly, Math.Min(hx, n), hy, residue);
                            lx = hx;
                            ly = hy;
                        }
                        if (lx >= n) break;
                    }

                    if (lx < n)
                    {
                        RenderLineMulti(lx, ly, n, ly, residue);
                    }
                }
                else
                {
                    Array.Clear(residue, 0, n);
                }
            }

            bool[] _stepFlags = new bool[64];
            int[] _finalY = new int[64];

            bool[] UnwrapPosts(PacketData1 data)
            {
                Array.Clear(this._stepFlags, 2, 62);
				this._stepFlags[0] = true;
				this._stepFlags[1] = true;

                Array.Clear(this._finalY, 2, 62);
				this._finalY[0] = data.Posts[0];
				this._finalY[1] = data.Posts[1];

                for (int i = 2; i < data.PostCount; i++)
                {
					int lowOfs = this._lNeigh[i];
					int highOfs = this._hNeigh[i];

					int predicted = RenderPoint(this._xList[lowOfs], this._finalY[lowOfs], this._xList[highOfs], this._finalY[highOfs], this._xList[i]);

					int val = data.Posts[i];
					int highroom = this._range - predicted;
					int lowroom = predicted;
                    int room;
                    if (highroom < lowroom)
                    {
                        room = highroom * 2;
                    }
                    else
                    {
                        room = lowroom * 2;
                    }
                    if (val != 0)
                    {
						this._stepFlags[lowOfs] = true;
						this._stepFlags[highOfs] = true;
						this._stepFlags[i] = true;

                        if (val >= room)
                        {
                            if (highroom > lowroom)
                            {
								this._finalY[i] = val - lowroom + predicted;
                            }
                            else
                            {
								this._finalY[i] = predicted - val + highroom - 1;
                            }
                        }
                        else
                        {
                            if ((val % 2) == 1)
                            {
								// odd
								this._finalY[i] = predicted - ((val + 1) / 2);
                            }
                            else
                            {
								// even
								this._finalY[i] = predicted + (val / 2);
                            }
                        }
                    }
                    else
                    {
						this._stepFlags[i] = false;
						this._finalY[i] = predicted;
                    }
                }

                for (int i = 0; i < data.PostCount; i++)
                {
                    data.Posts[i] = this._finalY[i];
                }

                return this._stepFlags;
            }

            int RenderPoint(int x0, int y0, int x1, int y1, int X)
            {
				int dy = y1 - y0;
				int adx = x1 - x0;
				int ady = Math.Abs(dy);
				int err = ady * (X - x0);
				int off = err / adx;
                if (dy < 0)
                {
                    return y0 - off;
                }
                else
                {
                    return y0 + off;
                }
            }

            void RenderLineMulti(int x0, int y0, int x1, int y1, float[] v)
            {
				int dy = y1 - y0;
				int adx = x1 - x0;
				int ady = Math.Abs(dy);
				int sy = 1 - (((dy >> 31) & 1) * 2);
				int b = dy / adx;
				int x = x0;
				int y = y0;
				int err = -adx;

                v[x0] *= inverse_dB_table[y0];
                ady -= Math.Abs(b) * adx;

                while (++x < x1)
                {
                    y += b;
                    err += ady;
                    if (err >= 0)
                    {
                        err -= adx;
                        y += sy;
                    }
                    v[x] *= inverse_dB_table[y];
                }
            }

            #region dB inversion table

            static readonly float[] inverse_dB_table = {
                                                        1.0649863e-07f, 1.1341951e-07f, 1.2079015e-07f, 1.2863978e-07f, 
                                                        1.3699951e-07f, 1.4590251e-07f, 1.5538408e-07f, 1.6548181e-07f, 
                                                        1.7623575e-07f, 1.8768855e-07f, 1.9988561e-07f, 2.1287530e-07f, 
                                                        2.2670913e-07f, 2.4144197e-07f, 2.5713223e-07f, 2.7384213e-07f, 
                                                        2.9163793e-07f, 3.1059021e-07f, 3.3077411e-07f, 3.5226968e-07f, 
                                                        3.7516214e-07f, 3.9954229e-07f, 4.2550680e-07f, 4.5315863e-07f, 
                                                        4.8260743e-07f, 5.1396998e-07f, 5.4737065e-07f, 5.8294187e-07f, 
                                                        6.2082472e-07f, 6.6116941e-07f, 7.0413592e-07f, 7.4989464e-07f, 
                                                        7.9862701e-07f, 8.5052630e-07f, 9.0579828e-07f, 9.6466216e-07f, 
                                                        1.0273513e-06f, 1.0941144e-06f, 1.1652161e-06f, 1.2409384e-06f, 
                                                        1.3215816e-06f, 1.4074654e-06f, 1.4989305e-06f, 1.5963394e-06f, 
                                                        1.7000785e-06f, 1.8105592e-06f, 1.9282195e-06f, 2.0535261e-06f, 
                                                        2.1869758e-06f, 2.3290978e-06f, 2.4804557e-06f, 2.6416497e-06f, 
                                                        2.8133190e-06f, 2.9961443e-06f, 3.1908506e-06f, 3.3982101e-06f, 
                                                        3.6190449e-06f, 3.8542308e-06f, 4.1047004e-06f, 4.3714470e-06f, 
                                                        4.6555282e-06f, 4.9580707e-06f, 5.2802740e-06f, 5.6234160e-06f, 
                                                        5.9888572e-06f, 6.3780469e-06f, 6.7925283e-06f, 7.2339451e-06f, 
                                                        7.7040476e-06f, 8.2047000e-06f, 8.7378876e-06f, 9.3057248e-06f, 
                                                        9.9104632e-06f, 1.0554501e-05f, 1.1240392e-05f, 1.1970856e-05f, 
                                                        1.2748789e-05f, 1.3577278e-05f, 1.4459606e-05f, 1.5399272e-05f, 
                                                        1.6400004e-05f, 1.7465768e-05f, 1.8600792e-05f, 1.9809576e-05f, 
                                                        2.1096914e-05f, 2.2467911e-05f, 2.3928002e-05f, 2.5482978e-05f, 
                                                        2.7139006e-05f, 2.8902651e-05f, 3.0780908e-05f, 3.2781225e-05f, 
                                                        3.4911534e-05f, 3.7180282e-05f, 3.9596466e-05f, 4.2169667e-05f, 
                                                        4.4910090e-05f, 4.7828601e-05f, 5.0936773e-05f, 5.4246931e-05f, 
                                                        5.7772202e-05f, 6.1526565e-05f, 6.5524908e-05f, 6.9783085e-05f, 
                                                        7.4317983e-05f, 7.9147585e-05f, 8.4291040e-05f, 8.9768747e-05f, 
                                                        9.5602426e-05f, 0.00010181521f, 0.00010843174f, 0.00011547824f, 
                                                        0.00012298267f, 0.00013097477f, 0.00013948625f, 0.00014855085f, 
                                                        0.00015820453f, 0.00016848555f, 0.00017943469f, 0.00019109536f, 
                                                        0.00020351382f, 0.00021673929f, 0.00023082423f, 0.00024582449f, 
                                                        0.00026179955f, 0.00027881276f, 0.00029693158f, 0.00031622787f, 
                                                        0.00033677814f, 0.00035866388f, 0.00038197188f, 0.00040679456f, 
                                                        0.00043323036f, 0.00046138411f, 0.00049136745f, 0.00052329927f, 
                                                        0.00055730621f, 0.00059352311f, 0.00063209358f, 0.00067317058f, 
                                                        0.00071691700f, 0.00076350630f, 0.00081312324f, 0.00086596457f, 
                                                        0.00092223983f, 0.00098217216f, 0.0010459992f,  0.0011139742f, 
                                                        0.0011863665f,  0.0012634633f,  0.0013455702f,  0.0014330129f, 
                                                        0.0015261382f,  0.0016253153f,  0.0017309374f,  0.0018434235f, 
                                                        0.0019632195f,  0.0020908006f,  0.0022266726f,  0.0023713743f, 
                                                        0.0025254795f,  0.0026895994f,  0.0028643847f,  0.0030505286f, 
                                                        0.0032487691f,  0.0034598925f,  0.0036847358f,  0.0039241906f, 
                                                        0.0041792066f,  0.0044507950f,  0.0047400328f,  0.0050480668f, 
                                                        0.0053761186f,  0.0057254891f,  0.0060975636f,  0.0064938176f, 
                                                        0.0069158225f,  0.0073652516f,  0.0078438871f,  0.0083536271f, 
                                                        0.0088964928f,  0.009474637f,   0.010090352f,   0.010746080f, 
                                                        0.011444421f,   0.012188144f,   0.012980198f,   0.013823725f, 
                                                        0.014722068f,   0.015678791f,   0.016697687f,   0.017782797f, 
                                                        0.018938423f,   0.020169149f,   0.021479854f,   0.022875735f, 
                                                        0.024362330f,   0.025945531f,   0.027631618f,   0.029427276f, 
                                                        0.031339626f,   0.033376252f,   0.035545228f,   0.037855157f, 
                                                        0.040315199f,   0.042935108f,   0.045725273f,   0.048696758f, 
                                                        0.051861348f,   0.055231591f,   0.058820850f,   0.062643361f, 
                                                        0.066714279f,   0.071049749f,   0.075666962f,   0.080584227f, 
                                                        0.085821044f,   0.091398179f,   0.097337747f,   0.10366330f, 
                                                        0.11039993f,    0.11757434f,    0.12521498f,    0.13335215f, 
                                                        0.14201813f,    0.15124727f,    0.16107617f,    0.17154380f, 
                                                        0.18269168f,    0.19456402f,    0.20720788f,    0.22067342f, 
                                                        0.23501402f,    0.25028656f,    0.26655159f,    0.28387361f, 
                                                        0.30232132f,    0.32196786f,    0.34289114f,    0.36517414f, 
                                                        0.38890521f,    0.41417847f,    0.44109412f,    0.46975890f, 
                                                        0.50028648f,    0.53279791f,    0.56742212f,    0.60429640f, 
                                                        0.64356699f,    0.68538959f,    0.72993007f,    0.77736504f, 
                                                        0.82788260f,    0.88168307f,    0.9389798f,     1.0f
                                                        };

            #endregion
        }
    }
}