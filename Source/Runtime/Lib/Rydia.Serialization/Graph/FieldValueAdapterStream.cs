using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Rydia.Serialization.Graph
{
    internal class FieldValueAdapterStream : Stream
    {
        private readonly FieldValueAttributeBase _attribute;
        private readonly byte[] _block;
        private int _blockOffset;

        public FieldValueAdapterStream(FieldValueAttributeBase attribute, object state)
        {
            this._attribute = attribute;
            this._block = new byte[attribute.BlockSize];
            State = state;
        }

        public object State { get; private set; }

        public override bool CanRead
        {
            get
            {
                return false;
            }
        }

        public override bool CanSeek
        {
            get
            {
                return false;
            }
        }

        public override bool CanWrite
        {
            get
            {
                return true;
            }
        }

        public override long Length
        {
            get
            {
                throw new InvalidOperationException();
            }
        }

        public override long Position
        {
            get
            {
                throw new InvalidOperationException();
            }

            set
            {
                throw new InvalidOperationException();
            }
        }

        public override void Flush()
        {
            if (this._blockOffset > 0)
            {
                State = this._attribute.GetUpdatedStateInternal(State, this._block, 0, this._blockOffset);
            }

            this._blockOffset = 0;
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotSupportedException();
        }

        public override void SetLength(long value)
        {
            throw new NotSupportedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            if (buffer == null)
            {
                throw new ArgumentNullException(nameof(buffer));
            }

            if (offset < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(offset), "< 0");
            }

            if (count < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count), "< 0");
            }

            // avoid possible integer overflow
            if (buffer.Length - offset < count)
            {
                throw new ArgumentException("array.Length - offset < count");
            }


            // reordered to avoid possible integer overflow
            if (this._blockOffset >= this._block.Length - count)
            {
                Flush();
                State = this._attribute.GetUpdatedStateInternal(State, buffer, offset, count);
            }
            else
            {
                Buffer.BlockCopy(buffer, offset, this._block, this._blockOffset, count);
                this._blockOffset += count;
            }
        }

        public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            Write(buffer, offset, count);
            return Task.CompletedTask;
        }

        protected override void Dispose(bool disposing)
        {
            Flush();
        }
    }
}