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
            this.sidebar.SuspendLayout();
            SuspendLayout();
            // 
            // sidebar
            // 
            this.sidebar.BackColor = Color.FromArgb(30, 32, 39);
            this.sidebar.Controls.Add(this.navigation);
            this.sidebar.Dock = DockStyle.Left;
            this.sidebar.Location = new Point(0, 0);
            this.sidebar.Name = "sidebar";
            this.sidebar.Size = new Size(220, 450);
            this.sidebar.TabIndex = 0;
            // 
            // navigation
            // 
            this.navigation.Dock = DockStyle.Top;
            this.navigation.FlowDirection = FlowDirection.TopDown;
            this.navigation.Location = new Point(0, 0);
            this.navigation.Name = "navigation";
            this.navigation.Padding = new Padding(10, 10, 10, 0);
            this.navigation.Size = new Size(220, 250);
            this.navigation.TabIndex = 1;
            this.navigation.WrapContents = false;
            // 
            // HubForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(this.sidebar);
            MaximizeBox = false;
            Name = "HubForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Hub";
            this.sidebar.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel sidebar;
        private FlowLayoutPanel navigation;
    }
}