using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using Aga.Controls.Tree.NodeControls;
using System.Drawing.Imaging;
using System.Threading;

namespace Aga.Controls.Tree
{
	public partial class TreeViewAdv
	{
		#region Keys

		protected override bool IsInputChar(char charCode)
		{
			return true;
		}

		protected override bool IsInputKey(Keys keyData)
		{
			if (((keyData & Keys.Up) == Keys.Up)
				|| ((keyData & Keys.Down) == Keys.Down)
				|| ((keyData & Keys.Left) == Keys.Left)
				|| ((keyData & Keys.Right) == Keys.Right))
				return true;
			else
				return base.IsInputKey(keyData);
		}

		internal void ChangeInput()
		{
			if ((ModifierKeys & Keys.Shift) == Keys.Shift)
			{
				if (!(Input is InputWithShift))
					Input = new InputWithShift(this);
			}
			else if ((ModifierKeys & Keys.Control) == Keys.Control)
			{
				if (!(Input is InputWithControl))
					Input = new InputWithControl(this);
			}
			else
			{
				if (!(Input.GetType() == typeof(NormalInputState)))
					Input = new NormalInputState(this);
			}
		}

		protected override void OnKeyDown(KeyEventArgs e)
		{
			this._justGotFocus = false;
			base.OnKeyDown(e);
			if (!e.Handled)
			{
				if (e.KeyCode == Keys.ShiftKey || e.KeyCode == Keys.ControlKey)
					ChangeInput();
				Input.KeyDown(e);
				if (!e.Handled)
				{
					foreach (NodeControlInfo item in GetNodeControls(CurrentNode))
					{
						item.Control.KeyDown(e);
						if (e.Handled)
							break;
					}
				}
			}
		}

		protected override void OnKeyUp(KeyEventArgs e)
		{
			this._justGotFocus = false;
			base.OnKeyUp(e);
			if (!e.Handled)
			{
				if (e.KeyCode == Keys.ShiftKey || e.KeyCode == Keys.ControlKey)
					ChangeInput();
				if (!e.Handled)
				{
					foreach (NodeControlInfo item in GetNodeControls(CurrentNode))
					{
						item.Control.KeyUp(e);
						if (e.Handled)
							return;
					}
				}
			}
		}

		protected override void OnKeyPress(KeyPressEventArgs e)
		{
			this._justGotFocus = false;
			base.OnKeyPress(e);
			if (!e.Handled)
				this._search.Search(e.KeyChar);
		}

		#endregion

		#region Mouse

		private TreeNodeAdvMouseEventArgs CreateMouseArgs(MouseEventArgs e)
		{
			TreeNodeAdvMouseEventArgs args = new TreeNodeAdvMouseEventArgs(e);
			args.ViewLocation = new Point(e.X + OffsetX,
				e.Y + this._rowLayout.GetRowBounds(FirstVisibleRow).Y - ColumnHeaderHeight);
			args.ModifierKeys = ModifierKeys;
			args.Node = GetNodeAt(e.Location);
			NodeControlInfo info = GetNodeControlInfoAt(args.Node, e.Location);
			args.ControlBounds = info.Bounds;
			args.Control = info.Control;
			args.JustGotFocus = this._justGotFocus;
			return args;
		}

		protected override void OnMouseWheel(MouseEventArgs e)
		{
			this._search.EndSearch();
			if (SystemInformation.MouseWheelScrollLines > 0)
			{
				int lines = e.Delta / 120 * SystemInformation.MouseWheelScrollLines;
				int newValue = this._vScrollBar.Value - lines;
				newValue = Math.Min(this._vScrollBar.Maximum - this._vScrollBar.LargeChange + 1, newValue);
				newValue = Math.Min(this._vScrollBar.Maximum, newValue);
				this._vScrollBar.Value = Math.Max(this._vScrollBar.Minimum, newValue);
			}
			base.OnMouseWheel(e);
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			if (CurrentEditorOwner != null)
			{
				CurrentEditorOwner.EndEdit(true);
				return;
			}

			if (!Focused)
				Focus();

			this._search.EndSearch();
			if (e.Button == MouseButtons.Left)
			{
				TreeColumn c;
				c = GetColumnDividerAt(e.Location);
				if (c != null)
				{
					Input = new ResizeColumnState(this, c, e.Location);
					return;
				}
				c = GetColumnAt(e.Location);
				if (c != null)
				{
					Input = new ClickColumnState(this, c, e.Location);
					UpdateView();
					return;
				}
			}

			ChangeInput();
			TreeNodeAdvMouseEventArgs args = CreateMouseArgs(e);

			if (args.Node != null && args.Control != null)
				args.Control.MouseDown(args);

			if (!args.Handled)
				Input.MouseDown(args);

			base.OnMouseDown(e);
		}

