/****************************************************************************
 * NVorbis                                                                  *
 * Copyright (C) 2013, Andrew Ward <afward@gmail.com>                       *
 *                                                                          *
 * See COPYING for license terms (Ms-PL).                                   *
 *                                                                          *
 ***************************************************************************/
using System;
using System.Collections.Generic;
using System.IO;

namespace Rydia.Audio.Api.NVorbis.Ogg
{
    /// <summary>
    /// Provides an <see cref="IContainerReader"/> implementation for basic Ogg files.
    /// </summary>
    public class ContainerReader : IContainerReader
    {
        Crc _crc = new Crc();
        BufferedReadStream _stream;
        Dictionary<int, PacketReader> _packetReaders;
        Dictionary<int, bool> _eosFlags;
        List<int> _streamSerials, _disposedStreamSerials;
        long _nextPageOffset;
        int _pageCount;

        byte[] _readBuffer = new byte[65025];   // up to a full page of data (but no more!)

        object _pageLock = new object();

        long _containerBits, _wasteBits;

		/// <summary>
		/// Gets the list of stream serials found in the container so far.
		/// </summary>
		public int[] StreamSerials
		{
			get
			{
				return this._streamSerials.ToArray();
			}
		}

		/// <summary>
		/// Event raised when a new logical stream is found in the container.
		/// </summary>
		public event EventHandler<NewStreamEventArgs> NewStream;

        /// <summary>
        /// Creates a new instance with the specified stream.  Optionally sets to close the stream when disposed.
        /// </summary>
        /// <param name="stream">The stream to read.</param>
        /// <param name="closeOnDispose"><c>True</c> to close the stream when <see cref="Dispose"/> is called, otherwise <c>False</c>.</param>
        public ContainerReader(Stream stream, bool closeOnDispose)
        {
			this._packetReaders = new Dictionary<int, PacketReader>();
			this._eosFlags = new Dictionary<int, bool>();
			this._streamSerials = new List<int>();
			this._disposedStreamSerials = new List<int>();

			this._stream = (stream as BufferedReadStream) ?? new BufferedReadStream(stream) { CloseBaseStream = closeOnDispose };
        }

        /// <summary>
        /// Initializes the container and finds the first stream.
        /// </summary>
        /// <returns><c>True</c> if a valid logical stream is found, otherwise <c>False</c>.</returns>
        public bool Init()
        {
            return GatherNextPage() != -1;
        }

        internal void DisposePacketReader(PacketReader packetReader)
        {
			this._disposedStreamSerials.Add(packetReader.StreamSerial);
			this._eosFlags[packetReader.StreamSerial] = true;
			this._streamSerials.Remove(packetReader.StreamSerial);
			this._packetReaders.Remove(packetReader.StreamSerial);
        }

        /// <summary>
        /// Disposes this instance.
        /// </summary>
        public void Dispose()
        {
            foreach (int streamSerial in this._streamSerials.ToArray())
            {
				this._packetReaders[streamSerial].Dispose();
            }

			this._nextPageOffset = 0L;
			this._containerBits = 0L;
			this._wasteBits = 0L;

			this._stream.Dispose();
        }

        /// <summary>
        /// Gets the <see cref="IPacketProvider"/> instance for the specified stream serial.
        /// </summary>
        /// <param name="streamSerial">The stream serial to look for.</param>
        /// <returns>An <see cref="IPacketProvider"/> instance.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The specified stream serial was not found.</exception>
        public IPacketProvider GetStream(int streamSerial)
        {
            PacketReader provider;
            if (!this._packetReaders.TryGetValue(streamSerial, out provider))
            {
                throw new ArgumentOutOfRangeException("streamSerial");
            }
            return provider;
        }

        class PageHeader
        {
            public int StreamSerial { get; set; }
            public PageFlags Flags { get; set; }
            public long GranulePosition { get; set; }
            public int SequenceNumber { get; set; }
            public long DataOffset { get; set; }
            public int[] PacketSizes { get; set; }
            public bool LastPacketContinues { get; set; }
            public bool IsResync { get; set; }
        }

