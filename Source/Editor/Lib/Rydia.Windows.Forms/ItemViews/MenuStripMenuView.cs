using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

using AdamsLair.WinForms.ItemModels;

namespace AdamsLair.WinForms.ItemViews
{
	public class MenuStripMenuView
	{
		private	ToolStripItemCollection						target				= null;
		private	IMenuModel									model				= null;
		private	Dictionary<IMenuModelItem,ToolStripItem>	viewItems			= new Dictionary<IMenuModelItem,ToolStripItem>();
		private	HashSet<ToolStripItem>						visibleSeparators	= new HashSet<ToolStripItem>();
		private	Comparison<IMenuModelItem>					comparison			= DefaultItemComparison;

		public event EventHandler<MenuStripMenuViewItemEventArgs> ItemInserted = null;
		public event EventHandler<MenuStripMenuViewItemEventArgs> ItemRemoved = null;

		public IMenuModel Model
		{
			get { return this.model; }
			set
			{
				if (this.model != value)
				{
					if (this.model != null)
					{
						this.model.ItemsAdded -= model_ItemsAdded;
						this.model.ItemsRemoved -= model_ItemsRemoved;
						this.model.ItemsChanged -= model_ItemsChanged;
						model_ItemsRemoved(this.model, new MenuModelItemsEventArgs(this.model.Items));
					}

					this.model = value;

					if (this.model != null)
					{
						model_ItemsAdded(this.model, new MenuModelItemsEventArgs(this.model.Items));
						this.model.ItemsAdded += model_ItemsAdded;
						this.model.ItemsRemoved += model_ItemsRemoved;
						this.model.ItemsChanged += model_ItemsChanged;
					}
				}
			}
		}
		public Comparison<IMenuModelItem> ItemSortComparison
		{
			get { return this.comparison; }
			set
			{
				value = value ?? DefaultItemComparison;
				if (this.comparison != value)
				{
					this.comparison = value;
					SortItems();
				}
			}
		}


		public MenuStripMenuView(ToolStripItemCollection target)
		{
			if (target == null) throw new ArgumentNullException("target");
			this.target = target;
		}
		
		public ToolStripItem GetViewItem(IMenuModelItem modelItem)
		{
			ToolStripItem viewItem;
			if (this.viewItems.TryGetValue(modelItem, out viewItem))
			{
				return viewItem;
			}
			else
			{
				return null;
			}
		}
		public IMenuModelItem GetModelItem(ToolStripItem viewItem)
		{
			return this.viewItems.Where(p => p.Value == viewItem).Select(p => p.Key).FirstOrDefault();
		}

		public void SortItems()
		{
			if (this.model == null) return;
			if (this.model.Items == null) return;

			IMenuModelItem[] items = this.model.Items.ToArray();
			model_ItemsRemoved(this.model, new MenuModelItemsEventArgs(items));
			model_ItemsAdded(this.model, new MenuModelItemsEventArgs(items));
		}

		protected virtual ToolStripItem CreateViewItem(IMenuModelItem modelItem)
		{
			switch (modelItem.TypeHint)
			{
				default:
				case MenuItemTypeHint.Item:			return new ToolStripMenuItem();
				case MenuItemTypeHint.Separator:	return new ToolStripSeparator();
			}
		}
		protected virtual void UpdateItemProperties(IMenuModelItem modelItem, ToolStripItem viewItem)
		{
			bool wasVisible = viewItem.Available;

			viewItem.Tag		= modelItem.Tag;
			viewItem.Image		= modelItem.Icon;
			viewItem.Name		= modelItem.Name;
			viewItem.Text		= modelItem.Name;
			viewItem.Available	= modelItem.Visible && (modelItem.TypeHint != MenuItemTypeHint.Separator || this.visibleSeparators.Contains(viewItem));
			viewItem.Enabled	= modelItem.Enabled;

			ToolStripMenuItem viewMenuItem = viewItem as ToolStripMenuItem;
			if (viewMenuItem != null)
			{
				viewMenuItem.ShortcutKeys	= modelItem.ShortcutKeys;
				viewMenuItem.CheckOnClick	= modelItem.Checkable;
				viewMenuItem.Checked		= modelItem.Checked;
			}

			if (wasVisible != viewItem.Available)
				UpdateSeparatorVisibility(GetTargetViewItemCollection(modelItem));
		}
		
