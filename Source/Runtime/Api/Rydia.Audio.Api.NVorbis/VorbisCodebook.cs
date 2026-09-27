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
    class VorbisCodebook
    {
        internal static VorbisCodebook Init(VorbisStreamDecoder vorbis, DataPacket packet, int number)
        {
			VorbisCodebook temp = new VorbisCodebook();
            temp.BookNum = number;
            temp.Init(packet);
            return temp;
        }

        private VorbisCodebook()
        {

        }

        internal void Init(DataPacket packet)
        {
			// first, check the sync pattern
			ulong chkVal = packet.ReadBits(24);
            if (chkVal != 0x564342UL) throw new InvalidDataException();

			// get the counts
			this.Dimensions = (int)packet.ReadBits(16);
			this.Entries = (int)packet.ReadBits(24);

			// init the storage
			this.Lengths = new int[this.Entries];

            InitTree(packet);
            InitLookupTable(packet);
        }

        void InitTree(DataPacket packet)
        {
            bool sparse;
            int total = 0;

            if (packet.ReadBit())
            {
				// ordered
				int len = (int)packet.ReadBits(5) + 1;
                for (int i = 0; i < this.Entries; )
                {
					int cnt = (int)packet.ReadBits(Utils.ilog(this.Entries - i));

                    while (--cnt >= 0)
                    {
						this.Lengths[i++] = len;
                    }

                    ++len;
                }
                total = 0;
                sparse = false;
            }
            else
            {
                // unordered
                sparse = packet.ReadBit();
                for (int i = 0; i < this.Entries; i++)
                {
                    if (!sparse || packet.ReadBit())
                    {
						this.Lengths[i] = (int)packet.ReadBits(5) + 1;
                        ++total;
                    }
                    else
                    {
						this.Lengths[i] = -1;
                    }
                }
            }
			this.MaxBits = this.Lengths.Max();

            int sortedCount = 0;
            int[] codewordLengths = null;
            if (sparse && total >= this.Entries >> 2)
            {
                codewordLengths = new int[this.Entries];
                Array.Copy(this.Lengths, codewordLengths, this.Entries);

                sparse = false;
            }

            // compute size of sorted tables
            if (sparse)
            {
                sortedCount = total;
            }
            else
            {
                sortedCount = 0;
            }

            int sortedEntries = sortedCount;

            int[] values = null;
            int[] codewords = null;
            if (!sparse)
            {
                codewords = new int[this.Entries];
            }
            else if (sortedEntries != 0)
            {
                codewordLengths = new int[sortedEntries];
                codewords = new int[sortedEntries];
                values = new int[sortedEntries];
            }

            if (!ComputeCodewords(sparse, sortedEntries, codewords, codewordLengths, len: this.Lengths, n: this.Entries, values: values)) throw new InvalidDataException();

			this.LTree = Huffman.BuildLinkedList(values ?? Enumerable.Range(0, codewords.Length).ToArray(), codewordLengths ?? this.Lengths, codewords);
        }

        bool ComputeCodewords(bool sparse, int sortedEntries, int[] codewords, int[] codewordLengths, int[] len, int n, int[] values)
        {
            int i, k, m = 0;
            uint[] available = new uint[32];

            for (k = 0; k < n; ++k) if (len[k] > 0) break;
            if (k == n) return true;

            AddEntry(sparse, codewords, codewordLengths, 0, k, m++, len[k], values);

            for (i = 1; i <= len[k]; ++i) available[i] = 1U << (32 - i);

            for (i = k + 1; i < n; ++i)
            {
                uint res;
                int z = len[i], y;
                if (z <= 0) continue;

                while (z > 0 && available[z] == 0) --z;
                if (z == 0) return false;
                res = available[z];
                available[z] = 0;
                AddEntry(sparse, codewords, codewordLengths, Utils.BitReverse(res), i, m++, len[i], values);

                if (z != len[i])
                {
                    for (y = len[i]; y > z; --y)
                    {
                        available[y] = res + (1U << (32 - y));
                    }
                }
            }

            return true;
        }

        void AddEntry(bool sparse, int[] codewords, int[] codewordLengths, uint huffCode, int symbol, int count, int len, int[] values)
        {
            if (sparse)
            {
                codewords[count] = (int)huffCode;
                codewordLengths[count] = len;
                values[count] = symbol;
            }
            else
            {
                codewords[symbol] = (int)huffCode;
            }
        }

        void InitLookupTable(DataPacket packet)
        {
			this.MapType = (int)packet.ReadBits(4);
            if (this.MapType == 0) return;

			float minValue = Utils.ConvertFromVorbisFloat32(packet.ReadUInt32());
			float deltaValue = Utils.ConvertFromVorbisFloat32(packet.ReadUInt32());
			int valueBits = (int)packet.ReadBits(4) + 1;
			bool sequence_p = packet.ReadBit();

			int lookupValueCount = this.Entries * this.Dimensions;
			float[] lookupTable = new float[lookupValueCount];
            if (this.MapType == 1)
            {
                lookupValueCount = lookup1_values();
            }

			uint[] multiplicands = new uint[lookupValueCount];
            for (int i = 0; i < lookupValueCount; i++)
            {
                multiplicands[i] = (uint)packet.ReadBits(valueBits);
            }

            // now that we have the initial data read in, calculate the entry tree
            if (this.MapType == 1)
            {
                for (int idx = 0; idx < this.Entries; idx++)
                {
					double last = 0.0;
					int idxDiv = 1;
                    for (int i = 0; i < this.Dimensions; i++)
                    {
						int moff = (idx / idxDiv) % lookupValueCount;
						double value = (float)multiplicands[moff] * deltaValue + minValue + last;
                        lookupTable[idx * this.Dimensions + i] = (float)value;

                        if (sequence_p) last = value;

                        idxDiv *= lookupValueCount;
                    }
                }
            }
            else
            {
                for (int idx = 0; idx < this.Entries; idx++)
                {
					double last = 0.0;
					int moff = idx * this.Dimensions;
                    for (int i = 0; i < this.Dimensions; i++)
                    {
						double value = multiplicands[moff] * deltaValue + minValue + last;
                        lookupTable[idx * this.Dimensions + i] = (float)value;

                        if (sequence_p) last = value;

                        ++moff;
                    }
                }
            }

			this.LookupTable = lookupTable;
        }

        int lookup1_values()
        {
			int r = (int)Math.Floor(Math.Exp(Math.Log(this.Entries) / this.Dimensions));
            
            if (Math.Floor(Math.Pow(r + 1, this.Dimensions)) <= this.Entries) ++r;
            
            return r;
        }

        internal int BookNum;

        internal int Dimensions;

        internal int Entries;

        int[] Lengths;

        float[] LookupTable;

        internal int MapType;

        HuffmanListNode LTree;
        int MaxBits;

        internal float this[int entry, int dim]
        {
            get
            {
                return this.LookupTable[entry * this.Dimensions + dim];
            }
        }

        internal int DecodeScalar(DataPacket packet)
        {
            // try to get as many bits as possible...
            int bitCnt; // we really don't care how many bits were read; try to decode anyway...
			int bits = (int)packet.TryPeekBits(this.MaxBits, out bitCnt);
            if (bitCnt == 0) return -1;

			// now go through the list and find the matching entry
			HuffmanListNode node = this.LTree;
            while (node != null)
            {
                if (node.Bits == (bits & node.Mask))
                {
                    node.HitCount++;
                    packet.SkipBits(node.Length);
                    return node.Value;
                }
                node = node.Next;
            }
            return -1;
        }
    }
}
