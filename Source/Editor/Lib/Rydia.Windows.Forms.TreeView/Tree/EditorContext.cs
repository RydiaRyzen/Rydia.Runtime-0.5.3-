using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using Aga.Controls.Tree.NodeControls;

namespace Aga.Controls.Tree
{
	public struct EditorContext
	{
		private TreeNodeAdv _currentNode;
		public TreeNodeAdv CurrentNode
		{
			get { return this._currentNode; }
			set { this._currentNode = value; }
		}

		private Control _editor;
		public Control Editor
		{
			get { return this._editor; }
			set { this._editor = value; }
		}

		private NodeControl _owner;
		public NodeControl Owner
		{
			get { return this._owner; }
			set { this._owner = value; }
		}

		private Rectangle _bounds;
		public Rectangle Bounds
		{
			get { return this._bounds; }
			set { this._bounds = value; }
		}

		private DrawContext _drawContext;
		public DrawContext DrawContext
		{
			get { return this._drawContext; }
			set { this._drawContext = value; }
		}
	}
}
