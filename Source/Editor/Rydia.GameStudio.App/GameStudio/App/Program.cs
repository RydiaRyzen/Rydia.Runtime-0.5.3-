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
                splash.Shown += async (_, _) =>
                {
                    try
                    {
                        await AppInitializer.InitializeAsync(splash.SetStatus);

                        splash.DialogResult = DialogResult.OK;
                        splash.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(
                            splash,
                            $"èâä˙âªÇ…é∏îsÇµÇ‹ÇµÇΩÅB\n{ex.Message}",
                            "ÉGÉâÅ[",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);

                        splash.DialogResult = DialogResult.Abort;
                        splash.Close();
                    }
                };

                if (splash.ShowDialog() != DialogResult.OK)
                    return;
            }
            Application.Run(new MainForm());
        }
    }
}