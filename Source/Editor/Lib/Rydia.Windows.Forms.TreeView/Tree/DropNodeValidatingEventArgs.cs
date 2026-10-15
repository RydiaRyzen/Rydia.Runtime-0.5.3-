using System;
using System.Drawing;

namespace Aga.Controls.Tree
{
	public class DropNodeValidatingEventArgs: EventArgs
	{
		Point _point;
		TreeNodeAdv _node;

		public DropNodeValidatingEventArgs(Point point, TreeNodeAdv node)
		{
			this._point = point;
			this._node = node;
		}

		public Point Point
		{
			get { return this._point; }
		}

		public TreeNodeAdv Node
		{
			get { return this._node; }
			set { this._node = value; }
		}
	}
}
