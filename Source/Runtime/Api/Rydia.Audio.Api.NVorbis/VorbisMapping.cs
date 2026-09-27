/****************************************************************************
 * NVorbis                                                                  *
 * Copyright (C) 2012, Andrew Ward <afward@gmail.com>                       *
 *                                                                          *
 * See COPYING for license terms (Ms-PL).                                   *
 *                                                                          *
 ***************************************************************************/
using System.IO;

namespace Rydia.Audio.Api.NVorbis
{
    abstract class VorbisMapping
    {
        internal static VorbisMapping Init(VorbisStreamDecoder vorbis, DataPacket packet)
        {
			int type = (int)packet.ReadBits(16);

            VorbisMapping mapping = null;
            switch (type)
            {
                case 0: mapping = new Mapping0(vorbis); break;
            }
            if (mapping == null) throw new InvalidDataException();

            mapping.Init(packet);
            return mapping;
        }

        VorbisStreamDecoder _vorbis;

        protected VorbisMapping(VorbisStreamDecoder vorbis)
        {
			this._vorbis = vorbis;
        }

        abstract protected void Init(DataPacket packet);

        internal Submap[] Submaps;

        internal Submap[] ChannelSubmap;

        internal CouplingStep[] CouplingSteps;

        class Mapping0 : VorbisMapping
        {
            internal Mapping0(VorbisStreamDecoder vorbis) : base(vorbis) { }

            protected override void Init(DataPacket packet)
            {
				int submapCount = 1;
                if (packet.ReadBit()) submapCount += (int)packet.ReadBits(4);

				// square polar mapping
				int couplingSteps = 0;
                if (packet.ReadBit())
                {
                    couplingSteps = (int)packet.ReadBits(8) + 1;
                }

				int couplingBits = Utils.ilog(this._vorbis._channels - 1);
				this.CouplingSteps = new CouplingStep[couplingSteps];
                for (int j = 0; j < couplingSteps; j++)
                {
					int magnitude = (int)packet.ReadBits(couplingBits);
					int angle = (int)packet.ReadBits(couplingBits);
                    if (magnitude == angle || magnitude > this._vorbis._channels - 1 || angle > this._vorbis._channels - 1)
                        throw new InvalidDataException();
					this.CouplingSteps[j] = new CouplingStep { Angle = angle, Magnitude = magnitude };
                }

                // reserved bits
                if (packet.ReadBits(2) != 0UL) throw new InvalidDataException();

				// channel multiplex
				int[] mux = new int[this._vorbis._channels];
                if (submapCount > 1)
                {
                    for (int c = 0; c < this.ChannelSubmap.Length; c++)
                    {
                        mux[c] = (int)packet.ReadBits(4);
                        if (mux[c] >= submapCount) throw new InvalidDataException();
                    }
                }

				// submaps
				this.Submaps = new Submap[submapCount];
                for (int j = 0; j < submapCount; j++)
                {
                    packet.ReadBits(8); // unused placeholder
					int floorNum = (int)packet.ReadBits(8);
                    if (floorNum >= this._vorbis.Floors.Length) throw new InvalidDataException();
					int residueNum = (int)packet.ReadBits(8);
                    if (residueNum >= this._vorbis.Residues.Length) throw new InvalidDataException();

					this.Submaps[j] = new Submap
                    {
                        Floor = this._vorbis.Floors[floorNum],
                        Residue = this._vorbis.Residues[floorNum]
                    };
                }

				this.ChannelSubmap = new Submap[this._vorbis._channels];
                for (int c = 0; c < this.ChannelSubmap.Length; c++)
                {
					this.ChannelSubmap[c] = this.Submaps[mux[c]];
                }
            }
        }

        internal class Submap
        {
            internal Submap() { }

            internal VorbisFloor Floor;
            internal VorbisResidue Residue;
        }

        internal class CouplingStep
        {
            internal CouplingStep() { }

            internal int Magnitude;
            internal int Angle;
        }
    }
}
