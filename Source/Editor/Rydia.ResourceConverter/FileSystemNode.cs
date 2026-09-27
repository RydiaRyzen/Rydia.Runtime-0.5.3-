using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rydia.ResourceConverter
{
    internal class FileSystemNode
    {

        public string Name { get; }
        public string FullPath { get; }
        public bool IsDirectory { get; }
        public List<FileSystemNode> Children { get; }

        public FileSystemNode(string path)
        {
            FullPath = path;
            Name = Path.GetFileName(path);
            if (string.IsNullOrEmpty(Name))
                Name = path;

            IsDirectory = Directory.Exists(path);
            Children = new List<FileSystemNode>();
        }

    }
}
