using System;
using System.Collections.Generic;
using System.Text;

namespace Rydia.Drawing
{
    public partial struct ColorRgba
    {

        #region TColorData.Palette

        /// <summary>
        /// RGBA値が#202020FFの色を<see cref="ColorRgba"/>で取得します
        /// </summary>
        public static ColorRgba VeryDarkGrey
        {
            get
            {
                return new ColorRgba(32, 32, 32, 255);
            }
        }

        /// <summary>
        /// RGBA値が#F0F8FFFFの色を<see cref="ColorRgba"/>で取得します。
        /// </summary>
        public static ColorRgba AliceBlue
        {
            get
            {
                return new ColorRgba(240, 248, 255, 255);
            }
        }

        /// <summary>
        /// RGBA値が#FAEBD7FFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba AntiqueWhite
        {
            get
            {
                return new ColorRgba(250, 235, 215, 255);
            }
        }

        /// <summary>
        /// RGBA値が#00FFFFFFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba Aqua
        {
            get
            {
                return new ColorRgba(0, 255, 255, 255);
            }
        }

        /// <summary>
        /// RGBA値が#7FFFD4FFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba Aquamarine
        {
            get
            {
                return new ColorRgba(127, 255, 212, 255);
            }
        }

        /// <summary>
        /// RGBA値が#F0FFFFFFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba Azure
        {
            get
            {
                return new ColorRgba(240, 255, 255, 255);
            }
        }

        /// <summary>
        /// RGBA値が#B4B4B4FFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba ActiveBorder
        {
            get
            {
                return new ColorRgba(180, 180, 180, 255);
            }
        }

        /// <summary>
        /// RGBA値が#99B4D1FFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba ActiveCaption
        {
            get
            {
                return new ColorRgba(153, 180, 209, 255);
            }
        }

        /// <summary>
        /// RGBA値が#000000FFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba ActiveCaptionText
        {
            get
            {
                return new ColorRgba(0, 0, 0, 255);
            }
        }

        /// <summary>
        /// RGBA値が#ABABABFFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba AppWorkspace
        {
            get
            {
                return new ColorRgba(171, 171, 171, 255);
            }
        }

        /// <summary>
        /// RGBA値が#F5F5DCFFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba Beige
        {
            get
            {
                return new ColorRgba(245, 245, 220, 255);
            }
        }

        /// <summary>
        /// RGBA値が#FFE4C4FFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba Bisque
        {
            get
            {
                return new ColorRgba(255, 228, 196, 255);
            }
        }

        /// <summary>
        /// RGBA値が#000000FFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba Black
        {
            get
            {
                return new ColorRgba(0, 0, 0, 255);
            }
        }

        /// <summary>
        /// RGBA値が#FFEBCDFFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba BlanchedAlmond
        {
            get
            {
                return new ColorRgba(255, 235, 205, 255);
            }
        }

        /// <summary>
        /// RGBA値が#0000FFFFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba Blue
        {
            get
            {
                return new ColorRgba(0, 0, 255, 255);
            }
        }

        /// <summary>
        /// RGBA値が#8A2BE2FFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba BlueViolet
        {
            get
            {
                return new ColorRgba(138, 43, 226, 255);
            }
        }

        /// <summary>
        /// RGBA値が#A52A2AFFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba Brown
        {
            get
            {
                return new ColorRgba(165, 42, 42, 255);
            }
        }

        /// <summary>
        /// RGBA値が#DEB887FFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba BurlyWood
        {
            get
            {
                return new ColorRgba(222, 184, 135, 255);
            }
        }

        /// <summary>
        /// RGBA値が#F0F0F0FFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba ButtonFace
        {
            get
            {
                return new ColorRgba(240, 240, 240, 255);
            }
        }

        /// <summary>
        /// RGBA値が#FFFFFFFFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba ButtonHighlight
        {
            get
            {
                return new ColorRgba(255, 255, 255, 255);
            }
        }

        /// <summary>
        /// RGBA値が#A0A0A0FFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba ButtonShadow
        {
            get
            {
                return new ColorRgba(160, 160, 160, 255);
            }
        }