        PageHeader ReadPageHeader(long position)
        {
			// set the stream's position
			this._stream.Seek(position, SeekOrigin.Begin);

            // header
            if (this._stream.Read(this._readBuffer, 0, 27) != 27) return null;

            // capture signature
            if (this._readBuffer[0] != 0x4f || this._readBuffer[1] != 0x67 || this._readBuffer[2] != 0x67 || this._readBuffer[3] != 0x53) return null;

            // check the stream version
            if (this._readBuffer[4] != 0) return null;

			// start populating the header
			PageHeader hdr = new PageHeader();

            // bit flags
            hdr.Flags = (PageFlags)this._readBuffer[5];

            // granulePosition
            hdr.GranulePosition = BitConverter.ToInt64(this._readBuffer, 6);

            // stream serial
            hdr.StreamSerial = BitConverter.ToInt32(this._readBuffer, 14);

            // sequence number
            hdr.SequenceNumber = BitConverter.ToInt32(this._readBuffer, 18);

			// save off the CRC
			uint crc = BitConverter.ToUInt32(this._readBuffer, 22);

			// start calculating the CRC value for this page
			this._crc.Reset();
            for (int i = 0; i < 22; i++)
            {
				this._crc.Update(this._readBuffer[i]);
            }
			this._crc.Update(0);
			this._crc.Update(0);
			this._crc.Update(0);
			this._crc.Update(0);
			this._crc.Update(this._readBuffer[26]);

			// figure out the length of the page
			int segCnt = (int)this._readBuffer[26];
            if (this._stream.Read(this._readBuffer, 0, segCnt) != segCnt) throw new EndOfStreamException();

			List<int> packetSizes = new List<int>(segCnt);

            int size = 0, idx = 0;
            for (int i = 0; i < segCnt; i++)
            {
				byte temp = this._readBuffer[i];
				this._crc.Update(temp);

                if (idx == packetSizes.Count) packetSizes.Add(0);
                packetSizes[idx] += temp;
                if (temp < 255)
                {
                    ++idx;
                    hdr.LastPacketContinues = false;
                }
                else
                {
                    hdr.LastPacketContinues = true;
                }

                size += temp;
            }
            hdr.PacketSizes = packetSizes.ToArray();
            hdr.DataOffset = position + 27 + segCnt;

            // now we have to go through every byte in the page
            if (this._stream.Read(this._readBuffer, 0, size) != size) throw new EndOfStreamException();
            for (int i = 0; i < size; i++)
            {
				this._crc.Update(this._readBuffer[i]);
            }

            if (this._crc.Test(crc))
            {
				this._containerBits += 8 * (27 + segCnt);
                ++this._pageCount;
                return hdr;
            }
            return null;
        }

        PageHeader FindNextPageHeader()
        {
			long startPos = this._nextPageOffset;

			bool isResync = false;
            PageHeader hdr;
            while ((hdr = ReadPageHeader(startPos)) == null)
            {
                isResync = true;
				this._wasteBits += 8;
				this._stream.Position = ++startPos;

				int cnt = 0;
                do
                {
                    if (this._stream.ReadByte() == 0x4f)
                    {
                        if (this._stream.ReadByte() == 0x67 && this._stream.ReadByte() == 0x67 && this._stream.ReadByte() == 0x53)
                        {
                            // found it!
                            startPos += cnt;
                            break;
                        }
                        else
                        {
							this._stream.Seek(-3, SeekOrigin.Current);
                        }
                    }
					this._wasteBits += 8;
                } while (++cnt < 65536);    // we will only search through 64KB of data to find the next sync marker.  if it can't be found, we have a badly corrupted stream.
                if (cnt == 65536) return null;
            }
            hdr.IsResync = isResync;

			this._nextPageOffset = hdr.DataOffset;
            for (int i = 0; i < hdr.PacketSizes.Length; i++)
            {
				this._nextPageOffset += hdr.PacketSizes[i];
            }

            return hdr;
        }

        bool AddPage(PageHeader hdr)
        {
            // get our packet reader (create one if we have to)
            PacketReader packetReader;
            if (!this._packetReaders.TryGetValue(hdr.StreamSerial, out packetReader))
            {
                packetReader = new PacketReader(this, hdr.StreamSerial);
            }

            // save off the container bits
            packetReader.ContainerBits += this._containerBits;
			this._containerBits = 0;

			// get our flags prepped
			bool isContinued = false;
			bool isContinuation = (hdr.Flags & PageFlags.ContinuesPacket) == PageFlags.ContinuesPacket;
			bool isEOS = (hdr.Flags & PageFlags.EndOfStream) == PageFlags.EndOfStream;
			bool isResync = hdr.IsResync;

			// add all the packets, making sure to update flags as needed
			long dataOffset = hdr.DataOffset;
			int cnt = hdr.PacketSizes.Length;
            foreach (int size in hdr.PacketSizes)
            {
				Packet packet = new Packet(this._stream, dataOffset, size)
                    {
                        PageGranulePosition = hdr.GranulePosition,
                        IsEndOfStream = isEOS,
                        PageSequenceNumber = hdr.SequenceNumber,
                        IsContinued = isContinued,
                        IsContinuation = isContinuation,
                        IsResync = isResync,
                    };
                packetReader.AddPacket(packet);

                // update the offset into the stream for each packet
                dataOffset += size;

                // only the first packet in a page can be a continuation or resync
                isContinuation = false;
                isResync = false;

                // only the last packet in a page can be continued
                if (--cnt == 1)
                {
                    isContinued = hdr.LastPacketContinues;
                }
            }

            // if the packet reader list doesn't include the serial in question, add it to all the collections and indicate a new stream to the caller
            if (!this._packetReaders.ContainsKey(hdr.StreamSerial))
            {
				int ss = hdr.StreamSerial;
				this._packetReaders.Add(ss, packetReader);
				this._eosFlags.Add(ss, isEOS);
				this._streamSerials.Add(ss);

                return true;
            }
            else
            {
				// otherwise, update the end of stream marker for the stream and indicate an existing stream to the caller
				this._eosFlags[hdr.StreamSerial] |= isEOS;
                return false;
            }
        }