		protected override void OnMouseClick(MouseEventArgs e)
		{
			//TODO: Disable when click on plusminus icon
			TreeNodeAdvMouseEventArgs args = CreateMouseArgs(e);
			if (args.Node != null)
				OnNodeMouseClick(args);

			base.OnMouseClick(e);
		}

		protected override void OnMouseDoubleClick(MouseEventArgs e)
		{
			TreeNodeAdvMouseEventArgs args = CreateMouseArgs(e);

			if (args.Node != null && args.Control != null)
				args.Control.MouseDoubleClick(args);

			if (!args.Handled)
			{
				if (args.Node != null)
					OnNodeMouseDoubleClick(args);
				else
					Input.MouseDoubleClick(args);

				if (!args.Handled)
				{
					if (args.Node != null && args.Button == MouseButtons.Left)
						args.Node.IsExpanded = !args.Node.IsExpanded;
				}
			}

			base.OnMouseDoubleClick(e);
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			TreeNodeAdvMouseEventArgs args = CreateMouseArgs(e);
			if (Input is ResizeColumnState)
				Input.MouseUp(args);
			else
			{
				if (args.Node != null && args.Control != null)
					args.Control.MouseUp(args);
				if (!args.Handled)
					Input.MouseUp(args);

				base.OnMouseUp(e);
			}
			this._justGotFocus = false;
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			if (Input.MouseMove(e))
				return;

			base.OnMouseMove(e);
			SetCursor(e);
			UpdateToolTip(e);
			if (ItemDragMode && Dist(e.Location, ItemDragStart) > ItemDragSensivity
				&& CurrentNode != null && CurrentNode.IsSelected)
			{
				ItemDragMode = false;
				this._toolTip.Active = false;
				OnItemDrag(e.Button, Selection.ToArray());
			}
		}

		protected override void OnMouseLeave(EventArgs e)
		{
			this._justGotFocus = false;
			this._hotColumn = null;
			UpdateHeaders();
			base.OnMouseLeave(e);
		}

		private void SetCursor(MouseEventArgs e)
		{
			TreeColumn col;
			col = GetColumnDividerAt(e.Location);
			if (col == null)
				this._innerCursor = null;
			else
			{
				if (col.Width == 0)
					this._innerCursor = ResourceHelper.DVSplitCursor;
				else
					this._innerCursor = Cursors.VSplit;
			}

			col = GetColumnAt(e.Location);
			if (col != this._hotColumn)
			{
				this._hotColumn = col;
				UpdateHeaders();
			}
		}

		internal TreeColumn GetColumnAt(Point p)
		{
			if (p.Y > ColumnHeaderHeight)
				return null;

			int x = -OffsetX;
			foreach (TreeColumn col in Columns)
			{
				if (col.IsVisible)
				{
					Rectangle rect = new Rectangle(x, 0, col.Width, ColumnHeaderHeight);
					x += col.Width;
					if (rect.Contains(p))
						return col;
				}
			}
			return null;
		}

		internal int GetColumnX(TreeColumn column)
		{
			int x = -OffsetX;
			foreach (TreeColumn col in Columns)
			{
				if (col.IsVisible)
				{
					if (column == col)
						return x;
					else
						x += col.Width;
				}
			}
			return x;
		}

