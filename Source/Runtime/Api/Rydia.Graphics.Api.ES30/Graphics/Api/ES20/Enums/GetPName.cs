using System;
using System.Collections.Generic;
using System.Text;

namespace Rydia.Graphics.Api.ES20
{

    //
    // 概要:
    //     Used in GL.Apple.GetInteger64, GL.GetBoolean and 2 other functions
    public enum GetPName
    {
        //
        // 概要:
        //     Original was GL_CURRENT_COLOR = 0x0B00
        CurrentColor = 2816,
        //
        // 概要:
        //     Original was GL_CURRENT_INDEX = 0x0B01
        CurrentIndex = 2817,
        //
        // 概要:
        //     Original was GL_CURRENT_NORMAL = 0x0B02
        CurrentNormal = 2818,
        //
        // 概要:
        //     Original was GL_CURRENT_TEXTURE_COORDS = 0x0B03
        CurrentTextureCoords = 2819,
        //
        // 概要:
        //     Original was GL_CURRENT_RASTER_COLOR = 0x0B04
        CurrentRasterColor = 2820,
        //
        // 概要:
        //     Original was GL_CURRENT_RASTER_INDEX = 0x0B05
        CurrentRasterIndex = 2821,
        //
        // 概要:
        //     Original was GL_CURRENT_RASTER_TEXTURE_COORDS = 0x0B06
        CurrentRasterTextureCoords = 2822,
        //
        // 概要:
        //     Original was GL_CURRENT_RASTER_POSITION = 0x0B07
        CurrentRasterPosition = 2823,
        //
        // 概要:
        //     Original was GL_CURRENT_RASTER_POSITION_VALID = 0x0B08
        CurrentRasterPositionValid = 2824,
        //
        // 概要:
        //     Original was GL_CURRENT_RASTER_DISTANCE = 0x0B09
        CurrentRasterDistance = 2825,
        //
        // 概要:
        //     Original was GL_POINT_SMOOTH = 0x0B10
        PointSmooth = 2832,
        //
        // 概要:
        //     Original was GL_POINT_SIZE = 0x0B11
        PointSize = 2833,
        //
        // 概要:
        //     Original was GL_POINT_SIZE_RANGE = 0x0B12
        PointSizeRange = 2834,
        //
        // 概要:
        //     Original was GL_SMOOTH_POINT_SIZE_RANGE = 0x0B12
        SmoothPointSizeRange = 2834,
        //
        // 概要:
        //     Original was GL_POINT_SIZE_GRANULARITY = 0x0B13
        PointSizeGranularity = 2835,
        //
        // 概要:
        //     Original was GL_SMOOTH_POINT_SIZE_GRANULARITY = 0x0B13
        SmoothPointSizeGranularity = 2835,
        //
        // 概要:
        //     Original was GL_LINE_SMOOTH = 0x0B20
        LineSmooth = 2848,
        //
        // 概要:
        //     Original was GL_LINE_WIDTH = 0x0B21
        LineWidth = 2849,
        //
        // 概要:
        //     Original was GL_LINE_WIDTH_RANGE = 0x0B22
        LineWidthRange = 2850,
        //
        // 概要:
        //     Original was GL_SMOOTH_LINE_WIDTH_RANGE = 0x0B22
        SmoothLineWidthRange = 2850,
        //
        // 概要:
        //     Original was GL_LINE_WIDTH_GRANULARITY = 0x0B23
        LineWidthGranularity = 2851,
        //
        // 概要:
        //     Original was GL_SMOOTH_LINE_WIDTH_GRANULARITY = 0x0B23
        SmoothLineWidthGranularity = 2851,
        //
        // 概要:
        //     Original was GL_LINE_STIPPLE = 0x0B24
        LineStipple = 2852,
        //
        // 概要:
        //     Original was GL_LINE_STIPPLE_PATTERN = 0x0B25
        LineStipplePattern = 2853,
        //
        // 概要:
        //     Original was GL_LINE_STIPPLE_REPEAT = 0x0B26
        LineStippleRepeat = 2854,
        //
        // 概要:
        //     Original was GL_LIST_MODE = 0x0B30
        ListMode = 2864,
        //
        // 概要:
        //     Original was GL_MAX_LIST_NESTING = 0x0B31
        MaxListNesting = 2865,
        //
        // 概要:
        //     Original was GL_LIST_BASE = 0x0B32
        ListBase = 2866,
        //
        // 概要:
        //     Original was GL_LIST_INDEX = 0x0B33
        ListIndex = 2867,
        //
        // 概要:
        //     Original was GL_POLYGON_MODE = 0x0B40
        PolygonMode = 2880,
        //
        // 概要:
        //     Original was GL_POLYGON_SMOOTH = 0x0B41
        PolygonSmooth = 2881,
        //
        // 概要:
        //     Original was GL_POLYGON_STIPPLE = 0x0B42
        PolygonStipple = 2882,
        //
        // 概要:
        //     Original was GL_EDGE_FLAG = 0x0B43
        EdgeFlag = 2883,
        //
        // 概要:
        //     Original was GL_CULL_FACE = 0x0B44
        CullFace = 2884,
        //
        // 概要:
        //     Original was GL_CULL_FACE_MODE = 0x0B45
        CullFaceMode = 2885,
        //
        // 概要:
        //     Original was GL_FRONT_FACE = 0x0B46
        FrontFace = 2886,
        //
        // 概要:
        //     Original was GL_LIGHTING = 0x0B50
        Lighting = 2896,
        //
        // 概要:
        //     Original was GL_LIGHT_MODEL_LOCAL_VIEWER = 0x0B51
        LightModelLocalViewer = 2897,
        //
        // 概要:
        //     Original was GL_LIGHT_MODEL_TWO_SIDE = 0x0B52
        LightModelTwoSide = 2898,
        //
        // 概要:
        //     Original was GL_LIGHT_MODEL_AMBIENT = 0x0B53
        LightModelAmbient = 2899,
        //
        // 概要:
        //     Original was GL_SHADE_MODEL = 0x0B54
        ShadeModel = 2900,
        //
        // 概要:
        //     Original was GL_COLOR_MATERIAL_FACE = 0x0B55
        ColorMaterialFace = 2901,
        //
        // 概要:
        //     Original was GL_COLOR_MATERIAL_PARAMETER = 0x0B56
        ColorMaterialParameter = 2902,
        //
        // 概要:
        //     Original was GL_COLOR_MATERIAL = 0x0B57
        ColorMaterial = 2903,
        //
        // 概要:
        //     Original was GL_FOG = 0x0B60
        Fog = 2912,
        //
        // 概要:
        //     Original was GL_FOG_INDEX = 0x0B61
        FogIndex = 2913,
        //
        // 概要:
        //     Original was GL_FOG_DENSITY = 0x0B62
        FogDensity = 2914,
        //
        // 概要:
        //     Original was GL_FOG_START = 0x0B63
        FogStart = 2915,
        //
        // 概要:
        //     Original was GL_FOG_END = 0x0B64
        FogEnd = 2916,
        //
        // 概要:
        //     Original was GL_FOG_MODE = 0x0B65
        FogMode = 2917,
        //
        // 概要:
        //     Original was GL_FOG_COLOR = 0x0B66
        FogColor = 2918,
        //
        // 概要:
        //     Original was GL_DEPTH_RANGE = 0x0B70
        DepthRange = 2928,
        //
        // 概要:
        //     Original was GL_DEPTH_TEST = 0x0B71
        DepthTest = 2929,
        //
        // 概要:
        //     Original was GL_DEPTH_WRITEMASK = 0x0B72
        DepthWritemask = 2930,
        //
        // 概要:
        //     Original was GL_DEPTH_CLEAR_VALUE = 0x0B73
        DepthClearValue = 2931,
        //
        // 概要:
        //     Original was GL_DEPTH_FUNC = 0x0B74
        DepthFunc = 2932,
        //
        // 概要:
        //     Original was GL_ACCUM_CLEAR_VALUE = 0x0B80
        AccumClearValue = 2944,
        //
        // 概要:
        //     Original was GL_STENCIL_TEST = 0x0B90
        StencilTest = 2960,
        //
        // 概要:
        //     Original was GL_STENCIL_CLEAR_VALUE = 0x0B91
        StencilClearValue = 2961,
        //
        // 概要:
        //     Original was GL_STENCIL_FUNC = 0x0B92
        StencilFunc = 2962,
        //
        // 概要:
        //     Original was GL_STENCIL_VALUE_MASK = 0x0B93
        StencilValueMask = 2963,
        //
        // 概要:
        //     Original was GL_STENCIL_FAIL = 0x0B94
        StencilFail = 2964,
        //
        // 概要:
        //     Original was GL_STENCIL_PASS_DEPTH_FAIL = 0x0B95
        StencilPassDepthFail = 2965,
        //
        // 概要:
        //     Original was GL_STENCIL_PASS_DEPTH_PASS = 0x0B96
        StencilPassDepthPass = 2966,
        //
        // 概要:
        //     Original was GL_STENCIL_REF = 0x0B97
        StencilRef = 2967,
        //
        // 概要:
        //     Original was GL_STENCIL_WRITEMASK = 0x0B98
        StencilWritemask = 2968,
        //
        // 概要:
        //     Original was GL_MATRIX_MODE = 0x0BA0
        MatrixMode = 2976,
        //
        // 概要:
        //     Original was GL_NORMALIZE = 0x0BA1
        Normalize = 2977,
        //
        // 概要:
        //     Original was GL_Viewport = 0X0ba2
        Viewport = 2978,
        //
        // 概要:
        //     Original was GL_MODELVIEW0_STACK_DEPTH_EXT = 0x0BA3
        Modelview0StackDepthExt = 2979,
        //
        // 概要:
        //     Original was GL_MODELVIEW_STACK_DEPTH = 0x0BA3
        ModelviewStackDepth = 2979,
        //
        // 概要:
        //     Original was GL_PROJECTION_STACK_DEPTH = 0x0BA4
        ProjectionStackDepth = 2980,
        //
        // 概要:
        //     Original was GL_TEXTURE_STACK_DEPTH = 0x0BA5
        TextureStackDepth = 2981,
        //
        // 概要:
        //     Original was GL_MODELVIEW0_MATRIX_EXT = 0x0BA6
        Modelview0MatrixExt = 2982,
        //
        // 概要:
        //     Original was GL_MODELVIEW_MATRIX = 0x0BA6
        ModelviewMatrix = 2982,
        //
        // 概要:
        //     Original was GL_PROJECTION_MATRIX = 0x0BA7
        ProjectionMatrix = 2983,
        //
        // 概要:
        //     Original was GL_TEXTURE_MATRIX = 0x0BA8
        TextureMatrix = 2984,
        //
        // 概要:
        //     Original was GL_ATTRIB_STACK_DEPTH = 0x0BB0
        AttribStackDepth = 2992,
        //
        // 概要:
        //     Original was GL_CLIENT_ATTRIB_STACK_DEPTH = 0x0BB1
        ClientAttribStackDepth = 2993,
        //
        // 概要:
        //     Original was GL_ALPHA_TEST = 0x0BC0
        AlphaTest = 3008,
        //
        // 概要:
        //     Original was GL_ALPHA_TEST_QCOM = 0x0BC0
        AlphaTestQcom = 3008,
        //
        // 概要:
        //     Original was GL_ALPHA_TEST_FUNC = 0x0BC1
        AlphaTestFunc = 3009,
        //
        // 概要:
        //     Original was GL_ALPHA_TEST_FUNC_QCOM = 0x0BC1
        AlphaTestFuncQcom = 3009,
        //
        // 概要:
        //     Original was GL_ALPHA_TEST_REF = 0x0BC2
        AlphaTestRef = 3010,
        //
        // 概要:
        //     Original was GL_ALPHA_TEST_REF_QCOM = 0x0BC2
        AlphaTestRefQcom = 3010,
        //
        // 概要:
        //     Original was GL_Dither = 0X0bd0
        Dither = 3024,
        //
        // 概要:
        //     Original was GL_BLEND_DST = 0x0BE0
        BlendDst = 3040,
        //
        // 概要:
        //     Original was GL_BLEND_SRC = 0x0BE1
        BlendSrc = 3041,
        //
        // 概要:
        //     Original was GL_Blend = 0X0be2
        Blend = 3042,
        //
        // 概要:
        //     Original was GL_LOGIC_OP_MODE = 0x0BF0
        LogicOpMode = 3056,
        //
        // 概要:
        //     Original was GL_INDEX_LOGIC_OP = 0x0BF1
        IndexLogicOp = 3057,
        //
        // 概要:
        //     Original was GL_LOGIC_OP = 0x0BF1
        LogicOp = 3057,
        //
        // 概要:
        //     Original was GL_COLOR_LOGIC_OP = 0x0BF2
        ColorLogicOp = 3058,
        //
        // 概要:
        //     Original was GL_AUX_BUFFERS = 0x0C00
        AuxBuffers = 3072,
        //
        // 概要:
        //     Original was GL_DRAW_BUFFER = 0x0C01
        DrawBuffer = 3073,
        //
        // 概要:
        //     Original was GL_DRAW_BUFFER_EXT = 0x0C01
        DrawBufferExt = 3073,
        //
        // 概要:
        //     Original was GL_READ_BUFFER = 0x0C02
        ReadBuffer = 3074,
        //
        // 概要:
        //     Original was GL_READ_BUFFER_EXT = 0x0C02
        ReadBufferExt = 3074,
        //
        // 概要:
        //     Original was GL_READ_BUFFER_NV = 0x0C02
        ReadBufferNv = 3074,
        //
        // 概要:
        //     Original was GL_SCISSOR_BOX = 0x0C10
        ScissorBox = 3088,
        //
        // 概要:
        //     Original was GL_SCISSOR_TEST = 0x0C11
        ScissorTest = 3089,
        //
        // 概要:
        //     Original was GL_INDEX_CLEAR_VALUE = 0x0C20
        IndexClearValue = 3104,
        //
        // 概要:
        //     Original was GL_INDEX_WRITEMASK = 0x0C21
        IndexWritemask = 3105,
        //
        // 概要:
        //     Original was GL_COLOR_CLEAR_VALUE = 0x0C22
        ColorClearValue = 3106,
        //
        // 概要:
        //     Original was GL_COLOR_WRITEMASK = 0x0C23
        ColorWritemask = 3107,
        //
        // 概要:
        //     Original was GL_INDEX_MODE = 0x0C30
        IndexMode = 3120,
        //
        // 概要:
        //     Original was GL_RGBA_MODE = 0x0C31
        RgbaMode = 3121,
        //
        // 概要:
        //     Original was GL_DOUBLEBUFFER = 0x0C32
        Doublebuffer = 3122,
        //
        // 概要:
        //     Original was GL_STEREO = 0x0C33
        Stereo = 3123,
        //
        // 概要:
        //     Original was GL_RENDER_MODE = 0x0C40
        RenderMode = 3136,
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
        //     Original was GL_TEXTURE_GEN_S = 0x0C60
        TextureGenS = 3168,
        //
        // 概要:
        //     Original was GL_TEXTURE_GEN_T = 0x0C61
        TextureGenT = 3169,
        //
        // 概要:
        //     Original was GL_TEXTURE_GEN_R = 0x0C62
        TextureGenR = 3170,
        //
        // 概要:
        //     Original was GL_TEXTURE_GEN_Q = 0x0C63
        TextureGenQ = 3171,
        //
        // 概要:
        //     Original was GL_PIXEL_MAP_I_TO_I_SIZE = 0x0CB0
        PixelMapIToISize = 3248,
        //
        // 概要:
        //     Original was GL_PIXEL_MAP_S_TO_S_SIZE = 0x0CB1
        PixelMapSToSSize = 3249,
        //
        // 概要:
        //     Original was GL_PIXEL_MAP_I_TO_R_SIZE = 0x0CB2
        PixelMapIToRSize = 3250,
        //
        // 概要:
        //     Original was GL_PIXEL_MAP_I_TO_G_SIZE = 0x0CB3
        PixelMapIToGSize = 3251,
        //
        // 概要:
        //     Original was GL_PIXEL_MAP_I_TO_B_SIZE = 0x0CB4
        PixelMapIToBSize = 3252,
        //
        // 概要:
        //     Original was GL_PIXEL_MAP_I_TO_A_SIZE = 0x0CB5
        PixelMapIToASize = 3253,
        //
        // 概要:
        //     Original was GL_PIXEL_MAP_R_TO_R_SIZE = 0x0CB6
        PixelMapRToRSize = 3254,
        //
        // 概要:
        //     Original was GL_PIXEL_MAP_G_TO_G_SIZE = 0x0CB7
        PixelMapGToGSize = 3255,
        //
        // 概要:
        //     Original was GL_PIXEL_MAP_B_TO_B_SIZE = 0x0CB8
        PixelMapBToBSize = 3256,
        //
        // 概要:
        //     Original was GL_PIXEL_MAP_A_TO_A_SIZE = 0x0CB9
        PixelMapAToASize = 3257,
        //
        // 概要:
        //     Original was GL_UNPACK_SWAP_BYTES = 0x0CF0
        UnpackSwapBytes = 3312,
        //
        // 概要:
        //     Original was GL_UNPACK_LSB_FIRST = 0x0CF1
        UnpackLsbFirst = 3313,
        //
        // 概要:
        //     Original was GL_UNPACK_ROW_LENGTH = 0x0CF2
        UnpackRowLength = 3314,
        //
        // 概要:
        //     Original was GL_UNPACK_SKIP_ROWS = 0x0CF3
        UnpackSkipRows = 3315,
        //
        // 概要:
        //     Original was GL_UNPACK_SKIP_PIXELS = 0x0CF4
        UnpackSkipPixels = 3316,
        //
        // 概要:
        //     Original was GL_UNPACK_ALIGNMENT = 0x0CF5
        UnpackAlignment = 3317,
        //
        // 概要:
        //     Original was GL_PACK_SWAP_BYTES = 0x0D00
        PackSwapBytes = 3328,
        //
        // 概要:
        //     Original was GL_PACK_LSB_FIRST = 0x0D01
        PackLsbFirst = 3329,
        //
        // 概要:
        //     Original was GL_PACK_ROW_LENGTH = 0x0D02
        PackRowLength = 3330,
        //
        // 概要:
        //     Original was GL_PACK_SKIP_ROWS = 0x0D03
        PackSkipRows = 3331,
        //
        // 概要:
        //     Original was GL_PACK_SKIP_PIXELS = 0x0D04
        PackSkipPixels = 3332,
        //
        // 概要:
        //     Original was GL_PACK_ALIGNMENT = 0x0D05
        PackAlignment = 3333,
        //
        // 概要:
        //     Original was GL_MAP_COLOR = 0x0D10
        MapColor = 3344,
        //
        // 概要:
        //     Original was GL_MAP_STENCIL = 0x0D11
        MapStencil = 3345,
        //
        // 概要:
        //     Original was GL_INDEX_SHIFT = 0x0D12
        IndexShift = 3346,
        //
        // 概要:
        //     Original was GL_INDEX_OFFSET = 0x0D13
        IndexOffset = 3347,
        //
        // 概要:
        //     Original was GL_RED_SCALE = 0x0D14
        RedScale = 3348,
        //
        // 概要:
        //     Original was GL_RED_BIAS = 0x0D15
        RedBias = 3349,
        //
        // 概要:
        //     Original was GL_ZOOM_X = 0x0D16
        ZoomX = 3350,
        //
        // 概要:
        //     Original was GL_ZOOM_Y = 0x0D17
        ZoomY = 3351,
        //
        // 概要:
        //     Original was GL_GREEN_SCALE = 0x0D18
        GreenScale = 3352,
        //
        // 概要:
        //     Original was GL_GREEN_BIAS = 0x0D19
        GreenBias = 3353,
        //
        // 概要:
        //     Original was GL_BLUE_SCALE = 0x0D1A
        BlueScale = 3354,
        //
        // 概要:
        //     Original was GL_BLUE_BIAS = 0x0D1B
        BlueBias = 3355,
        //
        // 概要:
        //     Original was GL_ALPHA_SCALE = 0x0D1C
        AlphaScale = 3356,
        //
        // 概要:
        //     Original was GL_ALPHA_BIAS = 0x0D1D
        AlphaBias = 3357,
        //
        // 概要:
        //     Original was GL_DEPTH_SCALE = 0x0D1E
        DepthScale = 3358,
        //
        // 概要:
        //     Original was GL_DEPTH_BIAS = 0x0D1F
        DepthBias = 3359,
        //
        // 概要:
        //     Original was GL_MAX_EVAL_ORDER = 0x0D30
        MaxEvalOrder = 3376,
        //
        // 概要:
        //     Original was GL_MAX_LIGHTS = 0x0D31
        MaxLights = 3377,
        //
        // 概要:
        //     Original was GL_MAX_CLIP_DISTANCES = 0x0D32
        MaxClipDistances = 3378,
        //
        // 概要:
        //     Original was GL_MAX_CLIP_PLANES = 0x0D32
        MaxClipPlanes = 3378,
        //
        // 概要:
        //     Original was GL_MAX_TEXTURE_SIZE = 0x0D33
        MaxTextureSize = 3379,
        //
        // 概要:
        //     Original was GL_MAX_PIXEL_MAP_TABLE = 0x0D34
        MaxPixelMapTable = 3380,
        //
        // 概要:
        //     Original was GL_MAX_ATTRIB_STACK_DEPTH = 0x0D35
        MaxAttribStackDepth = 3381,
        //
        // 概要:
        //     Original was GL_MAX_MODELVIEW_STACK_DEPTH = 0x0D36
        MaxModelviewStackDepth = 3382,
        //
        // 概要:
        //     Original was GL_MAX_NAME_STACK_DEPTH = 0x0D37
        MaxNameStackDepth = 3383,
        //
        // 概要:
        //     Original was GL_MAX_PROJECTION_STACK_DEPTH = 0x0D38
        MaxProjectionStackDepth = 3384,
        //
        // 概要:
        //     Original was GL_MAX_TEXTURE_STACK_DEPTH = 0x0D39
        MaxTextureStackDepth = 3385,
        //
        // 概要:
        //     Original was GL_MAX_VIEWPORT_DIMS = 0x0D3A
        MaxViewportDims = 3386,
        //
        // 概要:
        //     Original was GL_MAX_CLIENT_ATTRIB_STACK_DEPTH = 0x0D3B
        MaxClientAttribStackDepth = 3387,
        //
        // 概要:
        //     Original was GL_SUBPIXEL_BITS = 0x0D50
        SubpixelBits = 3408,
        //
        // 概要:
        //     Original was GL_INDEX_BITS = 0x0D51
        IndexBits = 3409,
        //
        // 概要:
        //     Original was GL_RED_BITS = 0x0D52
        RedBits = 3410,
        //
        // 概要:
        //     Original was GL_GREEN_BITS = 0x0D53
        GreenBits = 3411,
        //
        // 概要:
        //     Original was GL_BLUE_BITS = 0x0D54
        BlueBits = 3412,
        //
        // 概要:
        //     Original was GL_ALPHA_BITS = 0x0D55
        AlphaBits = 3413,
        //
        // 概要:
        //     Original was GL_DEPTH_BITS = 0x0D56
        DepthBits = 3414,
        //
        // 概要:
        //     Original was GL_STENCIL_BITS = 0x0D57
        StencilBits = 3415,
        //
        // 概要:
        //     Original was GL_ACCUM_RED_BITS = 0x0D58
        AccumRedBits = 3416,
        //
        // 概要:
        //     Original was GL_ACCUM_GREEN_BITS = 0x0D59
        AccumGreenBits = 3417,
        //
        // 概要:
        //     Original was GL_ACCUM_BLUE_BITS = 0x0D5A
        AccumBlueBits = 3418,
        //
        // 概要:
        //     Original was GL_ACCUM_ALPHA_BITS = 0x0D5B
        AccumAlphaBits = 3419,
        //
        // 概要:
        //     Original was GL_NAME_STACK_DEPTH = 0x0D70
        NameStackDepth = 3440,
        //
        // 概要:
        //     Original was GL_AUTO_NORMAL = 0x0D80
        AutoNormal = 3456,
        //
        // 概要:
        //     Original was GL_MAP1_COLOR_4 = 0x0D90
        Map1Color4 = 3472,
        //
        // 概要:
        //     Original was GL_MAP1_INDEX = 0x0D91
        Map1Index = 3473,
        //
        // 概要:
        //     Original was GL_MAP1_NORMAL = 0x0D92
        Map1Normal = 3474,
        //
        // 概要:
        //     Original was GL_MAP1_TEXTURE_COORD_1 = 0x0D93
        Map1TextureCoord1 = 3475,
        //
        // 概要:
        //     Original was GL_MAP1_TEXTURE_COORD_2 = 0x0D94
        Map1TextureCoord2 = 3476,
        //
        // 概要:
        //     Original was GL_MAP1_TEXTURE_COORD_3 = 0x0D95
        Map1TextureCoord3 = 3477,
        //
        // 概要:
        //     Original was GL_MAP1_TEXTURE_COORD_4 = 0x0D96
        Map1TextureCoord4 = 3478,
        //
        // 概要:
        //     Original was GL_MAP1_VERTEX_3 = 0x0D97
        Map1Vertex3 = 3479,
        //
        // 概要:
        //     Original was GL_MAP1_VERTEX_4 = 0x0D98
        Map1Vertex4 = 3480,
        //
        // 概要:
        //     Original was GL_MAP2_COLOR_4 = 0x0DB0
        Map2Color4 = 3504,
        //
        // 概要:
        //     Original was GL_MAP2_INDEX = 0x0DB1
        Map2Index = 3505,
        //
        // 概要:
        //     Original was GL_MAP2_NORMAL = 0x0DB2
        Map2Normal = 3506,
        //
        // 概要:
        //     Original was GL_MAP2_TEXTURE_COORD_1 = 0x0DB3
        Map2TextureCoord1 = 3507,
        //
        // 概要:
        //     Original was GL_MAP2_TEXTURE_COORD_2 = 0x0DB4
        Map2TextureCoord2 = 3508,
        //
        // 概要:
        //     Original was GL_MAP2_TEXTURE_COORD_3 = 0x0DB5
        Map2TextureCoord3 = 3509,
        //
        // 概要:
        //     Original was GL_MAP2_TEXTURE_COORD_4 = 0x0DB6
        Map2TextureCoord4 = 3510,
        //
        // 概要:
        //     Original was GL_MAP2_VERTEX_3 = 0x0DB7
        Map2Vertex3 = 3511,
        //
        // 概要:
        //     Original was GL_MAP2_VERTEX_4 = 0x0DB8
        Map2Vertex4 = 3512,
        //
        // 概要:
        //     Original was GL_MAP1_GRID_DOMAIN = 0x0DD0
        Map1GridDomain = 3536,
        //
        // 概要:
        //     Original was GL_MAP1_GRID_SEGMENTS = 0x0DD1
        Map1GridSegments = 3537,
        //
        // 概要:
        //     Original was GL_MAP2_GRID_DOMAIN = 0x0DD2
        Map2GridDomain = 3538,
        //
        // 概要:
        //     Original was GL_MAP2_GRID_SEGMENTS = 0x0DD3
        Map2GridSegments = 3539,
        //
        // 概要:
        //     Original was GL_TEXTURE_1D = 0x0DE0
        Texture1D = 3552,
        //
        // 概要:
        //     Original was GL_TEXTURE_2D = 0x0DE1
        Texture2D = 3553,
        //
        // 概要:
        //     Original was GL_FEEDBACK_BUFFER_SIZE = 0x0DF1
        FeedbackBufferSize = 3569,
        //
        // 概要:
        //     Original was GL_FEEDBACK_BUFFER_TYPE = 0x0DF2
        FeedbackBufferType = 3570,
        //
        // 概要:
        //     Original was GL_SELECTION_BUFFER_SIZE = 0x0DF4
        SelectionBufferSize = 3572,
        //
        // 概要:
        //     Original was GL_POLYGON_OFFSET_UNITS = 0x2A00
        PolygonOffsetUnits = 10752,
        //
        // 概要:
        //     Original was GL_POLYGON_OFFSET_POINT = 0x2A01
        PolygonOffsetPoint = 10753,
        //
        // 概要:
        //     Original was GL_POLYGON_OFFSET_LINE = 0x2A02
        PolygonOffsetLine = 10754,
        //
        // 概要:
        //     Original was GL_CLIP_PLANE0 = 0x3000
        ClipPlane0 = 12288,
        //
        // 概要:
        //     Original was GL_CLIP_PLANE1 = 0x3001
        ClipPlane1 = 12289,
        //
        // 概要:
        //     Original was GL_CLIP_PLANE2 = 0x3002
        ClipPlane2 = 12290,
        //
        // 概要:
        //     Original was GL_CLIP_PLANE3 = 0x3003
        ClipPlane3 = 12291,
        //
        // 概要:
        //     Original was GL_CLIP_PLANE4 = 0x3004
        ClipPlane4 = 12292,
        //
        // 概要:
        //     Original was GL_CLIP_PLANE5 = 0x3005
        ClipPlane5 = 12293,
        //
        // 概要:
        //     Original was GL_LIGHT0 = 0x4000
        Light0 = 0x4000,
        //
        // 概要:
        //     Original was GL_LIGHT1 = 0x4001
        Light1 = 16385,
        //
        // 概要:
        //     Original was GL_LIGHT2 = 0x4002
        Light2 = 16386,
        //
        // 概要:
        //     Original was GL_LIGHT3 = 0x4003
        Light3 = 16387,
        //
        // 概要:
        //     Original was GL_LIGHT4 = 0x4004
        Light4 = 16388,
        //
        // 概要:
        //     Original was GL_LIGHT5 = 0x4005
        Light5 = 16389,
        //
        // 概要:
        //     Original was GL_LIGHT6 = 0x4006
        Light6 = 16390,
        //
        // 概要:
        //     Original was GL_LIGHT7 = 0x4007
        Light7 = 16391,
        //
        // 概要:
        //     Original was GL_BLEND_COLOR_EXT = 0x8005
        BlendColorExt = 32773,
        //
        // 概要:
        //     Original was GL_BlendColor = 0X8005
        BlendColor = 32773,
        //
        // 概要:
        //     Original was GL_BLEND_EQUATION_EXT = 0x8009
        BlendEquationExt = 32777,
        //
        // 概要:
        //     Original was GL_BlendEquation = 0X8009
        BlendEquation = 32777,
        //
        // 概要:
        //     Original was GL_BlendEquationRgb = 0X8009
        BlendEquationRgb = 32777,
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
        //     Original was GL_CONVOLUTION_1D_EXT = 0x8010
        Convolution1DExt = 32784,
        //
        // 概要:
        //     Original was GL_CONVOLUTION_2D_EXT = 0x8011
        Convolution2DExt = 32785,
        //
        // 概要:
        //     Original was GL_SEPARABLE_2D_EXT = 0x8012
        Separable2DExt = 32786,
        //
        // 概要:
        //     Original was GL_POST_CONVOLUTION_RED_SCALE_EXT = 0x801C
        PostConvolutionRedScaleExt = 32796,
        //
        // 概要:
        //     Original was GL_POST_CONVOLUTION_GREEN_SCALE_EXT = 0x801D
        PostConvolutionGreenScaleExt = 32797,
        //
        // 概要:
        //     Original was GL_POST_CONVOLUTION_BLUE_SCALE_EXT = 0x801E
        PostConvolutionBlueScaleExt = 32798,
        //
        // 概要:
        //     Original was GL_POST_CONVOLUTION_ALPHA_SCALE_EXT = 0x801F
        PostConvolutionAlphaScaleExt = 32799,
        //
        // 概要:
        //     Original was GL_POST_CONVOLUTION_RED_BIAS_EXT = 0x8020
        PostConvolutionRedBiasExt = 32800,
        //
        // 概要:
        //     Original was GL_POST_CONVOLUTION_GREEN_BIAS_EXT = 0x8021
        PostConvolutionGreenBiasExt = 32801,
        //
        // 概要:
        //     Original was GL_POST_CONVOLUTION_BLUE_BIAS_EXT = 0x8022
        PostConvolutionBlueBiasExt = 32802,
        //
        // 概要:
        //     Original was GL_POST_CONVOLUTION_ALPHA_BIAS_EXT = 0x8023
        PostConvolutionAlphaBiasExt = 32803,
        //
        // 概要:
        //     Original was GL_HISTOGRAM_EXT = 0x8024
        HistogramExt = 32804,
        //
        // 概要:
        //     Original was GL_MINMAX_EXT = 0x802E
        MinmaxExt = 32814,
        //
        // 概要:
        //     Original was GL_POLYGON_OFFSET_FILL = 0x8037
        PolygonOffsetFill = 32823,
        //
        // 概要:
        //     Original was GL_POLYGON_OFFSET_FACTOR = 0x8038
        PolygonOffsetFactor = 32824,
        //
        // 概要:
        //     Original was GL_POLYGON_OFFSET_BIAS_EXT = 0x8039
        PolygonOffsetBiasExt = 32825,
        //
        // 概要:
        //     Original was GL_RESCALE_NORMAL_EXT = 0x803A
        RescaleNormalExt = 32826,
        //
        // 概要:
        //     Original was GL_TEXTURE_BINDING_1D = 0x8068
        TextureBinding1D = 32872,
        //
        // 概要:
        //     Original was GL_TEXTURE_BINDING_2D = 0x8069
        TextureBinding2D = 32873,
        //
        // 概要:
        //     Original was GL_TEXTURE_3D_BINDING_EXT = 0x806A
        Texture3DBindingExt = 32874,
        //
        // 概要:
        //     Original was GL_TEXTURE_BINDING_3D = 0x806A
        TextureBinding3D = 32874,
        //
        // 概要:
        //     Original was GL_TEXTURE_BINDING_3D_OES = 0x806A
        TextureBinding3DOes = 32874,
        //
        // 概要:
        //     Original was GL_PACK_SKIP_IMAGES_EXT = 0x806B
        PackSkipImagesExt = 32875,
        //
        // 概要:
        //     Original was GL_PACK_IMAGE_HEIGHT_EXT = 0x806C
        PackImageHeightExt = 32876,
        //
        // 概要:
        //     Original was GL_UNPACK_SKIP_IMAGES_EXT = 0x806D
        UnpackSkipImagesExt = 32877,
        //
        // 概要:
        //     Original was GL_UNPACK_IMAGE_HEIGHT_EXT = 0x806E
        UnpackImageHeightExt = 32878,
        //
        // 概要:
        //     Original was GL_TEXTURE_3D_EXT = 0x806F
        Texture3DExt = 32879,
        //
        // 概要:
        //     Original was GL_MAX_3D_TEXTURE_SIZE_EXT = 0x8073
        Max3DTextureSizeExt = 32883,
        //
        // 概要:
        //     Original was GL_MAX_3D_TEXTURE_SIZE_OES = 0x8073
        Max3DTextureSizeOes = 32883,
        //
        // 概要:
        //     Original was GL_VERTEX_ARRAY = 0x8074
        VertexArray = 32884,
        //
        // 概要:
        //     Original was GL_NORMAL_ARRAY = 0x8075
        NormalArray = 32885,
        //
        // 概要:
        //     Original was GL_COLOR_ARRAY = 0x8076
        ColorArray = 32886,
        //
        // 概要:
        //     Original was GL_INDEX_ARRAY = 0x8077
        IndexArray = 32887,
        //
        // 概要:
        //     Original was GL_TEXTURE_COORD_ARRAY = 0x8078
        TextureCoordArray = 32888,
        //
        // 概要:
        //     Original was GL_EDGE_FLAG_ARRAY = 0x8079
        EdgeFlagArray = 32889,
        //
        // 概要:
        //     Original was GL_VERTEX_ARRAY_SIZE = 0x807A
        VertexArraySize = 32890,
        //
        // 概要:
        //     Original was GL_VERTEX_ARRAY_TYPE = 0x807B
        VertexArrayType = 32891,
        //
        // 概要:
        //     Original was GL_VERTEX_ARRAY_STRIDE = 0x807C
        VertexArrayStride = 32892,
        //
        // 概要:
        //     Original was GL_VERTEX_ARRAY_COUNT_EXT = 0x807D
        VertexArrayCountExt = 32893,
        //
        // 概要:
        //     Original was GL_NORMAL_ARRAY_TYPE = 0x807E
        NormalArrayType = 32894,
        //
        // 概要:
        //     Original was GL_NORMAL_ARRAY_STRIDE = 0x807F
        NormalArrayStride = 32895,
        //
        // 概要:
        //     Original was GL_NORMAL_ARRAY_COUNT_EXT = 0x8080
        NormalArrayCountExt = 32896,
        //
        // 概要:
        //     Original was GL_COLOR_ARRAY_SIZE = 0x8081
        ColorArraySize = 32897,
        //
        // 概要:
        //     Original was GL_COLOR_ARRAY_TYPE = 0x8082
        ColorArrayType = 32898,
        //
        // 概要:
        //     Original was GL_COLOR_ARRAY_STRIDE = 0x8083
        ColorArrayStride = 32899,
        //
        // 概要:
        //     Original was GL_COLOR_ARRAY_COUNT_EXT = 0x8084
        ColorArrayCountExt = 32900,
        //
        // 概要:
        //     Original was GL_INDEX_ARRAY_TYPE = 0x8085
        IndexArrayType = 32901,
        //
        // 概要:
        //     Original was GL_INDEX_ARRAY_STRIDE = 0x8086
        IndexArrayStride = 32902,
        //
        // 概要:
        //     Original was GL_INDEX_ARRAY_COUNT_EXT = 0x8087
        IndexArrayCountExt = 32903,
        //
        // 概要:
        //     Original was GL_TEXTURE_COORD_ARRAY_SIZE = 0x8088
        TextureCoordArraySize = 32904,
        //
        // 概要:
        //     Original was GL_TEXTURE_COORD_ARRAY_TYPE = 0x8089
        TextureCoordArrayType = 32905,
        //
        // 概要:
        //     Original was GL_TEXTURE_COORD_ARRAY_STRIDE = 0x808A
        TextureCoordArrayStride = 32906,
        //
        // 概要:
        //     Original was GL_TEXTURE_COORD_ARRAY_COUNT_EXT = 0x808B
        TextureCoordArrayCountExt = 32907,
        //
        // 概要:
        //     Original was GL_EDGE_FLAG_ARRAY_STRIDE = 0x808C
        EdgeFlagArrayStride = 32908,
        //
        // 概要:
        //     Original was GL_EDGE_FLAG_ARRAY_COUNT_EXT = 0x808D
        EdgeFlagArrayCountExt = 32909,
        //
        // 概要:
        //     Original was GL_INTERLACE_SGIX = 0x8094
        InterlaceSgix = 32916,
        //
        // 概要:
        //     Original was GL_DETAIL_TEXTURE_2D_BINDING_SGIS = 0x8096
        DetailTexture2DBindingSgis = 32918,
        //
        // 概要:
        //     Original was GL_MULTISAMPLE_SGIS = 0x809D
        MultisampleSgis = 32925,
        //
        // 概要:
        //     Original was GL_SAMPLE_ALPHA_TO_MASK_SGIS = 0x809E
        SampleAlphaToMaskSgis = 32926,
        //
        // 概要:
        //     Original was GL_SampleAlphaToCoverage = 0X809e
        SampleAlphaToCoverage = 32926,
        //
        // 概要:
        //     Original was GL_SAMPLE_ALPHA_TO_ONE_SGIS = 0x809F
        SampleAlphaToOneSgis = 32927,
        //
        // 概要:
        //     Original was GL_SAMPLE_MASK_SGIS = 0x80A0
        SampleMaskSgis = 32928,
        //
        // 概要:
        //     Original was GL_SampleCoverage = 0X80a0
        SampleCoverage = 32928,
        //
        // 概要:
        //     Original was GL_SAMPLE_BUFFERS_SGIS = 0x80A8
        SampleBuffersSgis = 32936,
        //
        // 概要:
        //     Original was GL_SampleBuffers = 0X80a8
        SampleBuffers = 32936,
        //
        // 概要:
        //     Original was GL_SAMPLES_SGIS = 0x80A9
        SamplesSgis = 32937,
        //
        // 概要:
        //     Original was GL_Samples = 0X80a9
        Samples = 32937,
        //
        // 概要:
        //     Original was GL_SAMPLE_MASK_VALUE_SGIS = 0x80AA
        SampleMaskValueSgis = 32938,
        //
        // 概要:
        //     Original was GL_SampleCoverageValue = 0X80aa
        SampleCoverageValue = 32938,
        //
        // 概要:
        //     Original was GL_SAMPLE_MASK_INVERT_SGIS = 0x80AB
        SampleMaskInvertSgis = 32939,
        //
        // 概要:
        //     Original was GL_SampleCoverageInvert = 0X80ab
        SampleCoverageInvert = 32939,
        //
        // 概要:
        //     Original was GL_SAMPLE_PATTERN_SGIS = 0x80AC
        SamplePatternSgis = 32940,
        //
        // 概要:
        //     Original was GL_COLOR_MATRIX_SGI = 0x80B1
        ColorMatrixSgi = 32945,
        //
        // 概要:
        //     Original was GL_COLOR_MATRIX_STACK_DEPTH_SGI = 0x80B2
        ColorMatrixStackDepthSgi = 32946,
        //
        // 概要:
        //     Original was GL_MAX_COLOR_MATRIX_STACK_DEPTH_SGI = 0x80B3
        MaxColorMatrixStackDepthSgi = 32947,
        //
        // 概要:
        //     Original was GL_POST_COLOR_MATRIX_RED_SCALE_SGI = 0x80B4
        PostColorMatrixRedScaleSgi = 32948,
        //
        // 概要:
        //     Original was GL_POST_COLOR_MATRIX_GREEN_SCALE_SGI = 0x80B5
        PostColorMatrixGreenScaleSgi = 32949,
        //
        // 概要:
        //     Original was GL_POST_COLOR_MATRIX_BLUE_SCALE_SGI = 0x80B6
        PostColorMatrixBlueScaleSgi = 32950,
        //
        // 概要:
        //     Original was GL_POST_COLOR_MATRIX_ALPHA_SCALE_SGI = 0x80B7
        PostColorMatrixAlphaScaleSgi = 32951,
        //
        // 概要:
        //     Original was GL_POST_COLOR_MATRIX_RED_BIAS_SGI = 0x80B8
        PostColorMatrixRedBiasSgi = 32952,
        //
        // 概要:
        //     Original was GL_POST_COLOR_MATRIX_GREEN_BIAS_SGI = 0x80B9
        PostColorMatrixGreenBiasSgi = 32953,
        //
        // 概要:
        //     Original was GL_POST_COLOR_MATRIX_BLUE_BIAS_SGI = 0x80BA
        PostColorMatrixBlueBiasSgi = 32954,
        //
        // 概要:
        //     Original was GL_POST_COLOR_MATRIX_ALPHA_BIAS_SGI = 0x80BB
        PostColorMatrixAlphaBiasSgi = 32955,
        //
        // 概要:
        //     Original was GL_TEXTURE_COLOR_TABLE_SGI = 0x80BC
        TextureColorTableSgi = 32956,
        //
        // 概要:
        //     Original was GL_BlendDstRgb = 0X80c8
        BlendDstRgb = 32968,
        //
        // 概要:
        //     Original was GL_BlendSrcRgb = 0X80c9
        BlendSrcRgb = 32969,
        //
        // 概要:
        //     Original was GL_BlendDstAlpha = 0X80ca
        BlendDstAlpha = 32970,
        //
        // 概要:
        //     Original was GL_BlendSrcAlpha = 0X80cb
        BlendSrcAlpha = 32971,
        //
        // 概要:
        //     Original was GL_COLOR_TABLE_SGI = 0x80D0
        ColorTableSgi = 32976,
        //
        // 概要:
        //     Original was GL_POST_CONVOLUTION_COLOR_TABLE_SGI = 0x80D1
        PostConvolutionColorTableSgi = 32977,
        //
        // 概要:
        //     Original was GL_POST_COLOR_MATRIX_COLOR_TABLE_SGI = 0x80D2
        PostColorMatrixColorTableSgi = 32978,
        //
        // 概要:
        //     Original was GL_POINT_SIZE_MIN_SGIS = 0x8126
        PointSizeMinSgis = 33062,
        //
        // 概要:
        //     Original was GL_POINT_SIZE_MAX_SGIS = 0x8127
        PointSizeMaxSgis = 33063,
        //
        // 概要:
        //     Original was GL_POINT_FADE_THRESHOLD_SIZE_SGIS = 0x8128
        PointFadeThresholdSizeSgis = 33064,
        //
        // 概要:
        //     Original was GL_DISTANCE_ATTENUATION_SGIS = 0x8129
        DistanceAttenuationSgis = 33065,
        //
        // 概要:
        //     Original was GL_FOG_FUNC_POINTS_SGIS = 0x812B
        FogFuncPointsSgis = 33067,
        //
        // 概要:
        //     Original was GL_MAX_FOG_FUNC_POINTS_SGIS = 0x812C
        MaxFogFuncPointsSgis = 33068,
        //
        // 概要:
        //     Original was GL_PACK_SKIP_VOLUMES_SGIS = 0x8130
        PackSkipVolumesSgis = 33072,
        //
        // 概要:
        //     Original was GL_PACK_IMAGE_DEPTH_SGIS = 0x8131
        PackImageDepthSgis = 33073,
        //
        // 概要:
        //     Original was GL_UNPACK_SKIP_VOLUMES_SGIS = 0x8132
        UnpackSkipVolumesSgis = 33074,
        //
        // 概要:
        //     Original was GL_UNPACK_IMAGE_DEPTH_SGIS = 0x8133
        UnpackImageDepthSgis = 33075,
        //
        // 概要:
        //     Original was GL_TEXTURE_4D_SGIS = 0x8134
        Texture4DSgis = 33076,
        //
        // 概要:
        //     Original was GL_MAX_4D_TEXTURE_SIZE_SGIS = 0x8138
        Max4DTextureSizeSgis = 33080,
        //
        // 概要:
        //     Original was GL_PIXEL_TEX_GEN_SGIX = 0x8139
        PixelTexGenSgix = 33081,
        //
        // 概要:
        //     Original was GL_PIXEL_TILE_BEST_ALIGNMENT_SGIX = 0x813E
        PixelTileBestAlignmentSgix = 33086,
        //
        // 概要:
        //     Original was GL_PIXEL_TILE_CACHE_INCREMENT_SGIX = 0x813F
        PixelTileCacheIncrementSgix = 33087,
        //
        // 概要:
        //     Original was GL_PIXEL_TILE_WIDTH_SGIX = 0x8140
        PixelTileWidthSgix = 33088,
        //
        // 概要:
        //     Original was GL_PIXEL_TILE_HEIGHT_SGIX = 0x8141
        PixelTileHeightSgix = 33089,
        //
        // 概要:
        //     Original was GL_PIXEL_TILE_GRID_WIDTH_SGIX = 0x8142
        PixelTileGridWidthSgix = 33090,
        //
        // 概要:
        //     Original was GL_PIXEL_TILE_GRID_HEIGHT_SGIX = 0x8143
        PixelTileGridHeightSgix = 33091,
        //
        // 概要:
        //     Original was GL_PIXEL_TILE_GRID_DEPTH_SGIX = 0x8144
        PixelTileGridDepthSgix = 33092,
        //
        // 概要:
        //     Original was GL_PIXEL_TILE_CACHE_SIZE_SGIX = 0x8145
        PixelTileCacheSizeSgix = 33093,
        //
        // 概要:
        //     Original was GL_SPRITE_SGIX = 0x8148
        SpriteSgix = 33096,
        //
        // 概要:
        //     Original was GL_SPRITE_MODE_SGIX = 0x8149
        SpriteModeSgix = 33097,
        //
        // 概要:
        //     Original was GL_SPRITE_AXIS_SGIX = 0x814A
        SpriteAxisSgix = 33098,
        //
        // 概要:
        //     Original was GL_SPRITE_TRANSLATION_SGIX = 0x814B
        SpriteTranslationSgix = 33099,
        //
        // 概要:
        //     Original was GL_TEXTURE_4D_BINDING_SGIS = 0x814F
        Texture4DBindingSgis = 33103,
        //
        // 概要:
        //     Original was GL_MAX_CLIPMAP_DEPTH_SGIX = 0x8177
        MaxClipmapDepthSgix = 33143,
        //
        // 概要:
        //     Original was GL_MAX_CLIPMAP_VIRTUAL_DEPTH_SGIX = 0x8178
        MaxClipmapVirtualDepthSgix = 33144,
        //
        // 概要:
        //     Original was GL_POST_TEXTURE_FILTER_BIAS_RANGE_SGIX = 0x817B
        PostTextureFilterBiasRangeSgix = 33147,
        //
        // 概要:
        //     Original was GL_POST_TEXTURE_FILTER_SCALE_RANGE_SGIX = 0x817C
        PostTextureFilterScaleRangeSgix = 33148,
        //
        // 概要:
        //     Original was GL_REFERENCE_PLANE_SGIX = 0x817D
        ReferencePlaneSgix = 33149,
        //
        // 概要:
        //     Original was GL_REFERENCE_PLANE_EQUATION_SGIX = 0x817E
        ReferencePlaneEquationSgix = 33150,
        //
        // 概要:
        //     Original was GL_IR_INSTRUMENT1_SGIX = 0x817F
        IrInstrument1Sgix = 33151,
        //
        // 概要:
        //     Original was GL_INSTRUMENT_MEASUREMENTS_SGIX = 0x8181
        InstrumentMeasurementsSgix = 33153,
        //
        // 概要:
        //     Original was GL_CALLIGRAPHIC_FRAGMENT_SGIX = 0x8183
        CalligraphicFragmentSgix = 33155,
        //
        // 概要:
        //     Original was GL_FRAMEZOOM_SGIX = 0x818B
        FramezoomSgix = 33163,
        //
        // 概要:
        //     Original was GL_FRAMEZOOM_FACTOR_SGIX = 0x818C
        FramezoomFactorSgix = 33164,
        //
        // 概要:
        //     Original was GL_MAX_FRAMEZOOM_FACTOR_SGIX = 0x818D
        MaxFramezoomFactorSgix = 33165,
        //
        // 概要:
        //     Original was GL_GENERATE_MIPMAP_HINT_SGIS = 0x8192
        GenerateMipmapHintSgis = 33170,
        //
        // 概要:
        //     Original was GL_GenerateMipmapHint = 0X8192
        GenerateMipmapHint = 33170,
        //
        // 概要:
        //     Original was GL_DEFORMATIONS_MASK_SGIX = 0x8196
        DeformationsMaskSgix = 33174,
        //
        // 概要:
        //     Original was GL_FOG_OFFSET_SGIX = 0x8198
        FogOffsetSgix = 33176,
        //
        // 概要:
        //     Original was GL_FOG_OFFSET_VALUE_SGIX = 0x8199
        FogOffsetValueSgix = 33177,
        //
        // 概要:
        //     Original was GL_LIGHT_MODEL_COLOR_CONTROL = 0x81F8
        LightModelColorControl = 33272,
        //
        // 概要:
        //     Original was GL_SHARED_TEXTURE_PALETTE_EXT = 0x81FB
        SharedTexturePaletteExt = 33275,
        //
        // 概要:
        //     Original was GL_RESET_NOTIFICATION_STRATEGY = 0x8256
        ResetNotificationStrategy = 33366,
        //
        // 概要:
        //     Original was GL_CONTEXT_RELEASE_BEHAVIOR_KHR = 0x82FB
        ContextReleaseBehaviorKhr = 33531,
        //
        // 概要:
        //     Original was GL_CONVOLUTION_HINT_SGIX = 0x8316
        ConvolutionHintSgix = 33558,
        //
        // 概要:
        //     Original was GL_ASYNC_MARKER_SGIX = 0x8329
        AsyncMarkerSgix = 33577,
        //
        // 概要:
        //     Original was GL_PIXEL_TEX_GEN_MODE_SGIX = 0x832B
        PixelTexGenModeSgix = 33579,
        //
        // 概要:
        //     Original was GL_ASYNC_HISTOGRAM_SGIX = 0x832C
        AsyncHistogramSgix = 33580,
        //
        // 概要:
        //     Original was GL_MAX_ASYNC_HISTOGRAM_SGIX = 0x832D
        MaxAsyncHistogramSgix = 33581,
        //
        // 概要:
        //     Original was GL_PIXEL_TEXTURE_SGIS = 0x8353
        PixelTextureSgis = 33619,
        //
        // 概要:
        //     Original was GL_ASYNC_TEX_IMAGE_SGIX = 0x835C
        AsyncTexImageSgix = 33628,
        //
        // 概要:
        //     Original was GL_ASYNC_DRAW_PIXELS_SGIX = 0x835D
        AsyncDrawPixelsSgix = 33629,
        //
        // 概要:
        //     Original was GL_ASYNC_READ_PIXELS_SGIX = 0x835E
        AsyncReadPixelsSgix = 33630,
        //
        // 概要:
        //     Original was GL_MAX_ASYNC_TEX_IMAGE_SGIX = 0x835F
        MaxAsyncTexImageSgix = 33631,
        //
        // 概要:
        //     Original was GL_MAX_ASYNC_DRAW_PIXELS_SGIX = 0x8360
        MaxAsyncDrawPixelsSgix = 33632,
        //
        // 概要:
        //     Original was GL_MAX_ASYNC_READ_PIXELS_SGIX = 0x8361
        MaxAsyncReadPixelsSgix = 33633,
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
        //     Original was GL_FRAGMENT_LIGHTING_SGIX = 0x8400
        FragmentLightingSgix = 33792,
        //
        // 概要:
        //     Original was GL_FRAGMENT_COLOR_MATERIAL_SGIX = 0x8401
        FragmentColorMaterialSgix = 33793,
        //
        // 概要:
        //     Original was GL_FRAGMENT_COLOR_MATERIAL_FACE_SGIX = 0x8402
        FragmentColorMaterialFaceSgix = 33794,
        //
        // 概要:
        //     Original was GL_FRAGMENT_COLOR_MATERIAL_PARAMETER_SGIX = 0x8403
        FragmentColorMaterialParameterSgix = 33795,
        //
        // 概要:
        //     Original was GL_MAX_FRAGMENT_LIGHTS_SGIX = 0x8404
        MaxFragmentLightsSgix = 33796,
        //
        // 概要:
        //     Original was GL_MAX_ACTIVE_LIGHTS_SGIX = 0x8405
        MaxActiveLightsSgix = 33797,
        //
        // 概要:
        //     Original was GL_LIGHT_ENV_MODE_SGIX = 0x8407
        LightEnvModeSgix = 33799,
        //
        // 概要:
        //     Original was GL_FRAGMENT_LIGHT_MODEL_LOCAL_VIEWER_SGIX = 0x8408
        FragmentLightModelLocalViewerSgix = 33800,
        //
        // 概要:
        //     Original was GL_FRAGMENT_LIGHT_MODEL_TWO_SIDE_SGIX = 0x8409
        FragmentLightModelTwoSideSgix = 33801,
        //
        // 概要:
        //     Original was GL_FRAGMENT_LIGHT_MODEL_AMBIENT_SGIX = 0x840A
        FragmentLightModelAmbientSgix = 33802,
        //
        // 概要:
        //     Original was GL_FRAGMENT_LIGHT_MODEL_NORMAL_INTERPOLATION_SGIX = 0x840B
        FragmentLightModelNormalInterpolationSgix = 33803,
        //
        // 概要:
        //     Original was GL_FRAGMENT_LIGHT0_SGIX = 0x840C
        FragmentLight0Sgix = 33804,
        //
        // 概要:
        //     Original was GL_PACK_RESAMPLE_SGIX = 0x842C
        PackResampleSgix = 33836,
        //
        // 概要:
        //     Original was GL_UNPACK_RESAMPLE_SGIX = 0x842D
        UnpackResampleSgix = 33837,
        //
        // 概要:
        //     Original was GL_ALIASED_POINT_SIZE_RANGE = 0x846D
        AliasedPointSizeRange = 33901,
        //
        // 概要:
        //     Original was GL_ALIASED_LINE_WIDTH_RANGE = 0x846E
        AliasedLineWidthRange = 33902,
        //
        // 概要:
        //     Original was GL_ActiveTexture = 0X84e0
        ActiveTexture = 34016,
        //
        // 概要:
        //     Original was GL_MaxRenderbufferSize = 0X84e8
        MaxRenderbufferSize = 34024,
        //
        // 概要:
        //     Original was GL_TextureBindingCubeMap = 0X8514
        TextureBindingCubeMap = 34068,
        //
        // 概要:
        //     Original was GL_MaxCubeMapTextureSize = 0X851c
        MaxCubeMapTextureSize = 34076,
        //
        // 概要:
        //     Original was GL_PACK_SUBSAMPLE_RATE_SGIX = 0x85A0
        PackSubsampleRateSgix = 34208,
        //
        // 概要:
        //     Original was GL_UNPACK_SUBSAMPLE_RATE_SGIX = 0x85A1
        UnpackSubsampleRateSgix = 34209,
        //
        // 概要:
        //     Original was GL_NumCompressedTextureFormats = 0X86a2
        NumCompressedTextureFormats = 34466,
        //
        // 概要:
        //     Original was GL_CompressedTextureFormats = 0X86a3
        CompressedTextureFormats = 34467,
        //
        // 概要:
        //     Original was GL_StencilBackFunc = 0X8800
        StencilBackFunc = 34816,
        //
        // 概要:
        //     Original was GL_StencilBackFail = 0X8801
        StencilBackFail = 34817,
        //
        // 概要:
        //     Original was GL_StencilBackPassDepthFail = 0X8802
        StencilBackPassDepthFail = 34818,
        //
        // 概要:
        //     Original was GL_StencilBackPassDepthPass = 0X8803
        StencilBackPassDepthPass = 34819,
        //
        // 概要:
        //     Original was GL_BlendEquationAlpha = 0X883d
        BlendEquationAlpha = 34877,
        //
        // 概要:
        //     Original was GL_MaxVertexAttribs = 0X8869
        MaxVertexAttribs = 34921,
        //
        // 概要:
        //     Original was GL_MaxTextureImageUnits = 0X8872
        MaxTextureImageUnits = 34930,
        //
        // 概要:
        //     Original was GL_ArrayBufferBinding = 0X8894
        ArrayBufferBinding = 34964,
        //
        // 概要:
        //     Original was GL_ElementArrayBufferBinding = 0X8895
        ElementArrayBufferBinding = 34965,
        //
        // 概要:
        //     Original was GL_MaxVertexTextureImageUnits = 0X8b4c
        MaxVertexTextureImageUnits = 35660,
        //
        // 概要:
        //     Original was GL_MaxCombinedTextureImageUnits = 0X8b4d
        MaxCombinedTextureImageUnits = 35661,
        //
        // 概要:
        //     Original was GL_CurrentProgram = 0X8b8d
        CurrentProgram = 35725,
        //
        // 概要:
        //     Original was GL_ImplementationColorReadType = 0X8b9a
        ImplementationColorReadType = 35738,
        //
        // 概要:
        //     Original was GL_ImplementationColorReadFormat = 0X8b9b
        ImplementationColorReadFormat = 35739,
        //
        // 概要:
        //     Original was GL_StencilBackRef = 0X8ca3
        StencilBackRef = 36003,
        //
        // 概要:
        //     Original was GL_StencilBackValueMask = 0X8ca4
        StencilBackValueMask = 36004,
        //
        // 概要:
        //     Original was GL_StencilBackWritemask = 0X8ca5
        StencilBackWritemask = 36005,
        //
        // 概要:
        //     Original was GL_FramebufferBinding = 0X8ca6
        FramebufferBinding = 36006,
        //
        // 概要:
        //     Original was GL_RenderbufferBinding = 0X8ca7
        RenderbufferBinding = 36007,
        //
        // 概要:
        //     Original was GL_ShaderBinaryFormats = 0X8df8
        ShaderBinaryFormats = 36344,
        //
        // 概要:
        //     Original was GL_NumShaderBinaryFormats = 0X8df9
        NumShaderBinaryFormats = 36345,
        //
        // 概要:
        //     Original was GL_ShaderCompiler = 0X8dfa
        ShaderCompiler = 36346,
        //
        // 概要:
        //     Original was GL_MaxVertexUniformVectors = 0X8dfb
        MaxVertexUniformVectors = 36347,
        //
        // 概要:
        //     Original was GL_MaxVaryingVectors = 0X8dfc
        MaxVaryingVectors = 36348,
        //
        // 概要:
        //     Original was GL_MaxFragmentUniformVectors = 0X8dfd
        MaxFragmentUniformVectors = 36349,
        //
        // 概要:
        //     Original was GL_TIMESTAMP_EXT = 0x8E28
        TimestampExt = 36392,
        //
        // 概要:
        //     Original was GL_GPU_DISJOINT_EXT = 0x8FBB
        GpuDisjointExt = 36795,
        //
        // 概要:
        //     Original was GL_MAX_MULTIVIEW_BUFFERS_EXT = 0x90F2
        MaxMultiviewBuffersExt = 37106,
        //
        // 概要:
        //     Original was GL_CONTEXT_ROBUST_ACCESS = 0x90F3
        ContextRobustAccess = 37107
    }

}
