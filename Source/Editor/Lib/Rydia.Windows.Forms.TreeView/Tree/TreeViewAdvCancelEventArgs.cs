using System;
using System.Collections.Generic;
using System.Text;

namespace Aga.Controls.Tree
{
	public class TreeViewAdvCancelEventArgs : TreeViewAdvEventArgs
	{
		private bool _cancel;

		public bool Cancel
		{
			get { return this._cancel; }
			set { this._cancel = value; }
		}

		public TreeViewAdvCancelEventArgs(TreeNodeAdv node)
			: base(node)
		{
		}

	}
}
