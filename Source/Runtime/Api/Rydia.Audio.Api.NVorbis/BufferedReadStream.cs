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

namespace Rydia.Audio.Api.NVorbis
{
    /// <summary>
    /// A thread-safe, read-only, buffering stream wrapper.
    /// </summary>
    partial class BufferedReadStream : Stream
    {
        const int DEFAULT_INITIAL_SIZE = 32768; // 32KB  (1/2 full page)
        const int DEFAULT_MAX_SIZE = 262144;    // 256KB (4 full pages)

        Stream _baseStream;
        StreamReadBuffer _buffer;
        long _readPosition;
        object _localLock = new object();

        public BufferedReadStream(Stream baseStream)
            : this(baseStream, DEFAULT_INITIAL_SIZE, DEFAULT_MAX_SIZE, false)
        {
        }

        public BufferedReadStream(Stream baseStream, bool minimalRead)
            : this(baseStream, DEFAULT_INITIAL_SIZE, DEFAULT_MAX_SIZE, minimalRead)
        {
        }

        public BufferedReadStream(Stream baseStream, int initialSize, int maxSize)
            : this(baseStream, initialSize, maxSize, false)
        {
        }

        public BufferedReadStream(Stream baseStream, int initialSize, int maxBufferSize, bool minimalRead)
        {
            if (baseStream == null) throw new ArgumentNullException("baseStream");
            if (!baseStream.CanRead) throw new ArgumentException("baseStream");

            if (maxBufferSize < 1) maxBufferSize = 1;
            if (initialSize < 1) initialSize = 1;
            if (initialSize > maxBufferSize) initialSize = maxBufferSize;

			this._baseStream = baseStream;
			this._buffer = new StreamReadBuffer(baseStream, initialSize, maxBufferSize, minimalRead);
			this._buffer.MaxSize = maxBufferSize;
			this._buffer.MinimalRead = minimalRead;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                if (CloseBaseStream)
                {
					this._baseStream.Dispose();
                }
            }
        }

        public bool CloseBaseStream
        {
            get;
            set;
        }

        public bool MinimalRead
		{
			get
			{
				return this._buffer.MinimalRead;
			}

			set
			{
				this._buffer.MinimalRead = value;
			}
		}

		public int MaxBufferSize
		{
			get
			{
				return this._buffer.MaxSize;
			}

			set
			{
				lock (this._localLock)
				{
					this._buffer.MaxSize = value;
				}
			}
		}

		public long BufferBaseOffset
		{
			get
			{
				return this._buffer.BaseOffset;
			}
		}

		public int BufferBytesFilled
		{
			get
			{
				return this._buffer.BytesFilled;
			}
		}

		public void Discard(int bytes)
        {
            lock (this._localLock)
            {
				this._buffer.DiscardThrough(this._buffer.BaseOffset + bytes);
            }
        }

        public void DiscardThrough(long offset)
        {
            lock (this._localLock)
            {
				this._buffer.DiscardThrough(offset);
            }
        }

		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		public override bool CanSeek
		{
			get
			{
				return true;
			}
		}

		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		public override void Flush()
        {
            // no-op
        }

		public override long Length
		{
			get
			{
				return this._baseStream.Length;
			}
		}

		public override long Position
		{
			get
			{
				return this._readPosition;
			}

			set
			{
				Seek(value, SeekOrigin.Begin);
			}
		}

		public override int ReadByte()
        {
            lock (this._localLock)
            {
				int val = this._buffer.ReadByte(this._readPosition);
                if (val > -1)
                {
                    ++this._readPosition;
                }
                return val;
            }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            lock (this._localLock)
            {
				int cnt = this._buffer.Read(this._readPosition, buffer, offset, count);
				this._readPosition += cnt;
                return cnt;
            }
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            lock (this._localLock)
            {
                switch (origin)
                {
                    case SeekOrigin.Begin:
                        // no-op
                        break;
                    case SeekOrigin.Current:
                        offset += this._readPosition;
                        break;
                    case SeekOrigin.End:
                        offset += this._baseStream.Length;
                        break;
                }

                if (!this._baseStream.CanSeek)
                {
                    if (offset < this._buffer.BaseOffset) throw new InvalidOperationException("Cannot seek to before the start of the buffer!");
                    if (offset >= this._buffer.BufferEndOffset) throw new InvalidOperationException("Cannot seek to beyond the end of the buffer!  Discard some bytes.");
                }

                return (this._readPosition = offset);
            }
        }

        public override void SetLength(long value)
        {
            throw new NotSupportedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException();
        }
    }
}
