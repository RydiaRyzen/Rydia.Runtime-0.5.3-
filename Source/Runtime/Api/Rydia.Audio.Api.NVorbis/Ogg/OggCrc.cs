/****************************************************************************
 * NVorbis                                                                  *
 * Copyright (C) 2013, Andrew Ward <afward@gmail.com>                       *
 *                                                                          *
 * See COPYING for license terms (Ms-PL).                                   *
 *                                                                          *
 ***************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rydia.Audio.Api.NVorbis.Ogg
{
    class Crc
    {
        const uint CRC32_POLY = 0x04c11db7;
        static uint[] crcTable = new uint[256];

        static Crc()
        {
            for (uint i = 0; i < 256; i++)
            {
                uint s = i << 24;
                for (int j = 0; j < 8; ++j)
                {
                    s = (s << 1) ^ (s >= (1U << 31) ? CRC32_POLY : 0);
                }
                crcTable[i] = s;
            }
        }

        uint _crc;

        public Crc()
        {
            Reset();
        }

        public void Reset()
        {
			this._crc = 0U;
        }

        public void Update(int nextVal)
        {
			this._crc = (this._crc << 8) ^ crcTable[nextVal ^ (this._crc >> 24)];
        }

        public bool Test(uint checkCrc)
        {
            return this._crc == checkCrc;
        }
    }
}
