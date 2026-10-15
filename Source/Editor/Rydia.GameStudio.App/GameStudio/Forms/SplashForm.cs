using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Rydia.GameStudio.App;

namespace Rydia.GameStudio
{
    public partial class SplashForm : Form
    {

        private readonly PictureBox titlePictureBox;
        private readonly Label statusLabel;
        private readonly ProgressBar progressBar;

        public SplashForm()
        {
            InitializeComponent();
            Text = "起動中";
            ClientSize = new Size(640, 400);
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.CenterScreen;
            ShowInTaskbar = false;
            ControlBox = false;

            var embedded = new Rydia.IO.EmbeddedResources<SplashForm>();
            var stream = embedded.Open("Rydia.GameStudio.EmbeddedResources.SplashLogo.png");
            var image = Image.FromStream(stream);
            var bitmap = new Bitmap(image);
            BackColor = bitmap.GetPixel(0, 0); // これにより、画像が完全に読み込まれることを保証します。

            this.titlePictureBox = new PictureBox
            {
                Image = image,
                SizeMode = PictureBoxSizeMode.Zoom,
                Dock = DockStyle.Top,
                Height = 300,
                BackColor = Color.Transparent
            };

            this.statusLabel = new Label
            {
                Text = "起動準備中...",
                ForeColor = Color.LightGray,
                Font = new Font("Segoe UI", 10),
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleLeft,
                Dock = DockStyle.Top,
                Height = 45,
                Padding = new Padding(30, 0, 30, 0)
            };

            this.progressBar = new ProgressBar
            {
                Style = ProgressBarStyle.Marquee,
                MarqueeAnimationSpeed = 25,
                Dock = DockStyle.Top,
                Height = 12
            };

            Controls.Add(this.progressBar);
            Controls.Add(this.statusLabel);
            Controls.Add(this.titlePictureBox);
        }

        /// <summary>
        /// スプラッシュスクリーンの状態表示を更新します。
        /// </summary>
        public void SetStatus(string message)
        {
            if (InvokeRequired)
            {
                BeginInvoke(() => SetStatus(message));
                return;
            }

            this.statusLabel.Text = message;
        }

        protected override async void OnShown(EventArgs e)
        {
            base.OnShown(e);
            // フォームが表示された後に、初期化処理を開始する
            try
            {
                await AppInitializer.InitializeAsync(SetStatus);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    this,
                    $"初期化に失敗しました。\n{ex.Message}",
                    "エラー",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                DialogResult = DialogResult.Abort;
                Close();
            }
        }
    }
}
