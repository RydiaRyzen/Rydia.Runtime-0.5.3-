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
using System.Text;
using System.IO;

namespace Rydia.Audio.Api.NVorbis.Ogg
{
    class Packet : DataPacket
    {
        BufferedReadStream _stream;

        long _offset;
        long _length;
        Packet _mergedPacket;

        internal Packet Next { get; set; }
        internal Packet Prev { get; set; }
        internal bool IsContinued { get; set; }
        internal bool IsContinuation { get; set; }

        int _curOfs;

        internal Packet(BufferedReadStream stream, long streamOffset, int length)
            : base(length)
        {
			this._stream = stream;

			this._offset = streamOffset;
			this._length = length;
			this._curOfs = 0;
        }

        internal void MergeWith(NVorbis.DataPacket continuation)
        {
			Packet op = continuation as Packet;

            if (op == null) throw new ArgumentException("Incorrect packet type!");

            Length += continuation.Length;

            if (this._mergedPacket == null)
            {
				this._mergedPacket = op;
            }
            else
            {
				this._mergedPacket.MergeWith(continuation);
            }

            // per the spec, a partial packet goes with the next page's granulepos.  we'll go ahead and assign it to the next page as well
            PageGranulePosition = continuation.PageGranulePosition;
            PageSequenceNumber = continuation.PageSequenceNumber;
        }

        internal void Reset()
        {
			this._curOfs = 0;
            ResetBitReader();

            if (this._mergedPacket != null)
            {
				this._mergedPacket.Reset();
            }
        }

        protected override int ReadNextByte()
        {
            if (this._curOfs == this._length)
            {
                if (this._mergedPacket == null) return -1;

                return this._mergedPacket.ReadNextByte();
            }

			this._stream.Seek(this._curOfs + this._offset, SeekOrigin.Begin);

			int b = this._stream.ReadByte();
            ++this._curOfs;
            return b;
        }

        public override void Done()
        {
            if (this._mergedPacket != null)
            {
				this._mergedPacket.Done();
            }
            else
            {
				this._stream.DiscardThrough(this._offset + this._length);
            }
        }
    }
}