		internal TreeColumn GetColumnDividerAt(Point p)
		{
			if (p.Y > ColumnHeaderHeight)
				return null;

			int x = -OffsetX;
			TreeColumn prevCol = null;
			Rectangle left, right;
			foreach (TreeColumn col in Columns)
			{
				if (col.IsVisible)
				{
					if (col.Width > 0)
					{
						left = new Rectangle(x, 0, DividerWidth / 2, ColumnHeaderHeight);
						right = new Rectangle(x + col.Width - (DividerWidth / 2), 0, DividerWidth / 2, ColumnHeaderHeight);
						if (left.Contains(p) && prevCol != null)
							return prevCol;
						else if (right.Contains(p))
							return col;
					}
					prevCol = col;
					x += col.Width;
				}
			}

			left = new Rectangle(x, 0, DividerWidth / 2, ColumnHeaderHeight);
			if (left.Contains(p) && prevCol != null)
				return prevCol;

			return null;
		}

		TreeColumn _tooltipColumn;
		private void UpdateToolTip(MouseEventArgs e)
		{
			TreeColumn col = GetColumnAt(e.Location);
			if (col != null)
			{
				if (col != this._tooltipColumn)
					SetTooltip(col.TooltipText);
			}
			else
				DisplayNodesTooltip(e);
			this._tooltipColumn = col;
		}

		TreeNodeAdv _hotNode;
		NodeControl _hotControl;
		private void DisplayNodesTooltip(MouseEventArgs e)
		{
			if (ShowNodeToolTips)
			{
				TreeNodeAdvMouseEventArgs args = CreateMouseArgs(e);
				if (args.Node != null && args.Control != null)
				{
					if (args.Node != this._hotNode || args.Control != this._hotControl)
						SetTooltip(GetNodeToolTip(args));
				}
				else
					this._toolTip.SetToolTip(this, null);

				this._hotControl = args.Control;
				this._hotNode = args.Node;
			}
			else
				this._toolTip.SetToolTip(this, null);
		}

		private void SetTooltip(string text)
		{
			if (!string.IsNullOrEmpty(text))
			{
				this._toolTip.Active = false;
				this._toolTip.SetToolTip(this, text);
				this._toolTip.Active = true;
			}
			else
				this._toolTip.SetToolTip(this, null);
		}

		private string GetNodeToolTip(TreeNodeAdvMouseEventArgs args)
		{
			string msg = args.Control.GetToolTip(args.Node);

			BaseTextControl btc = args.Control as BaseTextControl;
			if (btc != null && btc.DisplayHiddenContentInToolTip && string.IsNullOrEmpty(msg))
			{
				Size ms = btc.GetActualSize(args.Node, this._measureContext);
				if (ms.Width > args.ControlBounds.Size.Width || ms.Height > args.ControlBounds.Size.Height
					|| args.ControlBounds.Right - OffsetX > DisplayRectangle.Width)
					msg = btc.GetLabel(args.Node);
			}

			if (string.IsNullOrEmpty(msg) && DefaultToolTipProvider != null)
				msg = DefaultToolTipProvider.GetToolTip(args.Node, args.Control);

			return msg;
		}

		#endregion

		#region DragDrop

		private bool _dragAutoScrollFlag = false;
		private Bitmap _dragBitmap = null;
		private System.Threading.Timer _dragTimer;

		private void StartDragTimer()
		{
			if (this._dragTimer == null)
				this._dragTimer = new System.Threading.Timer(new TimerCallback(DragTimerTick), null, 0, 100);
		}

		private void StopDragTimer()
		{
			if (this._dragTimer != null)
			{
				this._dragTimer.Dispose();
				this._dragTimer = null;
			}
		}

		private void SetDropPosition(Point pt)
		{
			TreeNodeAdv node = GetNodeAt(pt);
			OnDropNodeValidating(pt, ref node);
			this._dropPosition.Node = node;
			if (node != null)
			{
				Rectangle first = this._rowLayout.GetRowBounds(FirstVisibleRow);
				Rectangle bounds = this._rowLayout.GetRowBounds(node.Row);
				float pos = (pt.Y + first.Y - ColumnHeaderHeight - bounds.Y) / (float)bounds.Height;
				if (pos < TopEdgeSensivity)
					this._dropPosition.Position = NodePosition.Before;
				else if (pos > (1 - BottomEdgeSensivity))
					this._dropPosition.Position = NodePosition.After;
				else
					this._dropPosition.Position = NodePosition.Inside;
			}
		}

