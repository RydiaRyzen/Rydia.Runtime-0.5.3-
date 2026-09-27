﻿/****************************************************************************
 * NVorbis                                                                  *
 * Copyright (C) 2012, Andrew Ward <afward@gmail.com>                       *
 *                                                                          *
 * See COPYING for license terms (Ms-PL).                                   *
 *                                                                          *
 ***************************************************************************/
using System;
using System.Collections.Generic;
using System.IO;

namespace Rydia.Audio.Api.NVorbis
{
    /// <summary>
    /// A single data packet from a logical Vorbis stream.
    /// </summary>
    public abstract class DataPacket
    {
        ulong _bitBucket;
        int _bitCount;
        long _readBits;
        byte _overflowBits;

        /// <summary>
        /// Creates a new instance with the specified length.
        /// </summary>
        /// <param name="length">The length of the packet.</param>
        protected DataPacket(int length)
        {
            Length = length;
        }

        /// <summary>
        /// Reads the next byte of the packet.
        /// </summary>
        /// <returns>The next byte if available, otherwise -1.</returns>
        abstract protected int ReadNextByte();

        /// <summary>
        /// Indicates that the packet has been read and its data is no longer needed.
        /// </summary>
        virtual public void Done()
        {
        }

        /// <summary>
        /// Attempts to read the specified number of bits from the packet, but may return fewer.  Does not advance the position counter.
        /// </summary>
        /// <param name="count">The number of bits to attempt to read.</param>
        /// <param name="bitsRead">The number of bits actually read.</param>
        /// <returns>The value of the bits read.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="count"/> is not between 0 and 64.</exception>
        public ulong TryPeekBits(int count, out int bitsRead)
        {
            ulong value = 0;

            if (count < 0 || count > 64) throw new ArgumentOutOfRangeException("count");
            if (count == 0)
            {
                bitsRead = 0;
                return 0UL;
            }

            while (this._bitCount < count)
            {
				int val = ReadNextByte();
                if (val == -1)
                {
                    bitsRead = this._bitCount;
                    value = this._bitBucket;
					this._bitBucket = 0;
					this._bitCount = 0;

                    IsShort = true;

                    return value;
                }
				this._bitBucket = (ulong)(val & 0xFF) << this._bitCount | this._bitBucket;
				this._bitCount += 8;
                
                if (this._bitCount > 64)
                {
					this._overflowBits = (byte)(val >> (72 - this._bitCount));
                }
            }

            value = this._bitBucket;

            if (count < 64)
            {
                value &= (1UL << count) - 1;
            }

            bitsRead = count;
            return value;
        }

        /// <summary>
        /// Advances the position counter by the specified number of bits.
        /// </summary>
        /// <param name="count">The number of bits to advance.</param>
        public void SkipBits(int count)
        {
            if (count == 0)
            {
                // no-op
            }
            else if (this._bitCount > count)
            {
                // we still have bits left over...
                if (count > 63)
                {
					this._bitBucket = 0;
                }
                else
                {
					this._bitBucket >>= count;
                }
                if (this._bitCount > 64)
                {
					int overflowCount = this._bitCount - 64;
					this._bitBucket |= (ulong)this._overflowBits << (this._bitCount - count - overflowCount);

                    if (overflowCount > count)
                    {
						// ugh, we have to keep bits in overflow
						this._overflowBits >>= count;
                    }
                }

				this._bitCount -= count;
				this._readBits += count;
            }
            else if (this._bitCount == count)
            {
				this._bitBucket = 0UL;
				this._bitCount = 0;
				this._readBits += count;
            }
            else //  _bitCount < count
            {
                // we have to move more bits than we have available...
                count -= this._bitCount;
				this._readBits += this._bitCount;
				this._bitCount = 0;
				this._bitBucket = 0;

                while (count > 8)
                {
                    if (ReadNextByte() == -1)
                    {
                        count = 0;
                        IsShort = true;
                        break;
                    }
                    count -= 8;
					this._readBits += 8;
                }

                if (count > 0)
                {
					int temp = ReadNextByte();
                    if (temp == -1)
                    {
                        IsShort = true;
                    }
                    else
                    {
						this._bitBucket = (ulong)(temp >> count);
						this._bitCount = 8 - count;
						this._readBits += count;
                    }
                }
            }
        }

        /// <summary>
        /// Resets the bit reader.
        /// </summary>
        protected void ResetBitReader()
        {
			this._bitBucket = 0;
			this._bitCount = 0;
			this._readBits = 0;

            IsShort = false;
        }

        /// <summary>
        /// Gets whether the packet was found after a stream resync.
        /// </summary>
        public bool IsResync { get; internal set; }

        /// <summary>
        /// Gets the position of the last granule in the packet.
        /// </summary>
        public long GranulePosition { get; set; }

        /// <summary>
        /// Gets the position of the last granule in the page the packet is in.
        /// </summary>
        public long PageGranulePosition { get; internal set; }

        /// <summary>
        /// Gets the length of the packet.
        /// </summary>
        public int Length { get; protected set; }

