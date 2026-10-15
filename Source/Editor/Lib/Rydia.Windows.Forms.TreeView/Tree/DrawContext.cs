using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Aga.Controls.Tree.NodeControls;

namespace Aga.Controls.Tree
{
	public struct DrawContext
	{
		private Graphics _graphics;
		public Graphics Graphics
		{
			get { return this._graphics; }
			set { this._graphics = value; }
		}

		private Rectangle _bounds;
		public Rectangle Bounds
		{
			get { return this._bounds; }
			set { this._bounds = value; }
		}

		private Font _font;
		public Font Font
		{
			get { return this._font; }
			set { this._font = value; }
		}

		private DrawSelectionMode _drawSelection;
		public DrawSelectionMode DrawSelection
		{
			get { return this._drawSelection; }
			set { this._drawSelection = value; }
		}

		private bool _drawFocus;
		public bool DrawFocus
		{
			get { return this._drawFocus; }
			set { this._drawFocus = value; }
		}

		private NodeControl _currentEditorOwner;
		public NodeControl CurrentEditorOwner
		{
			get { return this._currentEditorOwner; }
			set { this._currentEditorOwner = value; }
		}

		private bool _enabled;
		public bool Enabled
		{
			get { return this._enabled; }
			set { this._enabled = value; }
		}
	}
}
