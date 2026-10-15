using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Aga.Controls.Tree.NodeControls;

namespace Aga.Controls.Tree
{
	public class AutoRowHeightLayout: IRowLayout
	{
		private DrawContext _measureContext;
		private TreeViewAdv _treeView;
		private List<Rectangle> _rowCache;

		public AutoRowHeightLayout(TreeViewAdv treeView, int rowHeight)
		{
			this._rowCache = new List<Rectangle>();
			this._treeView = treeView;
			PreferredRowHeight = rowHeight;
			this._measureContext = new DrawContext();
			this._measureContext.Graphics = Graphics.FromImage(new Bitmap(1, 1));
		}

		private int _rowHeight;
		public int PreferredRowHeight
		{
			get { return this._rowHeight; }
			set { this._rowHeight = value; }
		}


		public int VisiblePageRowCount
		{
			get 
			{
				if (this._treeView.RowCount == 0)
					return 0;
				else
				{
					int pageHeight = this._treeView.DisplayRectangle.Height - this._treeView.ColumnHeaderHeight;
					int y = 0;
					int visibleCount = 0;
					for (int i = this._treeView.RowCount - 1; i >= 0; i--)
					{
						if (this._treeView.RowMap[i].IsHidden)
							continue;
						y += GetRowHeight(i);
						if (y > pageHeight)
							return visibleCount;
						visibleCount++;
					}
					return this._treeView.RowCount;
				}
			}
		}

		public int CurrentPageSize
		{
			get
			{
				if (this._treeView.RowCount == 0)
					return 0;
				else
				{
					int pageHeight = this._treeView.DisplayRectangle.Height - this._treeView.ColumnHeaderHeight;
					int y = 0;
					for (int i = this._treeView.FirstVisibleRow; i < this._treeView.RowCount; i++)
					{
						y += GetRowHeight(i);
						if (y > pageHeight)
							return Math.Max(0, i - this._treeView.FirstVisibleRow);
					}
					return Math.Max(0, this._treeView.RowCount - this._treeView.FirstVisibleRow);
				}
			}
		}

		public Rectangle GetRowBounds(int rowNo)
		{
			if (rowNo >= this._rowCache.Count)
			{
				int count = this._rowCache.Count;
				int y = count > 0 ? this._rowCache[count - 1].Bottom : 0;
				for (int i = count; i <= rowNo; i++)
				{
					int height = GetRowHeight(i);
					this._rowCache.Add(new Rectangle(0, y, 0, height));
					y += height;
				}
				if (rowNo < this._rowCache.Count - 1)
					return Rectangle.Empty;
			}
			if (rowNo >= 0 && rowNo < this._rowCache.Count)
				return this._rowCache[rowNo];
			else
				return Rectangle.Empty;
		}

		private int GetRowHeight(int rowNo)
		{
			if (rowNo < this._treeView.RowMap.Count)
			{
				TreeNodeAdv node = this._treeView.RowMap[rowNo];
				if (node.IsHidden) return 0;
				if (node.Height == null)
				{
					int res = 0;
					this._measureContext.Font = this._treeView.Font;
					foreach (NodeControl nc in this._treeView.NodeControls)
					{
						int h = nc.GetActualSize(node, this._measureContext).Height;
						if (h > res)
							res = h;
					}
					node.Height = res;
				}
				return node.Height.Value;
			}
			else
				return 0;
		}

		public int GetRowAt(Point point)
		{
			int py = point.Y - this._treeView.ColumnHeaderHeight;
			if (py < 0)  // Header column
				return -1; 
			int y = 0;
			for (int i = this._treeView.FirstVisibleRow; i < this._treeView.RowCount; i++)
			{
				int h = GetRowHeight(i);
				if (py >= y && py < y + h)
					return i;
				else
					y += h;
			}
			return -1;
		}

		public int GetFirstRow(int lastPageRow)
		{
			int pageHeight = this._treeView.DisplayRectangle.Height - this._treeView.ColumnHeaderHeight;
			int y = 0;
			for (int i = lastPageRow; i >= 0; i--)
			{
				y += GetRowHeight(i);
				if (y > pageHeight)
					return Math.Max(0, i + 1);
			}
			return 0;
		}

		public void ClearCache()
		{
			this._rowCache.Clear();
		}
	}
}
