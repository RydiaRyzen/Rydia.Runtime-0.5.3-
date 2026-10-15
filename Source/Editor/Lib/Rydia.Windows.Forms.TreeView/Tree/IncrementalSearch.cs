using System;
using System.Collections.Generic;
using System.Text;
using Aga.Controls.Tree.NodeControls;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Aga.Controls.Tree
{
	internal class IncrementalSearch
	{
		private const int SearchTimeout = 300; //end of incremental search timeot in msec

		private TreeViewAdv _tree;
		private TreeNodeAdv _currentNode;
		private string _searchString = "";
		private DateTime _lastKeyPressed = DateTime.Now;

		public IncrementalSearch(TreeViewAdv tree)
		{
			this._tree = tree;
		}

		public void Search(char value)
		{
			if (!char.IsControl(value))
			{
				char ch = char.ToLowerInvariant(value);
				DateTime dt = DateTime.Now;
				TimeSpan ts = dt - this._lastKeyPressed;
				this._lastKeyPressed = dt;
				if (ts.TotalMilliseconds < SearchTimeout)
				{
					if (this._searchString == value.ToString())
						FirstCharSearch(ch);
					else
						ContinuousSearch(ch);
				}
				else
				{
					FirstCharSearch(ch);
				}
			}
		}

		private void ContinuousSearch(char value)
		{
			if (value == ' ' && string.IsNullOrEmpty(this._searchString))
				return; //Ingnore leading space

			this._searchString += value;
			DoContinuousSearch();
		}

		private void FirstCharSearch(char value)
		{
			if (value == ' ')
				return;

			this._searchString = value.ToString();
			TreeNodeAdv node = null;
			if (this._tree.SelectedNode != null)
				node = this._tree.SelectedNode.NextVisibleNode;
			if (node == null)
				node = this._tree.Root.NextVisibleNode;

			if (node != null)
				foreach (string label in IterateNodeLabels(node))
				{
					if (label.StartsWith(this._searchString))
					{
						this._tree.SelectedNode = this._currentNode;
						return;
					}
				}
		}

		public virtual void EndSearch()
		{
			this._currentNode = null;
			this._searchString = "";
		}

		protected IEnumerable<string> IterateNodeLabels(TreeNodeAdv start)
		{
			this._currentNode = start;
			while(this._currentNode != null)
			{
				foreach (string label in GetNodeLabels(this._currentNode))
					yield return label;

				this._currentNode = this._currentNode.NextVisibleNode;
				if (this._currentNode == null)
					this._currentNode = this._tree.Root;

				if (start == this._currentNode)
					break;
			} 
		}

		private IEnumerable<string> GetNodeLabels(TreeNodeAdv node)
		{
			foreach (NodeControl nc in this._tree.NodeControls)
			{
				BindableControl bc = nc as BindableControl;
				if (bc != null && bc.IncrementalSearchEnabled)
				{
					object obj = bc.GetValue(node);
					if (obj != null)
						yield return obj.ToString().ToLowerInvariant();
				}
			}
		}

		private bool DoContinuousSearch()
		{
			bool found = false;
			if (!string.IsNullOrEmpty(this._searchString))
			{
				TreeNodeAdv node = null;
				if (this._tree.SelectedNode != null)
					node = this._tree.SelectedNode;
				if (node == null)
					node = this._tree.Root.NextVisibleNode;

				if (!string.IsNullOrEmpty(this._searchString))
				{
					foreach (string label in IterateNodeLabels(node))
					{
						if (label.StartsWith(this._searchString))
						{
							found = true;
							this._tree.SelectedNode = this._currentNode;
							break;
						}
					}
				}
			}
			return found;
		}

	}
}