        /// <summary>
        /// RGBA値が#5F9EA0FFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba CadetBlue
        {
            get
            {
                return new ColorRgba(95, 158, 160, 255);
            }
        }

        /// <summary>
        /// RGBA値が#7FFF00FFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba Chartreuse
        {
            get
            {
                return new ColorRgba(127, 255, 0, 255);
            }
        }

        /// <summary>
        /// RGBA値が#D2691EFFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba Chocolate
        {
            get
            {
                return new ColorRgba(210, 105, 30, 255);
            }
        }

        /// <summary>
        /// RGBA値が#FF7F50FFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba Coral
        {
            get
            {
                return new ColorRgba(255, 127, 80, 255);
            }
        }

        /// <summary>
        /// RGBA値が#6495EDFFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba CornflowerBlue
        {
            get
            {
                return new ColorRgba(100, 149, 237, 255);
            }
        }

        /// <summary>
        /// RGBA値が#FFF8DCFFの色を<see cref="IColorData"/>で取得しま・ｷ。
        /// </summary>
        public static ColorRgba Cornsilk
        {
            get
            {
                return new ColorRgba(255, 248, 220, 255);
            }
        }

        /// <summary>
        /// RGBA値が#DC143CFFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba Crimson
        {
            get
            {
                return new ColorRgba(220, 20, 60, 255);
            }
        }

        /// <summary>
        /// RGBA値が#00FFFFFFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba Cyan
        {
            get
            {
                return new ColorRgba(0, 255, 255, 255);
            }
        }

        /// <summary>
        /// RGBA値が#F0F0F0FFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba Control
        {
            get
            {
                return new ColorRgba(240, 240, 240, 255);
            }
        }

        /// <summary>
        /// RGBA値が#A0A0A0FFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba ControlDark
        {
            get
            {
                return new ColorRgba(160, 160, 160, 255);
            }
        }

        /// <summary>
        /// RGBA値が#696969FFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba ControlDarkDark
        {
            get
            {
                return new ColorRgba(105, 105, 105, 255);
            }
        }

        /// <summary>
        /// RGBA値が#E3E3E3FFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba ControlLight
        {
            get
            {
                return new ColorRgba(227, 227, 227, 255);
            }
        }

        /// <summary>
        /// RGBA値が#FFFFFFFFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba ControlLightLight
        {
            get
            {
                return new ColorRgba(255, 255, 255, 255);
            }
        }

        /// <summary>
        /// RGBA値が#000000FFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba ControlText
        {
            get
            {
                return new ColorRgba(0, 0, 0, 255);
            }
        }

        /// <summary>
        /// RGBA値が#00008BFFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba DarkBlue
        {
            get
            {
                return new ColorRgba(0, 0, 139, 255);
            }
        }

        /// <summary>
        /// RGBA値が#008B8BFFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba DarkCyan
        {
            get
            {
                return new ColorRgba(0, 139, 139, 255);
            }
        }

        /// <summary>
        /// RGBA値が#B8860BFFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba DarkGoldenrod
        {
            get
            {
                return new ColorRgba(184, 134, 11, 255);
            }
        }

        /// <summary>
        /// RGBA値が#A9A9A9FFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba DarkGray
        {
            get
            {
                return new ColorRgba(169, 169, 169, 255);
            }
        }

        /// <summary>
        /// RGBA値が#404040FFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba DarkGrey
        {
            get
            {
                return new ColorRgba(64, 64, 64, 255);
            }
        }

        /// <summary>
        /// RGBA値が#006400FFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba DarkGreen
        {
            get
            {
                return new ColorRgba(0, 100, 0, 255);
            }
        }

        /// <summary>
        /// RGBA値が#BDB76BFFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba DarkKhaki
        {
            get
            {
                return new ColorRgba(189, 183, 107, 255);
            }
        }

        /// <summary>
        /// RGBA値が#8B008BFFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba DarkMagenta
        {
            get
            {
                return new ColorRgba(139, 0, 139, 255);
            }
        }

        /// <summary>
        /// RGBA値が#556B2FFFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba DarkOliveGreen
        {
            get
            {
                return new ColorRgba(85, 107, 47, 255);
            }
        }

