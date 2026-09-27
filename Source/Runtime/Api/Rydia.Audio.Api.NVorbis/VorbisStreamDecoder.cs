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

namespace Rydia.Audio.Api.NVorbis
{
    class VorbisStreamDecoder : IVorbisStreamStatus, IDisposable
    {
        internal int _upperBitrate;
        internal int _nominalBitrate;
        internal int _lowerBitrate;

        internal string _vendor;
        internal string[] _comments;

        internal int _channels;
        internal int _sampleRate;
        internal int Block0Size;
        internal int Block1Size;

        internal VorbisCodebook[] Books;
        internal VorbisTime[] Times;
        internal VorbisFloor[] Floors;
        internal VorbisResidue[] Residues;
        internal VorbisMapping[] Maps;
        internal VorbisMode[] Modes;

        int _modeFieldBits;

        #region Stat Fields

        internal long _glueBits;
        internal long _metaBits;
        internal long _bookBits;
        internal long _timeHdrBits;
        internal long _floorHdrBits;
        internal long _resHdrBits;
        internal long _mapHdrBits;
        internal long _modeHdrBits;
        internal long _wasteHdrBits;

        internal long _modeBits;
        internal long _floorBits;
        internal long _resBits;
        internal long _wasteBits;

        internal long _samples;

        internal int _packetCount;

        internal System.Diagnostics.Stopwatch _sw = new System.Diagnostics.Stopwatch();

        #endregion

        IPacketProvider _packetProvider;

        List<int> _pagesSeen;
        int _lastPageSeen;

        bool _eosFound;

        internal VorbisStreamDecoder(IPacketProvider packetProvider)
        {
			this._packetProvider = packetProvider;

			this._pagesSeen = new List<int>();
			this._lastPageSeen = -1;
        }

        internal bool TryInit()
        {
			DataPacket initialPacket = this._packetProvider.GetNextPacket();

            // make sure it's a vorbis stream...
            if (!initialPacket.ReadBytes(7).SequenceEqual(new byte[] { 0x01, 0x76, 0x6f, 0x72, 0x62, 0x69, 0x73 }))
            {
				this._glueBits += initialPacket.Length * 8;
                return false;
            }

			this._glueBits += 56;

            // now load the initial header
            ProcessStreamHeader(initialPacket);

            // finally, load the comment and book headers...
            DataPacket commentsPacket = null, booksPacket = null;
            while (commentsPacket == null || booksPacket == null)
            {
				DataPacket packet = this._packetProvider.GetNextPacket();
                if (packet.IsResync) throw new InvalidDataException("Missing header packets!");

                if (!this._pagesSeen.Contains(packet.PageSequenceNumber)) this._pagesSeen.Add(packet.PageSequenceNumber);

                switch (packet.PeekByte())
                {
                    case 1: throw new InvalidDataException("Found second init header!");
                    case 3: LoadComments(packet); commentsPacket = packet; break;
                    case 5: LoadBooks(packet); booksPacket = packet; break;
                }
            }

            // tell the packets that we're done with them
            initialPacket.Done();
            commentsPacket.Done();
            booksPacket.Done();

            // get the decoding logic bootstrapped
            InitDecoder();

            return true;
        }

        public void Dispose()
        {
            if (this._packetProvider != null)
            {
				IPacketProvider temp = this._packetProvider;
				this._packetProvider = null;
                temp.Dispose();
            }
        }

        #region Header Decode

        void ProcessStreamHeader(DataPacket packet)
        {
			this._pagesSeen.Add(packet.PageSequenceNumber);

			long startPos = packet.BitsRead;

            if (packet.ReadInt32() != 0) throw new InvalidDataException("Only Vorbis stream version 0 is supported.");

			this._channels = packet.ReadByte();
			this._sampleRate = packet.ReadInt32();
			this._upperBitrate = packet.ReadInt32();
			this._nominalBitrate = packet.ReadInt32();
			this._lowerBitrate = packet.ReadInt32();

			this.Block0Size = 1 << (int)packet.ReadBits(4);
			this.Block1Size = 1 << (int)packet.ReadBits(4);

            if (this._nominalBitrate == 0)
            {
                if (this._upperBitrate > 0 && this._lowerBitrate > 0)
                {
					this._nominalBitrate = (this._upperBitrate + this._lowerBitrate) / 2;
                }
            }

			this._metaBits += packet.BitsRead - startPos + 8;

			this._wasteHdrBits += 8 * packet.Length - packet.BitsRead;
        }

