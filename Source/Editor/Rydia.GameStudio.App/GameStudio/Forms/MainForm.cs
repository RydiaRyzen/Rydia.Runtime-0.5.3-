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
    public partial class MainForm : Form
    {

        public MainForm()
        {
            InitializeComponent();
            this.menuStrip1.Items.Add(new ToolStripMenuItem("ファイル"));
            this.menuStrip1.Items.Add(new ToolStripMenuItem("編集"));
            this.menuStrip1.Items.Add(new ToolStripMenuItem("表示"));
            this.menuStrip1.Items.Add(new ToolStripMenuItem("実行"));
            this.menuStrip1.Items.Add(new ToolStripMenuItem("設定"));
            this.menuStrip1.Items.Add(new ToolStripMenuItem("ヘルプ"));
        }

    }
}
