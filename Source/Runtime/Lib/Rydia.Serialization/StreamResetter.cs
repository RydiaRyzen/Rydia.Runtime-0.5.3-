using System;
using System.IO;

namespace Rydia.Serialization
{
    internal class StreamResetter : IDisposable
    {
        private readonly Stream _stream;
        private long? _position;

        public StreamResetter(Stream stream, bool resetOnDispose = true)
        {
            if (!resetOnDispose)
            {
                return;
            }

            this._stream = stream;
            this._position = this._stream.Position;
        }

        public void Dispose()
        {
            if (this._position == null)
            {
                return;
            }

            if (!this._stream.CanSeek)
            {
                throw new InvalidOperationException("Not supported on non-seekable streams");
            }

            this._stream.Position = (long)this._position;
        }

        public void CancelReset()
        {
            this._position = null;
        }
    }
}