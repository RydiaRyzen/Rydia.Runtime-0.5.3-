using System;
using System.Collections.Generic;
using System.Text;

namespace Aga.Controls.Tree.NodeControls
{
	public class NodeEventArgs : EventArgs
	{
		private TreeNodeAdv _node;
		public TreeNodeAdv Node
		{
			get { return this._node; }
		}

		public NodeEventArgs(TreeNodeAdv node)
		{
			this._node = node;
		}
	}
}
