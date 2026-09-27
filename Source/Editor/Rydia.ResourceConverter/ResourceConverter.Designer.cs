namespace Rydia.ResourceConverter
{
    partial class ResourceConverter
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
            this.groupBox2 = new GroupBox();
            this.progressBar = new ProgressBar();
            this.btnConvert = new Button();
            this.label1 = new Label();
            this.listBoxFiles = new ListBox();
            this.groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.progressBar);
            this.groupBox2.Controls.Add(this.btnConvert);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.listBoxFiles);
            this.groupBox2.Location = new Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(469, 305);
            this.groupBox2.TabIndex = 20;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Mediaフォルダ";
            // 
            // progressBar
            // 
            this.progressBar.Location = new Point(165, 210);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new Size(298, 84);
            this.progressBar.TabIndex = 21;
            // 
            // btnConvert
            // 
            this.btnConvert.Location = new Point(6, 210);
            this.btnConvert.Name = "btnConvert";
            this.btnConvert.Size = new Size(153, 84);
            this.btnConvert.TabIndex = 20;
            this.btnConvert.Text = "変換";
            this.btnConvert.UseVisualStyleBackColor = true;
            this.btnConvert.Click += btnConvert_Click_1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new Point(6, 19);
            this.label1.Name = "label1";
            this.label1.Size = new Size(397, 30);
            this.label1.TabIndex = 19;
            this.label1.Text = "Jpeg, jpg, png, bmp, tga, wav, mp3, oggファイルをドラッグ＆ドロップしてください。\r\nMedia フォルダにコピーした後、変換ボタンでリソースファイルを作成します";
            // 
            // listBoxFiles
            // 
            this.listBoxFiles.AllowDrop = true;
            this.listBoxFiles.FormattingEnabled = true;
            this.listBoxFiles.Location = new Point(6, 50);
            this.listBoxFiles.Name = "listBoxFiles";
            this.listBoxFiles.Size = new Size(457, 154);
            this.listBoxFiles.TabIndex = 18;
            this.listBoxFiles.DragDrop += ListBoxFiles_DragDrop;
            this.listBoxFiles.DragEnter += ListBoxFiles_DragEnter;
            // 
            // ResourceConverter
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(490, 323);
            Controls.Add(this.groupBox2);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "ResourceConverter";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ResourceConverter";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox2;
        private ProgressBar progressBar;
        private Button btnConvert;
        private Label label1;
        private ListBox listBoxFiles;
    }
}