		private void DragTimerTick(object state)
		{
			this._dragAutoScrollFlag = true;
		}

		private void DragAutoScroll()
		{
			this._dragAutoScrollFlag = false;
			if (this._vScrollBar.Minimum != this._vScrollBar.Maximum)
			{
				Point pt = PointToClient(MousePosition);
				if (pt.Y < 20 && this._vScrollBar.Value > 0)
					this._vScrollBar.Value--;
				else if (pt.Y > Height - 20 && this._vScrollBar.Value <= this._vScrollBar.Maximum - this._vScrollBar.LargeChange)
					this._vScrollBar.Value++;
			}
		}

		public void DoDragDropSelectedNodes(DragDropEffects allowedEffects)
		{
			if (SelectedNodes.Count > 0)
			{
				TreeNodeAdv[] nodes = new TreeNodeAdv[SelectedNodes.Count];
				SelectedNodes.CopyTo(nodes, 0);
				DoDragDrop(nodes, allowedEffects);
			}
		}

		private void CreateDragBitmap(IDataObject data)
		{
			if (UseColumns || !DisplayDraggingNodes)
				return;

			TreeNodeAdv[] nodes = data.GetData(typeof(TreeNodeAdv[])) as TreeNodeAdv[];
			if (nodes != null && nodes.Length > 0)
			{
				Rectangle rect = DisplayRectangle;
				Bitmap bitmap = new Bitmap(rect.Width, rect.Height);
				using (Graphics gr = Graphics.FromImage(bitmap))
				{
					gr.Clear(BackColor);
					DrawContext context = new DrawContext();
					context.Graphics = gr;
					context.Font = Font;
					context.Enabled = true;
					int y = 0;
					int maxWidth = 0;
					foreach (TreeNodeAdv node in nodes)
					{
						if (node.Tree == this)
						{
							int x = 0;
							int height = this._rowLayout.GetRowBounds(node.Row).Height;
							foreach (NodeControl c in NodeControls)
							{
								Size s = c.GetActualSize(node, context);
								if (!s.IsEmpty)
								{
									int width = s.Width;
									rect = new Rectangle(x, y, width, height);
									x += (width + 1);
									context.Bounds = rect;
									c.Draw(node, context);
								}
							}
							y += height;
							maxWidth = Math.Max(maxWidth, x);
						}
					}

					if (maxWidth > 0 && y > 0)
					{
						this._dragBitmap = new Bitmap(maxWidth, y, PixelFormat.Format32bppArgb);
						using (Graphics tgr = Graphics.FromImage(this._dragBitmap))
							tgr.DrawImage(bitmap, Point.Empty);
						BitmapHelper.SetAlphaChanelValue(this._dragBitmap, 150);
					}
					else
						this._dragBitmap = null;
				}
			}
		}

		protected override void OnDragOver(DragEventArgs drgevent)
		{
			ItemDragMode = false;
			Point pt = PointToClient(new Point(drgevent.X, drgevent.Y));
			if (this._dragAutoScrollFlag)
				DragAutoScroll();
			SetDropPosition(pt);
			UpdateView();
			base.OnDragOver(drgevent);
		}

		protected override void OnDragEnter(DragEventArgs drgevent)
		{
			this._search.EndSearch();
			DragMode = true;
			CreateDragBitmap(drgevent.Data);
			base.OnDragEnter(drgevent);
		}

		protected override void OnDragLeave(EventArgs e)
		{
			DragMode = false;
			UpdateView();
			base.OnDragLeave(e);
		}

		protected override void OnDragDrop(DragEventArgs drgevent)
		{
			DragMode = false;
			UpdateView();
			base.OnDragDrop(drgevent);
		}

		#endregion
	}
}