        internal class PageReaderLock : IDisposable
        {
            object _lock;

            public PageReaderLock(object pageLock)
            {
                System.Threading.Monitor.Enter(pageLock);
				this._lock = pageLock;
            }

            public bool Validate(object pageLock)
            {
                return ReferenceEquals(pageLock, this._lock);
            }

            public void Dispose()
            {
                System.Threading.Monitor.Exit(this._lock);
            }
        }

        internal PageReaderLock TakePageReaderLock()
        {
            return new PageReaderLock(this._pageLock);
        }

        int GatherNextPage()
        {
            while (true)
            {
				// get our next header
				PageHeader hdr = FindNextPageHeader();
                if (hdr == null)
                {
                    return -1;
                }
                
                // if it's in a disposed stream, grab the next page instead
                if (this._disposedStreamSerials.Contains(hdr.StreamSerial)) continue;
                
                // otherwise, add it
                if (AddPage(hdr))
                {
					EventHandler<NewStreamEventArgs> callback = NewStream;
                    if (callback != null)
                    {
						NewStreamEventArgs ea = new NewStreamEventArgs(this._packetReaders[hdr.StreamSerial]);
                        callback(this, ea);
                        if (ea.IgnoreStream)
                        {
							this._packetReaders[hdr.StreamSerial].Dispose();
                            continue;
                        }
                    }
                }
                return hdr.StreamSerial;
            }
        }

        /// <summary>
        /// Gathers pages until finding a page for the stream indicated
        /// </summary>
        internal void GatherNextPage(int streamSerial, PageReaderLock pageLock)
        {
            // pageLock is just so we know the caller took a lock... we don't actually need it for anything else

            if (pageLock == null) throw new ArgumentNullException("pageLock");
            if (!pageLock.Validate(this._pageLock)) throw new ArgumentException("pageLock");
            if (!this._eosFlags.ContainsKey(streamSerial)) throw new ArgumentOutOfRangeException("streamSerial");

            int nextSerial;
            do
            {
                if (this._eosFlags[streamSerial]) throw new EndOfStreamException();
                
                nextSerial = GatherNextPage();
                if (nextSerial == -1) throw new InvalidDataException("Could not find next page.");
            } while (nextSerial != streamSerial);
        }

        /// <summary>
        /// Finds the next new stream in the container.
        /// </summary>
        /// <returns><c>True</c> if a new stream was found, otherwise <c>False</c>.</returns>
        /// <exception cref="InvalidOperationException"><see cref="CanSeek"/> is <c>False</c>.</exception>
        public bool FindNextStream()
        {
            if (!CanSeek) throw new InvalidOperationException();

			// goes through all the pages until the serial count increases
			int cnt = this._packetReaders.Count;
            using (PageReaderLock pageLock = TakePageReaderLock())
            {
                // read pages until we're done...
                while (this._packetReaders.Count == cnt)
                {
                    if (GatherNextPage() == -1)
                    {
                        break;
                    }
                }

                return cnt > this._packetReaders.Count;
            }
        }

		/// <summary>
		/// Gets the number of pages that have been read in the container.
		/// </summary>
		public int PagesRead
		{
			get
			{
				return this._pageCount;
			}
		}

		/// <summary>
		/// Retrieves the total number of pages in the container.
		/// </summary>
		/// <returns>The total number of pages.</returns>
		/// <exception cref="InvalidOperationException"><see cref="CanSeek"/> is <c>False</c>.</exception>
		public int GetTotalPageCount()
        {
            if (!CanSeek) throw new InvalidOperationException();

			// add an invalid stream serial as a dummy...
			this._eosFlags.Add(-1, false);

            // there cannot possibly be another page less than 28 bytes from the end of the file
            while (this._stream.Position < this._stream.Length - 28)
            {
                using (PageReaderLock pageLock = TakePageReaderLock())
                {
                    GatherNextPage(-1, pageLock);
                }
            }

			this._eosFlags.Remove(-1);

            return this._pageCount;
        }

		/// <summary>
		/// Gets whether the container supports seeking.
		/// </summary>
		public bool CanSeek
		{
			get
			{
				return this._stream.CanSeek;
			}
		}

		/// <summary>
		/// Gets the number of bits in the container that are not associated with a logical stream.
		/// </summary>
		public long WasteBits
		{
			get
			{
				return this._wasteBits;
			}
		}
	}
}
