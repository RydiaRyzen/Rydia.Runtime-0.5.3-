namespace Rydia
{

    /// <summary>
    /// 2D空間アラインメントを表します。
    /// </summary>
    public enum Alignment
    {
        /// <summary>
        /// 中心に合わせます
        /// </summary>
        Center = 0x0,

        /// <summary>
        /// 左に合わせます
        /// </summary>
        Left = 0x1,
        /// <summary>
        /// 右に合わせます
        /// </summary>
        Right = 0x2,
        /// <summary>
        /// 上に合わせます
        /// </summary>
        Top = 0x4,
        /// <summary>
        /// 下に合わせます
        /// </summary>
        Bottom = 0x8,

        /// <summary>
        /// 左上に合わせます
        /// </summary>
        TopLeft = Top | Left,
        /// <summary>
        /// 右上に合わせます
        /// </summary>
        TopRight = Top | Right,
        /// <summary>
        /// 左下に合わせます
        /// </summary>
        BottomLeft = Bottom | Left,
        /// <summary>
        /// 右下に合わせます
        /// </summary>
        BottomRight = Bottom | Right
    }

}
