using System;
using System.Collections.Generic;
using System.Text;

namespace Rydia.Graphics.Api.ES20
{

    //
    // 概要:
    //     Used in GL.PixelStore
    public enum PixelStoreParameter
    {
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
        //     Original was GL_UNPACK_ROW_LENGTH_EXT = 0x0CF2
        UnpackRowLengthExt = 3314,
        //
        // 概要:
        //     Original was GL_UNPACK_SKIP_ROWS = 0x0CF3
        UnpackSkipRows = 3315,
        //
        // 概要:
        //     Original was GL_UNPACK_SKIP_ROWS_EXT = 0x0CF3
        UnpackSkipRowsExt = 3315,
        //
        // 概要:
        //     Original was GL_UNPACK_SKIP_PIXELS = 0x0CF4
        UnpackSkipPixels = 3316,
        //
        // 概要:
        //     Original was GL_UNPACK_SKIP_PIXELS_EXT = 0x0CF4
        UnpackSkipPixelsExt = 3316,
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
        //     Original was GL_PACK_SKIP_IMAGES = 0x806B
        PackSkipImages = 32875,
        //
        // 概要:
        //     Original was GL_PACK_SKIP_IMAGES_EXT = 0x806B
        PackSkipImagesExt = 32875,
        //
        // 概要:
        //     Original was GL_PACK_IMAGE_HEIGHT = 0x806C
        PackImageHeight = 32876,
        //
        // 概要:
        //     Original was GL_PACK_IMAGE_HEIGHT_EXT = 0x806C
        PackImageHeightExt = 32876,
        //
        // 概要:
        //     Original was GL_UNPACK_SKIP_IMAGES = 0x806D
        UnpackSkipImages = 32877,
        //
        // 概要:
        //     Original was GL_UNPACK_SKIP_IMAGES_EXT = 0x806D
        UnpackSkipImagesExt = 32877,
        //
        // 概要:
        //     Original was GL_UNPACK_IMAGE_HEIGHT = 0x806E
        UnpackImageHeight = 32878,
        //
        // 概要:
        //     Original was GL_UNPACK_IMAGE_HEIGHT_EXT = 0x806E
        UnpackImageHeightExt = 32878,
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
        //     Original was GL_PACK_RESAMPLE_SGIX = 0x842C
        PackResampleSgix = 33836,
        //
        // 概要:
        //     Original was GL_UNPACK_RESAMPLE_SGIX = 0x842D
        UnpackResampleSgix = 33837,
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
        //     Original was GL_PACK_RESAMPLE_OML = 0x8984
        PackResampleOml = 35204,
        //
        // 概要:
        //     Original was GL_UNPACK_RESAMPLE_OML = 0x8985
        UnpackResampleOml = 35205
    }

}
