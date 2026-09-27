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

namespace Rydia.Audio.Api.NVorbis
{
    class RingBuffer
    {
        float[] _buffer;
        int _start;
        int _end;
        int _bufLen;

        internal RingBuffer(int size)
        {
			this._buffer = new float[size];
			this._start = this._end = 0;
			this._bufLen = size;
        }

        internal void EnsureSize(int size)
        {
            // because _end == _start signifies no data, and _end is always 1 more than the data we have, we must make the buffer {channels} entries bigger than requested
            size += this.Channels;

            if (this._bufLen < size)
            {
				float[] temp = new float[size];
                Array.Copy(this._buffer, this._start, temp, 0, this._bufLen - this._start);
                if (this._end < this._start)
                {
                    Array.Copy(this._buffer, 0, temp, this._bufLen - this._start, this._end);
                }
				int end = Length;
				this._start = 0;
				this._end = end;
				this._buffer = temp;

				this._bufLen = size;
            }
        }

        internal int Channels;

        internal void CopyTo(float[] buffer, int index, int count)
        {
            if (index < 0 || index + count > buffer.Length) throw new ArgumentOutOfRangeException("index");

			int start = this._start;
            RemoveItems(count);

			// this is used to pull data out of the buffer, so we'll update the start position too...
			int len = (this._end - start + this._bufLen) % this._bufLen;
            if (count > len) throw new ArgumentOutOfRangeException("count");

			int cnt = Math.Min(count, this._bufLen - start);
            Array.Copy(this._buffer, start, buffer, index, cnt);

            if (cnt < count)
            {
                Array.Copy(this._buffer, 0, buffer, index + cnt, count - cnt);
            }
        }

        internal void RemoveItems(int count)
        {
			int cnt = (count + this._start) % this._bufLen;
            if (this._end > this._start)
            {
                if (cnt > this._end || cnt < this._start) throw new ArgumentOutOfRangeException();
            }
            else
            {
                // wrap-around
                if (cnt < this._start && cnt > this._end) throw new ArgumentOutOfRangeException();
            }

			this._start = cnt;
        }

        internal void Clear()
        {
			this._start = this._end = 0;
        }

        internal int Length
        {
            get
            {
				int temp = this._end - this._start;
                if (temp < 0) temp += this._bufLen;
                return temp;
            }
        }

        internal void Write(int channel, int index, int start, int switchPoint, int end, float[] pcm, float[] window)
        {
			// this is the index of the first sample to merge
			int idx = (index + start) * this.Channels + channel + this._start;
            while (idx >= this._bufLen)
            {
                idx -= this._bufLen;
            }

            // blech...  gotta fix the first packet's pointers
            if (idx < 0)
            {
                start -= index;
                idx = channel;
            }

            // go through and do the overlap
            for (; idx < this._bufLen && start < switchPoint; idx += this.Channels, ++start)
            {
				this._buffer[idx] += pcm[start] * window[start];
            }
            if (idx >= this._bufLen)
            {
                idx -= this._bufLen;
                for (; start < switchPoint; idx += this.Channels, ++start)
                {
					this._buffer[idx] += pcm[start] * window[start];
                }
            }

            // go through and write the rest
            for (; idx < this._bufLen && start < end; idx += this.Channels, ++start)
            {
				this._buffer[idx] = pcm[start] * window[start];
            }
            if (idx >= this._bufLen)
            {
                idx -= this._bufLen;
                for (; start < end; idx += this.Channels, ++start)
                {
					this._buffer[idx] = pcm[start] * window[start];
                }
            }

			// finally, make sure the buffer end is set correctly
			this._end = idx;
        }
    }
}
