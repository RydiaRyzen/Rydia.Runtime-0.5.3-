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
    /// 新しいプロジェクトを作成するためのフォームを表します。
    /// </summary>
    public partial class NewProjectForm : Form
    {

        private TextBox projectNameTextBox;
        private ComboBox templateComboBox;
        private Button createButton;
        private Button cancelButton;

        /// <summary>
        /// 作成するプロジェクトの名前を取得します。
        /// </summary>
        public string ProjectName
        {
            get
            {
                return this.projectNameTextBox.Text.Trim();
            }
        }

        /// <summary>
        /// プロジェクトの保存先ディレクトリを取得します。
        /// </summary>
        public string ProjectLocation
        {
            get
            {
                return Path.Combine(Environment.CurrentDirectory, "Projects");
            }
        }

        /// <summary>
        /// 選択されたプロジェクトテンプレートを取得します。
        /// </summary>
        public string SelectedTemplate
        {
            get
            {
                return this.templateComboBox.SelectedItem?.ToString() ?? string.Empty;
            }
        }

        /// <summary>
        /// 新しいプロジェクト作成フォームを初期化します。
        /// </summary>
        public NewProjectForm()
        {
            InitializeComponent();

            var root = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 4,
                Padding = new Padding(24),
                BackColor = BackColor
            };

            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 60));
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 100));
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 52));

            var heading = new Label
            {
                Text = "新しいプロジェクト",
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleLeft
            };

            var templatePanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 2,
                BackColor = BackColor
            };
            templatePanel.ColumnStyles.Add(
                new ColumnStyle(SizeType.Absolute, 120));
            templatePanel.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 100));
            templatePanel.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 32));
            templatePanel.RowStyles.Add(
                new RowStyle(SizeType.Percent, 100));

            var templateLabel = CreateLabel("テンプレート");

            this.templateComboBox = new ComboBox
            {
                Dock = DockStyle.Fill,
                DropDownStyle = ComboBoxStyle.DropDownList,
                BackColor = Color.FromArgb(55, 55, 58),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };

            this.templateComboBox.Items.AddRange(new object[]
            {
            "3D プロジェクト",
            "2D プロジェクト",
            "空のプロジェクト"
            });
            this.templateComboBox.SelectedIndex = 0;

            templatePanel.Controls.Add(templateLabel, 0, 0);
            templatePanel.SetColumnSpan(templateLabel, 2);
            templatePanel.Controls.Add(this.templateComboBox, 0, 1);
            templatePanel.SetColumnSpan(this.templateComboBox, 2);

            var settingsPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 4,
                BackColor = BackColor
            };
            settingsPanel.ColumnStyles.Add(
                new ColumnStyle(SizeType.Absolute, 120));
            settingsPanel.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 100));

            settingsPanel.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 32));
            settingsPanel.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 40));
            settingsPanel.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 32));
            settingsPanel.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 40));

            var nameLabel = CreateLabel("プロジェクト名");

            this.projectNameTextBox = new TextBox
            {
                Text = "MyProject",
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(55, 55, 58),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };
            settingsPanel.Controls.Add(nameLabel, 0, 0);
            settingsPanel.Controls.Add(this.projectNameTextBox, 0, 1);
            settingsPanel.SetColumnSpan(this.projectNameTextBox, 2);
            var buttonPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.RightToLeft,
                WrapContents = false,
                BackColor = BackColor,
                Padding = new Padding(0, 8, 0, 0)
            };

            this.createButton = new Button
            {
                Text = "作成",
                Width = 100,
                Height = 32,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(45, 110, 190),
                ForeColor = Color.White,
                DialogResult = DialogResult.None
            };
            this.createButton.FlatAppearance.BorderSize = 0;
            this.createButton.Click += createButton_Click;

            this.cancelButton = new Button
            {
                Text = "キャンセル",
                Width = 100,
                Height = 32,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(63, 63, 65),
                ForeColor = Color.White,
                DialogResult = DialogResult.Cancel
            };
            this.cancelButton.FlatAppearance.BorderSize = 0;
            this.cancelButton.Click += (_, _) => Close();

            buttonPanel.Controls.Add(this.createButton);
            buttonPanel.Controls.Add(this.cancelButton);

            root.Controls.Add(heading, 0, 0);
            root.Controls.Add(templatePanel, 0, 1);
            root.Controls.Add(settingsPanel, 0, 2);
            root.Controls.Add(buttonPanel, 0, 3);

            Controls.Add(root);

            AcceptButton = this.createButton;
            CancelButton = this.cancelButton;

        }

        /// <summary>
        /// 指定された文字列を表示するラベルを生成します。
        /// </summary>
        /// <param name="text">ラベルに表示する文字列。</param>
        /// <returns>生成されたラベル。</returns>
        private static Label CreateLabel(string text)
        {
            return new Label
            {
                Text = text,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                ForeColor = Color.Gainsboro
            };
        }

        /// <summary>
        /// 入力内容を検証し、新しいプロジェクトの作成を確定します。
        /// </summary>
        private void createButton_Click(object sender, EventArgs e)
        {
            string name = ProjectName;
            string location = ProjectLocation;

            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show(
                    this,
                    "プロジェクト名を入力してください。",
                    "入力エラー",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                this.projectNameTextBox.Focus();
                return;
            }

            if (name.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0 ||
                name == "." || name == "..")
            {
                MessageBox.Show(
                    this,
                    "プロジェクト名に使用できない文字が含まれています。",
                    "入力エラー",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                this.projectNameTextBox.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(location))
            {
                MessageBox.Show(
                    this,
                    "保存場所を指定してください。",
                    "入力エラー",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                //this.locationTextBox.Focus();
                return;
            }

            try
            {
                // 保存先として指定されたパスが有効か確認します。
                _ = Path.GetFullPath(location);
            }
            catch (Exception ex) when (
                ex is ArgumentException ||
                ex is NotSupportedException ||
                ex is PathTooLongException)
            {
                MessageBox.Show(
                    this,
                    "保存場所が正しくありません。",
                    "入力エラー",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                //this.locationTextBox.Focus();
                return;
            }

            // TODO : プロジェクト作成

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