        /// <summary>
        /// Gets whether the packet is the last one in the logical stream.
        /// </summary>
        public bool IsEndOfStream { get; internal set; }

		/// <summary>
		/// Gets the number of bits read from the packet.
		/// </summary>
		public long BitsRead
		{
			get
			{
				return this._readBits;
			}
		}

		/// <summary>
		/// Gets the number of granules in the packet.  If <c>null</c>, the packet has not been decoded yet.
		/// </summary>
		public long? GranuleCount { get; set; }

        internal int PageSequenceNumber { get; set; }

        internal bool IsShort { get; private set; }

        /// <summary>
        /// Reads the specified number of bits from the packet and advances the position counter.
        /// </summary>
        /// <param name="count">The number of bits to read.</param>
        /// <returns>The value of the bits read.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The number of bits specified is not between 0 and 64.</exception>
        public ulong ReadBits(int count)
        {
            // short-circuit 0
            if (count == 0) return 0UL;

            int temp;
			ulong value = TryPeekBits(count, out temp);

            SkipBits(count);

            return value;
        }

        /// <summary>
        /// Reads the next byte from the packet.  Does not advance the position counter.
        /// </summary>
        /// <returns>The byte read from the packet.</returns>
        public byte PeekByte()
        {
            int temp;
            return (byte)TryPeekBits(8, out temp);
        }

        /// <summary>
        /// Reads the next byte from the packet and advances the position counter.
        /// </summary>
        /// <returns>The byte read from the packet.</returns>
        public byte ReadByte()
        {
            return (byte)ReadBits(8);
        }

        /// <summary>
        /// Reads the specified number of bytes from the packet and advances the position counter.
        /// </summary>
        /// <param name="count">The number of bytes to read.</param>
        /// <returns>A byte array holding the data read.</returns>
        public byte[] ReadBytes(int count)
        {
			List<byte> buf = new List<byte>(count);

            while (buf.Count < count)
            {
                buf.Add(ReadByte());
            }

            return buf.ToArray();
        }

        /// <summary>
        /// Reads the specified number of bytes from the packet into the buffer specified and advances the position counter.
        /// </summary>
        /// <param name="buffer">The buffer to read into.</param>
        /// <param name="index">The index into the buffer to start placing the read data.</param>
        /// <param name="count">The number of bytes to read.</param>
        /// <returns>The number of bytes read.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> is less than 0 or <paramref name="index"/> + <paramref name="count"/> is past the end of <paramref name="buffer"/>.</exception>
        public int Read(byte[] buffer, int index, int count)
        {
            if (index < 0 || index + count > buffer.Length) throw new ArgumentOutOfRangeException("index");
            for (int i = 0; i < count; i++)
            {
                int cnt;
                byte val = (byte)TryPeekBits(8, out cnt);
                if (cnt == 0)
                {
                    return i;
                }
                buffer[index++] = val;
                SkipBits(8);
            }
            return count;
        }

        /// <summary>
        /// Reads the next bit from the packet and advances the position counter.
        /// </summary>
        /// <returns>The value of the bit read.</returns>
        public bool ReadBit()
        {
            return ReadBits(1) == 1;
        }

        /// <summary>
        /// Retrieves the next 16 bits from the packet as a <see cref="short"/> and advances the position counter.
        /// </summary>
        /// <returns>The value of the next 16 bits.</returns>
        public short ReadInt16()
        {
            return (short)ReadBits(16);
        }

        /// <summary>
        /// Retrieves the next 32 bits from the packet as a <see cref="int"/> and advances the position counter.
        /// </summary>
        /// <returns>The value of the next 32 bits.</returns>
        public int ReadInt32()
        {
            return (int)ReadBits(32);
        }

        /// <summary>
        /// Retrieves the next 64 bits from the packet as a <see cref="long"/> and advances the position counter.
        /// </summary>
        /// <returns>The value of the next 64 bits.</returns>
        public long ReadInt64()
        {
            return (long)ReadBits(64);
        }

        /// <summary>
        /// Retrieves the next 16 bits from the packet as a <see cref="ushort"/> and advances the position counter.
        /// </summary>
        /// <returns>The value of the next 16 bits.</returns>
        public ushort ReadUInt16()
        {
            return (ushort)ReadBits(16);
        }

        /// <summary>
        /// Retrieves the next 32 bits from the packet as a <see cref="uint"/> and advances the position counter.
        /// </summary>
        /// <returns>The value of the next 32 bits.</returns>
        public uint ReadUInt32()
        {
            return (uint)ReadBits(32);
        }

        /// <summary>
        /// Retrieves the next 64 bits from the packet as a <see cref="ulong"/> and advances the position counter.
        /// </summary>
        /// <returns>The value of the next 64 bits.</returns>
        public ulong ReadUInt64()
        {
            return (ulong)ReadBits(64);
        }

        /// <summary>
        /// Advances the position counter by the specified number of bytes.
        /// </summary>
        /// <param name="count">The number of bytes to advance.</param>
        public void SkipBytes(int count)
        {
            SkipBits(count * 8);
        }
    }
}