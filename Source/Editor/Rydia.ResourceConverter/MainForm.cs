using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Rydia.ResourceConverter.Assets.Importer;

namespace Rydia.ResourceConverter
{
    public partial class MainForm : Form
    {

        public static string MediaDir
        {
            get;
        }

        public static string AssetsDir
        {
            get;
        }

        static MainForm()
        {
            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            MediaDir = System.IO.Path.Combine(baseDir, "Media");
            AssetsDir = System.IO.Path.Combine(baseDir, "Assets");
            Directory.CreateDirectory(MediaDir);
            Directory.CreateDirectory(AssetsDir);
            PixelDataImporter.Init();
            AudioDataImporter.Init();
            AudioDataImporter.Registered(AudioDataImporter.Mp3, new Mp3Importer());
        }

        public MainForm()
        {
            InitializeComponent();
            OpenResourceConverter();
            OpenSystemFontConverter();
            OpenTTFFontConverter();
            OpenProjectView();
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void OpenProjectView()
        {
            var form = new ProjectView();
            form.MdiParent = this;
            form.Show();
        }

        private void OpenResourceConverter()
        {
            var form = new ResourceConverter();
            form.MdiParent = this;
            form.Show();
        }

        private void OpenSystemFontConverter()
        {
            var form = new SystemFontConverter();
            form.MdiParent = this;
            form.Show();
        }

        private void OpenTTFFontConverter()
        {
            var form = new TTFFontConverter();
            form.MdiParent = this;
            form.Show();
        }

        /// <summary>
        /// Resourceコンバーター
        /// </summary>
        private void resourceConverterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenResourceConverter();
        }

        private void システムフォントコンバーターToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenSystemFontConverter();
        }

        private void tTFフォントToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenTTFFontConverter();
        }
        /// <summary>
        /// Assetsフォルダを開くボタン
        /// </summary>
        private void assetsフォルダToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dir = MainForm.AssetsDir;
            Directory.CreateDirectory(dir);
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo()
            {
                FileName = dir,
                UseShellExecute = true
            });
        }

        /// <summary>
        /// Mediaフォルダを開くボタン
        /// </summary>
        private void mediaフォルダToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dir = MainForm.MediaDir;
            Directory.CreateDirectory(dir);
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo()
            {
                FileName = dir,
                UseShellExecute = true,
            });
        }

    }
}
