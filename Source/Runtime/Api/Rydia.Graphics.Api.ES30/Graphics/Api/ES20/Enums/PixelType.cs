using System;
using System.Collections.Generic;
using System.Text;

namespace Rydia.Graphics.Api.ES20
{

    //
    // 概要:
    //     Used in GL.ReadnPixels, GL.ReadPixels and 5 other functions
    public enum PixelType
    {
        //
        // 概要:
        //     Original was GL_BYTE = 0x1400
        Byte = 5120,
        //
        // 概要:
        //     Original was GL_UNSIGNED_BYTE = 0x1401
        UnsignedByte = 5121,
        //
        // 概要:
        //     Original was GL_SHORT = 0x1402
        Short = 5122,
        //
        // 概要:
        //     Original was GL_UNSIGNED_SHORT = 0x1403
        UnsignedShort = 5123,
        //
        // 概要:
        //     Original was GL_INT = 0x1404
        Int = 5124,
        //
        // 概要:
        //     Original was GL_UNSIGNED_INT = 0x1405
        UnsignedInt = 5125,
        //
        // 概要:
        //     Original was GL_FLOAT = 0x1406
        Float = 5126,
        //
        // 概要:
        //     Original was GL_BITMAP = 0x1A00
        Bitmap = 6656,
        //
        // 概要:
        //     Original was GL_UNSIGNED_BYTE_3_3_2 = 0x8032
        UnsignedByte332 = 32818,
        //
        // 概要:
        //     Original was GL_UNSIGNED_BYTE_3_3_2_EXT = 0x8032
        UnsignedByte332Ext = 32818,
        //
        // 概要:
        //     Original was GL_UNSIGNED_SHORT_4_4_4_4 = 0x8033
        UnsignedShort4444 = 32819,
        //
        // 概要:
        //     Original was GL_UNSIGNED_SHORT_4_4_4_4_EXT = 0x8033
        UnsignedShort4444Ext = 32819,
        //
        // 概要:
        //     Original was GL_UNSIGNED_SHORT_5_5_5_1 = 0x8034
        UnsignedShort5551 = 32820,
        //
        // 概要:
        //     Original was GL_UNSIGNED_SHORT_5_5_5_1_EXT = 0x8034
        UnsignedShort5551Ext = 32820,
        //
        // 概要:
        //     Original was GL_UNSIGNED_INT_8_8_8_8 = 0x8035
        UnsignedInt8888 = 32821,
        //
        // 概要:
        //     Original was GL_UNSIGNED_INT_8_8_8_8_EXT = 0x8035
        UnsignedInt8888Ext = 32821,
        //
        // 概要:
        //     Original was GL_UNSIGNED_INT_10_10_10_2 = 0x8036
        UnsignedInt1010102 = 32822,
        //
        // 概要:
        //     Original was GL_UNSIGNED_INT_10_10_10_2_EXT = 0x8036
        UnsignedInt1010102Ext = 32822,
        //
        // 概要:
        //     Original was GL_UnsignedShort565 = 0X8363
        UnsignedShort565 = 33635
    }

}
