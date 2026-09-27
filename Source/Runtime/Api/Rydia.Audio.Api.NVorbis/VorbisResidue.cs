/****************************************************************************
 * NVorbis                                                                  *
 * Copyright (C) 2012, Andrew Ward <afward@gmail.com>                       *
 *                                                                          *
 * See COPYING for license terms (Ms-PL).                                   *
 *                                                                          *
 ***************************************************************************/
using System;
using System.Linq;
using System.IO;

namespace Rydia.Audio.Api.NVorbis
{
    abstract class VorbisResidue
    {
        internal static VorbisResidue Init(VorbisStreamDecoder vorbis, DataPacket packet)
        {
			int type = (int)packet.ReadBits(16);

            VorbisResidue residue = null;
            switch (type)
            {
                case 0: residue = new Residue0(vorbis); break;
                case 1: residue = new Residue1(vorbis); break;
                case 2: residue = new Residue2(vorbis); break;
            }
            if (residue == null) throw new InvalidDataException();

            residue.Init(packet);
            return residue;
        }

        static int icount(int v)
        {
			int ret = 0;
            while (v != 0)
            {
                ret += (v & 1);
                v >>= 1;
            }
            return ret;
        }

        VorbisStreamDecoder _vorbis;
        float[][] _residue;

        protected VorbisResidue(VorbisStreamDecoder vorbis)
        {
			this._vorbis = vorbis;

			this._residue = new float[this._vorbis._channels][];
            for (int i = 0; i < this._vorbis._channels; i++)
            {
				this._residue[i] = new float[this._vorbis.Block1Size];
            }
        }

        protected float[][] GetResidueBuffer(int channels)
        {
			float[][] temp = this._residue;
            if (channels < this._vorbis._channels)
            {
                temp = new float[channels][];
                Array.Copy(this._residue, temp, channels);
            }
            for (int i = 0; i < channels; i++)
            {
                Array.Clear(temp[i], 0, temp[i].Length);
            }
            return temp;
        }

        abstract internal float[][] Decode(DataPacket packet, bool[] doNotDecode, int channels, int blockSize);

        abstract protected void Init(DataPacket packet);

        // residue type 0... samples are grouped by channel, then stored with non-interleaved dimensions (d0, d0, d0, d0, ..., d1, d1, d1, d1, ..., d2, d2, d2, d2, etc...)
        class Residue0 : VorbisResidue
        {
            int _begin;
            int _end;
            int _partitionSize;
            int _classifications;
            int _maxStages;

            VorbisCodebook[][] _books;
            VorbisCodebook _classBook;

            int[] _cascade, _entryCache;
            int[][] _decodeMap;
            int[][][] _partWordCache;

            internal Residue0(VorbisStreamDecoder vorbis) : base(vorbis) { }

            protected override void Init(DataPacket packet)
            {
				// this is pretty well stolen directly from libvorbis...  BSD license
				this._begin = (int)packet.ReadBits(24);
				this._end = (int)packet.ReadBits(24);
				this._partitionSize = (int)packet.ReadBits(24) + 1;
				this._classifications = (int)packet.ReadBits(6) + 1;
				this._classBook = this._vorbis.Books[(int)packet.ReadBits(8)];

				this._cascade = new int[this._classifications];
				int acc = 0;
                for (int i = 0; i < this._classifications; i++)
                {
					int low_bits = (int)packet.ReadBits(3);
                    if (packet.ReadBit())
                    {
						this._cascade[i] = (int)packet.ReadBits(5) << 3 | low_bits;
                    }
                    else
                    {
						this._cascade[i] = low_bits;
                    }
                    acc += icount(this._cascade[i]);
                }

				int[] bookNums = new int[acc];
                for (int i = 0; i < acc; i++)
                {
                    bookNums[i] = (int)packet.ReadBits(8);
                    if (this._vorbis.Books[bookNums[i]].MapType == 0) throw new InvalidDataException();
                }

				int entries = this._classBook.Entries;
				int dim = this._classBook.Dimensions;
				int partvals = 1;
                while (dim > 0)
                {
                    partvals *= this._classifications;
                    if (partvals > entries) throw new InvalidDataException();
                    --dim;
                }

                // now the lookups
                dim = this._classBook.Dimensions;

				this._books = new VorbisCodebook[this._classifications][];

                acc = 0;
				int maxstage = 0;
                int stages;
                for (int j = 0; j < this._classifications; j++)
                {
                    stages = Utils.ilog(this._cascade[j]);
					this._books[j] = new VorbisCodebook[stages];
                    if (stages > 0)
                    {
                        maxstage = Math.Max(maxstage, stages);
                        for (int k = 0; k < stages; k++)
                        {
                            if ((this._cascade[j] & (1 << k)) > 0)
                            {
								this._books[j][k] = this._vorbis.Books[bookNums[acc++]];
                            }
                        }
                    }
                }
				this._maxStages = maxstage;

				this._decodeMap = new int[partvals][];
                for (int j = 0; j < partvals; j++)
                {
					int val = j;
					int mult = partvals / this._classifications;
					this._decodeMap[j] = new int[this._classBook.Dimensions];
                    for (int k = 0; k < this._classBook.Dimensions; k++)
                    {
						int deco = val / mult;
                        val -= deco * mult;
                        mult /= this._classifications;
						this._decodeMap[j][k] = deco;
                    }
                }

				this._entryCache = new int[this._partitionSize];

				this._partWordCache = new int[this._vorbis._channels][][];
				int maxPartWords = ((this._end - this._begin) / this._partitionSize + this._classBook.Dimensions - 1) / this._classBook.Dimensions;
                for (int ch = 0; ch < this._vorbis._channels; ch++)
                {
					this._partWordCache[ch] = new int[maxPartWords][];
                }
            }

