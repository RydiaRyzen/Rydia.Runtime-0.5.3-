using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rydia.GameStudio
{
    public partial class HubForm : Form
    {

        private readonly Color backgroundColor =
            Color.FromArgb(24, 26, 32);

        private readonly Color sidebarColor =
            Color.FromArgb(30, 32, 39);

        private readonly Color panelColor =
            Color.FromArgb(38, 41, 49);

        private readonly Color accentColor =
            Color.FromArgb(0, 160, 240);

        private ListView projectList;

        public HubForm()
        {
            InitializeComponent();

            Text = "Rydia GameStudio";
            ClientSize = new Size(1200, 760);
            MinimumSize = new Size(900, 600);
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = this.backgroundColor;
            ForeColor = Color.White;
            Font = new System.Drawing.Font("Segoe UI", 10);

            // 左側のナビゲーション
            var embedded = new Rydia.IO.EmbeddedResources<SplashForm>();
            var stream = embedded.Open("Rydia.GameStudio.EmbeddedResources.SplashLogo.png");
            var image = System.Drawing.Image.FromStream(stream);
            var bitmap = new Bitmap(image);
            this.sidebar.BackColor = bitmap.GetPixel(0, 0);

            this.projectButton.FlatAppearance.BorderSize = 0;
            this.installButton.FlatAppearance.BorderSize = 0;
            this.learnButton.FlatAppearance.BorderSize = 0;
            this.communityButton.FlatAppearance.BorderSize = 0;

            var engineAssembly = typeof(Rydia.Engine.LauncherApp).Assembly;
            var editorAssembly = typeof(HubForm).Assembly;

            this.versionLabel2.Text = $"{"Rydia Engine".PadRight(20)}{engineAssembly.GetName().Version}";
            this.versionLabel1.Text = $"{"Rydia GameStudio".PadRight(15)}{editorAssembly.GetName().Version}";

            // メイン領域
            this.contentPanel.BackColor = this.backgroundColor;

            projectButton_Click(this, EventArgs.Empty);
        }

        private Button CreateActionButton(
            string text, Color color)
        {
            var button = new System.Windows.Forms.Button
            {
                Text = text,
                BackColor = color,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new System.Drawing.Font("Segoe UI", 10),
                Cursor = Cursors.Hand
            };

            button.FlatAppearance.BorderSize = 0;

            return button;
        }

        private void CreateProject()
        {
            // TODO:
            // プロジェクト名、テンプレート、保存先を指定する
            // NewProjectForm を実装してここから起動する。
            var form = new NewProjectForm();

            var dResult = form.ShowDialog(this);
            if(dResult == DialogResult.OK)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void OpenProject()
        {
            using var dialog = new FolderBrowserDialog
            {
                Description = "プロジェクトのフォルダーを選択してください。"
            };

            if (dialog.ShowDialog(this) != DialogResult.OK)
                return;

            AddProject(dialog.SelectedPath);
        }

        private void AddProject(string path)
        {
            if (!Directory.Exists(path))
                return;

            string name = Path.GetFileName(
                path.TrimEnd(
                    Path.DirectorySeparatorChar,
                    Path.AltDirectorySeparatorChar));

            if (string.IsNullOrEmpty(name))
                name = path;

            foreach (ListViewItem existing in this.projectList.Items)
            {
                if (string.Equals(
                    existing.SubItems[1].Text,
                    path,
                    StringComparison.OrdinalIgnoreCase))
                {
                    existing.Selected = true;
                    existing.Focused = true;
                    existing.EnsureVisible();
                    return;
                }
            }

            var item = new ListViewItem(name);
            item.SubItems.Add(path);
            item.SubItems.Add("登録済み");

            item.Tag = path;
            this.projectList.Items.Add(item);
        }

        private void OpenSelectedProject()
        {
            if (this.projectList.SelectedItems.Count == 0)
                return;

            string path =
                this.projectList.SelectedItems[0].SubItems[1].Text;

            if (!Directory.Exists(path))
            {
                MessageBox.Show(
                    this,
                    "プロジェクトのフォルダーが存在しません。",
                    "エラー",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            // TODO:
            // Rydia のプロジェクトファイルを読み込み、
            // エディター本体を起動する。
            MessageBox.Show(
                this,
                $"プロジェクトを選択しました。\n{path}",
                "Rydia GameStudio",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void projectButton_Click(object sender, EventArgs e)
        {
            this.contentPanel.Controls.Clear();
            // ヘッダー
            var header = new Panel
            {
                Dock = DockStyle.Top,
                Height = 100,
                Location = new Point(250, 0)
            };

            var heading = new Label
            {
                Text = "プロジェクト",
                Font = new System.Drawing.Font("Segoe UI", 24, FontStyle.Bold),
                ForeColor = Color.White,
                Dock = DockStyle.Top,
                Height = 45
            };

            var description = new Label
            {
                Text = "Rydia Engine プロジェクトを管理します。",
                ForeColor = Color.Silver,
                Dock = DockStyle.Top,
                Height = 30,
                Location = new Point(100, 50)
            };

            header.Controls.Add(description);
            header.Controls.Add(heading);
            this.contentPanel.Controls.Add(header);

            // 操作ボタン
            var toolbar = new Panel
            {
                Dock = DockStyle.Top,
                Height = 65
            };

            var newButton = CreateActionButton(
                "+  新規プロジェクト",
                this.accentColor);

            newButton.Location = new Point(0, 10);
            newButton.Size = new Size(190, 42);
            newButton.Click += (_, _) => CreateProject();

            var openButton = CreateActionButton(
                "プロジェクトフォルダーを開く",
                this.panelColor);

            openButton.Location = new Point(200, 10);
            openButton.Size = new Size(190, 42);
            openButton.Click += (_, _) => OpenProject();

            toolbar.Controls.Add(newButton);
            toolbar.Controls.Add(openButton);
            this.contentPanel.Controls.Add(toolbar);

            // プロジェクト一覧
            this.projectList = new System.Windows.Forms.ListView
            {
                Dock = DockStyle.Fill,
                View = View.Details,
                FullRowSelect = true,
                GridLines = false,
                BorderStyle = BorderStyle.None,
                BackColor = this.panelColor,
                ForeColor = Color.White,
                Font = new System.Drawing.Font("Segoe UI", 10),
                HideSelection = false
            };

            this.projectList.Columns.Add("プロジェクト名", 230);
            this.projectList.Columns.Add("保存場所", 450);
            this.projectList.Columns.Add("状態", 130);

            this.projectList.DoubleClick += (_, _) =>
                OpenSelectedProject();

            this.contentPanel.Controls.Add(this.projectList);

            // Dock の順序を整理
            this.contentPanel.Controls.SetChildIndex(header, 0);
            this.contentPanel.Controls.SetChildIndex(toolbar, 1);
            this.contentPanel.Controls.SetChildIndex(this.projectList, 2);
        }

        private void installButton_Click(object sender, EventArgs e)
        {
            // TODO: インストール画面を表示する
            this.contentPanel.Controls.Clear();
            MessageBox.Show(
                this,
                "ここにインストール画面を接続します。",
                "Rydia GameStudio",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void learnButton_Click(object sender, EventArgs e)
        {
            // TODO: 学習画面を表示する
            this.contentPanel.Controls.Clear();
            MessageBox.Show(
                this,
                "ここに学習画面を接続します。",
                "Rydia GameStudio",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void communityButton_Click(object sender, EventArgs e)
        {
            // TODO: コミュニティ画面を表示する
            this.contentPanel.Controls.Clear();
            MessageBox.Show(
                this,
                "ここにコミュニティ画面を接続します。",
                "Rydia GameStudio",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

    }
}
