using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rydia.Drawing;
using Rydia.Serialization;

namespace Rydia.Resources
{
    /// <summary>
    /// <see cref="PixelData"/>クラスは単一の画像を表し、
    /// <see cref="Pixmap"/>は複数の<see cref="PixelData"/>を格納し、
    /// アトラスを設定することで、チップ画像としての利用を想定した機能を提供します
    /// </summary>
    public class PixelData
    {

        [FieldOrder(0)]
        public int Width { get; set; }

        [FieldOrder(1)]
        public int Height { get; set; }

        [FieldOrder(2)]
        public int DataLength;

        [FieldOrder(3)]
        [FieldLength(nameof(DataLength))]
        public byte[] Data { get; set; }

        public void Transparent(ColorRgba color)
        {
            if (Data == null || Data.Length != Width * Height * 4)
                return;
            for (int i = 0; i < Data.Length; i += 4)
            {
                if (Data[i] == color.R && Data[i + 1] == color.G && Data[i + 2] == color.B && Data[i + 3] == color.A)
                {
                    Data[i] = 0;     // R
                    Data[i + 1] = 0; // G
                    Data[i + 2] = 0; // B
                    Data[i + 3] = 0; // A (fully transparent)
                }
            }
        }

    }
}
