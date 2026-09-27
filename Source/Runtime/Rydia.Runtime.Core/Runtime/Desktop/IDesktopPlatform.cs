namespace Rydia.Runtime.Desktop
{
    public interface IDesktopPlatform : IPlatform
    {

        WindowOptions WindowOptions
        {
            get;
        }

        IDesktopWindow Window
        {
            get;
        }

    }

}