        /// <summary>
        /// RGBA値が#FF8C00FFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba DarkOrange
        {
            get
            {
                return new ColorRgba(255, 140, 0, 255);
            }
        }

        /// <summary>
        /// RGBA値が#9932CCFFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba DarkOrchid
        {
            get
            {
                return new ColorRgba(153, 50, 204, 255);
            }
        }

        /// <summary>
        /// RGBA値が#8B0000FFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba DarkRed
        {
            get
            {
                return new ColorRgba(139, 0, 0, 255);
            }
        }

        /// <summary>
        /// RGBA値が#E9967AFFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba DarkSalmon
        {
            get
            {
                return new ColorRgba(233, 150, 122, 255);
            }
        }

        /// <summary>
        /// RGBA値が#8FBC8BFFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba DarkSeaGreen
        {
            get
            {
                return new ColorRgba(143, 188, 139, 255);
            }
        }

        /// <summary>
        /// RGBA値が#483D8BFFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba DarkSlateBlue
        {
            get
            {
                return new ColorRgba(72, 61, 139, 255);
            }
        }

        /// <summary>
        /// RGBA値が#2F4F4FFFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba DarkSlateGray
        {
            get
            {
                return new ColorRgba(47, 79, 79, 255);
            }
        }

        /// <summary>
        /// RGBA値が#00CED1FFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba DarkTurquoise
        {
            get
            {
                return new ColorRgba(0, 206, 209, 255);
            }
        }

        /// <summary>
        /// RGBA値が#9400D3FFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba DarkViolet
        {
            get
            {
                return new ColorRgba(148, 0, 211, 255);
            }
        }

        /// <summary>
        /// RGBA値が#FF1493FFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba DeepPink
        {
            get
            {
                return new ColorRgba(255, 20, 147, 255);
            }
        }

        /// <summary>
        /// RGBA値が#00BFFFFFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba DeepSkyBlue
        {
            get
            {
                return new ColorRgba(0, 191, 255, 255);
            }
        }

        /// <summary>
        /// RGBA値が#696969FFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba DimGray
        {
            get
            {
                return new ColorRgba(105, 105, 105, 255);
            }
        }

        /// <summary>
        /// RGBA値が#000000FFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba Desktop
        {
            get
            {
                return new ColorRgba(0, 0, 0, 255);
            }
        }

        /// <summary>
        /// RGBA値が#1E90FFFFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba DodgerBlue
        {
            get
            {
                return new ColorRgba(30, 144, 255, 255);
            }
        }

        /// <summary>
        /// RGBA値が#B22222FFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba Firebrick
        {
            get
            {
                return new ColorRgba(178, 34, 34, 255);
            }
        }

        /// <summary>
        /// RGBA値が#FFFAF0FFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba FloralWhite
        {
            get
            {
                return new ColorRgba(255, 250, 240, 255);
            }
        }

        /// <summary>
        /// RGBA値が#228B22FFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba ForestGreen
        {
            get
            {
                return new ColorRgba(34, 139, 34, 255);
            }
        }

        /// <summary>
        /// RGBA値が#FF00FFFFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba Fuchsia
        {
            get
            {
                return new ColorRgba(255, 0, 255, 255);
            }
        }

        /// <summary>
        /// RGBA値が#DCDCDCFFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba Gainsboro
        {
            get
            {
                return new ColorRgba(220, 220, 220, 255);
            }
        }

        /// <summary>
        /// RGBA値が#F8F8FFFFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba GhostWhite
        {
            get
            {
                return new ColorRgba(248, 248, 255, 255);
            }
        }

        /// <summary>
        /// RGBA値が#FFD700FFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba Gold
        {
            get
            {
                return new ColorRgba(255, 215, 0, 255);
            }
        }

        /// <summary>
        /// RGBA値が#DAA520FFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba Goldenrod
        {
            get
            {
                return new ColorRgba(218, 165, 32, 255);
            }
        }

        /// <summary>
        /// RGBA値が#808080FFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba Gray
        {
            get
            {
                return new ColorRgba(128, 128, 128, 255);
            }
        }

