namespace Rydia.ResourceConverter
{
    partial class TTFFontConverter
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new GroupBox();
            this.label2 = new Label();
            this.btnOK = new Button();
            this.chkItalic = new CheckBox();
            this.txtPreview = new TextBox();
            this.listBox1 = new ListBox();
            this.chkBold = new CheckBox();
            this.nudFontSize = new NumericUpDown();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)this.nudFontSize).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btnOK);
            this.groupBox1.Controls.Add(this.chkItalic);
            this.groupBox1.Controls.Add(this.txtPreview);
            this.groupBox1.Controls.Add(this.listBox1);
            this.groupBox1.Controls.Add(this.chkBold);
            this.groupBox1.Controls.Add(this.nudFontSize);
            this.groupBox1.Location = new Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(851, 305);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "ttfフォント";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new Point(6, 19);
            this.label2.Name = "label2";
            this.label2.Size = new Size(253, 30);
            this.label2.TabIndex = 20;
            this.label2.Text = "フォントを選択し、サイズとオプションを選択してください\r\n変換ボタンでリソースファイルが作成されます";
            // 
            // btnOK
            // 
            this.btnOK.Location = new Point(242, 212);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(196, 84);
            this.btnOK.TabIndex = 11;
            this.btnOK.Text = "変換";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += btnOK_Click;
            // 
            // chkItalic
            // 
            this.chkItalic.AutoSize = true;
            this.chkItalic.Location = new Point(368, 53);
            this.chkItalic.Name = "chkItalic";
            this.chkItalic.Size = new Size(50, 19);
            this.chkItalic.TabIndex = 9;
            this.chkItalic.Text = "斜体";
            this.chkItalic.UseVisualStyleBackColor = true;
            // 
            // txtPreview
            // 
            this.txtPreview.Location = new Point(242, 81);
            this.txtPreview.Multiline = true;
            this.txtPreview.Name = "txtPreview";
            this.txtPreview.Size = new Size(600, 125);
            this.txtPreview.TabIndex = 10;
            // 
            // listBox1
            // 
            this.listBox1.AllowDrop = true;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new Point(6, 52);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new Size(230, 244);
            this.listBox1.TabIndex = 13;
            // 
            // chkBold
            // 
            this.chkBold.AutoSize = true;
            this.chkBold.Location = new Point(452, 53);
            this.chkBold.Name = "chkBold";
            this.chkBold.Size = new Size(50, 19);
            this.chkBold.TabIndex = 8;
            this.chkBold.Text = "太字";
            this.chkBold.UseVisualStyleBackColor = true;
            // 
            // nudFontSize
            // 
            this.nudFontSize.Location = new Point(242, 52);
            this.nudFontSize.Maximum = new decimal(new int[] { 70, 0, 0, 0 });
            this.nudFontSize.Minimum = new decimal(new int[] { 8, 0, 0, 0 });
            this.nudFontSize.Name = "nudFontSize";
            this.nudFontSize.Size = new Size(120, 23);
            this.nudFontSize.TabIndex = 7;
            this.nudFontSize.Value = new decimal(new int[] { 70, 0, 0, 0 });
            // 
            // TTFFontConverter
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(874, 323);
            Controls.Add(this.groupBox1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "TTFFontConverter";
            Text = "TTFFontConverter";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)this.nudFontSize).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Label label2;
        private Button btnOK;
        private CheckBox chkItalic;
        private TextBox txtPreview;
        private ListBox listBox1;
        private CheckBox chkBold;
        private NumericUpDown nudFontSize;
    }
}