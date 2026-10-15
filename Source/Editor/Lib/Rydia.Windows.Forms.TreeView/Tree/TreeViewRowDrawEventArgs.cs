using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Aga.Controls.Tree
{
	public class TreeViewRowDrawEventArgs: PaintEventArgs
	{
		TreeNodeAdv _node;
		DrawContext _context;
		int _row;
		Rectangle _rowRect;

		public TreeViewRowDrawEventArgs(Graphics graphics, Rectangle clipRectangle, TreeNodeAdv node, DrawContext context, int row, Rectangle rowRect)
			: base(graphics, clipRectangle)
		{
			this._node = node;
			this._context = context;
			this._row = row;
			this._rowRect = rowRect;
		}

		public TreeNodeAdv Node
		{
			get { return this._node; }
		}

		public DrawContext Context
		{
			get { return this._context; }
		}

		public int Row
		{
			get { return this._row; }
		}

		public Rectangle RowRect
		{
			get { return this._rowRect; }
		}
	}

}
