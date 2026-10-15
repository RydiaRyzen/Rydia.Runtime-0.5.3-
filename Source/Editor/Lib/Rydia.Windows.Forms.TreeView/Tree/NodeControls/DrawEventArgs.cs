using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Aga.Controls.Tree.NodeControls
{
	public class DrawEventArgs : NodeEventArgs
	{
		private DrawContext _context;
		public DrawContext Context
		{
			get { return this._context; }
		}

		private NodeControl _control;
		public NodeControl Control
		{
			get { return this._control; }
		}
		public EditableControl EditableControl
		{
			get { return this._control as EditableControl; }
		}

		public DrawEventArgs(TreeNodeAdv node, NodeControl control, DrawContext context) : base(node)
		{
			this._control = control;
			this._context = context;
		}
	}
}
