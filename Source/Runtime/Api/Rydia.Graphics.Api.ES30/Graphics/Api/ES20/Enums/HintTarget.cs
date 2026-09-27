using System;
using System.Collections.Generic;
using System.Text;

namespace Rydia.Graphics.Api.ES20
{

    //
    // 概要:
    //     Used in GL.Hint
    public enum HintTarget
    {
        //
        // 概要:
        //     Original was GL_PERSPECTIVE_CORRECTION_HINT = 0x0C50
        PerspectiveCorrectionHint = 3152,
        //
        // 概要:
        //     Original was GL_POINT_SMOOTH_HINT = 0x0C51
        PointSmoothHint = 3153,
        //
        // 概要:
        //     Original was GL_LINE_SMOOTH_HINT = 0x0C52
        LineSmoothHint = 3154,
        //
        // 概要:
        //     Original was GL_POLYGON_SMOOTH_HINT = 0x0C53
        PolygonSmoothHint = 3155,
        //
        // 概要:
        //     Original was GL_FOG_HINT = 0x0C54
        FogHint = 3156,
        //
        // 概要:
        //     Original was GL_PREFER_DOUBLEBUFFER_HINT_PGI = 0x1A1F8
        PreferDoublebufferHintPgi = 107000,
        //
        // 概要:
        //     Original was GL_CONSERVE_MEMORY_HINT_PGI = 0x1A1FD
        ConserveMemoryHintPgi = 107005,
        //
        // 概要:
        //     Original was GL_RECLAIM_MEMORY_HINT_PGI = 0x1A1FE
        ReclaimMemoryHintPgi = 107006,
        //
        // 概要:
        //     Original was GL_NATIVE_GRAPHICS_BEGIN_HINT_PGI = 0x1A203
        NativeGraphicsBeginHintPgi = 107011,
        //
        // 概要:
        //     Original was GL_NATIVE_GRAPHICS_END_HINT_PGI = 0x1A204
        NativeGraphicsEndHintPgi = 107012,
        //
        // 概要:
        //     Original was GL_ALWAYS_FAST_HINT_PGI = 0x1A20C
        AlwaysFastHintPgi = 107020,
        //
        // 概要:
        //     Original was GL_ALWAYS_SOFT_HINT_PGI = 0x1A20D
        AlwaysSoftHintPgi = 107021,
        //
        // 概要:
        //     Original was GL_ALLOW_DRAW_OBJ_HINT_PGI = 0x1A20E
        AllowDrawObjHintPgi = 107022,
        //
        // 概要:
        //     Original was GL_ALLOW_DRAW_WIN_HINT_PGI = 0x1A20F
        AllowDrawWinHintPgi = 107023,
        //
        // 概要:
        //     Original was GL_ALLOW_DRAW_FRG_HINT_PGI = 0x1A210
        AllowDrawFrgHintPgi = 107024,
        //
        // 概要:
        //     Original was GL_ALLOW_DRAW_MEM_HINT_PGI = 0x1A211
        AllowDrawMemHintPgi = 107025,
        //
        // 概要:
        //     Original was GL_STRICT_DEPTHFUNC_HINT_PGI = 0x1A216
        StrictDepthfuncHintPgi = 107030,
        //
        // 概要:
        //     Original was GL_STRICT_LIGHTING_HINT_PGI = 0x1A217
        StrictLightingHintPgi = 107031,
        //
        // 概要:
        //     Original was GL_STRICT_SCISSOR_HINT_PGI = 0x1A218
        StrictScissorHintPgi = 107032,
        //
        // 概要:
        //     Original was GL_FULL_STIPPLE_HINT_PGI = 0x1A219
        FullStippleHintPgi = 107033,
        //
        // 概要:
        //     Original was GL_CLIP_NEAR_HINT_PGI = 0x1A220
        ClipNearHintPgi = 107040,
        //
        // 概要:
        //     Original was GL_CLIP_FAR_HINT_PGI = 0x1A221
        ClipFarHintPgi = 107041,
        //
        // 概要:
        //     Original was GL_WIDE_LINE_HINT_PGI = 0x1A222
        WideLineHintPgi = 107042,
        //
        // 概要:
        //     Original was GL_BACK_NORMALS_HINT_PGI = 0x1A223
        BackNormalsHintPgi = 107043,
        //
        // 概要:
        //     Original was GL_VERTEX_DATA_HINT_PGI = 0x1A22A
        VertexDataHintPgi = 107050,
        //
        // 概要:
        //     Original was GL_VERTEX_CONSISTENT_HINT_PGI = 0x1A22B
        VertexConsistentHintPgi = 107051,
        //
        // 概要:
        //     Original was GL_MATERIAL_SIDE_HINT_PGI = 0x1A22C
        MaterialSideHintPgi = 107052,
        //
        // 概要:
        //     Original was GL_MAX_VERTEX_HINT_PGI = 0x1A22D
        MaxVertexHintPgi = 107053,
        //
        // 概要:
        //     Original was GL_PACK_CMYK_HINT_EXT = 0x800E
        PackCmykHintExt = 32782,
        //
        // 概要:
        //     Original was GL_UNPACK_CMYK_HINT_EXT = 0x800F
        UnpackCmykHintExt = 32783,
        //
        // 概要:
        //     Original was GL_PHONG_HINT_WIN = 0x80EB
        PhongHintWin = 33003,
        //
        // 概要:
        //     Original was GL_CLIP_VOLUME_CLIPPING_HINT_EXT = 0x80F0
        ClipVolumeClippingHintExt = 33008,
        //
        // 概要:
        //     Original was GL_TEXTURE_MULTI_BUFFER_HINT_SGIX = 0x812E
        TextureMultiBufferHintSgix = 33070,
        //
        // 概要:
        //     Original was GL_GENERATE_MIPMAP_HINT = 0x8192
        GenerateMipmapHint = 33170,
        //
        // 概要:
        //     Original was GL_GENERATE_MIPMAP_HINT_SGIS = 0x8192
        GenerateMipmapHintSgis = 33170,
        //
        // 概要:
        //     Original was GL_PROGRAM_BINARY_RETRIEVABLE_HINT = 0x8257
        ProgramBinaryRetrievableHint = 33367,
        //
        // 概要:
        //     Original was GL_CONVOLUTION_HINT_SGIX = 0x8316
        ConvolutionHintSgix = 33558,
        //
        // 概要:
        //     Original was GL_SCALEBIAS_HINT_SGIX = 0x8322
        ScalebiasHintSgix = 33570,
        //
        // 概要:
        //     Original was GL_LINE_QUALITY_HINT_SGIX = 0x835B
        LineQualityHintSgix = 33627,
        //
        // 概要:
        //     Original was GL_VERTEX_PRECLIP_SGIX = 0x83EE
        VertexPreclipSgix = 33774,
        //
        // 概要:
        //     Original was GL_VERTEX_PRECLIP_HINT_SGIX = 0x83EF
        VertexPreclipHintSgix = 33775,
        //
        // 概要:
        //     Original was GL_TEXTURE_COMPRESSION_HINT = 0x84EF
        TextureCompressionHint = 34031,
        //
        // 概要:
        //     Original was GL_TEXTURE_COMPRESSION_HINT_ARB = 0x84EF
        TextureCompressionHintArb = 34031,
        //
        // 概要:
        //     Original was GL_VERTEX_ARRAY_STORAGE_HINT_APPLE = 0x851F
        VertexArrayStorageHintApple = 34079,
        //
        // 概要:
        //     Original was GL_MULTISAMPLE_FILTER_HINT_NV = 0x8534
        MultisampleFilterHintNv = 34100,
        //
        // 概要:
        //     Original was GL_TRANSFORM_HINT_APPLE = 0x85B1
        TransformHintApple = 34225,
        //
        // 概要:
        //     Original was GL_TEXTURE_STORAGE_HINT_APPLE = 0x85BC
        TextureStorageHintApple = 34236,
        //
        // 概要:
        //     Original was GL_FRAGMENT_SHADER_DERIVATIVE_HINT = 0x8B8B
        FragmentShaderDerivativeHint = 35723,
        //
        // 概要:
        //     Original was GL_FRAGMENT_SHADER_DERIVATIVE_HINT_ARB = 0x8B8B
        FragmentShaderDerivativeHintArb = 35723,
        //
        // 概要:
        //     Original was GL_FRAGMENT_SHADER_DERIVATIVE_HINT_OES = 0x8B8B
        FragmentShaderDerivativeHintOes = 35723,
        //
        // 概要:
        //     Original was GL_BINNING_CONTROL_HINT_QCOM = 0x8FB0
        BinningControlHintQcom = 36784
    }

}
