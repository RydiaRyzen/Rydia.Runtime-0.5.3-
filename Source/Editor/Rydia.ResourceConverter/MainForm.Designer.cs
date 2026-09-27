namespace Rydia.ResourceConverter
{
    partial class MainForm
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
            this.menuStrip1 = new MenuStrip();
            this.assetsフォルダToolStripMenuItem = new ToolStripMenuItem();
            this.mediaフォルダToolStripMenuItem = new ToolStripMenuItem();
            this.resourceConverterToolStripMenuItem = new ToolStripMenuItem();
            this.システムフォントコンバーターToolStripMenuItem = new ToolStripMenuItem();
            this.tTFフォントToolStripMenuItem = new ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new ToolStripItem[] { this.assetsフォルダToolStripMenuItem, this.mediaフォルダToolStripMenuItem, this.resourceConverterToolStripMenuItem, this.システムフォントコンバーターToolStripMenuItem, this.tTFフォントToolStripMenuItem });
            this.menuStrip1.Location = new Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new Size(699, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // assetsフォルダToolStripMenuItem
            // 
            this.assetsフォルダToolStripMenuItem.Name = "assetsフォルダToolStripMenuItem";
            this.assetsフォルダToolStripMenuItem.Size = new Size(87, 20);
            this.assetsフォルダToolStripMenuItem.Text = "Assetsフォルダ";
            this.assetsフォルダToolStripMenuItem.Click += assetsフォルダToolStripMenuItem_Click;
            // 
            // mediaフォルダToolStripMenuItem
            // 
            this.mediaフォルダToolStripMenuItem.Name = "mediaフォルダToolStripMenuItem";
            this.mediaフォルダToolStripMenuItem.Size = new Size(87, 20);
            this.mediaフォルダToolStripMenuItem.Text = "Mediaフォルダ";
            this.mediaフォルダToolStripMenuItem.Click += mediaフォルダToolStripMenuItem_Click;
            // 
            // resourceConverterToolStripMenuItem
            // 
            this.resourceConverterToolStripMenuItem.Name = "resourceConverterToolStripMenuItem";
            this.resourceConverterToolStripMenuItem.Size = new Size(105, 20);
            this.resourceConverterToolStripMenuItem.Text = "リソースコンバーター";
            this.resourceConverterToolStripMenuItem.Click += resourceConverterToolStripMenuItem_Click;
            // 
            // システムフォントコンバーターToolStripMenuItem
            // 
            this.システムフォントコンバーターToolStripMenuItem.Name = "システムフォントコンバーターToolStripMenuItem";
            this.システムフォントコンバーターToolStripMenuItem.Size = new Size(141, 20);
            this.システムフォントコンバーターToolStripMenuItem.Text = "システムフォントコンバーター";
            this.システムフォントコンバーターToolStripMenuItem.Click += システムフォントコンバーターToolStripMenuItem_Click;
            // 
            // tTFフォントToolStripMenuItem
            // 
            this.tTFフォントToolStripMenuItem.Name = "tTFフォントToolStripMenuItem";
            this.tTFフォントToolStripMenuItem.Size = new Size(122, 20);
            this.tTFフォントToolStripMenuItem.Text = "TTFフォントコンバーター";
            this.tTFフォントToolStripMenuItem.Click += tTFフォントToolStripMenuItem_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(699, 413);
            Controls.Add(this.menuStrip1);
            IsMdiContainer = true;
            MainMenuStrip = this.menuStrip1;
            MaximizeBox = false;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MainForm";
            WindowState = FormWindowState.Maximized;
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private MenuStrip menuStrip1;
        private ToolStripMenuItem assetsフォルダToolStripMenuItem;
        private ToolStripMenuItem mediaフォルダToolStripMenuItem;
        private ToolStripMenuItem resourceConverterToolStripMenuItem;
        private ToolStripMenuItem システムフォントコンバーターToolStripMenuItem;
        private ToolStripMenuItem tTFフォントToolStripMenuItem;
    }
}