        /// <summary>
        /// RGBA値が#00FF00FFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba Green
        {
            get
            {
                return new ColorRgba(0, 255, 0, 255);
            }
        }

        /// <summary>
        /// RGBA値が#ADFF2FFFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba GreenYellow
        {
            get
            {
                return new ColorRgba(173, 255, 47, 255);
            }
        }

        /// <summary>
        /// RGBA値が#B9D1EAFFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba GradientActiveCaption
        {
            get
            {
                return new ColorRgba(185, 209, 234, 255);
            }
        }

        /// <summary>
        /// RGBA値が#D7E4F2FFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba GradientInactiveCaption
        {
            get
            {
                return new ColorRgba(215, 228, 242, 255);
            }
        }

        /// <summary>
        /// RGBA値が#6D6D6DFFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba GrayText
        {
            get
            {
                return new ColorRgba(109, 109, 109, 255);
            }
        }

        /// <summary>
        /// RGBA値が#008000FFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba HalfGreen
        {
            get
            {
                return new ColorRgba(0, 128, 0, 255);
            }
        }

        /// <summary>
        /// RGBA値が#F0FFF0FFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba Honeydew
        {
            get
            {
                return new ColorRgba(240, 255, 240, 255);
            }
        }

        /// <summary>
        /// RGBA値が#FF69B4FFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba HotPink
        {
            get
            {
                return new ColorRgba(255, 105, 180, 255);
            }
        }

        /// <summary>
        /// RGBA値が#3399FFFFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba Highlight
        {
            get
            {
                return new ColorRgba(51, 153, 255, 255);
            }
        }

        /// <summary>
        /// RGBA値が#FFFFFFFFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba HighlightText
        {
            get
            {
                return new ColorRgba(255, 255, 255, 255);
            }
        }

        /// <summary>
        /// RGBA値が#0066CCFFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba HotTrack
        {
            get
            {
                return new ColorRgba(0, 102, 204, 255);
            }
        }

        /// <summary>
        /// RGBA値が#CD5C5CFFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba IndianRed
        {
            get
            {
                return new ColorRgba(205, 92, 92, 255);
            }
        }

        /// <summary>
        /// RGBA値が#4B0082FFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba Indigo
        {
            get
            {
                return new ColorRgba(75, 0, 130, 255);
            }
        }

        /// <summary>
        /// RGBA値が#FFFFF0FFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba Ivory
        {
            get
            {
                return new ColorRgba(255, 255, 240, 255);
            }
        }

        /// <summary>
        /// RGBA値が#F4F7FCFFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba InactiveBorder
        {
            get
            {
                return new ColorRgba(244, 247, 252, 255);
            }
        }

        /// <summary>
        /// RGBA値が#BFCDDBFFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba InactiveCaption
        {
            get
            {
                return new ColorRgba(191, 205, 219, 255);
            }
        }

        /// <summary>
        /// RGBA値が#434E54FFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba InactiveCaptionText
        {
            get
            {
                return new ColorRgba(67, 78, 84, 255);
            }
        }

        /// <summary>
        /// RGBA値が#FFFFE1FFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba Info
        {
            get
            {
                return new ColorRgba(255, 255, 225, 255);
            }
        }

        /// <summary>
        /// RGBA値が#000000FFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba InfoText
        {
            get
            {
                return new ColorRgba(0, 0, 0, 255);
            }
        }

        /// <summary>
        /// RGBA値が#F0E68CFFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba Khaki
        {
            get
            {
                return new ColorRgba(240, 230, 140, 255);
            }
        }

        /// <summary>
        /// RGBA値が#E6E6FAFFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba Lavender
        {
            get
            {
                return new ColorRgba(230, 230, 250, 255);
            }
        }

        /// <summary>
        /// RGBA値が#FFF0F5FFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba LavenderBlush
        {
            get
            {
                return new ColorRgba(255, 240, 245, 255);
            }
        }

        /// <summary>
        /// RGBA値が#7CFC00FFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba LawnGreen
        {
            get
            {
                return new ColorRgba(124, 252, 0, 255);
            }
        }

