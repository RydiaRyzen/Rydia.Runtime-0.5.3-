using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rydia.ResourceConverter
{
    internal class DirectoryTreeBuilder
    {

        public static FileSystemNode Build(string rootPath)
        {
            var root = new FileSystemNode(rootPath);
            BuildInternal(root);
            return root;
        }

        private static void BuildInternal(FileSystemNode node)
        {
            if (!node.IsDirectory)
                return;

            try
            {
                foreach (var dir in Directory.GetDirectories(node.FullPath))
                {
                    var child = new FileSystemNode(dir);
                    node.Children.Add(child);
                    BuildInternal(child);
                }

                foreach (var file in Directory.GetFiles(node.FullPath))
                {
                    node.Children.Add(new FileSystemNode(file));
                }
            }
            catch (UnauthorizedAccessException)
            {
                // アクセス不可は無視
            }
        }

    }
}