        void LoadComments(DataPacket packet)
        {
            packet.SkipBits(8);
            if (!packet.ReadBytes(6).SequenceEqual(new byte[] { 0x76, 0x6f, 0x72, 0x62, 0x69, 0x73 })) throw new InvalidDataException("Corrupted comment header!");

			this._glueBits += 56;

			{
				byte[] vendorBytes = packet.ReadBytes(packet.ReadInt32());
				this._vendor = Encoding.UTF8.GetString(vendorBytes, 0, vendorBytes.Length);
			}

			this._comments = new string[packet.ReadInt32()];
            for (int i = 0; i < this._comments.Length; i++)
            {
				byte[] commentBytes = packet.ReadBytes(packet.ReadInt32());
				this._comments[i] = Encoding.UTF8.GetString(commentBytes, 0, commentBytes.Length);
            }

			this._metaBits += packet.BitsRead - 56;
			this._wasteHdrBits += 8 * packet.Length - packet.BitsRead;
        }

        void LoadBooks(DataPacket packet)
        {
            packet.SkipBits(8);
            if (!packet.ReadBytes(6).SequenceEqual(new byte[] { 0x76, 0x6f, 0x72, 0x62, 0x69, 0x73 })) throw new InvalidDataException("Corrupted book header!");

			long bits = packet.BitsRead;

			this._glueBits += packet.BitsRead;

			// get books
			this.Books = new VorbisCodebook[packet.ReadByte() + 1];
            for (int i = 0; i < this.Books.Length; i++)
            {
				this.Books[i] = VorbisCodebook.Init(this, packet, i);
            }

			this._bookBits += packet.BitsRead - bits;
            bits = packet.BitsRead;

			// get times
			this.Times = new VorbisTime[(int)packet.ReadBits(6) + 1];
            for (int i = 0; i < this.Times.Length; i++)
            {
				this.Times[i] = VorbisTime.Init(this, packet);
            }

			this._timeHdrBits += packet.BitsRead - bits;
            bits = packet.BitsRead;

			// get floor
			this.Floors = new VorbisFloor[(int)packet.ReadBits(6) + 1];
            for (int i = 0; i < this.Floors.Length; i++)
            {
				this.Floors[i] = VorbisFloor.Init(this, packet);
            }

			this._floorHdrBits += packet.BitsRead - bits;
            bits = packet.BitsRead;

			// get residue
			this.Residues = new VorbisResidue[(int)packet.ReadBits(6) + 1];
            for (int i = 0; i < this.Residues.Length; i++)
            {
				this.Residues[i] = VorbisResidue.Init(this, packet);
            }

			this._resHdrBits += packet.BitsRead - bits;
            bits = packet.BitsRead;

			// get map
			this.Maps = new VorbisMapping[(int)packet.ReadBits(6) + 1];
            for (int i = 0; i < this.Maps.Length; i++)
            {
				this.Maps[i] = VorbisMapping.Init(this, packet);
            }

			this._mapHdrBits += packet.BitsRead - bits;
            bits = packet.BitsRead;

			// get mode settings
			this.Modes = new VorbisMode[(int)packet.ReadBits(6) + 1];
            for (int i = 0; i < this.Modes.Length; i++)
            {
				this.Modes[i] = VorbisMode.Init(this, packet);
            }

			this._modeHdrBits += packet.BitsRead - bits;

            // check the framing bit
            if (!packet.ReadBit()) throw new InvalidDataException();

            ++this._glueBits;

			this._wasteHdrBits += 8 * packet.Length - packet.BitsRead;

			this._modeFieldBits = Utils.ilog(this.Modes.Length - 1);
        }

        #endregion

        #region Data Decode

        float[] _prevBuffer;
        RingBuffer _outputBuffer;
        Queue<int> _bitsPerPacketHistory;
        Queue<int> _sampleCountHistory;
        int _preparedLength;
        internal bool _clipped = false;

        Stack<DataPacket> _resyncQueue;

        long _currentPosition;

        VorbisMode _mode;
        bool _prevFlag, _nextFlag;
        bool[] _noExecuteChannel;
        VorbisFloor.PacketData[] _floorData;
        float[][] _residue;