        /// <summary>
        /// RGBA値が#FFFACDFFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba LemonChiffon
        {
            get
            {
                return new ColorRgba(255, 250, 205, 255);
            }
        }

        /// <summary>
        /// RGBA値が#ADD8E6FFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba LightBlue
        {
            get
            {
                return new ColorRgba(173, 216, 230, 255);
            }
        }

        /// <summary>
        /// RGBA値が#F08080FFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba LightCoral
        {
            get
            {
                return new ColorRgba(240, 128, 128, 255);
            }
        }

        /// <summary>
        /// RGBA値が#E0FFFFFFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba LightCyan
        {
            get
            {
                return new ColorRgba(224, 255, 255, 255);
            }
        }

        /// <summary>
        /// RGBA値が#FAFAD2FFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba LightGoldenrodYellow
        {
            get
            {
                return new ColorRgba(250, 250, 210, 255);
            }
        }

        /// <summary>
        /// RGBA値が#D3D3D3FFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba LightGray
        {
            get
            {
                return new ColorRgba(211, 211, 211, 255);
            }
        }

        /// <summary>
        /// RGBA値が#C0C0C0FFの色を<see cref="IColorData"/>で・謫ｾします。
        /// </summary>
        public static ColorRgba LightGrey
        {
            get
            {
                return new ColorRgba(192, 192, 192, 255);
            }
        }

        /// <summary>
        /// RGBA値が#90EE90FFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba LightGreen
        {
            get
            {
                return new ColorRgba(144, 238, 144, 255);
            }
        }

        /// <summary>
        /// RGBA値が#FFB6C1FFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba LightPink
        {
            get
            {
                return new ColorRgba(255, 182, 193, 255);
            }
        }

        /// <summary>
        /// RGBA値が#FFA07AFFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba LightSalmon
        {
            get
            {
                return new ColorRgba(255, 160, 122, 255);
            }
        }

        /// <summary>
        /// RGBA値が#20B2AAFFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba LightSeaGreen
        {
            get
            {
                return new ColorRgba(32, 178, 170, 255);
            }
        }

        /// <summary>
        /// RGBA値が#87CEFAFFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba LightSkyBlue
        {
            get
            {
                return new ColorRgba(135, 206, 250, 255);
            }
        }

        /// <summary>
        /// RGBA値が#778899FFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba LightSlateGray
        {
            get
            {
                return new ColorRgba(119, 136, 153, 255);
            }
        }

        /// <summary>
        /// RGBA値が#B0C4DEFFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba LightSteelBlue
        {
            get
            {
                return new ColorRgba(176, 196, 222, 255);
            }
        }

        /// <summary>
        /// RGBA値が#FFFFE0FFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba LightYellow
        {
            get
            {
                return new ColorRgba(255, 255, 224, 255);
            }
        }

        /// <summary>
        /// RGBA値が#00FF00FFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba Lime
        {
            get
            {
                return new ColorRgba(0, 255, 0, 255);
            }
        }

        /// <summary>
        /// RGBA値が#32CD32FFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba LimeGreen
        {
            get
            {
                return new ColorRgba(50, 205, 50, 255);
            }
        }

        /// <summary>
        /// RGBA値が#FAF0E6FFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba Linen
        {
            get
            {
                return new ColorRgba(250, 240, 230, 255);
            }
        }

        /// <summary>
        /// RGBA値が#FF00FFFFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba Magenta
        {
            get
            {
                return new ColorRgba(255, 0, 255, 255);
            }
        }

        /// <summary>
        /// RGBA値が#800000FFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba Maroon
        {
            get
            {
                return new ColorRgba(128, 0, 0, 255);
            }
        }

        /// <summary>
        /// RGBA値が#66CDAAFFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba MediumAquamarine
        {
            get
            {
                return new ColorRgba(102, 205, 170, 255);
            }
        }

        /// <summary>
        /// RGBA値が#0000CDFFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba MediumBlue
        {
            get
            {
                return new ColorRgba(0, 0, 205, 255);
            }
        }

        /// <summary>
        /// RGBA値が#BA55D3FFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba MediumOrchid
        {
            get
            {
                return new ColorRgba(186, 85, 211, 255);
            }
        }

