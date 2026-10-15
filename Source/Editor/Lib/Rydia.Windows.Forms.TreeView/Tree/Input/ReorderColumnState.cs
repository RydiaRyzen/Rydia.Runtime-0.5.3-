using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Aga.Controls.Tree
{
	internal class ReorderColumnState : ColumnState
	{
		#region Properties

		private Point _location;
		public Point Location
		{
			get { return this._location; }
		}

		private Bitmap _ghostImage;
		public Bitmap GhostImage
		{
			get { return this._ghostImage; }
		}

		private TreeColumn _dropColumn;
		public TreeColumn DropColumn
		{
			get { return this._dropColumn; }
		}

		private int _dragOffset;
		public int DragOffset
		{
			get { return this._dragOffset; }
		}

		#endregion

		public ReorderColumnState(TreeViewAdv tree, TreeColumn column, Point initialMouseLocation)
			: base(tree, column)
		{
			this._location = new Point(initialMouseLocation.X + Tree.OffsetX, 0);
			this._dragOffset = tree.GetColumnX(column) - initialMouseLocation.X;
			this._ghostImage = column.CreateGhostImage(new Rectangle(0, 0, column.Width, tree.ColumnHeaderHeight), tree.Font);
		}

		public override void KeyDown(KeyEventArgs args)
		{
			args.Handled = true;
			if (args.KeyCode == Keys.Escape)
				FinishResize();
		}

		public override void MouseDown(TreeNodeAdvMouseEventArgs args)
		{
		}

		public override void MouseUp(TreeNodeAdvMouseEventArgs args)
		{
			FinishResize();
		}

		public override bool MouseMove(MouseEventArgs args)
		{
			this._dropColumn = null;
			this._location = new Point(args.X + Tree.OffsetX, 0);
			int x = 0;
			foreach (TreeColumn c in Tree.Columns)
			{
				if (c.IsVisible)
				{
					if (this._location.X < x + c.Width / 2)
					{
						this._dropColumn = c;
						break;
					}
					x += c.Width;
				}
			}
			Tree.UpdateHeaders();
			return true;
		}

		private void FinishResize()
		{
			Tree.ChangeInput();
			if (Column == DropColumn)
				Tree.UpdateView();
			else
			{
				Tree.Columns.Remove(Column);
				if (DropColumn == null)
					Tree.Columns.Add(Column);
				else
					Tree.Columns.Insert(Tree.Columns.IndexOf(DropColumn), Column);

				Tree.OnColumnReordered(Column);
			}
		}
	}
}