        void InitDecoder()
        {
            if (this._outputBuffer != null)
            {
                SaveBuffer();
            }

			this._noExecuteChannel = new bool[this._channels];
			this._floorData = new VorbisFloor.PacketData[this._channels];

			this._residue = new float[this._channels][];
            for (int i = 0; i < this._channels; i++)
            {
				this._residue[i] = new float[this.Block1Size];
            }

			this._outputBuffer = new RingBuffer(this.Block1Size * 2 * this._channels);
			this._outputBuffer.Channels = this._channels;

			this._preparedLength = 0;
			this._currentPosition = 0L;

			this._resyncQueue = new Stack<DataPacket>();

			this._bitsPerPacketHistory = new Queue<int>();
			this._sampleCountHistory = new Queue<int>();
        }

        void ResetDecoder()
        {
            // this is called when the decoder encounters a "hiccup" in the data stream...
            // it is also called when a seek happens

            // save off the existing "good" data
            if (this._preparedLength > 0)
            {
                SaveBuffer();
            }
			this._outputBuffer.Clear();
			this._preparedLength = 0;
        }

        void SaveBuffer()
        {
			float[] buf = new float[this._preparedLength * this._channels];
            ReadSamples(buf, 0, buf.Length);
			this._prevBuffer = buf;
        }

        bool UnpackPacket(DataPacket packet)
        {
            // make sure we're on an audio packet
            if (packet.ReadBit())
            {
                // we really can't do anything... count the bits as waste
                return false;
            }

			// get mode and prev/next flags
			int modeBits = this._modeFieldBits;
			this._mode = this.Modes[(int)packet.ReadBits(this._modeFieldBits)];
            if (this._mode.BlockFlag)
            {
				this._prevFlag = packet.ReadBit();
				this._nextFlag = packet.ReadBit();
                modeBits += 2;
            }
            else
            {
				this._prevFlag = this._nextFlag = false;
            }

            if (packet.IsShort) return false;

			long startBits = packet.BitsRead;

			int halfBlockSize = this._mode.BlockSize / 2;

            // read the noise floor data (but don't decode yet)
            for (int i = 0; i < this._channels; i++)
            {
				this._floorData[i] = this._mode.Mapping.ChannelSubmap[i].Floor.UnpackPacket(packet, this._mode.BlockSize, i);
				this._noExecuteChannel[i] = !this._floorData[i].ExecuteChannel;

                // go ahead and clear the residue buffers
                Array.Clear(this._residue[i], 0, halfBlockSize);
            }

            // make sure we handle no-energy channels correctly given the couplings...
            foreach (VorbisMapping.CouplingStep step in this._mode.Mapping.CouplingSteps)
            {
                if (this._floorData[step.Angle].ExecuteChannel || this._floorData[step.Magnitude].ExecuteChannel)
                {
					this._floorData[step.Angle].ForceEnergy = true;
					this._floorData[step.Magnitude].ForceEnergy = true;
                }
            }

			long floorBits = packet.BitsRead - startBits;
            startBits = packet.BitsRead;

            foreach (VorbisMapping.Submap subMap in this._mode.Mapping.Submaps)
            {
                for (int j = 0; j < this._channels; j++)
                {
                    if (this._mode.Mapping.ChannelSubmap[j] != subMap)
                    {
						this._floorData[j].ForceNoEnergy = true;
                    }
                }

				float[][] rTemp = subMap.Residue.Decode(packet, this._noExecuteChannel, this._channels, this._mode.BlockSize);
                for (int c = 0; c < this._channels; c++)
                {
					float[] r = this._residue[c];
					float[] rt = rTemp[c];
                    for (int i = 0; i < halfBlockSize; i++)
                    {
                        r[i] += rt[i];
                    }
                }
            }

			this._glueBits += 1;
			this._modeBits += modeBits;
			this._floorBits += floorBits;
			this._resBits += packet.BitsRead - startBits;
			this._wasteBits += 8 * packet.Length - packet.BitsRead;

			this._packetCount += 1;

            return true;
        }