        /// <summary>
        /// RGBA値が#9370DBFFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba MediumPurple
        {
            get
            {
                return new ColorRgba(147, 112, 219, 255);
            }
        }

        /// <summary>
        /// RGBA値が#3CB371FFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba MediumSeaGreen
        {
            get
            {
                return new ColorRgba(60, 179, 113, 255);
            }
        }

        /// <summary>
        /// RGBA値が#7B68EEFFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba MediumSlateBlue
        {
            get
            {
                return new ColorRgba(123, 104, 238, 255);
            }
        }

        /// <summary>
        /// RGBA値が#00FA9AFFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba MediumSpringGreen
        {
            get
            {
                return new ColorRgba(0, 250, 154, 255);
            }
        }

        /// <summary>
        /// RGBA値が#48D1CCFFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba MediumTurquoise
        {
            get
            {
                return new ColorRgba(72, 209, 204, 255);
            }
        }

        /// <summary>
        /// RGBA値が#C71585FFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba MediumVioletRed
        {
            get
            {
                return new ColorRgba(199, 21, 133, 255);
            }
        }

        /// <summary>
        /// RGBA値が#191970FFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba MidnightBlue
        {
            get
            {
                return new ColorRgba(25, 25, 112, 255);
            }
        }

        /// <summary>
        /// RGBA値が#FFE4E1FFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba MistyRose
        {
            get
            {
                return new ColorRgba(255, 228, 225, 255);
            }
        }

        /// <summary>
        /// RGBA値が#FFE4B5FFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba Moccasin
        {
            get
            {
                return new ColorRgba(255, 228, 181, 255);
            }
        }

        /// <summary>
        /// RGBA値が#F0F0F0FFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba Menu
        {
            get
            {
                return new ColorRgba(240, 240, 240, 255);
            }
        }

        /// <summary>
        /// RGBA値が#F0F0F0FFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba MenuBar
        {
            get
            {
                return new ColorRgba(240, 240, 240, 255);
            }
        }

        /// <summary>
        /// RGBA値が#3399FFFFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba MenuHighlight
        {
            get
            {
                return new ColorRgba(51, 153, 255, 255);
            }
        }

        /// <summary>
        /// RGBA値が#000000FFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba MenuText
        {
            get
            {
                return new ColorRgba(0, 0, 0, 255);
            }
        }

        /// <summary>
        /// RGBA値が#F5FFFAFFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba MintCream
        {
            get
            {
                return new ColorRgba(245, 255, 250, 255);
            }
        }

        /// <summary>
        /// RGBA値が#FFDEADFFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba NavajoWhite
        {
            get
            {
                return new ColorRgba(255, 222, 173, 255);
            }
        }

        /// <summary>
        /// RGBA値が#000080FFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba Navy
        {
            get
            {
                return new ColorRgba(0, 0, 128, 255);
            }
        }

        /// <summary>
        /// RGBA値が#FDF5E6FFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba OldLace
        {
            get
            {
                return new ColorRgba(253, 245, 230, 255);
            }
        }

        /// <summary>
        /// RGBA値が#808000FFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba Olive
        {
            get
            {
                return new ColorRgba(128, 128, 0, 255);
            }
        }

        /// <summary>
        /// RGBA値が#6B8E23FFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba OliveDrab
        {
            get
            {
                return new ColorRgba(107, 142, 35, 255);
            }
        }

        /// <summary>
        /// RGBA値が#FFA500FFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba Orange
        {
            get
            {
                return new ColorRgba(255, 165, 0, 255);
            }
        }

        /// <summary>
        /// RGBA値が#FF4500FFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba OrangeRed
        {
            get
            {
                return new ColorRgba(255, 69, 0, 255);
            }
        }

        /// <summary>
        /// RGBA値が#DA70D6FFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba Orchid
        {
            get
            {
                return new ColorRgba(218, 112, 214, 255);
            }
        }

        /// <summary>
        /// RGBA値が#EEE8AAFFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba PaleGoldenrod
        {
            get
            {
                return new ColorRgba(238, 232, 170, 255);
            }
        }

