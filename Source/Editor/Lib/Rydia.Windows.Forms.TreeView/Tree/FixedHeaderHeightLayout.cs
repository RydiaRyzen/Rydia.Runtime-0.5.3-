namespace Aga.Controls.Tree
{
	class FixedHeaderHeightLayout : IHeaderLayout
	{
		TreeViewAdv _treeView;
		int _headerHeight;

		public FixedHeaderHeightLayout(TreeViewAdv treeView, int headerHeight)
		{
			this._treeView = treeView;
			PreferredHeaderHeight = headerHeight;
		}

		#region Implementation of IHeaderLayout

		public int PreferredHeaderHeight
		{
			get { return this._headerHeight; }
			set { this._headerHeight = value; }
		}

		public void ClearCache()
		{
		}

		#endregion
	}
}
