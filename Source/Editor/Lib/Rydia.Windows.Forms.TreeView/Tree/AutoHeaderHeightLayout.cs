using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Aga.Controls.Tree
{
	using System.Drawing;

	public class AutoHeaderHeightLayout : IHeaderLayout
	{
		DrawContext _measureContext;
		TreeViewAdv _treeView;

		public AutoHeaderHeightLayout(TreeViewAdv treeView, int headerHeight)
		{
			this._treeView = treeView;
			PreferredHeaderHeight = headerHeight;
			this._measureContext = new DrawContext();
			this._measureContext.Graphics = Graphics.FromImage(new Bitmap(1, 1));
		}

		int? _headerHeight;

		bool _computed;

		#region Implementation of IHeaderLayout

		public int PreferredHeaderHeight
		{
			get { return GetHeaderHeight(); }
			set
			{
				this._headerHeight = value;
				this._computed = false;
			}
		}

		public void ClearCache()
		{
			this._computed = false;
		}

		int GetHeaderHeight()
		{
			if (!this._computed)
			{
				int res = 0;
				this._measureContext.Font = this._treeView.Font;
				foreach (TreeColumn nc in this._treeView.Columns)
				{
					int h = nc.GetActualSize(this._measureContext).Height;
					if (h > res) res = h;
				}
				this._headerHeight = res;

				this._computed = true;
			}

			return this._headerHeight.Value;
		}

		#endregion
	}
}