        /// <summary>
        /// RGBA値が#98FB98FFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba PaleGreen
        {
            get
            {
                return new ColorRgba(152, 251, 152, 255);
            }
        }

        /// <summary>
        /// RGBA値が#AFEEEEFFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba PaleTurquoise
        {
            get
            {
                return new ColorRgba(175, 238, 238, 255);
            }
        }

        /// <summary>
        /// RGBA値が#DB7093FFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba PaleVioletRed
        {
            get
            {
                return new ColorRgba(219, 112, 147, 255);
            }
        }

        /// <summary>
        /// RGBA値が#FFEFD5FFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba PapayaWhip
        {
            get
            {
                return new ColorRgba(255, 239, 213, 255);
            }
        }

        /// <summary>
        /// RGBA値が#FFDAB9FFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba PeachPuff
        {
            get
            {
                return new ColorRgba(255, 218, 185, 255);
            }
        }

        /// <summary>
        /// RGBA値が#CD853FFFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba Peru
        {
            get
            {
                return new ColorRgba(205, 133, 63, 255);
            }
        }

        /// <summary>
        /// RGBA値が#FFC0CBFFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba Pink
        {
            get
            {
                return new ColorRgba(255, 192, 203, 255);
            }
        }

        /// <summary>
        /// RGBA値が#DDA0DDFFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba Plum
        {
            get
            {
                return new ColorRgba(221, 160, 221, 255);
            }
        }

        /// <summary>
        /// RGBA値が#B0E0E6FFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba PowderBlue
        {
            get
            {
                return new ColorRgba(176, 224, 230, 255);
            }
        }

        /// <summary>
        /// RGBA値が#800080FFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba Purple
        {
            get
            {
                return new ColorRgba(128, 0, 128, 255);
            }
        }

        /// <summary>
        /// RGBA値が#FF0000FFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba Red
        {
            get
            {
                return new ColorRgba(255, 0, 0, 255);
            }
        }

        /// <summary>
        /// RGBA値が#BC8F8FFFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba RosyBrown
        {
            get
            {
                return new ColorRgba(188, 143, 143, 255);
            }
        }

        /// <summary>
        /// RGBA値が#4169E1FFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba RoyalBlue
        {
            get
            {
                return new ColorRgba(65, 105, 225, 255);
            }
        }

        /// <summary>
        /// RGBA値が#8B4513FFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba SaddleBrown
        {
            get
            {
                return new ColorRgba(139, 69, 19, 255);
            }
        }

        /// <summary>
        /// RGBA値が#FA8072FFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba Salmon
        {
            get
            {
                return new ColorRgba(250, 128, 114, 255);
            }
        }

        /// <summary>
        /// RGBA値が#F4A460FFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba SandyBrown
        {
            get
            {
                return new ColorRgba(244, 164, 96, 255);
            }
        }

        /// <summary>
        /// RGBA値が#2E8B57FFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba SeaGreen
        {
            get
            {
                return new ColorRgba(46, 139, 87, 255);
            }
        }

        /// <summary>
        /// RGBA値が#FFF5EEFFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba SeaShell
        {
            get
            {
                return new ColorRgba(255, 245, 238, 255);
            }
        }

        /// <summary>
        /// RGBA値が#A0522DFFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba Sienna
        {
            get
            {
                return new ColorRgba(160, 82, 45, 255);
            }
        }

        /// <summary>
        /// RGBA値が#C0C0C0FFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba Silver
        {
            get
            {
                return new ColorRgba(192, 192, 192, 255);
            }
        }

        /// <summary>
        /// RGBA値が#87CEEBFFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba SkyBlue
        {
            get
            {
                return new ColorRgba(135, 206, 235, 255);
            }
        }

        /// <summary>
        /// RGBA値が#6A5ACDFFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba SlateBlue
        {
            get
            {
                return new ColorRgba(106, 90, 205, 255);
            }
        }

        /// <summary>
        /// RGBA値が#708090FFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba SlateGray
        {
            get
            {
                return new ColorRgba(112, 128, 144, 255);
            }
        }

