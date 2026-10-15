using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Aga.Controls.Tree
{
	public class ListModel : TreeModelBase
	{
		private IList _list;

		public int Count
		{
			get { return this._list.Count; }
		}

		public ListModel()
		{
			this._list = new List<object>();
		}

		public ListModel(IList list)
		{
			this._list = list;
		}

		public override IEnumerable GetChildren(TreePath treePath)
		{
			return this._list;
		}

		public override bool IsLeaf(TreePath treePath)
		{
			return true;
		}

		public void AddRange(IEnumerable items)
		{
			foreach (object obj in items)
				this._list.Add(obj);
			OnStructureChanged(new TreePathEventArgs(TreePath.Empty));
		}

		public void Add(object item)
		{
			this._list.Add(item);
			OnNodesInserted(new TreeModelEventArgs(TreePath.Empty, new int[] { this._list.Count - 1 }, new object[] { item }));
		}

		public void Clear()
		{
			this._list.Clear();
			OnStructureChanged(new TreePathEventArgs(TreePath.Empty));
		}
	}
}
