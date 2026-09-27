using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Aga.Controls.Tree.NodeControls;
using Aga.Controls.Tree;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Diagnostics;

namespace Rydia.ResourceConverter
{

    public partial class ProjectView : Form
    {

        private System.Windows.Forms.TreeView treeView;
        private ContextMenuStrip treeMenu;
        private FileSystemWatcher watcher;

        public ProjectView()
        {
            InitializeComponent();

            // TreeView
            this.treeView = new System.Windows.Forms.TreeView();
            this.treeView.Dock = DockStyle.Fill;
            this.treeView.LabelEdit = true;
            this.treeView.BeforeExpand += treeView1_BeforeExpand;
            this.treeView.AfterSelect += treeView1_AfterSelect;
            this.treeView.NodeMouseClick += treeView1_NodeMouseClick;
            this.treeView.AfterLabelEdit += treeView_AfterLabelEdit;
            Controls.Add(this.treeView);
            

            InitializeContextMenu();
            InitializeTree(MainForm.AssetsDir);
            InitializeFileWatcher(MainForm.AssetsDir);
        }

        #region ContextMenu

        private void InitializeContextMenu()
        {
            this.treeMenu = new ContextMenuStrip();

            var createFolder = new ToolStripMenuItem("フォルダー作成");
            var createFile = new ToolStripMenuItem("ファイル作成");
            var rename = new ToolStripMenuItem("リネーム");
            var delete = new ToolStripMenuItem("削除");

            var openInExplorer = new ToolStripMenuItem("エクスプローラーで開く");

            createFolder.Click += CreateFolder_Click;
            createFile.Click += CreateFile_Click;
            rename.Click += Rename_Click;
            delete.Click += Delete_Click;
            openInExplorer.Click += OpenInExplorer_Click;

            this.treeMenu.Items.AddRange(new ToolStripItem[]
            {
                openInExplorer,
                new ToolStripSeparator(),
                createFolder,
                createFile,
                new ToolStripSeparator(),
                rename,
                delete
            });
        }

        private void CreateFolder_Click(object sender, EventArgs e)
        {
            var node = this.treeView.SelectedNode;
            if (node == null) return;

            string parentPath = node.Tag as string;
            if (File.Exists(parentPath))
                parentPath = Path.GetDirectoryName(parentPath);

            string newDir = Path.Combine(parentPath, "NewFolder");

            int index = 1;
            while (Directory.Exists(newDir))
                newDir = Path.Combine(parentPath, $"NewFolder{index++}");

            Directory.CreateDirectory(newDir);

            node.Nodes.Clear();

            InitializeTree(MainForm.AssetsDir);
            node.Expand();
        }

        private void CreateFile_Click(object sender, EventArgs e)
        {
            var node = this.treeView.SelectedNode;
            if (node == null) return;

            string parentPath = node.Tag as string;
            if (File.Exists(parentPath))
                parentPath = Path.GetDirectoryName(parentPath);

            string newFile = Path.Combine(parentPath, "NewFile.txt");

            int index = 1;
            while (File.Exists(newFile))
                newFile = Path.Combine(parentPath, $"NewFile{index}.txt");

            File.WriteAllText(newFile, "");

            node.Nodes.Clear();
            node.Nodes.Add(new TreeNode(Path.GetFileName(newFile)) { Tag = newFile });
            node.Expand();
        }

