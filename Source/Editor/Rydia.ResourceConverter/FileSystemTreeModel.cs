using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aga.Controls.Tree;

namespace Rydia.ResourceConverter
{
    internal class FileSystemTreeModel : ITreeModel
    {

        private readonly FileSystemNode _root;

        public FileSystemTreeModel(FileSystemNode root)
        {
            this._root = root;
        }

        public event EventHandler<TreeModelEventArgs> NodesChanged;
        public event EventHandler<TreeModelEventArgs> NodesInserted;
        public event EventHandler<TreeModelEventArgs> NodesRemoved;
        public event EventHandler<TreePathEventArgs> StructureChanged;

        public IEnumerable GetChildren(TreePath treePath)
        {
            if (treePath.IsEmpty())
            {
                // ルート
                return new[] { this._root };
            }

            var node = treePath.LastNode as FileSystemNode;
            return node?.Children;
        }

        public bool IsLeaf(TreePath treePath)
        {
            var node = treePath.LastNode as FileSystemNode;
            if (node == null)
                return true;

            return !node.IsDirectory;
        }

        // 必要に応じて呼び出す
        protected void OnStructureChanged(TreePath path)
        {
            StructureChanged?.Invoke(this, new TreePathEventArgs(path));
        }
    }
}
