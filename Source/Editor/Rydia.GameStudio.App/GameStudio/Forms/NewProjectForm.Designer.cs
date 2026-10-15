namespace Rydia.GameStudio
{
    partial class NewProjectForm
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
            this.button1 = new Button();
            SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new Point(556, 335);
            this.button1.Name = "button1";
            this.button1.Size = new Size(172, 68);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += button1_Click;
            // 
            // NewProjectForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(this.button1);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "NewProjectForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "新規プロジェクト作成";
            ResumeLayout(false);
        }

        #endregion

        private Button button1;
    }
}