        /// <summary>
        /// RGBA値が#FFFAFAFFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba Snow
        {
            get
            {
                return new ColorRgba(255, 250, 250, 255);
            }
        }

        /// <summary>
        /// RGBA値が#00FF7FFFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba SpringGreen
        {
            get
            {
                return new ColorRgba(0, 255, 127, 255);
            }
        }

        /// <summary>
        /// RGBA値が#4682B4FFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba SteelBlue
        {
            get
            {
                return new ColorRgba(70, 130, 180, 255);
            }
        }

        /// <summary>
        /// RGBA値が#C8C8C8FFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba ScrollBar
        {
            get
            {
                return new ColorRgba(200, 200, 200, 255);
            }
        }

        /// <summary>
        /// RGBA値が#D2B48CFFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba Tan
        {
            get
            {
                return new ColorRgba(210, 180, 140, 255);
            }
        }

        /// <summary>
        /// RGBA値が#008080FFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba Teal
        {
            get
            {
                return new ColorRgba(0, 128, 128, 255);
            }
        }

        /// <summary>
        /// RGBA値が#D8BFD8FFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba Thistle
        {
            get
            {
                return new ColorRgba(216, 191, 216, 255);
            }
        }

        /// <summary>
        /// RGBA値が#FF6347FFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba Tomato
        {
            get
            {
                return new ColorRgba(255, 99, 71, 255);
            }
        }

        /// <summary>
        /// RGBA値が#40E0D0FFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba Turquoise
        {
            get
            {
                return new ColorRgba(64, 224, 208, 255);
            }
        }

        /// <summary>
        /// RGBA値が#00000000の色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba TransparentBlack
        {
            get
            {
                return new ColorRgba(0, 0, 0, 0);
            }
        }

        /// <summary>
        /// RGBA値が#FFFFFF00の色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba TransparentWhite
        {
            get
            {
                return new ColorRgba(255, 255, 255, 0);
            }
        }

        /// <summary>
        /// RGBA値が#EE82EEFFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba Violet
        {
            get
            {
                return new ColorRgba(238, 130, 238, 255);
            }
        }

        /// <summary>
        /// RGBA値が#C8C8C8FFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba VeryLightGray
        {
            get
            {
                return new ColorRgba(200, 200, 200, 255);
            }
        }

        /// <summary>
        /// RGBA値が#E0E0E0FFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba VeryLightGrey
        {
            get
            {
                return new ColorRgba(224, 224, 224, 255);
            }
        }

        /// <summary>
        /// RGBA値が#202020FFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba VeryDarkGray
        {
            get
            {
                return new ColorRgba(32, 32, 32, 255);
            }
        }

        /// <summary>
        /// RGBA値が#F5DEB3FFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba Wheat
        {
            get
            {
                return new ColorRgba(245, 222, 179, 255);
            }
        }

        /// <summary>
        /// RGBA値が#FFFFFFFFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba White
        {
            get
            {
                return new ColorRgba(255, 255, 255, 255);
            }
        }

        /// <summary>
        /// RGBA値が#F5F5F5FFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba WhiteSmoke
        {
            get
            {
                return new ColorRgba(245, 245, 245, 255);
            }
        }

        /// <summary>
        /// RGBA値が#FFFFFFFFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba Window
        {
            get
            {
                return new ColorRgba(255, 255, 255, 255);
            }
        }

        /// <summary>
        /// RGBA値が#646464FFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba WindowFrame
        {
            get
            {
                return new ColorRgba(100, 100, 100, 255);
            }
        }

        /// <summary>
        /// RGBA値が#000000FFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba WindowText
        {
            get
            {
                return new ColorRgba(0, 0, 0, 255);
            }
        }

        /// <summary>
        /// RGBA値が#FFFF00FFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba Yellow
        {
            get
            {
                return new ColorRgba(255, 255, 0, 255);
            }
        }

        /// <summary>
        /// RGBA値が#9ACD32FFの色を<see cref="IColorData"/>で取得します。
        /// </summary>
        public static ColorRgba YellowGreen
        {
            get
            {
                return new ColorRgba(154, 205, 50, 255);
            }
        }

        #endregion //TColorData.Palette

    }
}
