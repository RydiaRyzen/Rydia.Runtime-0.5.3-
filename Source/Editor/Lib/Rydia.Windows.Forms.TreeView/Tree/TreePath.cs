using System;
using System.Text;
using System.Collections.ObjectModel;

namespace Aga.Controls.Tree
{
	public class TreePath
	{
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
		public static readonly TreePath Empty = new TreePath();

		private object[] _path;
		public object[] FullPath
		{
			get { return this._path; }
		}

		public object LastNode
		{
			get
			{
				if (this._path.Length > 0)
					return this._path[this._path.Length - 1];
				else
					return null;
			}
		}

		public object FirstNode
		{
			get
			{
				if (this._path.Length > 0)
					return this._path[0];
				else
					return null;
			}
		}

		public TreePath()
		{
			this._path = new object[0];
		}

		public TreePath(object node)
		{
			this._path = new object[] { node };
		}

		public TreePath(object[] path)
		{
			this._path = path;
		}

		public TreePath(TreePath parent, object node)
		{
			this._path = new object[parent.FullPath.Length + 1];
			for (int i = 0; i < this._path.Length - 1; i++)
				this._path[i] = parent.FullPath[i];
			this._path[this._path.Length - 1] = node;
		}

		public bool IsEmpty()
		{
			return (this._path.Length == 0);
		}
	}
}
