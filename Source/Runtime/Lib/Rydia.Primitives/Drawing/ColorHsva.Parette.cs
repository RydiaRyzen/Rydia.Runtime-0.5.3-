using System;
using System.Collections.Generic;
using System.Text;

namespace Rydia.Drawing
{
    public partial struct ColorHsva
    {

        #region TColorData.Palette

        /// <summary>
        /// RGBA値が#202020FFの色を<see cref="ColorHsva"/>で取得します
        /// </summary>
        public static ColorHsva VeryDarkGrey
        {
            get
            {
                return ColorRgba.VeryDarkGray.ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#F0F8FFFFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva AliceBlue
        {
            get
            {
                return new ColorRgba(240, 248, 255, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#FAEBD7FFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva AntiqueWhite
        {
            get
            {
                return new ColorRgba(250, 235, 215, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#00FFFFFFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva Aqua
        {
            get
            {
                return new ColorRgba(0, 255, 255, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#7FFFD4FFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva Aquamarine
        {
            get
            {
                return new ColorRgba(127, 255, 212, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#F0FFFFFFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva Azure
        {
            get
            {
                return new ColorRgba(240, 255, 255, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#B4B4B4FFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva ActiveBorder
        {
            get
            {
                return new ColorRgba(180, 180, 180, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#99B4D1FFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva ActiveCaption
        {
            get
            {
                return new ColorRgba(153, 180, 209, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#000000FFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva ActiveCaptionText
        {
            get
            {
                return new ColorRgba(0, 0, 0, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#ABABABFFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva AppWorkspace
        {
            get
            {
                return new ColorRgba(171, 171, 171, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#F5F5DCFFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva Beige
        {
            get
            {
                return new ColorRgba(245, 245, 220, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#FFE4C4FFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva Bisque
        {
            get
            {
                return new ColorRgba(255, 228, 196, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#000000FFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva Black
        {
            get
            {
                return new ColorRgba(0, 0, 0, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#FFEBCDFFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva BlanchedAlmond
        {
            get
            {
                return new ColorRgba(255, 235, 205, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#0000FFFFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva Blue
        {
            get
            {
                return new ColorRgba(0, 0, 255, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#8A2BE2FFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva BlueViolet
        {
            get
            {
                return new ColorRgba(138, 43, 226, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#A52A2AFFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva Brown
        {
            get
            {
                return new ColorRgba(165, 42, 42, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#DEB887FFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva BurlyWood
        {
            get
            {
                return new ColorRgba(222, 184, 135, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#F0F0F0FFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva ButtonFace
        {
            get
            {
                return new ColorRgba(240, 240, 240, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#FFFFFFFFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva ButtonHighlight
        {
            get
            {
                return new ColorRgba(255, 255, 255, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#A0A0A0FFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva ButtonShadow
        {
            get
            {
                return new ColorRgba(160, 160, 160, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#5F9EA0FFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva CadetBlue
        {
            get
            {
                return new ColorRgba(95, 158, 160, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#7FFF00FFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva Chartreuse
        {
            get
            {
                return new ColorRgba(127, 255, 0, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#D2691EFFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva Chocolate
        {
            get
            {
                return new ColorRgba(210, 105, 30, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#FF7F50FFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva Coral
        {
            get
            {
                return new ColorRgba(255, 127, 80, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#6495EDFFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva CornflowerBlue
        {
            get
            {
                return new ColorRgba(100, 149, 237, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#FFF8DCFFの色を<see cref="IColorData"/>で取得しま・ｷ。
        /// </summary>
        public static ColorHsva Cornsilk
        {
            get
            {
                return new ColorRgba(255, 248, 220, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#DC143CFFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva Crimson
        {
            get
            {
                return new ColorRgba(220, 20, 60, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#00FFFFFFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva Cyan
        {
            get
            {
                return new ColorRgba(0, 255, 255, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#F0F0F0FFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva Control
        {
            get
            {
                return new ColorRgba(240, 240, 240, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#A0A0A0FFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva ControlDark
        {
            get
            {
                return new ColorRgba(160, 160, 160, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#696969FFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva ControlDarkDark
        {
            get
            {
                return new ColorRgba(105, 105, 105, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#E3E3E3FFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva ControlLight
        {
            get
            {
                return new ColorRgba(227, 227, 227, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#FFFFFFFFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva ControlLightLight
        {
            get
            {
                return new ColorRgba(255, 255, 255, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#000000FFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva ControlText
        {
            get
            {
                return new ColorRgba(0, 0, 0, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#00008BFFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva DarkBlue
        {
            get
            {
                return new ColorRgba(0, 0, 139, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#008B8BFFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva DarkCyan
        {
            get
            {
                return new ColorRgba(0, 139, 139, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#B8860BFFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva DarkGoldenrod
        {
            get
            {
                return new ColorRgba(184, 134, 11, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#A9A9A9FFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva DarkGray
        {
            get
            {
                return new ColorRgba(169, 169, 169, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#404040FFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva DarkGrey
        {
            get
            {
                return new ColorRgba(64, 64, 64, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#006400FFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva DarkGreen
        {
            get
            {
                return new ColorRgba(0, 100, 0, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#BDB76BFFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva DarkKhaki
        {
            get
            {
                return new ColorRgba(189, 183, 107, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#8B008BFFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva DarkMagenta
        {
            get
            {
                return new ColorRgba(139, 0, 139, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#556B2FFFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva DarkOliveGreen
        {
            get
            {
                return new ColorRgba(85, 107, 47, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#FF8C00FFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva DarkOrange
        {
            get
            {
                return new ColorRgba(255, 140, 0, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#9932CCFFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva DarkOrchid
        {
            get
            {
                return new ColorRgba(153, 50, 204, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#8B0000FFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva DarkRed
        {
            get
            {
                return new ColorRgba(139, 0, 0, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#E9967AFFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva DarkSalmon
        {
            get
            {
                return new ColorRgba(233, 150, 122, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#8FBC8BFFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva DarkSeaGreen
        {
            get
            {
                return new ColorRgba(143, 188, 139, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#483D8BFFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva DarkSlateBlue
        {
            get
            {
                return new ColorRgba(72, 61, 139, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#2F4F4FFFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva DarkSlateGray
        {
            get
            {
                return new ColorRgba(47, 79, 79, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#00CED1FFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva DarkTurquoise
        {
            get
            {
                return new ColorRgba(0, 206, 209, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#9400D3FFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva DarkViolet
        {
            get
            {
                return new ColorRgba(148, 0, 211, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#FF1493FFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva DeepPink
        {
            get
            {
                return new ColorRgba(255, 20, 147, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#00BFFFFFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva DeepSkyBlue
        {
            get
            {
                return new ColorRgba(0, 191, 255, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#696969FFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva DimGray
        {
            get
            {
                return new ColorRgba(105, 105, 105, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#000000FFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva Desktop
        {
            get
            {
                return new ColorRgba(0, 0, 0, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#1E90FFFFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva DodgerBlue
        {
            get
            {
                return new ColorRgba(30, 144, 255, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#B22222FFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva Firebrick
        {
            get
            {
                return new ColorRgba(178, 34, 34, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#FFFAF0FFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva FloralWhite
        {
            get
            {
                return new ColorRgba(255, 250, 240, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#228B22FFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva ForestGreen
        {
            get
            {
                return new ColorRgba(34, 139, 34, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#FF00FFFFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva Fuchsia
        {
            get
            {
                return new ColorRgba(255, 0, 255, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#DCDCDCFFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva Gainsboro
        {
            get
            {
                return new ColorRgba(220, 220, 220, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#F8F8FFFFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva GhostWhite
        {
            get
            {
                return new ColorRgba(248, 248, 255, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#FFD700FFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva Gold
        {
            get
            {
                return new ColorRgba(255, 215, 0, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#DAA520FFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva Goldenrod
        {
            get
            {
                return new ColorRgba(218, 165, 32, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#808080FFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva Gray
        {
            get
            {
                return new ColorRgba(128, 128, 128, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#00FF00FFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva Green
        {
            get
            {
                return new ColorRgba(0, 255, 0, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#ADFF2FFFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva GreenYellow
        {
            get
            {
                return new ColorRgba(173, 255, 47, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#B9D1EAFFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva GradientActiveCaption
        {
            get
            {
                return new ColorRgba(185, 209, 234, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#D7E4F2FFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva GradientInactiveCaption
        {
            get
            {
                return new ColorRgba(215, 228, 242, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#6D6D6DFFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva GrayText
        {
            get
            {
                return new ColorRgba(109, 109, 109, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#008000FFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva HalfGreen
        {
            get
            {
                return new ColorRgba(0, 128, 0, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#F0FFF0FFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva Honeydew
        {
            get
            {
                return new ColorRgba(240, 255, 240, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#FF69B4FFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva HotPink
        {
            get
            {
                return new ColorRgba(255, 105, 180, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#3399FFFFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva Highlight
        {
            get
            {
                return new ColorRgba(51, 153, 255, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#FFFFFFFFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva HighlightText
        {
            get
            {
                return new ColorRgba(255, 255, 255, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#0066CCFFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva HotTrack
        {
            get
            {
                return new ColorRgba(0, 102, 204, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#CD5C5CFFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva IndianRed
        {
            get
            {
                return new ColorRgba(205, 92, 92, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#4B0082FFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva Indigo
        {
            get
            {
                return new ColorRgba(75, 0, 130, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#FFFFF0FFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva Ivory
        {
            get
            {
                return new ColorRgba(255, 255, 240, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#F4F7FCFFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva InactiveBorder
        {
            get
            {
                return new ColorRgba(244, 247, 252, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#BFCDDBFFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva InactiveCaption
        {
            get
            {
                return new ColorRgba(191, 205, 219, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#434E54FFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva InactiveCaptionText
        {
            get
            {
                return new ColorRgba(67, 78, 84, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#FFFFE1FFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva Info
        {
            get
            {
                return new ColorRgba(255, 255, 225, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#000000FFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva InfoText
        {
            get
            {
                return new ColorRgba(0, 0, 0, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#F0E68CFFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva Khaki
        {
            get
            {
                return new ColorRgba(240, 230, 140, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#E6E6FAFFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva Lavender
        {
            get
            {
                return new ColorRgba(230, 230, 250, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#FFF0F5FFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva LavenderBlush
        {
            get
            {
                return new ColorRgba(255, 240, 245, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#7CFC00FFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva LawnGreen
        {
            get
            {
                return new ColorRgba(124, 252, 0, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#FFFACDFFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva LemonChiffon
        {
            get
            {
                return new ColorRgba(255, 250, 205, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#ADD8E6FFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva LightBlue
        {
            get
            {
                return new ColorRgba(173, 216, 230, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#F08080FFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva LightCoral
        {
            get
            {
                return new ColorRgba(240, 128, 128, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#E0FFFFFFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva LightCyan
        {
            get
            {
                return new ColorRgba(224, 255, 255, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#FAFAD2FFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva LightGoldenrodYellow
        {
            get
            {
                return new ColorRgba(250, 250, 210, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#D3D3D3FFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva LightGray
        {
            get
            {
                return new ColorRgba(211, 211, 211, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#C0C0C0FFの色を<see cref="IColorData"/>で・謫ｾします。
        /// </summary>
        public static ColorHsva LightGrey
        {
            get
            {
                return new ColorRgba(192, 192, 192, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#90EE90FFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva LightGreen
        {
            get
            {
                return new ColorRgba(144, 238, 144, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#FFB6C1FFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva LightPink
        {
            get
            {
                return new ColorRgba(255, 182, 193, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#FFA07AFFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva LightSalmon
        {
            get
            {
                return new ColorRgba(255, 160, 122, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#20B2AAFFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva LightSeaGreen
        {
            get
            {
                return new ColorRgba(32, 178, 170, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#87CEFAFFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva LightSkyBlue
        {
            get
            {
                return new ColorRgba(135, 206, 250, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#778899FFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva LightSlateGray
        {
            get
            {
                return new ColorRgba(119, 136, 153, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#B0C4DEFFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva LightSteelBlue
        {
            get
            {
                return new ColorRgba(176, 196, 222, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#FFFFE0FFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva LightYellow
        {
            get
            {
                return new ColorRgba(255, 255, 224, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#00FF00FFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva Lime
        {
            get
            {
                return new ColorRgba(0, 255, 0, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#32CD32FFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva LimeGreen
        {
            get
            {
                return new ColorRgba(50, 205, 50, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#FAF0E6FFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva Linen
        {
            get
            {
                return new ColorRgba(250, 240, 230, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#FF00FFFFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva Magenta
        {
            get
            {
                return new ColorRgba(255, 0, 255, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#800000FFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva Maroon
        {
            get
            {
                return new ColorRgba(128, 0, 0, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#66CDAAFFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva MediumAquamarine
        {
            get
            {
                return new ColorRgba(102, 205, 170, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#0000CDFFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva MediumBlue
        {
            get
            {
                return new ColorRgba(0, 0, 205, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#BA55D3FFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva MediumOrchid
        {
            get
            {
                return new ColorRgba(186, 85, 211, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#9370DBFFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva MediumPurple
        {
            get
            {
                return new ColorRgba(147, 112, 219, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#3CB371FFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva MediumSeaGreen
        {
            get
            {
                return new ColorRgba(60, 179, 113, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#7B68EEFFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva MediumSlateBlue
        {
            get
            {
                return new ColorRgba(123, 104, 238, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#00FA9AFFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva MediumSpringGreen
        {
            get
            {
                return new ColorRgba(0, 250, 154, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#48D1CCFFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva MediumTurquoise
        {
            get
            {
                return new ColorRgba(72, 209, 204, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#C71585FFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva MediumVioletRed
        {
            get
            {
                return new ColorRgba(199, 21, 133, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#191970FFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva MidnightBlue
        {
            get
            {
                return new ColorRgba(25, 25, 112, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#FFE4E1FFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva MistyRose
        {
            get
            {
                return new ColorRgba(255, 228, 225, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#FFE4B5FFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva Moccasin
        {
            get
            {
                return new ColorRgba(255, 228, 181, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#F0F0F0FFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva Menu
        {
            get
            {
                return new ColorRgba(240, 240, 240, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#F0F0F0FFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva MenuBar
        {
            get
            {
                return new ColorRgba(240, 240, 240, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#3399FFFFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva MenuHighlight
        {
            get
            {
                return new ColorRgba(51, 153, 255, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#000000FFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva MenuText
        {
            get
            {
                return new ColorRgba(0, 0, 0, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#F5FFFAFFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva MintCream
        {
            get
            {
                return new ColorRgba(245, 255, 250, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#FFDEADFFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva NavajoWhite
        {
            get
            {
                return new ColorRgba(255, 222, 173, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#000080FFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva Navy
        {
            get
            {
                return new ColorRgba(0, 0, 128, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#FDF5E6FFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva OldLace
        {
            get
            {
                return new ColorRgba(253, 245, 230, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#808000FFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva Olive
        {
            get
            {
                return new ColorRgba(128, 128, 0, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#6B8E23FFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva OliveDrab
        {
            get
            {
                return new ColorRgba(107, 142, 35, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#FFA500FFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva Orange
        {
            get
            {
                return new ColorRgba(255, 165, 0, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#FF4500FFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva OrangeRed
        {
            get
            {
                return new ColorRgba(255, 69, 0, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#DA70D6FFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva Orchid
        {
            get
            {
                return new ColorRgba(218, 112, 214, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#EEE8AAFFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva PaleGoldenrod
        {
            get
            {
                return new ColorRgba(238, 232, 170, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#98FB98FFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva PaleGreen
        {
            get
            {
                return new ColorRgba(152, 251, 152, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#AFEEEEFFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva PaleTurquoise
        {
            get
            {
                return new ColorRgba(175, 238, 238, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#DB7093FFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva PaleVioletRed
        {
            get
            {
                return new ColorRgba(219, 112, 147, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#FFEFD5FFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva PapayaWhip
        {
            get
            {
                return new ColorRgba(255, 239, 213, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#FFDAB9FFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva PeachPuff
        {
            get
            {
                return new ColorRgba(255, 218, 185, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#CD853FFFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva Peru
        {
            get
            {
                return new ColorRgba(205, 133, 63, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#FFC0CBFFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva Pink
        {
            get
            {
                return new ColorRgba(255, 192, 203, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#DDA0DDFFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva Plum
        {
            get
            {
                return new ColorRgba(221, 160, 221, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#B0E0E6FFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva PowderBlue
        {
            get
            {
                return new ColorRgba(176, 224, 230, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#800080FFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva Purple
        {
            get
            {
                return new ColorRgba(128, 0, 128, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#FF0000FFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva Red
        {
            get
            {
                return new ColorRgba(255, 0, 0, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#BC8F8FFFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva RosyBrown
        {
            get
            {
                return new ColorRgba(188, 143, 143, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#4169E1FFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva RoyalBlue
        {
            get
            {
                return new ColorRgba(65, 105, 225, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#8B4513FFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva SaddleBrown
        {
            get
            {
                return new ColorRgba(139, 69, 19, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#FA8072FFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva Salmon
        {
            get
            {
                return new ColorRgba(250, 128, 114, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#F4A460FFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva SandyBrown
        {
            get
            {
                return new ColorRgba(244, 164, 96, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#2E8B57FFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva SeaGreen
        {
            get
            {
                return new ColorRgba(46, 139, 87, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#FFF5EEFFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva SeaShell
        {
            get
            {
                return new ColorRgba(255, 245, 238, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#A0522DFFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva Sienna
        {
            get
            {
                return new ColorRgba(160, 82, 45, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#C0C0C0FFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva Silver
        {
            get
            {
                return new ColorRgba(192, 192, 192, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#87CEEBFFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva SkyBlue
        {
            get
            {
                return new ColorRgba(135, 206, 235, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#6A5ACDFFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva SlateBlue
        {
            get
            {
                return new ColorRgba(106, 90, 205, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#708090FFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva SlateGray
        {
            get
            {
                return new ColorRgba(112, 128, 144, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#FFFAFAFFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva Snow
        {
            get
            {
                return new ColorRgba(255, 250, 250, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#00FF7FFFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva SpringGreen
        {
            get
            {
                return new ColorRgba(0, 255, 127, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#4682B4FFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva SteelBlue
        {
            get
            {
                return new ColorRgba(70, 130, 180, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#C8C8C8FFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva ScrollBar
        {
            get
            {
                return new ColorRgba(200, 200, 200, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#D2B48CFFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva Tan
        {
            get
            {
                return new ColorRgba(210, 180, 140, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#008080FFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva Teal
        {
            get
            {
                return new ColorRgba(0, 128, 128, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#D8BFD8FFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva Thistle
        {
            get
            {
                return new ColorRgba(216, 191, 216, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#FF6347FFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva Tomato
        {
            get
            {
                return new ColorRgba(255, 99, 71, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#40E0D0FFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva Turquoise
        {
            get
            {
                return new ColorRgba(64, 224, 208, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#00000000の色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva TransparentBlack
        {
            get
            {
                return new ColorRgba(0, 0, 0, 0).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#FFFFFF00の色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva TransparentWhite
        {
            get
            {
                return new ColorRgba(255, 255, 255, 0).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#EE82EEFFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva Violet
        {
            get
            {
                return new ColorRgba(238, 130, 238, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#C8C8C8FFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva VeryLightGray
        {
            get
            {
                return new ColorRgba(200, 200, 200, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#E0E0E0FFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva VeryLightGrey
        {
            get
            {
                return new ColorRgba(224, 224, 224, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#202020FFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva VeryDarkGray
        {
            get
            {
                return new ColorRgba(32, 32, 32, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#F5DEB3FFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva Wheat
        {
            get
            {
                return new ColorRgba(245, 222, 179, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#FFFFFFFFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva White
        {
            get
            {
                return new ColorRgba(255, 255, 255, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#F5F5F5FFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva WhiteSmoke
        {
            get
            {
                return new ColorRgba(245, 245, 245, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#FFFFFFFFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva Window
        {
            get
            {
                return new ColorRgba(255, 255, 255, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#646464FFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva WindowFrame
        {
            get
            {
                return new ColorRgba(100, 100, 100, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#000000FFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva WindowText
        {
            get
            {
                return new ColorRgba(0, 0, 0, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#FFFF00FFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva Yellow
        {
            get
            {
                return new ColorRgba(255, 255, 0, 255).ToColorHsva();
            }
        }

        /// <summary>
        /// RGBA値が#9ACD32FFの色を<see cref="ColorHsva"/>で取得します。
        /// </summary>
        public static ColorHsva YellowGreen
        {
            get
            {
                return new ColorRgba(154, 205, 50, 255).ToColorHsva();
            }
        }

        #endregion //TColorData.Palette


    }
}
