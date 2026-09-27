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
    public class VorbisReader : IDisposable
    {
        int _streamIdx;

        IContainerReader _containerReader;
        List<VorbisStreamDecoder> _decoders;
        List<int> _serials;

        VorbisReader()
        {
            ClipSamples = true;

			this._decoders = new List<VorbisStreamDecoder>();
			this._serials = new List<int>();

        }

        public VorbisReader(Stream stream, bool closeStreamOnDispose)
            : this()
        {
			BufferedReadStream bufferedStream = new BufferedReadStream(stream);
            bufferedStream.CloseBaseStream = closeStreamOnDispose;

			// try Ogg first
			Ogg.ContainerReader oggContainer = new Ogg.ContainerReader(bufferedStream, closeStreamOnDispose);
            if (!LoadContainer(oggContainer))
            {
                // oops, not Ogg!
                // we don't support any other container types yet, so error out
                bufferedStream.Dispose();
                throw new InvalidDataException("Could not determine container type!");
            }
			this._containerReader = oggContainer;

            if (this._decoders.Count == 0) throw new InvalidDataException("No Vorbis data found!");
        }

        public VorbisReader(IContainerReader containerReader)
            : this()
        {
            if (!LoadContainer(containerReader))
            {
                throw new InvalidDataException("Container did not initialize!");
            }
			this._containerReader = containerReader;

            if (this._decoders.Count == 0) throw new InvalidDataException("No Vorbis data found!");
        }

        public VorbisReader(IPacketProvider packetProvider)
            : this()
        {
			NewStreamEventArgs ea = new NewStreamEventArgs(packetProvider);
            NewStream(this, ea);
            if (ea.IgnoreStream) throw new InvalidDataException("No Vorbis data found!");
        }

        bool LoadContainer(IContainerReader containerReader)
        {
            containerReader.NewStream += NewStream;
            if (!containerReader.Init())
            {
                containerReader.NewStream -= NewStream;
                return false;
            }
            return true;
        }

        void NewStream(object sender, NewStreamEventArgs ea)
        {
			IPacketProvider packetProvider = ea.PacketProvider;
			VorbisStreamDecoder decoder = new VorbisStreamDecoder(packetProvider);
            if (decoder.TryInit())
            {
				this._decoders.Add(decoder);
				this._serials.Add(packetProvider.StreamSerial);
            }
            else
            {
                // NB: This could be an Ogg Skeleton stream...  We should check that, just in case
                // NB: This could be a RTP stream...  We should check that, just in case

                ea.IgnoreStream = true;
            }
        }

        public void Dispose()
        {
            if (this._decoders != null)
            {
                foreach (VorbisStreamDecoder decoder in this._decoders)
                {
                    decoder.Dispose();
                }
				this._decoders.Clear();
				this._decoders = null;
            }

            if (this._containerReader != null)
            {
				this._containerReader.NewStream -= NewStream;
				this._containerReader.Dispose();
				this._containerReader = null;
            }
        }

        VorbisStreamDecoder ActiveDecoder
        {
            get
            {
                if (this._decoders == null) throw new ObjectDisposedException("VorbisReader");
                return this._decoders[this._streamIdx];
            }
        }

		#region Public Interface

		/// <summary>
		/// Gets the number of channels in the current selected Vorbis stream
		/// </summary>
		public int Channels
		{
			get
			{
				return ActiveDecoder._channels;
			}
		}

		/// <summary>
		/// Gets the sample rate of the current selected Vorbis stream
		/// </summary>
		public int SampleRate
		{
			get
			{
				return ActiveDecoder._sampleRate;
			}
		}

		/// <summary>
		/// Gets the encoder's upper bitrate of the current selected Vorbis stream
		/// </summary>
		public int UpperBitrate
		{
			get
			{
				return ActiveDecoder._upperBitrate;
			}
		}

		/// <summary>
		/// Gets the encoder's nominal bitrate of the current selected Vorbis stream
		/// </summary>
		public int NominalBitrate
		{
			get
			{
				return ActiveDecoder._nominalBitrate;
			}
		}

		/// <summary>
		/// Gets the encoder's lower bitrate of the current selected Vorbis stream
		/// </summary>
		public int LowerBitrate
		{
			get
			{
				return ActiveDecoder._lowerBitrate;
			}
		}

		/// <summary>
		/// Gets the encoder's vendor string for the current selected Vorbis stream
		/// </summary>
		public string Vendor
		{
			get
			{
				return ActiveDecoder._vendor;
			}
		}

		/// <summary>
		/// Gets the comments in the current selected Vorbis stream
		/// </summary>
		public string[] Comments
		{
			get
			{
				return ActiveDecoder._comments;
			}
		}

		/// <summary>
		/// Gets the number of bits read that are related to framing and transport alone
		/// </summary>
		public long ContainerOverheadBits
		{
			get
			{
				return ActiveDecoder.ContainerBits;
			}
		}

		/// <summary>
		/// Gets or sets whether to automatically apply clipping to samples returned by <see cref="VorbisReader.ReadSamples"/>.
		/// </summary>
		public bool ClipSamples { get; set; }

		/// <summary>
		/// Gets stats from each decoder stream available
		/// </summary>
		public IVorbisStreamStatus[] Stats
		{
			get
			{
				return this._decoders.Select(d => d).Cast<IVorbisStreamStatus>().ToArray();
			}
		}

		/// <summary>
		/// Gets the currently-selected stream's index
		/// </summary>
		public int StreamIndex
		{
			get
			{
				return this._streamIdx;
			}
		}

		/// <summary>
		/// Reads decoded samples from the current logical stream
		/// </summary>
		/// <param name="buffer">The buffer to write the samples to</param>
		/// <param name="offset">The offset into the buffer to write the samples to</param>
		/// <param name="count">The number of samples to write</param>
		/// <returns>The number of samples written</returns>
		public int ReadSamples(float[] buffer, int offset, int count)
        {
            if (offset < 0) throw new ArgumentOutOfRangeException("offset");
            if (count < 0 || offset + count > buffer.Length) throw new ArgumentOutOfRangeException("count");

            count = ActiveDecoder.ReadSamples(buffer, offset, count);

            if (ClipSamples)
            {
				VorbisStreamDecoder decoder = this._decoders[this._streamIdx];
                for (int i = 0; i < count; i++, offset++)
                {
                    buffer[offset] = Utils.ClipValue(buffer[offset], ref decoder._clipped);
                }
            }

            return count;
        }

		/// <summary>
		/// Returns the number of logical streams found so far in the physical container
		/// </summary>
		public int StreamCount
		{
			get
			{
				return this._decoders.Count;
			}
		}

		/// <summary>
		/// Switches to an alternate logical stream.
		/// </summary>
		/// <param name="index">The logical stream index to switch to</param>
		/// <returns><c>True</c> if the properties of the logical stream differ from those of the one previously being decoded. Otherwise, <c>False</c>.</returns>
		public bool SwitchStreams(int index)
        {
            if (index < 0 || index >= StreamCount) throw new ArgumentOutOfRangeException("index");

            if (this._decoders == null) throw new ObjectDisposedException("VorbisReader");

            if (this._streamIdx == index) return false;

			VorbisStreamDecoder curDecoder = this._decoders[this._streamIdx];
			this._streamIdx = index;
			VorbisStreamDecoder newDecoder = this._decoders[this._streamIdx];

            return curDecoder._channels != newDecoder._channels || curDecoder._sampleRate != newDecoder._sampleRate;
        }

        /// <summary>
        /// Gets or Sets the current timestamp of the decoder.  Is the timestamp before the next sample to be decoded
        /// </summary>
        public TimeSpan DecodedTime
		{
			get
			{
				VorbisStreamDecoder decoder = ActiveDecoder;
				return TimeSpan.FromSeconds((double)decoder.CurrentPosition / decoder._sampleRate);
			}
			set
			{
				ActiveDecoder.SeekTo((long)(value.TotalSeconds * SampleRate));
			}
		}

		/// <summary>
		/// Gets the total length of the current logical stream
		/// </summary>
		public TimeSpan TotalTime
        {
            get
            {
				VorbisStreamDecoder decoder = ActiveDecoder;
                if (decoder.CanSeek)
                {
                    return TimeSpan.FromSeconds((double)decoder.GetLastGranulePos() / decoder._sampleRate);
                }
                else
                {
                    return TimeSpan.MaxValue;
                }
            }
        }
        
        #endregion
    }
}
