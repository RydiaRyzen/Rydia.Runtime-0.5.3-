using System;
using System.Collections.Generic;
using System.Text;

namespace Aga.Controls.Tree
{
	public struct DropPosition
	{
		private TreeNodeAdv _node;
		public TreeNodeAdv Node
		{
			get { return this._node; }
			set { this._node = value; }
		}

		private NodePosition _position;
		public NodePosition Position
		{
			get { return this._position; }
			set { this._position = value; }
		}
	}
}
