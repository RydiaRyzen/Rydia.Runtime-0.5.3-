using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Rydia.Windows.Forms.TreeView.Properties;

namespace Aga.Controls.Tree.NodeControls
{
	public class NodeStateIcon: NodeIcon
	{
		private Image _leaf;
		private Image _opened;
		private Image _closed;

		public NodeStateIcon()
		{
			this._leaf = MakeTransparent(Resources.Leaf);
			this._opened = MakeTransparent(Resources.Folder);
			this._closed = MakeTransparent(Resources.FolderClosed);
		}

		private static Image MakeTransparent(Bitmap bitmap)
		{
			bitmap.MakeTransparent(bitmap.GetPixel(0,0));
			return bitmap;
		}

		protected override Image GetIcon(TreeNodeAdv node)
		{
			Image icon = base.GetIcon(node);
			if (icon != null)
				return icon;
			else if (node.IsLeaf)
				return this._leaf;
			else if (node.CanExpand && node.IsExpanded)
				return this._opened;
			else
				return this._closed;
		}
	}
}
