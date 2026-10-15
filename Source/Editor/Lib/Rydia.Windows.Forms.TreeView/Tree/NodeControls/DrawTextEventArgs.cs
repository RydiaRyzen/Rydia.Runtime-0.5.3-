using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Aga.Controls.Tree.NodeControls
{
	public class DrawTextEventArgs : DrawEventArgs
	{
		private Brush _backgroundBrush;
		public Brush BackgroundBrush
		{
            get { return this._backgroundBrush; }
			set { this._backgroundBrush = value; }
		}

		private Font _font;
		public Font Font
		{
			get { return this._font; }
			set { this._font = value; }
		}

		private Color _textColor;
		public Color TextColor
		{
			get { return this._textColor; }
			set { this._textColor = value; }
		}

		private string _text;
		public string Text
		{
			get { return this._text; }
			set { this._text = value; }
		}

		public DrawTextEventArgs(TreeNodeAdv node, NodeControl control, DrawContext context, string text)
			: base(node, control, context)
		{
			this._text = text;
		}
	}
}