		private void UpdateSeparatorVisibility(ToolStripItemCollection parentCollection)
		{
			this.visibleSeparators.Clear();
			bool separatorStreak = false;
			bool isFirstVisibleItem = true;
			int lastSeparatorIndex = -1;
			for (int i = 0; i < parentCollection.Count; i++)
			{
				ToolStripItem viewItem = parentCollection[i];
				IMenuModelItem modelItem = GetModelItem(viewItem);
				if (!modelItem.Visible) continue;

				bool isSeparator = modelItem.TypeHint == MenuItemTypeHint.Separator;

				if (isSeparator)
				{
					viewItem.Available = false;
					separatorStreak = true;
					lastSeparatorIndex = i;
				}
				else if (separatorStreak && !isFirstVisibleItem)
				{
					ToolStripItem lastViewItem = parentCollection[lastSeparatorIndex];
					IMenuModelItem lastModelItem = GetModelItem(lastViewItem);
					lastViewItem.Available = lastModelItem.Visible;
					this.visibleSeparators.Add(lastViewItem);
				}

				if (!isSeparator)
				{
					isFirstVisibleItem = false;
					separatorStreak = false;
				}
			}
		}
		private void InsertViewItem(ToolStripItemCollection parentCollection, ToolStripItem viewItem, IMenuModelItem modelItem)
		{
			ToolStripMenuItem viewMenuItem = viewItem as ToolStripMenuItem;

			// Determine where to insert the item
			int insertIndex = parentCollection.Count;
			for (int i = 0; i < parentCollection.Count; i++)
			{
				IMenuModelItem item = GetModelItem(parentCollection[i]);
				if (this.comparison(item, modelItem) > 0)
				{
					insertIndex = i;
					break;
				}
			}

			// Insert the item
			if (insertIndex < parentCollection.Count)
				parentCollection.Insert(insertIndex, viewItem);
			else
				parentCollection.Add(viewItem);

			this.viewItems.Add(modelItem, viewItem);
			viewItem.Click += viewMenuItem_Click;
			if (viewMenuItem != null)
			{
				viewMenuItem.CheckedChanged += viewMenuItem_CheckedChanged;
				viewMenuItem.DropDown.Closing += viewMenuItem_DropDown_Closing;
			}

			UpdateSeparatorVisibility(parentCollection);
			RaiseItemInserted(modelItem, viewItem);
		}
		private void RemoveViewItem(ToolStripItemCollection parentCollection, ToolStripItem viewItem, IMenuModelItem modelItem)
		{
			ToolStripMenuItem viewMenuItem = viewItem as ToolStripMenuItem;

			parentCollection.Remove(viewItem);
			this.viewItems.Remove(modelItem);
			viewItem.Click -= viewMenuItem_Click;
			if (viewMenuItem != null)
			{
				viewMenuItem.CheckedChanged -= viewMenuItem_CheckedChanged;
				viewMenuItem.DropDown.Closing -= viewMenuItem_DropDown_Closing;
			}

			UpdateSeparatorVisibility(parentCollection);
			RaiseItemRemoved(modelItem, viewItem);
		}
		private void CreateViewItems(ToolStripItemCollection parentCollection, IEnumerable<IMenuModelItem> modelItems)
		{
			foreach (IMenuModelItem modelItem in modelItems)
			{
				ToolStripItem viewItem = CreateViewItem(modelItem);
				UpdateItemProperties(modelItem, viewItem);

				InsertViewItem(parentCollection ?? GetTargetViewItemCollection(modelItem), viewItem, modelItem);

				ToolStripMenuItem viewMenuItem = viewItem as ToolStripMenuItem;
				if (viewMenuItem != null)
				{
					CreateViewItems(viewMenuItem.DropDownItems, modelItem.Items);
				}
			}
		}
		private void DestroyViewItems(ToolStripItemCollection parentCollection, IEnumerable<IMenuModelItem> modelItems)
		{
			foreach (IMenuModelItem modelItem in modelItems)
			{
				ToolStripItem viewItem;
				if (!this.viewItems.TryGetValue(modelItem, out viewItem)) continue;

				RemoveViewItem(parentCollection ?? GetTargetViewItemCollection(modelItem), viewItem, modelItem);

				ToolStripMenuItem viewMenuItem = viewItem as ToolStripMenuItem;
				if (viewMenuItem != null)
				{
					DestroyViewItems(viewMenuItem.DropDownItems, modelItem.Items);
				}

				viewItem.Dispose();
			}
		}
		private ToolStripItemCollection GetTargetViewItemCollection(IMenuModelItem modelItem)
		{
			if (modelItem.Parent == null) return this.target;

			ToolStripItem viewItemParent;
			if (!this.viewItems.TryGetValue(modelItem.Parent, out viewItemParent)) return this.target;

			ToolStripMenuItem viewMenuItem = viewItemParent as ToolStripMenuItem;
			if (viewMenuItem == null) return this.target;

			return viewMenuItem.DropDownItems;
		}
		