            internal override float[][] Decode(DataPacket packet, bool[] doNotDecode, int channels, int blockSize)
            {
				float[][] residue = GetResidueBuffer(doNotDecode.Length);

				// this is pretty well stolen directly from libvorbis...  BSD license
				int end = this._end < blockSize / 2 ? this._end : blockSize / 2;
				int n = end - this._begin;

                if (n > 0 && doNotDecode.Contains(false))
                {
					int partVals = n / this._partitionSize;

					int partWords = (partVals + this._classBook.Dimensions - 1) / this._classBook.Dimensions;
                    for (int j = 0; j < channels; j++)
                    {
                        Array.Clear(this._partWordCache[j], 0, partWords);
                    }

                    for (int s = 0; s < this._maxStages; s++)
                    {
                        for (int i = 0, l = 0; i < partVals; l++)
                        {
                            if (s == 0)
                            {
                                for (int j = 0; j < channels; j++)
                                {
                                    try
                                    {
										this._partWordCache[j][l] = this._decodeMap[this._classBook.DecodeScalar(packet)];
                                    }
                                    catch (IndexOutOfRangeException)
                                    {
                                        i = partVals;
                                        s = this._maxStages;
                                        break;
                                    }
                                }
                            }
                            for (int k = 0; i < partVals && k < this._classBook.Dimensions; k++, i++)
                            {
								int offset = this._begin + i * this._partitionSize;
                                for (int j = 0; j < channels; j++)
                                {
									int idx = this._partWordCache[j][l][k];
                                    if ((this._cascade[idx] & (1 << s)) != 0)
                                    {
										VorbisCodebook book = this._books[idx][s];
                                        if (book != null)
                                        {
                                            if (WriteVectors(book, packet, residue, j, offset, this._partitionSize))
                                            {
                                                // bad packet...  exit now and try to use what we already have
                                                i = partVals;
                                                s = this._maxStages;
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                return residue;
            }

            virtual protected bool WriteVectors(VorbisCodebook codebook, DataPacket packet, float[][] residue, int channel, int offset, int partitionSize)
            {
				float[] res = residue[channel];
				int step = partitionSize / codebook.Dimensions;

                for (int i = 0; i < step; i++)
                {
                    if ((this._entryCache[i] = codebook.DecodeScalar(packet)) == -1)
                    {
                        return true;
                    }
                }
                for (int i = 0; i < codebook.Dimensions; i++)
                {
                    for (int j = 0; j < step; j++, offset++)
                    {
                        res[offset] += codebook[this._entryCache[j], i];
                    }
                }
                return false;
            }
        }

        // residue type 1... samples are grouped by channel, then stored with interleaved dimensions (d0, d1, d2, d0, d1, d2, etc...)
        class Residue1 : Residue0
        {
            internal Residue1(VorbisStreamDecoder vorbis) : base(vorbis) { }

            protected override bool WriteVectors(VorbisCodebook codebook, DataPacket packet, float[][] residue, int channel, int offset, int partitionSize)
            {
				float[] res = residue[channel];

                for (int i = 0; i < partitionSize; )
                {
					int entry = codebook.DecodeScalar(packet);
                    if (entry == -1)
                    {
                        return true;
                    }
                    for (int j = 0; j < codebook.Dimensions; i++, j++)
                    {
                        res[offset + i] += codebook[entry, j];
                    }
                }

                return false;
            }
        }

        // residue type 2... basically type 0, but samples are interleaved between channels (ch0, ch1, ch0, ch1, etc...)
        class Residue2 : Residue0
        {
            int _channels;

            internal Residue2(VorbisStreamDecoder vorbis) : base(vorbis) { }

            // We can use the type 0 logic by saying we're doing a single channel buffer big enough to hold the samples for all channels
            // This works because WriteVectors(...) "knows" the correct channel count and processes the data accordingly.
            internal override float[][] Decode(DataPacket packet, bool[] doNotDecode, int channels, int blockSize)
            {
				this._channels = channels;

                return base.Decode(packet, doNotDecode, 1, blockSize * channels);
            }

            protected override bool WriteVectors(VorbisCodebook codebook, DataPacket packet, float[][] residue, int channel, int offset, int partitionSize)
            {
				int chPtr = 0;

                offset /= this._channels;
                for (int c = 0; c < partitionSize; )
                {
					int entry = codebook.DecodeScalar(packet);
                    if (entry == -1)
                    {
                        return true;
                    }
                    for (int d = 0; d < codebook.Dimensions; d++, c++)
                    {
                        residue[chPtr][offset] += codebook[entry, d];
                        if (++chPtr == this._channels)
                        {
                            chPtr = 0;
                            offset++;
                        }
                    }
                }

                return false;
            }
        }
    }
}
