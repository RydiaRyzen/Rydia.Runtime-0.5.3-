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
    /// プロジェクト生成処理のログを表示するフォームを表します。
    /// </summary>
    public partial class ProjectGenerationLogForm : Form
    {

        private readonly TextBox logTextBox;
        private readonly Label statusLabel;
        private readonly Button closeButton;

        /// <summary>
        /// プロジェクト生成ログ表示フォームを初期化します。
        /// </summary>
        public ProjectGenerationLogForm()
        {
            InitializeComponent();
            Text = "プロジェクト生成";
            ClientSize = new Size(760, 480);
            MinimumSize = new Size(560, 320);
            BackColor = Color.FromArgb(30, 32, 39);
            ForeColor = Color.Gainsboro;
            Font = new Font("Segoe UI", 9F);

            var root = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(16),
                ColumnCount = 1,
                RowCount = 3,
                BackColor = BackColor
            };

            root.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 32));
            root.RowStyles.Add(
                new RowStyle(SizeType.Percent, 100));
            root.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 44));

            this.statusLabel = new Label
            {
                Text = "プロジェクトを準備しています...",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                ForeColor = Color.Gainsboro
            };

            this.logTextBox = new TextBox
            {
                Dock = DockStyle.Fill,
                Multiline = true,
                ReadOnly = true,
                WordWrap = false,
                ScrollBars = ScrollBars.Both,
                BackColor = Color.FromArgb(20, 22, 27),
                ForeColor = Color.Gainsboro,
                BorderStyle = BorderStyle.FixedSingle,
                Font = new Font("Consolas", 9F)
            };

            var buttonPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.RightToLeft,
                WrapContents = false,
                Padding = new Padding(0, 8, 0, 0),
                BackColor = BackColor
            };

            this.closeButton = new Button
            {
                Text = "閉じる",
                Width = 100,
                Height = 30,
                Enabled = false
            };

            this.closeButton.Click += (_, _) => Close();
            buttonPanel.Controls.Add(this.closeButton);

            root.Controls.Add(this.statusLabel, 0, 0);
            root.Controls.Add(this.logTextBox, 0, 1);
            root.Controls.Add(buttonPanel, 0, 2);

            Controls.Add(root);
        }

        public void AppendTitle(string title)
        {
            if (IsDisposed || Disposing)
            {
                return;
            }
            if (InvokeRequired)
            {
                try
                {
                    BeginInvoke(new Action(() => AppendTitle(title)));
                }
                catch (InvalidOperationException)
                {
                    // フォームが閉じられた場合はログを破棄します。
                }
                return;
            }
            Text = title;
        }

        /// <summary>
        /// ログを追加して表示します。
        /// </summary>
        /// <param name="message">表示するログメッセージ。</param>
        public void AppendLog(string message)
        {
            if (IsDisposed || Disposing)
            {
                return;
            }

            if (InvokeRequired)
            {
                try
                {
                    BeginInvoke(new Action(() => AppendLog(message)));
                }
                catch (InvalidOperationException)
                {
                    // フォームが閉じられた場合はログを破棄します。
                }

                return;
            }

            this.logTextBox.AppendText(
                $"[{DateTime.Now:HH:mm:ss}] {message}{Environment.NewLine}");
        }

        /// <summary>
        /// 表示中の処理状態を更新します。
        /// </summary>
        /// <param name="message">表示する状態。</param>
        public void SetStatus(string message)
        {
            if (IsDisposed || Disposing)
            {
                return;
            }

            if (InvokeRequired)
            {
                try
                {
                    BeginInvoke(new Action(() => SetStatus(message)));
                }
                catch (InvalidOperationException)
                {
                    // フォームが閉じられた場合は更新しません。
                }

                return;
            }

            this.statusLabel.Text = message;
        }

        /// <summary>
        /// 処理の終了を通知し、フォームを閉じられる状態にします。
        /// </summary>
        /// <param name="success">処理が成功した場合は true。</param>
        public void Complete(bool success)
        {
            if (IsDisposed || Disposing)
            {
                return;
            }

            if (InvokeRequired)
            {
                try
                {
                    BeginInvoke(new Action(() => Complete(success)));
                }
                catch (InvalidOperationException)
                {
                    // フォームが閉じられた場合は更新しません。
                }

                return;
            }

            this.statusLabel.Text = success
                ? "プロジェクトの生成が完了しました。"
                : "プロジェクトの生成に失敗しました。";

            this.closeButton.Enabled = true;
            this.closeButton.Focus();
        }

    }
}
