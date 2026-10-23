namespace Rydia.GameStudio
{
    partial class HubForm
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
            this.sidebar = new Panel();
            this.navigation = new FlowLayoutPanel();
            this.projectButton = new Button();
            this.installButton = new Button();
            this.learnButton = new Button();
            this.communityButton = new Button();
            this.versionLabel1 = new Label();
            this.versionLabel2 = new Label();
            this.logoPict = new PictureBox();
            this.contentPanel = new Panel();
            this.sidebar.SuspendLayout();
            this.navigation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)this.logoPict).BeginInit();
            SuspendLayout();
            // 
            // sidebar
            // 
            this.sidebar.BackColor = Color.FromArgb(30, 32, 39);
            this.sidebar.Controls.Add(this.navigation);
            this.sidebar.Controls.Add(this.versionLabel1);
            this.sidebar.Controls.Add(this.versionLabel2);
            this.sidebar.Controls.Add(this.logoPict);
            this.sidebar.Dock = DockStyle.Left;
            this.sidebar.Location = new Point(0, 0);
            this.sidebar.Name = "sidebar";
            this.sidebar.Size = new Size(220, 450);
            this.sidebar.TabIndex = 0;
            // 
            // navigation
            // 
            this.navigation.Controls.Add(this.projectButton);
            this.navigation.Controls.Add(this.installButton);
            this.navigation.Controls.Add(this.learnButton);
            this.navigation.Controls.Add(this.communityButton);
            this.navigation.Dock = DockStyle.Top;
            this.navigation.FlowDirection = FlowDirection.TopDown;
            this.navigation.Location = new Point(0, 151);
            this.navigation.Name = "navigation";
            this.navigation.Padding = new Padding(10, 10, 10, 0);
            this.navigation.Size = new Size(220, 220);
            this.navigation.TabIndex = 1;
            this.navigation.WrapContents = false;
            // 
            // projectButton
            // 
            this.projectButton.BackColor = Color.FromArgb(48, 57, 72);
            this.projectButton.Cursor = Cursors.Hand;
            this.projectButton.FlatStyle = FlatStyle.Flat;
            this.projectButton.ForeColor = Color.FromArgb(60, 180, 255);
            this.projectButton.Location = new Point(10, 10);
            this.projectButton.Margin = new Padding(0, 0, 0, 5);
            this.projectButton.Name = "projectButton";
            this.projectButton.Padding = new Padding(12, 0, 0, 0);
            this.projectButton.Size = new Size(190, 46);
            this.projectButton.TabIndex = 1;
            this.projectButton.Text = "▦   プロジェクト";
            this.projectButton.TextAlign = ContentAlignment.MiddleLeft;
            this.projectButton.UseVisualStyleBackColor = false;
            this.projectButton.Click += projectButton_Click;
            // 
            // installButton
            // 
            this.installButton.BackColor = Color.FromArgb(48, 57, 72);
            this.installButton.Cursor = Cursors.Hand;
            this.installButton.FlatStyle = FlatStyle.Flat;
            this.installButton.ForeColor = Color.FromArgb(60, 180, 255);
            this.installButton.Location = new Point(10, 61);
            this.installButton.Margin = new Padding(0, 0, 0, 5);
            this.installButton.Name = "installButton";
            this.installButton.Padding = new Padding(12, 0, 0, 0);
            this.installButton.Size = new Size(190, 46);
            this.installButton.TabIndex = 2;
            this.installButton.Text = "▣   インストール";
            this.installButton.TextAlign = ContentAlignment.MiddleLeft;
            this.installButton.UseVisualStyleBackColor = false;
            this.installButton.Click += installButton_Click;
            // 
            // learnButton
            // 
            this.learnButton.BackColor = Color.FromArgb(48, 57, 72);
            this.learnButton.Cursor = Cursors.Hand;
            this.learnButton.FlatStyle = FlatStyle.Flat;
            this.learnButton.ForeColor = Color.FromArgb(60, 180, 255);
            this.learnButton.Location = new Point(10, 112);
            this.learnButton.Margin = new Padding(0, 0, 0, 5);
            this.learnButton.Name = "learnButton";
            this.learnButton.Padding = new Padding(12, 0, 0, 0);
            this.learnButton.Size = new Size(190, 46);
            this.learnButton.TabIndex = 3;
            this.learnButton.Text = "◇   学習";
            this.learnButton.TextAlign = ContentAlignment.MiddleLeft;
            this.learnButton.UseVisualStyleBackColor = false;
            this.learnButton.Click += learnButton_Click;
            // 
            // communityButton
            // 
            this.communityButton.BackColor = Color.FromArgb(48, 57, 72);
            this.communityButton.Cursor = Cursors.Hand;
            this.communityButton.FlatStyle = FlatStyle.Flat;
            this.communityButton.ForeColor = Color.FromArgb(60, 180, 255);
            this.communityButton.Location = new Point(10, 163);
            this.communityButton.Margin = new Padding(0, 0, 0, 5);
            this.communityButton.Name = "communityButton";
            this.communityButton.Padding = new Padding(12, 0, 0, 0);
            this.communityButton.Size = new Size(190, 46);
            this.communityButton.TabIndex = 4;
            this.communityButton.Text = "◎   コミュニティ";
            this.communityButton.TextAlign = ContentAlignment.MiddleLeft;
            this.communityButton.UseVisualStyleBackColor = false;
            this.communityButton.Click += communityButton_Click;
            // 
            // versionLabel1
            // 
            this.versionLabel1.AutoSize = true;
            this.versionLabel1.Dock = DockStyle.Top;
            this.versionLabel1.ForeColor = Color.Gray;
            this.versionLabel1.Location = new Point(0, 136);
            this.versionLabel1.Name = "versionLabel1";
            this.versionLabel1.Size = new Size(111, 15);
            this.versionLabel1.TabIndex = 1;
            this.versionLabel1.Text = "Engine Version 6.0.0";
            this.versionLabel1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // versionLabel2
            // 
            this.versionLabel2.AutoSize = true;
            this.versionLabel2.Dock = DockStyle.Top;
            this.versionLabel2.ForeColor = Color.Gray;
            this.versionLabel2.Location = new Point(0, 121);
            this.versionLabel2.Name = "versionLabel2";
            this.versionLabel2.Size = new Size(112, 15);
            this.versionLabel2.TabIndex = 2;
            this.versionLabel2.Text = "Editor   Version 6.0.0";
            this.versionLabel2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // logoPict
            // 
            this.logoPict.BackgroundImageLayout = ImageLayout.Center;
            this.logoPict.Dock = DockStyle.Top;
            this.logoPict.Image = Properties.EditorResource.SplashLogo;
            this.logoPict.Location = new Point(0, 0);
            this.logoPict.Name = "logoPict";
            this.logoPict.Size = new Size(220, 121);
            this.logoPict.SizeMode = PictureBoxSizeMode.Zoom;
            this.logoPict.TabIndex = 1;
            this.logoPict.TabStop = false;
            // 
            // contentPanel
            // 
            this.contentPanel.BackColor = Color.FromArgb(24, 26, 32);
            this.contentPanel.Dock = DockStyle.Fill;
            this.contentPanel.Location = new Point(220, 0);
            this.contentPanel.Name = "contentPanel";
            this.contentPanel.Size = new Size(846, 450);
            this.contentPanel.TabIndex = 1;
            // 
            // HubForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1066, 450);
            Controls.Add(this.contentPanel);
            Controls.Add(this.sidebar);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Name = "HubForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Rydia Engine Hub";
            this.sidebar.ResumeLayout(false);
            this.sidebar.PerformLayout();
            this.navigation.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)this.logoPict).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel sidebar;
        private FlowLayoutPanel navigation;
        private Button projectButton;
        private Button installButton;
        private Button learnButton;
        private Button communityButton;
        private PictureBox logoPict;
        private Label versionLabel1;
        private Label versionLabel2;
        private Panel contentPanel;
    }
}