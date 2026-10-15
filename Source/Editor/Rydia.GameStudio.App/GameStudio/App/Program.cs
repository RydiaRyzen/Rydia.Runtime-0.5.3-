using Rydia.GameStudio.Forms;

namespace Rydia.GameStudio.App
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            using (var splash = new SplashForm())
            {
                if (splash.ShowDialog() != DialogResult.OK)
                    return;
                using (var hub = new HubForm())
                {
                    if (hub.ShowDialog() != DialogResult.OK)
                        return;
                }
            }
            Application.Run(new MainForm());
        }
    }
}