using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using System.Drawing;

namespace Aga.Controls.Tree
{
	public class Node
	{
		#region NodeCollection

		private class NodeCollection : Collection<Node>
		{
			private Node _owner;

			public NodeCollection(Node owner)
			{
				this._owner = owner;
			}

			protected override void ClearItems()
			{
				while (Count != 0)
					RemoveAt(Count - 1);
			}

			protected override void InsertItem(int index, Node item)
			{
				if (item == null)
					throw new ArgumentNullException("item");

				if (item.Parent != this._owner)
				{
					if (item.Parent != null)
						item.Parent.Nodes.Remove(item);
					item._parent = this._owner;
					item._index = index;
					for (int i = index; i < Count; i++)
						this[i]._index++;
					base.InsertItem(index, item);

					TreeModel model = this._owner.FindModel();
					if (model != null)
						model.OnNodeInserted(this._owner, index, item);
				}
			}

			protected override void RemoveItem(int index)
			{
				Node item = this[index];
				item._parent = null;
				item._index = -1;
				for (int i = index + 1; i < Count; i++)
					this[i]._index--;
				base.RemoveItem(index);

				TreeModel model = this._owner.FindModel();
				if (model != null)
					model.OnNodeRemoved(this._owner, index, item);
			}

			protected override void SetItem(int index, Node item)
			{
				if (item == null)
					throw new ArgumentNullException("item");

				RemoveAt(index);
				InsertItem(index, item);
			}
		}

		#endregion

		#region Properties

		private TreeModel _model;
		internal TreeModel Model
		{
			get { return this._model; }
			set { this._model = value; }
		}

		private NodeCollection _nodes;
		public Collection<Node> Nodes
		{
			get { return this._nodes; }
		}

		private Node _parent;
		public Node Parent
		{
			get { return this._parent; }
			set 
			{
				if (value != this._parent)
				{
					if (this._parent != null)
						this._parent.Nodes.Remove(this);

					if (value != null)
						value.Nodes.Add(this);
				}
			}
		}

		private int _index = -1;
		public int Index
		{
			get
			{
				return this._index;
			}
		}

		public Node PreviousNode
		{
			get
			{
				int index = Index;
				if (index > 0)
					return this._parent.Nodes[index - 1];
				else
					return null;
			}
		}

		public Node NextNode
		{
			get
			{
				int index = Index;
				if (index >= 0 && index < this._parent.Nodes.Count - 1)
					return this._parent.Nodes[index + 1];
				else
					return null;
			}
		}

		private string _text;
		public virtual string Text
		{
			get { return this._text; }
			set 
			{
				if (this._text != value)
				{
					this._text = value;
					NotifyModel();
				}
			}
		}

		private bool _hidden;
		public bool IsHidden
		{
			get { return this._hidden; }
			set 
			{
				if (this._hidden != value)
				{
					this._hidden = value;
					NotifyModel();
				}
			}
		}

		private CheckState _checkState;
		public virtual CheckState CheckState
		{
			get { return this._checkState; }
			set 
			{
				if (this._checkState != value)
				{
					this._checkState = value;
					NotifyModel();
				}
			}
		}

		private Image _image;
		public Image Image
		{
			get { return this._image; }
			set 
			{
				if (this._image != value)
				{
					this._image = value;
					NotifyModel();
				}
			}
		}

		private object _tag;
		public object Tag
		{
			get { return this._tag; }
			set { this._tag = value; }
		}

		public bool IsChecked
		{
			get 
			{ 
				return CheckState != CheckState.Unchecked;
			}
			set 
			{
				if (value)
					CheckState = CheckState.Checked;
				else
					CheckState = CheckState.Unchecked;
			}
		}

		public virtual bool IsLeaf
		{
			get
			{
				return false;
			}
		}

		#endregion

		public Node()
			: this(string.Empty)
		{
		}

		public Node(string text)
		{
			this._text = text;
			this._nodes = new NodeCollection(this);
		}

		public override string ToString()
		{
			return Text;
		}

		private TreeModel FindModel()
		{
			Node node = this;
			while (node != null)
			{
				if (node.Model != null)
					return node.Model;
				node = node.Parent;
			}
			return null;
		}

		protected void NotifyModel()
		{
			TreeModel model = FindModel();
			if (model != null && Parent != null) model.OnNodesChanged(Parent, Index, this);
		}
	}
}