        private void Rename_Click(object sender, EventArgs e)
        {
            this.treeView.SelectedNode?.BeginEdit();
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            var node = this.treeView.SelectedNode;
            if (node == null) return;

            string path = node.Tag as string;

            if (MessageBox.Show(
                $"削除しますか？\n{path}",
                "確認",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            try
            {
                if (Directory.Exists(path))
                    Directory.Delete(path, true);
                else
                    File.Delete(path);

                node.Remove();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void OpenInExplorer_Click(object sender, EventArgs e)
        {
            var node = this.treeView.SelectedNode;
            if (node == null)
                return;

            string path = node.Tag as string;
            if (string.IsNullOrEmpty(path))
                return;

            if (Directory.Exists(path))
            {
                Process.Start("explorer.exe", $"\"{path}\"");
            }
            else if (File.Exists(path))
            {
                Process.Start("explorer.exe", $"/select,\"{path}\"");
            }
        }

        #endregion

        private void InitializeFileWatcher(string rootPath)
        {
            this.watcher = new FileSystemWatcher(rootPath)
            {
                IncludeSubdirectories = true,
                NotifyFilter =
                    NotifyFilters.FileName |
                    NotifyFilters.DirectoryName |
                    NotifyFilters.LastWrite
            };

            this.watcher.Created += OnFileSystemChanged;
            this.watcher.Deleted += OnFileSystemChanged;
            this.watcher.Renamed += OnFileSystemRenamed;

            this.watcher.EnableRaisingEvents = true;
        }

        private void OnFileSystemChanged(object sender, FileSystemEventArgs e)
        {
            if (IsDisposed) return;

            BeginInvoke(new Action(() =>
            {
                RefreshNodeByPath(Path.GetDirectoryName(e.FullPath));
            }));
        }

        private void OnFileSystemRenamed(object sender, RenamedEventArgs e)
        {
            if (IsDisposed) return;

            BeginInvoke(new Action(() =>
            {
                RefreshNodeByPath(Path.GetDirectoryName(e.FullPath));
                RefreshNodeByPath(Path.GetDirectoryName(e.OldFullPath));
            }));
        }

        private void RefreshNodeByPath(string directoryPath)
        {
            if (string.IsNullOrEmpty(directoryPath))
                return;

            var node = FindNodeByPath(this.treeView.Nodes, directoryPath);
            if (node == null)
                return;

            // 再読み込み
            node.Nodes.Clear();
            node.Nodes.Add(new TreeNode()); // ダミー
            this.treeView.CollapseAll();
            this.treeView.Nodes[0].Expand();
        }

        private TreeNode FindNodeByPath(TreeNodeCollection nodes, string path)
        {
            foreach (TreeNode node in nodes)
            {
                if (string.Equals(node.Tag as string, path,
                    StringComparison.OrdinalIgnoreCase))
                    return node;

                var found = FindNodeByPath(node.Nodes, path);
                if (found != null)
                    return found;
            }
            return null;
        }


        private void InitializeTree(string rootPath)
        {
            this.treeView.Nodes.Clear();

            var rootNode = CreateDirectoryNode(rootPath);
            this.treeView.Nodes.Add(rootNode);
            this.treeView.ExpandAll();
        }

        private TreeNode CreateDirectoryNode(string path)
        {
            string name = Path.GetFileName(path);
            if (string.IsNullOrEmpty(name))
                name = path;
            var node = new TreeNode(name)
            {
                Tag = path
            };

            // 展開可能に見せるためのダミー
            node.Nodes.Add(new TreeNode());

            return node;
        }

        private void treeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            // すでに読み込み済みなら何もしない
            if (e.Node.Nodes.Count != 1 || e.Node.Nodes[0].Tag != null)
                return;

            e.Node.Nodes.Clear();

            string path = e.Node.Tag as string;

            try
            {
                // ディレクトリ
                foreach (var dir in Directory.GetDirectories(path))
                {
                    e.Node.Nodes.Add(CreateDirectoryNode(dir));
                }

                // ファイル
                foreach (var file in Directory.GetFiles(path))
                {
                    e.Node.Nodes.Add(new TreeNode(Path.GetFileName(file))
                    {
                        Tag = file
                    });
                }
            }
            catch (UnauthorizedAccessException)
            {
                // アクセス不可は無視
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string fullPath = e.Node.Tag as string;

        }
        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.treeView.SelectedNode = e.Node;
                this.treeMenu.Show(this.treeView, e.Location);
            }
        }



        private void treeView_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e.Label == null)
                return;

            string oldPath = e.Node.Tag as string;
            string newPath = Path.Combine(Path.GetDirectoryName(oldPath), e.Label);

            try
            {
                if (Directory.Exists(oldPath))
                    Directory.Move(oldPath, newPath);
                else
                    File.Move(oldPath, newPath);

                e.Node.Tag = newPath;
            }
            catch
            {
                e.CancelEdit = true;
            }
        }
        

        private void BuildTreeRecursive(TreeNode parent)
        {
            string path = parent.Tag as string;

            try
            {
                foreach (var dir in Directory.GetDirectories(path))
                {
                    var node = new TreeNode(Path.GetFileName(dir)) { Tag = dir };
                    parent.Nodes.Add(node);
                    BuildTreeRecursive(node);
                }

                foreach (var file in Directory.GetFiles(path))
                {
                    parent.Nodes.Add(new TreeNode(Path.GetFileName(file)) { Tag = file });
                }
            }
            catch (UnauthorizedAccessException)
            {
            }
        }
    }
}
