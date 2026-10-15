using System;
using System.Collections.Generic;
using System.Text;

namespace Aga.Controls.Tree
{
	public class TreeViewAdvEventArgs : EventArgs
	{
		private TreeNodeAdv _node;

		public TreeNodeAdv Node
		{
			get { return this._node; }
		}

		public TreeViewAdvEventArgs(TreeNodeAdv node)
		{
			this._node = node;
		}
	}
}