        void DecodePacket()
        {
			// inverse coupling
			VorbisMapping.CouplingStep[] steps = this._mode.Mapping.CouplingSteps;
			int halfSizeW = this._mode.BlockSize / 2;
            for (int i = steps.Length - 1; i >= 0; i--)
            {
                if (this._floorData[steps[i].Angle].ExecuteChannel || this._floorData[steps[i].Magnitude].ExecuteChannel)
                {
					float[] magnitude = this._residue[steps[i].Magnitude];
					float[] angle = this._residue[steps[i].Angle];

                    // we only have to do the first half; MDCT ignores the last half
                    for (int j = 0; j < halfSizeW; j++)
                    {
                        float newM, newA;

                        if (magnitude[j] > 0)
                        {
                            if (angle[j] > 0)
                            {
                                newM = magnitude[j];
                                newA = magnitude[j] - angle[j];
                            }
                            else
                            {
                                newA = magnitude[j];
                                newM = magnitude[j] + angle[j];
                            }
                        }
                        else
                        {
                            if (angle[j] > 0)
                            {
                                newM = magnitude[j];
                                newA = magnitude[j] + angle[j];
                            }
                            else
                            {
                                newA = magnitude[j];
                                newM = magnitude[j] - angle[j];
                            }
                        }

                        magnitude[j] = newM;
                        angle[j] = newA;
                    }
                }
            }

            // apply floor / dot product / MDCT (only run if we have sound energy in that channel)
            for (int c = 0; c < this._channels; c++)
            {
				VorbisFloor.PacketData floorData = this._floorData[c];
				float[] res = this._residue[c];
                if (floorData.ExecuteChannel)
                {
					this._mode.Mapping.ChannelSubmap[c].Floor.Apply(floorData, res);
                    Mdct.Reverse(res, this._mode.BlockSize);
                }
                else
                {
                    // since we aren't doing the IMDCT, we have to explicitly clear the back half of the block
                    Array.Clear(res, halfSizeW, halfSizeW);
                }
            }
        }

        int OverlapSamples()
        {
			// window
			float[] window = this._mode.GetWindow(this._prevFlag, this._nextFlag);
			// this is applied as part of the lapping operation

			// now lap the data into the buffer...

			int sizeW = this._mode.BlockSize;
			int right = sizeW;
			int center = right >> 1;
			int left = 0;
			int begin = -center;
			int end = center;

            if (this._mode.BlockFlag)
            {
                // if the flag is true, it's a long block
                // if the flag is false, it's a short block
                if (!this._prevFlag)
                {
                    // previous block was short
                    left = this.Block1Size / 4 - this.Block0Size / 4;  // where to start in pcm[][]
                    center = left + this.Block0Size / 2;     // adjust the center so we're correctly clearing the buffer...
                    begin = this.Block0Size / -2 - left;     // where to start in _outputBuffer[,]
                }

                if (!this._nextFlag)
                {
                    // next block is short
                    right -= sizeW / 4 - this.Block0Size / 4;
                    end = sizeW / 4 + this.Block0Size / 4;
                }
            }
			// short blocks don't need any adjustments

			int idx = this._outputBuffer.Length / this._channels + begin;
            for (int c = 0; c < this._channels; c++)
            {
				this._outputBuffer.Write(c, idx, left, center, right, this._residue[c], window);
            }

			int newPrepLen = this._outputBuffer.Length / this._channels - end;
			int samplesDecoded = newPrepLen - this._preparedLength;
			this._preparedLength = newPrepLen;

            return samplesDecoded;
        }

        void UpdatePosition(int samplesDecoded, DataPacket packet)
        {
			this._samples += samplesDecoded;

            if (packet.IsResync)
            {
				// during a resync, we have to go through and watch for the next "marker"
				this._currentPosition = -packet.PageGranulePosition;
				// _currentPosition will now be end of the page...  wait for the value to change, then go back and repopulate the granule positions accordingly...
				this._resyncQueue.Push(packet);
            }
            else
            {
                if (samplesDecoded > 0)
                {
					this._currentPosition += samplesDecoded;
                    packet.GranulePosition = this._currentPosition;

                    if (this._currentPosition < 0)
                    {
                        if (packet.PageGranulePosition > -this._currentPosition)
                        {
							// we now have a valid granuleposition...  populate the queued packets' GranulePositions
							long gp = this._currentPosition - samplesDecoded;
                            while (this._resyncQueue.Count > 0)
                            {
								DataPacket pkt = this._resyncQueue.Pop();

								long temp = pkt.GranulePosition + gp;
                                pkt.GranulePosition = gp;
                                gp = temp;
                            }
                        }
                        else
                        {
                            packet.GranulePosition = -samplesDecoded;
							this._resyncQueue.Push(packet);
                        }
                    }
                    else if (packet.IsEndOfStream && this._currentPosition > packet.PageGranulePosition)
                    {
						int diff = (int)(this._currentPosition - packet.PageGranulePosition);
                        if (diff >= 0)
                        {
							this._preparedLength -= diff;
							this._currentPosition -= diff;
                        }
                        else
                        {
							// uh-oh.  We're supposed to have more samples to this point...
							this._preparedLength = 0;
                        }
                        packet.GranulePosition = packet.PageGranulePosition;
						this._eosFound = true;
                    }
                }
            }
        }

