using System;
using System.Collections.Generic;
using System.Text;

namespace Rydia.Graphics.Api.ES20
{

    //
    // 概要:
    //     Used in GL.TexParameter, GL.Ext.TexParameterI
    public enum TextureParameterName
    {
        //
        // 概要:
        //     Original was GL_TEXTURE_BORDER_COLOR = 0x1004
        TextureBorderColor = 4100,
        //
        // 概要:
        //     Original was GL_TEXTURE_MAG_FILTER = 0x2800
        TextureMagFilter = 10240,
        //
        // 概要:
        //     Original was GL_TEXTURE_MIN_FILTER = 0x2801
        TextureMinFilter = 10241,
        //
        // 概要:
        //     Original was GL_TEXTURE_WRAP_S = 0x2802
        TextureWrapS = 10242,
        //
        // 概要:
        //     Original was GL_TEXTURE_WRAP_T = 0x2803
        TextureWrapT = 10243,
        //
        // 概要:
        //     Original was GL_TEXTURE_PRIORITY = 0x8066
        TexturePriority = 32870,
        //
        // 概要:
        //     Original was GL_TEXTURE_PRIORITY_EXT = 0x8066
        TexturePriorityExt = 32870,
        //
        // 概要:
        //     Original was GL_TEXTURE_WRAP_R = 0x8072
        TextureWrapR = 32882,
        //
        // 概要:
        //     Original was GL_TEXTURE_WRAP_R_EXT = 0x8072
        TextureWrapRExt = 32882,
        //
        // 概要:
        //     Original was GL_TEXTURE_WRAP_R_OES = 0x8072
        TextureWrapROes = 32882,
        //
        // 概要:
        //     Original was GL_DETAIL_TEXTURE_LEVEL_SGIS = 0x809A
        DetailTextureLevelSgis = 32922,
        //
        // 概要:
        //     Original was GL_DETAIL_TEXTURE_MODE_SGIS = 0x809B
        DetailTextureModeSgis = 32923,
        //
        // 概要:
        //     Original was GL_SHADOW_AMBIENT_SGIX = 0x80BF
        ShadowAmbientSgix = 32959,
        //
        // 概要:
        //     Original was GL_DUAL_TEXTURE_SELECT_SGIS = 0x8124
        DualTextureSelectSgis = 33060,
        //
        // 概要:
        //     Original was GL_QUAD_TEXTURE_SELECT_SGIS = 0x8125
        QuadTextureSelectSgis = 33061,
        //
        // 概要:
        //     Original was GL_TEXTURE_WRAP_Q_SGIS = 0x8137
        TextureWrapQSgis = 33079,
        //
        // 概要:
        //     Original was GL_TEXTURE_CLIPMAP_CENTER_SGIX = 0x8171
        TextureClipmapCenterSgix = 33137,
        //
        // 概要:
        //     Original was GL_TEXTURE_CLIPMAP_FRAME_SGIX = 0x8172
        TextureClipmapFrameSgix = 33138,
        //
        // 概要:
        //     Original was GL_TEXTURE_CLIPMAP_OFFSET_SGIX = 0x8173
        TextureClipmapOffsetSgix = 33139,
        //
        // 概要:
        //     Original was GL_TEXTURE_CLIPMAP_VIRTUAL_DEPTH_SGIX = 0x8174
        TextureClipmapVirtualDepthSgix = 33140,
        //
        // 概要:
        //     Original was GL_TEXTURE_CLIPMAP_LOD_OFFSET_SGIX = 0x8175
        TextureClipmapLodOffsetSgix = 33141,
        //
        // 概要:
        //     Original was GL_TEXTURE_CLIPMAP_DEPTH_SGIX = 0x8176
        TextureClipmapDepthSgix = 33142,
        //
        // 概要:
        //     Original was GL_POST_TEXTURE_FILTER_BIAS_SGIX = 0x8179
        PostTextureFilterBiasSgix = 33145,
        //
        // 概要:
        //     Original was GL_POST_TEXTURE_FILTER_SCALE_SGIX = 0x817A
        PostTextureFilterScaleSgix = 33146,
        //
        // 概要:
        //     Original was GL_TEXTURE_LOD_BIAS_S_SGIX = 0x818E
        TextureLodBiasSSgix = 33166,
        //
        // 概要:
        //     Original was GL_TEXTURE_LOD_BIAS_T_SGIX = 0x818F
        TextureLodBiasTSgix = 33167,
        //
        // 概要:
        //     Original was GL_TEXTURE_LOD_BIAS_R_SGIX = 0x8190
        TextureLodBiasRSgix = 33168,
        //
        // 概要:
        //     Original was GL_GENERATE_MIPMAP = 0x8191
        GenerateMipmap = 33169,
        //
        // 概要:
        //     Original was GL_GENERATE_MIPMAP_SGIS = 0x8191
        GenerateMipmapSgis = 33169,
        //
        // 概要:
        //     Original was GL_TEXTURE_COMPARE_SGIX = 0x819A
        TextureCompareSgix = 33178,
        //
        // 概要:
        //     Original was GL_TEXTURE_MAX_CLAMP_S_SGIX = 0x8369
        TextureMaxClampSSgix = 33641,
        //
        // 概要:
        //     Original was GL_TEXTURE_MAX_CLAMP_T_SGIX = 0x836A
        TextureMaxClampTSgix = 33642,
        //
        // 概要:
        //     Original was GL_TEXTURE_MAX_CLAMP_R_SGIX = 0x836B
        TextureMaxClampRSgix = 33643
    }

}
