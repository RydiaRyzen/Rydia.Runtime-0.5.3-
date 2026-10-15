using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.Design;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing.Design;

namespace Aga.Controls.Tree.NodeControls
{
	internal class NodeControlsCollection : Collection<NodeControl>
	{
		private TreeViewAdv _tree;

		public NodeControlsCollection(TreeViewAdv tree)
		{
			this._tree = tree;
		}

		protected override void ClearItems()
		{
			this._tree.BeginUpdate();
			try
			{
				while (Count != 0)
					RemoveAt(Count - 1);
			}
			finally
			{
				this._tree.EndUpdate();
			}
		}

		protected override void InsertItem(int index, NodeControl item)
		{
			if (item == null)
				throw new ArgumentNullException("item");

			if (item.Parent != this._tree)
			{
				if (item.Parent != null)
				{
					item.Parent.NodeControls.Remove(item);
				}
				base.InsertItem(index, item);
				item.AssignParent(this._tree);
				this._tree.FullUpdate();
			}
		}

		protected override void RemoveItem(int index)
		{
			NodeControl value = this[index];
			value.AssignParent(null);
			base.RemoveItem(index);
			this._tree.FullUpdate();
		}

		protected override void SetItem(int index, NodeControl item)
		{
			if (item == null)
				throw new ArgumentNullException("item");

			this._tree.BeginUpdate();
			try
			{
				RemoveAt(index);
				InsertItem(index, item);
			}
			finally
			{
				this._tree.EndUpdate();
			}
		}
	}

	internal class NodeControlCollectionEditor : CollectionEditor
	{
		private Type[] _types;

		public NodeControlCollectionEditor(Type type)
			: base(type)
		{
			this._types = new Type[] { typeof(NodeTextBox), typeof(NodeIntegerTextBox), typeof(NodeDecimalTextBox), 
				typeof(NodeComboBox), typeof(NodeCheckBox),
				typeof(NodeStateIcon), typeof(NodeIcon), typeof(NodeNumericUpDown), typeof(ExpandingIcon)  };
		}

		protected override System.Type[] CreateNewItemTypes()
		{
			return this._types;
		}
	}
}