        void DecodeNextPacket()
        {
			this._sw.Start();

            DataPacket packet = null;
            try
            {
				// get the next packet
				IPacketProvider packetProvider = this._packetProvider;
                if (packetProvider != null)
                {
                    packet = packetProvider.GetNextPacket();
                }

                // if the packet is null, our packet reader is gone...
                if (packet == null)
                {
					this._eosFound = true;
                    return;
                }

                // keep our page count in sync
                if (!this._pagesSeen.Contains((this._lastPageSeen = packet.PageSequenceNumber))) this._pagesSeen.Add(this._lastPageSeen);

                // check for resync
                if (packet.IsResync)
                {
                    ResetDecoder(); // if we're a resync, our current decoder state is invalid...
                }

                if (!UnpackPacket(packet))
                {
                    packet.Done();
					this._wasteBits += 8 * packet.Length;
                    return;
                }
                packet.Done();

                // we can now safely decode all the data without having to worry about a corrupt or partial packet

                DecodePacket();
				int samplesDecoded = OverlapSamples();

                // we can do something cool here...  mark down how many samples were decoded in this packet
                if (packet.GranuleCount.HasValue == false)
                {
                    packet.GranuleCount = samplesDecoded;
                }

                // update our position

                UpdatePosition(samplesDecoded, packet);

				// a little statistical housekeeping...
				int sc = Utils.Sum(this._sampleCountHistory) + samplesDecoded;

				this._bitsPerPacketHistory.Enqueue((int)packet.BitsRead);
				this._sampleCountHistory.Enqueue(samplesDecoded);

                while (sc > this._sampleRate)
                {
					this._bitsPerPacketHistory.Dequeue();
                    sc -= this._sampleCountHistory.Dequeue();
                }
            }
            catch
            {
                if (packet != null)
                {
                    packet.Done();
                }
                throw;
            }
            finally
            {
				this._sw.Stop();
            }
        }

        internal int GetPacketLength(DataPacket curPacket, DataPacket lastPacket)
        {
            // if we don't have a previous packet, or we're re-syncing, this packet has no audio data to return
            if (lastPacket == null || curPacket.IsResync) return 0;

            // make sure they are audio packets
            if (curPacket.ReadBit()) return 0;
            if (lastPacket.ReadBit()) return 0;

			// get the current packet's information
			int modeIdx = (int)curPacket.ReadBits(this._modeFieldBits);
            if (modeIdx < 0 || modeIdx >= this.Modes.Length) return 0;
			VorbisMode mode = this.Modes[modeIdx];

            // get the last packet's information
            modeIdx = (int)lastPacket.ReadBits(this._modeFieldBits);
            if (modeIdx < 0 || modeIdx >= this.Modes.Length) return 0;
			VorbisMode prevMode = this.Modes[modeIdx];

            // now calculate the totals...
            return mode.BlockSize / 4 + prevMode.BlockSize / 4;
        }
        
        #endregion

        internal int ReadSamples(float[] buffer, int offset, int count)
        {
            int samplesRead = 0;

            if (this._prevBuffer != null)
            {
				// get samples from the previous buffer's data
				int cnt = Math.Min(count, this._prevBuffer.Length);
                Buffer.BlockCopy(this._prevBuffer, 0, buffer, offset, cnt * sizeof(float));

                // if we have samples left over, rebuild the previous buffer array...
                if (cnt < this._prevBuffer.Length)
                {
					float[] buf = new float[this._prevBuffer.Length - cnt];
                    Buffer.BlockCopy(this._prevBuffer, cnt * sizeof(float), buf, 0, (this._prevBuffer.Length - cnt) * sizeof(float));
					this._prevBuffer = buf;
                }
                else
                {
					// if no samples left over, clear the previous buffer
					this._prevBuffer = null;
                }

                // reduce the desired sample count & increase the desired sample offset
                count -= cnt;
                offset += cnt;
                samplesRead = cnt;
            }

            int minSize = count + this.Block1Size * this._channels;
			this._outputBuffer.EnsureSize(minSize);

            while (this._preparedLength * this._channels < count && !this._eosFound)
            {
                try
                {
                    DecodeNextPacket();

                    // we can safely assume the _prevBuffer was null when we entered this loop
                    if (this._prevBuffer != null)
                    {
                        // uh-oh... something is wrong...
                        return ReadSamples(buffer, offset, this._prevBuffer.Length);
                    }
                }
                catch (EndOfStreamException)
                {
					this._eosFound = true;
                    break;
                }
            }

            if (this._preparedLength * this._channels < count)
            {
                // we can safely assume we've read the last packet...
                count = this._preparedLength * this._channels;
            }

			this._outputBuffer.CopyTo(buffer, offset, count);
			this._preparedLength -= count / this._channels;

            return samplesRead + count;
        }

