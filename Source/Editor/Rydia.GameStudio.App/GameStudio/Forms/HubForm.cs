using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Rydia.GameStudio.Forms;
using static System.Net.Mime.MediaTypeNames;

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

        private readonly ListView projectList;

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
            /*
            this.sidebar = new Panel
            {
                Dock = DockStyle.Left,
                Width = 220,
                BackColor = this.sidebarColor
            };
            */
            var embedded = new Rydia.IO.EmbeddedResources<SplashForm>();
            var stream = embedded.Open("Rydia.GameStudio.EmbeddedResources.SplashLogo.png");
            var image = System.Drawing.Image.FromStream(stream);
            var bitmap = new Bitmap(image);
            this.sidebar.BackColor = bitmap.GetPixel(0, 0);

            var logo = new PictureBox
            {
                Image = image,
                SizeMode = PictureBoxSizeMode.Zoom,
                Dock = DockStyle.Top,
                Height = 300,
                BackColor = Color.Transparent
            };
            this.sidebar.Controls.Add(logo);

            this.navigation.Controls.Add(CreateNavButton("▦   プロジェクト", true));

            this.navigation.Controls.Add(CreateNavButton("▣   インストール", false));

            this.navigation.Controls.Add(CreateNavButton("◇   学習", false));

            this.navigation.Controls.Add(CreateNavButton("◎   コミュニティ", false));

            this.sidebar.Controls.Add(this.navigation);

            var versionLabel = new Label
            {
                Text = "Rydia Engine",
                Dock = DockStyle.Bottom,
                Height = 45,
                ForeColor = Color.Gray,
                TextAlign = ContentAlignment.MiddleCenter
            };

            this.sidebar.Controls.Add(versionLabel);
            //Controls.Add(this.sidebar);

            // メイン領域
            var content = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = this.backgroundColor,
                Padding = new Padding(232, 24, 32, 24)
            };

            Controls.Add(content);

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
            content.Controls.Add(header);

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
                "開く",
                this.panelColor);

            openButton.Location = new Point(200, 10);
            openButton.Size = new Size(100, 42);
            openButton.Click += (_, _) => OpenProject();

            toolbar.Controls.Add(newButton);
            toolbar.Controls.Add(openButton);
            content.Controls.Add(toolbar);

            // プロジェクト一覧
            this.projectList = new ListView
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

            content.Controls.Add(this.projectList);

            // Dock の順序を整理
            content.Controls.SetChildIndex(header, 0);
            content.Controls.SetChildIndex(toolbar, 1);
            content.Controls.SetChildIndex(this.projectList, 2);
        }

        private Button CreateNavButton(
            string text, bool selected)
        {
            var button = new Button
            {
                Text = text,
                Width = 190,
                Height = 46,
                FlatStyle = FlatStyle.Flat,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(12, 0, 0, 0),
                BackColor = selected
                    ? Color.FromArgb(48, 57, 72)
                    : this.sidebarColor,
                ForeColor = selected
                    ? Color.FromArgb(60, 180, 255)
                    : Color.Gainsboro,
                Cursor = Cursors.Hand,
                Margin = new Padding(0, 0, 0, 5)
            };

            button.FlatAppearance.BorderSize = 0;

            return button;
        }

        private Button CreateActionButton(
            string text, Color color)
        {
            var button = new Button
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
            
            DialogResult = MessageBox.Show(
                this,
                "ここに新規プロジェクト作成画面を接続します。",
                "Rydia GameStudio",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            Close();
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

    }
}
