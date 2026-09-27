using Rydia.Runtime.Desktop;

namespace Rydia.Runtime.Desktop
{
    public class WindowOptions
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public string Title { get; set; }
        public RefreshMode RefreshMode { get; set; }
        public ScreenMode ScreenMode { get; set; }
    }
}