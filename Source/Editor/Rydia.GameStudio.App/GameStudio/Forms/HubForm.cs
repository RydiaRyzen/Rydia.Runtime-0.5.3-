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

    /// <summary>
    /// Rydia GameStudio のプロジェクト管理画面を提供します。
    /// </summary>
    /// <remarks>
    /// プロジェクトの一覧表示、新規プロジェクトの作成、既存プロジェクトの登録および
    /// 各種管理画面への切り替えを行います。
    /// </remarks>
    public partial class HubForm : Form
    {

        /// <summary>
        /// メイン領域の背景色を取得します。
        /// </summary>
        private readonly Color backgroundColor =
            Color.FromArgb(24, 26, 32);

        /// <summary>
        /// パネルおよびプロジェクト一覧の背景色を取得します。
        /// </summary>
        private readonly Color panelColor =
            Color.FromArgb(38, 41, 49);

        /// <summary>
        /// 選択状態や主要な操作ボタンに使用するアクセントカラーを取得します。
        /// </summary>
        private readonly Color accentColor =
            Color.FromArgb(0, 160, 240);

        /// <summary>
        /// 登録されたプロジェクトの一覧を表示するリストビューを取得または設定します。
        /// </summary>
        private ListView projectList;

        /// <summary>
        /// <see cref="HubForm"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <remarks>
        /// フォームの基本設定、サイドバーの配色、アセンブリのバージョン表示を設定し、
        /// プロジェクト管理画面を初期表示します。
        /// </remarks>
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

        /// <summary>
        /// 指定された文字列と背景色を使用して操作ボタンを生成します。
        /// </summary>
        /// <param name="text">ボタンに表示する文字列。</param>
        /// <param name="color">ボタンの背景色。</param>
        /// <returns>指定された設定を適用したボタン。</returns>
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

        /// <summary>
        /// 新規プロジェクトの作成画面を表示します。
        /// </summary>
        /// <remarks>
        /// 作成画面が <see cref="DialogResult.OK"/> を返した場合、
        /// 本フォームを閉じ、作成処理の完了を呼び出し元に通知します。
        /// </remarks>
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

        /// <summary>
        /// フォルダー選択ダイアログを表示し、既存のプロジェクトフォルダーを登録します。
        /// </summary>
        /// <remarks>
        /// 初期ディレクトリには、エディターのアセンブリが配置されたディレクトリ内の
        /// Projects フォルダーを指定します。
        /// </remarks>
        private void OpenProject()
        {
            using var dialog = new FolderBrowserDialog
            {
                Description = "プロジェクトのフォルダーを選択してください。"
            };
            var dir = typeof(HubForm).Assembly.Location;
            dir = Path.GetDirectoryName(dir);
            dialog.InitialDirectory = Path.Combine(dir, "Projects");
            if (dialog.ShowDialog(this) != DialogResult.OK)
                return;

            AddProject(dialog.SelectedPath);
        }

        /// <summary>
        /// 指定されたフォルダーをプロジェクト一覧に登録します。
        /// </summary>
        /// <param name="path">登録するプロジェクトフォルダーのパス。</param>
        /// <remarks>
        /// フォルダーが存在しない場合は登録しません。
        /// 同じパスがすでに登録されている場合は、新しい項目を追加せず、
        /// 該当する項目を選択して表示します。
        /// </remarks>
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

            // TODO: プロジェクトの状態を判定する処理を追加する

            // プロジェクトをリストに追加する
            var item = new ListViewItem(name);
            item.SubItems.Add(path);
            item.SubItems.Add("登録済み");

            item.Tag = path;
            this.projectList.Items.Add(item);
        }

        /// <summary>
        /// 一覧で選択されているプロジェクトを開きます。
        /// </summary>
        /// <remarks>
        /// 選択項目が存在しない場合や、プロジェクトフォルダーが存在しない場合は
        /// 処理を終了します。
        /// 現在は選択されたプロジェクトのパスをメッセージボックスに表示します。
        /// </remarks>
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

        /// <summary>
        /// プロジェクト管理画面を構築して表示します。
        /// </summary>
        /// <param name="sender">イベントの発生元。</param>
        /// <param name="e">イベントデータ。</param>
        /// <remarks>
        /// メイン領域にヘッダー、操作ボタン、新規プロジェクトの作成ボタン、
        /// プロジェクトフォルダーの選択ボタン、およびプロジェクト一覧を配置します。
        /// </remarks>
        private void projectButton_Click(object sender, EventArgs e)
        {
            this.contentPanel.SuspendLayout();
            this.contentPanel.Controls.Clear();

            // メイン領域を3行に分割
            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 3,
                BackColor = this.backgroundColor,
                Padding = new Padding(32, 24, 32, 24),
                Margin = Padding.Empty
            };
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

            layout.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 100));

            layout.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 65));

            layout.RowStyles.Add(
                new RowStyle(SizeType.Percent, 100));
            // ヘッダー
            var header = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = this.backgroundColor
            };

            var heading = new Label
            {
                Text = "プロジェクト",
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                ForeColor = Color.White,
                Dock = DockStyle.Top,
                Height = 45
            };

            var description = new Label
            {
                Text = "Rydia Engine プロジェクトを管理します。",
                ForeColor = Color.Silver,
                Dock = DockStyle.Top,
                Height = 30
            };

            header.Controls.Add(description);
            header.Controls.Add(heading);

            // 操作ボタン
            var toolbar = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = this.backgroundColor
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
                Font = new Font("Segoe UI", 10),
                HideSelection = false,
                MultiSelect = false
            };

            this.projectList.Columns.Add("プロジェクト名", 230);
            this.projectList.Columns.Add("保存場所", 450);
            this.projectList.Columns.Add("状態", 130);

            this.projectList.DoubleClick += (_, _) =>
                OpenSelectedProject();

            layout.Controls.Add(header, 0, 0);
            layout.Controls.Add(toolbar, 0, 1);
            layout.Controls.Add(this.projectList, 0, 2);

            this.contentPanel.Controls.Add(layout);

            this.contentPanel.ResumeLayout(true);
        }

        /// <summary>
        /// インストール管理画面への切り替えを処理します。
        /// </summary>
        /// <param name="sender">イベントの発生元。</param>
        /// <param name="e">イベントデータ。</param>
        /// <remarks>
        /// 現在はメイン領域をクリアし、インストール画面が未実装であることを表示します。
        /// </remarks>
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

        /// <summary>
        /// 学習画面への切り替えを処理します。
        /// </summary>
        /// <param name="sender">イベントの発生元。</param>
        /// <param name="e">イベントデータ。</param>
        /// <remarks>
        /// 現在はメイン領域をクリアし、学習画面が未実装であることを表示します。
        /// </remarks>
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

        /// <summary>
        /// コミュニティ画面への切り替えを処理します。
        /// </summary>
        /// <param name="sender">イベントの発生元。</param>
        /// <param name="e">イベントデータ。</param>
        /// <remarks>
        /// 現在はメイン領域をクリアし、コミュニティ画面が未実装であることを表示します。
        /// </remarks>
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
