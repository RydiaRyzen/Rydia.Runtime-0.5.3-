using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Aga.Controls.Tree
{
	public class SortedTreeModel: TreeModelBase
	{
		private ITreeModel _innerModel;
		public ITreeModel InnerModel
		{
			get { return this._innerModel; }
		}

		private IComparer _comparer;
		public IComparer Comparer
		{
			get { return this._comparer; }
			set 
			{
				this._comparer = value;
				OnStructureChanged(new TreePathEventArgs(TreePath.Empty));
			}
		}

		public SortedTreeModel(ITreeModel innerModel)
		{
			this._innerModel = innerModel;
			this._innerModel.NodesChanged += new EventHandler<TreeModelEventArgs>(_innerModel_NodesChanged);
			this._innerModel.NodesInserted += new EventHandler<TreeModelEventArgs>(_innerModel_NodesInserted);
			this._innerModel.NodesRemoved += new EventHandler<TreeModelEventArgs>(_innerModel_NodesRemoved);
			this._innerModel.StructureChanged += new EventHandler<TreePathEventArgs>(_innerModel_StructureChanged);
		}

		void _innerModel_StructureChanged(object sender, TreePathEventArgs e)
		{
			OnStructureChanged(e);
		}

		void _innerModel_NodesRemoved(object sender, TreeModelEventArgs e)
		{
			OnStructureChanged(new TreePathEventArgs(e.Path));
		}

		void _innerModel_NodesInserted(object sender, TreeModelEventArgs e)
		{
			OnStructureChanged(new TreePathEventArgs(e.Path));
		}

		void _innerModel_NodesChanged(object sender, TreeModelEventArgs e)
		{
			OnStructureChanged(new TreePathEventArgs(e.Path));
		}

		public override IEnumerable GetChildren(TreePath treePath)
		{
			if (Comparer != null)
			{
				ArrayList list = new ArrayList();
				IEnumerable res = InnerModel.GetChildren(treePath);
				if (res != null)
				{
					foreach (object obj in res)
						list.Add(obj);
					list.Sort(Comparer);
					return list;
				}
				else
					return null;
			}
			else
				return InnerModel.GetChildren(treePath);
		}

		public override bool IsLeaf(TreePath treePath)
		{
			return InnerModel.IsLeaf(treePath);
		}
	}
}