		internal bool CanSeek
		{
			get
			{
				return this._packetProvider.CanSeek;
			}
		}

		internal void SeekTo(long granulePos)
        {
            if (!this._packetProvider.CanSeek) throw new NotSupportedException();

            if (granulePos < 0) throw new ArgumentOutOfRangeException("granulePos");

			int targetPacketIndex = 3;
            if (granulePos > 0)
            {
				int idx = this._packetProvider.FindPacket(granulePos, GetPacketLength);
                if (idx == -1) throw new ArgumentOutOfRangeException("granulePos");
                targetPacketIndex = idx - 1;  // move to the previous packet to prime the decoder
            }

			// seek the stream
			this._packetProvider.SeekToPacket(targetPacketIndex);

			// now figure out where we are and how many samples we need to discard...
			// note that we use the granule position of the "current" packet, since it will be discarded no matter what

			// get the packet that we'll decode next
			DataPacket dataPacket = this._packetProvider.PeekNextPacket();

            // now read samples until we are exactly at the granule position requested
            CurrentPosition = dataPacket.GranulePosition;
			int cnt = (int)((granulePos - CurrentPosition) * this._channels);
            if (cnt > 0)
            {
				float[] seekBuffer = new float[cnt];
                while (cnt > 0)
                {
					int temp = ReadSamples(seekBuffer, 0, cnt);
                    if (temp == 0) break;   // we're at the end...
                    cnt -= temp;
                }
            }
        }

        internal long CurrentPosition
		{
			get
			{
				return this._currentPosition - this._preparedLength;
			}

			set
			{
				this._currentPosition = value;
				this._preparedLength = 0;
				this._eosFound = false;

				ResetDecoder();
				this._prevBuffer = null;
			}
		}

		internal long GetLastGranulePos()
        {
            return this._packetProvider.GetGranuleCount();
        }

		internal long ContainerBits
		{
			get
			{
				return this._packetProvider.ContainerBits;
			}
		}

		public void ResetStats()
        {
			// only reset the stream info...  don't mess with the container, book, and hdr bits...

			this._clipped = false;
			this._packetCount = 0;
			this._floorBits = 0L;
			this._glueBits = 0L;
			this._modeBits = 0L;
			this._resBits = 0L;
			this._wasteBits = 0L;
			this._samples = 0L;
			this._sw.Reset();
        }

        public int EffectiveBitRate
        {
            get
            {
                if (this._samples == 0L) return 0;

				double decodedSeconds = (double)(this._currentPosition - this._preparedLength) / this._sampleRate;

                return (int)(AudioBits / decodedSeconds);
            }
        }

        public int InstantBitRate
        {
            get
            {
                try
                {
                    return (int)((long)this._bitsPerPacketHistory.Sum() * this._sampleRate / this._sampleCountHistory.Sum());
                }
                catch (DivideByZeroException)
                {
                    return -1;
                }
            }
        }

		public TimeSpan PageLatency
		{
			get
			{
				return TimeSpan.FromTicks(this._sw.ElapsedTicks / PagesRead);
			}
		}

		public TimeSpan PacketLatency
		{
			get
			{
				return TimeSpan.FromTicks(this._sw.ElapsedTicks / this._packetCount);
			}
		}

		public TimeSpan SecondLatency
		{
			get
			{
				return TimeSpan.FromTicks((this._sw.ElapsedTicks / this._samples) * this._sampleRate);
			}
		}

		public long OverheadBits
		{
			get
			{
				return this._glueBits + this._metaBits + this._timeHdrBits + this._wasteHdrBits + this._wasteBits + this._packetProvider.ContainerBits;
			}
		}

		public long AudioBits
		{
			get
			{
				return this._bookBits + this._floorHdrBits + this._resHdrBits + this._mapHdrBits + this._modeHdrBits + this._modeBits + this._floorBits + this._resBits;
			}
		}

		public int PagesRead
		{
			get
			{
				return this._pagesSeen.IndexOf(this._lastPageSeen) + 1;
			}
		}

		public int TotalPages
		{
			get
			{
				return this._packetProvider.GetTotalPageCount();
			}
		}

		public bool Clipped
		{
			get
			{
				return this._clipped;
			}
		}
	}
}
