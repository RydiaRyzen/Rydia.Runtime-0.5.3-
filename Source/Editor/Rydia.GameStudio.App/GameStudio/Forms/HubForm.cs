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

            Text = "Rydia Engine Hub";
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
            // TODO : プロジェクトのバージョン情報を取得して表示する処理を追加する
            // 現時点ではバージョン情報の保存方法が無いため、登録済みとする
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
            
            /*
             * Projectフォルダー内のプロジェクトを一覧表示するため、ボタンをコメントアウトする
            var openButton = CreateActionButton(
                "プロジェクトフォルダーを開く",
                this.panelColor);

            openButton.Location = new Point(200, 10);
            openButton.Size = new Size(190, 42);
            openButton.Click += (_, _) => OpenProject();
            */
            //toolbar.Controls.Add(openButton);
            toolbar.Controls.Add(newButton);

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

            // TODO: プロジェクトフォルダー内のプロジェクトを自動的に読み込む処理を追加する
            var projectsFolder = Path.Combine(Environment.CurrentDirectory, "Projects");
            foreach(var dir in Directory.GetDirectories(projectsFolder))
            {
                AddProject(dir);
            }

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
            this.contentPanel.SuspendLayout();
            this.contentPanel.Controls.Clear();

            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 4,
                BackColor = this.backgroundColor,
                Padding = new Padding(24)
            };

            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 75));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 55));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 90));

            var header = new Label
            {
                Text = "ランタイム管理",
                Dock = DockStyle.Fill,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleLeft
            };

            var description = new Label
            {
                Text = "Rydia Engine のランタイムを確認し、最新の状態に更新します。",
                Dock = DockStyle.Fill,
                ForeColor = Color.LightGray,
                Font = new Font("Segoe UI", 10),
                TextAlign = ContentAlignment.MiddleLeft
            };

            var runtimeList = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                BackColor = this.backgroundColor,
                Padding = new Padding(0, 8, 8, 8)
            };

            AddRuntimeItem(
                runtimeList,
                "Rydia Engine Runtime",
                "エンジンの基本実行環境",
                "状態未確認");

            var bottomPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = this.backgroundColor
            };

            var statusLabel = new Label
            {
                Text = "ランタイムの状態を確認してください。",
                ForeColor = Color.LightGray,
                Font = new Font("Segoe UI", 9),
                AutoSize = true,
                Location = new Point(0, 10)
            };

            var updateButton = CreateActionButton(
                "ランタイムを更新",
                this.accentColor);

            updateButton.Size = new Size(190, 42);
            updateButton.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            updateButton.Location = new Point(
                Math.Max(0, bottomPanel.ClientSize.Width - 190),
                25);

            bottomPanel.Resize += (_, _) =>
            {
                updateButton.Location = new Point(
                    Math.Max(0, bottomPanel.ClientSize.Width
                        - updateButton.Width),
                    25);
            };

            updateButton.Click += (_, _) =>
            {
                statusLabel.Text =
                    "更新サービスが未接続です。";

                MessageBox.Show(
                    "ランタイムの更新処理は、更新サービスの実装後に利用できます。",
                    "ランタイム管理",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            };

            bottomPanel.Controls.Add(statusLabel);
            bottomPanel.Controls.Add(updateButton);

            layout.Controls.Add(header, 0, 0);
            layout.Controls.Add(description, 0, 1);
            layout.Controls.Add(runtimeList, 0, 2);
            layout.Controls.Add(bottomPanel, 0, 3);

            this.contentPanel.Controls.Add(layout);
            this.contentPanel.ResumeLayout(true);
        }

        private void AddRuntimeItem(FlowLayoutPanel parent, string name, string description, string status)
        {
            var item = new Panel
            {
                Width = Math.Max(300, parent.ClientSize.Width - 30),
                Height = 88,
                BackColor = this.panelColor,
                Margin = new Padding(0, 0, 0, 12),
                Padding = new Padding(16)
            };

            var nameLabel = new Label
            {
                Text = name,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Location = new Point(16, 12),
                AutoSize = true
            };

            var descriptionLabel = new Label
            {
                Text = description,
                ForeColor = Color.LightGray,
                Font = new Font("Segoe UI", 9),
                Location = new Point(16, 43),
                AutoSize = true
            };

            var statusLabel = new Label
            {
                Text = status,
                ForeColor = Color.Silver,
                Font = new Font("Segoe UI", 9),
                AutoSize = true,
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };

            item.Controls.Add(nameLabel);
            item.Controls.Add(descriptionLabel);
            item.Controls.Add(statusLabel);

            item.Resize += (_, _) =>
            {
                statusLabel.Location = new Point(
                    Math.Max(16, item.ClientSize.Width
                        - statusLabel.Width - 16),
                    16);
            };

            statusLabel.Location = new Point(
                Math.Max(16, item.ClientSize.Width
                    - statusLabel.Width - 16),
                16);

            parent.Controls.Add(item);
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
            this.contentPanel.SuspendLayout();
            this.contentPanel.Controls.Clear();

            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 3,
                BackColor = this.backgroundColor,
                Padding = new Padding(32, 24, 32, 24)
            };

            layout.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 100));
            layout.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 100));
            layout.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 60));
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
                Text = "学習",
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                ForeColor = Color.White,
                Dock = DockStyle.Top,
                Height = 45
            };

            var description = new Label
            {
                Text = "Rydia Engine の機能や使い方を学習します。",
                ForeColor = Color.Silver,
                Dock = DockStyle.Top,
                Height = 30
            };

            header.Controls.Add(description);
            header.Controls.Add(heading);

            // カテゴリ選択
            var categoryPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                AutoScroll = true,
                BackColor = this.backgroundColor,
                Padding = new Padding(0, 8, 0, 8)
            };

            var detailTitle = new Label
            {
                Text = "学習項目を選択してください",
                Dock = DockStyle.Top,
                Height = 36,
                Font = new Font("Segoe UI", 15, FontStyle.Bold),
                ForeColor = Color.White
            };

            var detailDescription = new Label
            {
                Text = "左側のカテゴリから学習したい項目を選択してください。",
                Dock = DockStyle.Fill,
                ForeColor = Color.Silver,
                Font = new Font("Segoe UI", 10),
                Padding = new Padding(0, 12, 0, 0),
                TextAlign = ContentAlignment.TopLeft
            };

            var detailPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = this.panelColor,
                Padding = new Padding(24)
            };

            detailPanel.Controls.Add(detailDescription);
            detailPanel.Controls.Add(detailTitle);

            // 学習コンテンツ
            var courses = new[]
            {
        new
        {
            Category = "入門",
            Title = "Rydia Engine の概要",
            Description =
                "Rydia Engine の基本構成や、GameStudio の役割を学びます。\n\n" +
                "学習内容\n" +
                "・エンジンとエディターの役割\n" +
                "・プロジェクトの構成\n" +
                "・アプリケーションの起動から終了まで"
        },
        new
        {
            Category = "入門",
            Title = "開発環境の準備",
            Description =
                "C# による開発環境とプロジェクトの準備について学びます。\n\n" +
                "学習内容\n" +
                "・必要な開発ツール\n" +
                "・プロジェクトの作成\n" +
                "・ビルドと実行"
        },
        new
        {
            Category = "グラフィックス",
            Title = "描画処理の基礎",
            Description =
                "ゲーム画面の描画に必要な基本概念を学びます。\n\n" +
                "学習内容\n" +
                "・レンダリングの基本\n" +
                "・頂点バッファとインデックスバッファ\n" +
                "・描画コマンドの流れ"
        },
        new
        {
            Category = "グラフィックス",
            Title = "シェーダー",
            Description =
                "GPU 上で実行されるシェーダーの基本を学びます。\n\n" +
                "学習内容\n" +
                "・頂点シェーダー\n" +
                "・フラグメントシェーダー\n" +
                "・ユニフォーム変数"
        },
        new
        {
            Category = "プログラミング",
            Title = "ゲームループ",
            Description =
                "ゲームの更新処理と描画処理の基本を学びます。\n\n" +
                "学習内容\n" +
                "・Update と Render\n" +
                "・経過時間の管理\n" +
                "・フレームレート"
        },
        new
        {
            Category = "プログラミング",
            Title = "入力処理",
            Description =
                "キーボード、マウス、タッチ入力の扱い方を学びます。\n\n" +
                "学習内容\n" +
                "・キー入力の取得\n" +
                "・マウス操作\n" +
                "・タッチイベント"
        },
        new
        {
            Category = "リソース",
            Title = "テクスチャとフォント",
            Description =
                "画像やフォントなどのリソースの基本を学びます。\n\n" +
                "学習内容\n" +
                "・画像リソースの読み込み\n" +
                "・テクスチャの利用\n" +
                "・文字の描画"
        },
        new
        {
            Category = "リソース",
            Title = "オーディオ",
            Description =
                "ゲームで音声を再生するための基本を学びます。\n\n" +
                "学習内容\n" +
                "・オーディオデバイス\n" +
                "・音声データの読み込み\n" +
                "・再生と停止"
        }
    };

            var categories = new[]
            {
        "すべて",
        "入門",
        "グラフィックス",
        "プログラミング",
        "リソース"
    };

            string selectedCategory = "すべて";

            var courseList = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = true,
                BackColor = this.panelColor,
                Padding = new Padding(12)
            };

            void RefreshCourses()
            {
                courseList.SuspendLayout();
                courseList.Controls.Clear();

                foreach (var course in courses)
                {
                    if (selectedCategory != "すべて" &&
                        course.Category != selectedCategory)
                    {
                        continue;
                    }

                    var courseButton = new Button
                    {
                        Text = $"{course.Category}  |  {course.Title}",
                        Width = Math.Max(300, courseList.ClientSize.Width - 35),
                        Height = 48,
                        TextAlign = ContentAlignment.MiddleLeft,
                        FlatStyle = FlatStyle.Flat,
                        BackColor = this.backgroundColor,
                        ForeColor = Color.White,
                        Font = new Font("Segoe UI", 10),
                        Margin = new Padding(0, 0, 0, 8),
                        Cursor = Cursors.Hand
                    };

                    courseButton.FlatAppearance.BorderSize = 0;

                    courseButton.Click += (_, _) =>
                    {
                        detailTitle.Text = course.Title;
                        detailDescription.Text = course.Description;
                    };

                    courseList.Controls.Add(courseButton);
                }

                courseList.ResumeLayout(true);
            }

            foreach (string category in categories)
            {
                var categoryButton = CreateActionButton(
                    category,
                    category == selectedCategory
                        ? this.accentColor
                        : this.panelColor);

                categoryButton.Height = 36;
                categoryButton.Width =
                    category == "グラフィックス" ? 140 :
                    category == "プログラミング" ? 145 :
                    category == "すべて" ? 90 : 110;

                categoryButton.Margin = new Padding(0, 0, 8, 0);

                categoryButton.Click += (_, _) =>
                {
                    selectedCategory = category;

                    foreach (Control control in categoryPanel.Controls)
                    {
                        if (control is Button button)
                        {
                            button.BackColor =
                                button.Text == selectedCategory
                                    ? this.accentColor
                                    : this.panelColor;
                        }
                    }

                    RefreshCourses();
                };

                categoryPanel.Controls.Add(categoryButton);
            }
            // 一覧と詳細を左右に配置
            var content = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                BackColor = this.backgroundColor,
                Padding = new Padding(0, 12, 0, 0)
            };

            content.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 48));
            content.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 52));
            content.RowStyles.Add(
                new RowStyle(SizeType.Percent, 100));

            var leftPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = this.backgroundColor,
                Padding = new Padding(0, 0, 12, 0)
            };

            var listHeading = new Label
            {
                Text = "学習コース",
                Dock = DockStyle.Top,
                Height = 36,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 13, FontStyle.Bold)
            };

            leftPanel.Controls.Add(courseList);
            leftPanel.Controls.Add(listHeading);

            content.Controls.Add(leftPanel, 0, 0);
            content.Controls.Add(detailPanel, 1, 0);

            layout.Controls.Add(header, 0, 0);
            layout.Controls.Add(categoryPanel, 0, 1);
            layout.Controls.Add(content, 0, 2);

            this.contentPanel.Controls.Add(layout);

            RefreshCourses();

            this.contentPanel.ResumeLayout(true);
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
            this.contentPanel.SuspendLayout();
            this.contentPanel.Controls.Clear();

            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 3,
                BackColor = this.backgroundColor,
                Padding = new Padding(32, 24, 32, 24),
                Margin = Padding.Empty
            };

            layout.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 100));

            layout.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 100));

            layout.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 100));

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
                Text = "コミュニティ",
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                ForeColor = Color.White,
                Dock = DockStyle.Top,
                Height = 45
            };

            var description = new Label
            {
                Text = "Rydia Engine の情報を確認し、開発コミュニティに参加できます。",
                ForeColor = Color.Silver,
                Dock = DockStyle.Top,
                Height = 30
            };

            header.Controls.Add(description);
            header.Controls.Add(heading);

            // 概要
            var introduction = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = this.panelColor,
                Padding = new Padding(20)
            };

            var introductionTitle = new Label
            {
                Text = "Rydia GameStudio Community",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.White,
                Dock = DockStyle.Top,
                Height = 30
            };

            var introductionText = new Label
            {
                Text =
                    "Rydia Engine の開発情報やドキュメントを確認できます。\n" +
                    "各リンクの接続先は、公式 URL を設定してから使用してください。",
                ForeColor = Color.Silver,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft
            };

            introduction.Controls.Add(introductionText);
            introduction.Controls.Add(introductionTitle);

            // コミュニティリンク
            var links = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 2,
                BackColor = this.backgroundColor,
                Padding = new Padding(0, 20, 0, 0)
            };

            links.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 50));

            links.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 50));

            links.RowStyles.Add(
                new RowStyle(SizeType.Percent, 50));

            links.RowStyles.Add(
                new RowStyle(SizeType.Percent, 50));

            links.Controls.Add(
                CreateCommunityCard(
                    "公式サイト",
                    "Rydia Engine の公式サイトを開きます。",
                    string.Empty),
                0, 0);

            links.Controls.Add(
                CreateCommunityCard(
                    "GitHub",
                    "ソースコードやリポジトリを確認します。",
                    "https://github.com/RydiaRyzen/Rydia.Runtime-0.5.3-"),
                1, 0);

            links.Controls.Add(
                CreateCommunityCard(
                    "ドキュメント",
                    "エンジンの使用方法や API を確認します。",
                    string.Empty),
                0, 1);

            links.Controls.Add(
                CreateCommunityCard(
                    "ディスカッション",
                    "質問や機能提案、開発に関する議論を行います。",
                    string.Empty),
                1, 1);

            layout.Controls.Add(header, 0, 0);
            layout.Controls.Add(introduction, 0, 1);
            layout.Controls.Add(links, 0, 2);

            this.contentPanel.Controls.Add(layout);
            this.contentPanel.ResumeLayout(true);
        }

        /// <summary>
        /// コミュニティへのリンクを表示するカードを生成します。
        /// </summary>
        /// <param name="title">カードのタイトル。</param>
        /// <param name="description">カードの説明。</param>
        /// <param name="url">リンク先の URL。未設定の場合はボタンを無効にします。</param>
        /// <returns>生成されたコミュニティカード。</returns>
        private Panel CreateCommunityCard(
            string title,
            string description,
            string url)
        {
            var card = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = this.panelColor,
                Margin = new Padding(0, 0, 12, 12),
                Padding = new Padding(20)
            };

            var heading = new Label
            {
                Text = title,
                Dock = DockStyle.Top,
                Height = 32,
                Font = new Font("Segoe UI", 13, FontStyle.Bold),
                ForeColor = Color.White
            };

            var detail = new Label
            {
                Text = description,
                Dock = DockStyle.Fill,
                ForeColor = Color.Silver,
                TextAlign = ContentAlignment.MiddleLeft
            };

            var openButton = CreateActionButton(
                "開く",
                this.accentColor);

            openButton.Size = new Size(100, 34);
            openButton.Dock = DockStyle.Bottom;

            // URL が設定されていない場合はリンクを無効にします。
            openButton.Enabled = Uri.TryCreate(
                url,
                UriKind.Absolute,
                out var uri)
                && (uri.Scheme == Uri.UriSchemeHttp
                    || uri.Scheme == Uri.UriSchemeHttps);

            openButton.Click += (_, _) =>
            {
                if (Uri.TryCreate(url, UriKind.Absolute, out var target)
                    && (target.Scheme == Uri.UriSchemeHttp
                        || target.Scheme == Uri.UriSchemeHttps))
                {
                    try
                    {
                        System.Diagnostics.Process.Start(
                            new System.Diagnostics.ProcessStartInfo
                            {
                                FileName = target.AbsoluteUri,
                                UseShellExecute = true
                            });
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(
                            this,
                            $"リンクを開けませんでした。\n{ex.Message}",
                            "エラー",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
            };

            card.Controls.Add(detail);
            card.Controls.Add(openButton);
            card.Controls.Add(heading);

            return card;
        }

    }
}