		private void RaiseItemInserted(IMenuModelItem modelItem, ToolStripItem viewItem)
		{
			if (ItemInserted != null)
				ItemInserted(this, new MenuStripMenuViewItemEventArgs(modelItem, viewItem, this));
		}
		private void RaiseItemRemoved(IMenuModelItem modelItem, ToolStripItem viewItem)
		{
			if (ItemRemoved != null)
				ItemRemoved(this, new MenuStripMenuViewItemEventArgs(modelItem, viewItem, this));
		}

		private void viewMenuItem_DropDown_Closing(object sender, ToolStripDropDownClosingEventArgs e)
		{
			if (e.CloseReason != ToolStripDropDownCloseReason.ItemClicked) return;

			ToolStripDropDownMenu menu = sender as ToolStripDropDownMenu;
			ToolStripItem viewItem = menu != null ? menu.GetItemAt(menu.PointToClient(Cursor.Position)) : null;
			if (viewItem == null) return;

			IMenuModelItem modelItem = GetModelItem(viewItem);
			if (modelItem == null) return;

			if (modelItem.Checkable) e.Cancel = true;
		}
		private void viewMenuItem_CheckedChanged(object sender, EventArgs e)
		{
			ToolStripMenuItem viewItem = sender as ToolStripMenuItem;
			if (viewItem == null) return;

			IMenuModelItem modelItem = GetModelItem(viewItem);
			if (modelItem != null)
			{
				modelItem.Checked = viewItem.Checked;
			}
		}
		private void viewMenuItem_Click(object sender, EventArgs e)
		{
			ToolStripItem viewItem = sender as ToolStripItem;
			if (viewItem == null) return;

			IMenuModelItem modelItem = GetModelItem(viewItem);
			if (modelItem != null)
			{
				modelItem.RaisePerformAction();
			}
		}
		private void model_ItemsAdded(object sender, MenuModelItemsEventArgs e)
		{
			CreateViewItems(null, e.Items);
		}
		private void model_ItemsRemoved(object sender, MenuModelItemsEventArgs e)
		{
			DestroyViewItems(null, e.Items);
		}
		private void model_ItemsChanged(object sender, MenuModelItemsEventArgs e)
		{
			foreach (IMenuModelItem modelItem in e.Items)
			{
				ToolStripItem viewItem;
				if (this.viewItems.TryGetValue(modelItem, out viewItem))
				{
					UpdateItemProperties(modelItem, viewItem);
					if (e.IsSortingAffected)
					{
						ToolStripItemCollection collection = null;
						ToolStripItem parentViewItem;
						if (modelItem.Parent != null && this.viewItems.TryGetValue(modelItem.Parent, out parentViewItem) && parentViewItem is ToolStripMenuItem)
						{
							collection = (parentViewItem as ToolStripMenuItem).DropDownItems;
						}
						else if (modelItem.Parent == null)
						{
							collection = this.target;
						}
						if (collection != null)
						{
							RemoveViewItem(collection, viewItem, modelItem);
							InsertViewItem(collection, viewItem, modelItem);
						}
					}
				}
			}
		}

		private static int DefaultItemComparison(IMenuModelItem itemA, IMenuModelItem itemB)
		{
			return itemA.SortValue - itemB.SortValue;
		}
	}
}
