using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AdamsLair.WinForms.PropertyEditing.Templates
{
	public partial class ComboBoxDropDown : ListBox
	{
		private bool openedWithCtrl	= false;
		private int hoveredIndex = -1;

		public event EventHandler AcceptSelection = null;
		public event EventHandler RequestClose = null;

		public int HoveredIndex
		{
			get { return this.hoveredIndex; }
		}

		public ComboBoxDropDown()
		{
			IntegralHeight = false;
			Font = Font; // Prevents parent PopupControl from interfering on resize.
		}
		public ComboBoxDropDown(IEnumerable<object> items) : this()
		{
			Items.AddRange(items.ToArray());
		}

		protected void Accept()
		{
			if (AcceptSelection != null) AcceptSelection(this, EventArgs.Empty);
		}
		protected void Close()
		{
			if (RequestClose != null) RequestClose(this, EventArgs.Empty);
		}

		protected override void OnGotFocus(EventArgs e)
		{
			base.OnGotFocus(e);
			this.openedWithCtrl = (Control.ModifierKeys & Keys.Control) == Keys.Control;
		}
		protected override void OnClick(EventArgs e)
		{
			base.OnClick(e);
			if (SelectionMode == SelectionMode.One)
			{
				Accept();
				Close();
			}
		}
		protected override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);
			if (e.KeyCode == Keys.Return || e.KeyCode == Keys.Left)
			{
				Accept();
				Close();
			}
			else if (e.KeyCode == Keys.C && e.Control)
			{
				if (SelectedItem != null)
					Clipboard.SetText(SelectedItem.ToString());
			}
			else if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Escape)
			{
				Close();
			}
		}
		protected override void OnKeyUp(KeyEventArgs e)
		{
			base.OnKeyUp(e);
			if (e.KeyCode == Keys.ControlKey && this.openedWithCtrl)
			{
				Accept();
				Close();
			}
		}
		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			int clientY = e.Y - ClientRectangle.Y;
			this.hoveredIndex = (clientY / ItemHeight) + TopIndex;
			if (this.hoveredIndex < 0 || this.hoveredIndex >= Items.Count) this.hoveredIndex = -1;
		}
		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
			this.hoveredIndex = -1;
		}
		protected override void OnSelectedIndexChanged(EventArgs e)
		{
			base.OnSelectedIndexChanged(e);
			if (this.openedWithCtrl) Accept();
		}
	}
}
