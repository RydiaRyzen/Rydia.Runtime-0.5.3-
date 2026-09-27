using System;
using System.Collections.Generic;
using System.Text;
using Rydia.Runtime.GLES2Samples;

namespace Rydia.Runtime.GLES2Samples.Shared
{

    public class SampleApp
    {

        public static AppRunner E(int index)
        {
            switch (index)
            {
                case 1:
                    return new E01HelloTriangleApp();
                case 2:
                    return new E02SimpleVertexShaderApp();
                case 3:
                    return new E03SimpleTexture2DApp();
                case 4:
                    return new E04MipMap2DApp();
                case 5:
                    return new E05TextureWrapApp();
                case 6:
                    return new E06SimpleTextureCubeMapApp();
                case 7:
                    return new E07MultiTextureApp();
                case 8:
                    return new E08StencilOperationsApp();
                case 9:
                    return new E09ParticleSystemApp();
                case 10:
                    return new E10MandelbrotApp();
                case 11:
                    return new E11CartoonSunApp();
                case 12:
                    return new E12StripedCubesApp();
                case 13:
                    return new E13CartoonTorusApp();
                case 14:
                    return new E14MetallicTorusApp();
                case 15:
                    return new E15NoiseTorusApp();
                case 16:
                    return new E16PhongTorusApp();
                case 17:
                    return new E17HeliumApp();
                case 18:
                    return new E18CubeMappingApp();
                case 19:
                    return new E19ShadedObjectsApp();
                case 20:
                    return new E20MorphingApp();
                case 21:
                    return new E21ParallaxMapApp();
                case 22:
                    return new E22RecursiveTextureApp();
                case 23:
                    return new E23ALSinewaveTestApp();
                case 24:
                    return new E24ALTestApp();
                case 25:
                    return new E25FontSampleApp();
                case 26:
                    return new E26ImGuiApp();
            }
            throw new ArgumentOutOfRangeException("index");
        }

    }
}
