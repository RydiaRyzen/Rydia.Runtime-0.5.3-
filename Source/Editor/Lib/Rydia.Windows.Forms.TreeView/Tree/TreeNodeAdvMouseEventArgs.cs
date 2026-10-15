using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using Aga.Controls.Tree.NodeControls;

namespace Aga.Controls.Tree
{
	public class TreeNodeAdvMouseEventArgs : MouseEventArgs
	{
		private TreeNodeAdv _node;
		public TreeNodeAdv Node
		{
			get { return this._node; }
			internal set { this._node = value; }
		}

		private NodeControl _control;
		public NodeControl Control
		{
			get { return this._control; }
			internal set { this._control = value; }
		}

		private Point _viewLocation;
		public Point ViewLocation
		{
			get { return this._viewLocation; }
			internal set { this._viewLocation = value; }
		}

		private Keys _modifierKeys;
		public Keys ModifierKeys
		{
			get { return this._modifierKeys; }
			internal set { this._modifierKeys = value; }
		}

		private bool _handled;
		public bool Handled
		{
			get { return this._handled; }
			set { this._handled = value; }
		}

		private bool _justGotFocus;
		public bool JustGotFocus
		{
			get { return this._justGotFocus; }
			internal set { this._justGotFocus = value; }
		}

		private Rectangle _controlBounds;
		public Rectangle ControlBounds
		{
			get { return this._controlBounds; }
			internal set { this._controlBounds = value; }
		}

		public TreeNodeAdvMouseEventArgs(MouseEventArgs args)
			: base(args.Button, args.Clicks, args.X, args.Y, args.Delta)
		{
		}
	}
}
