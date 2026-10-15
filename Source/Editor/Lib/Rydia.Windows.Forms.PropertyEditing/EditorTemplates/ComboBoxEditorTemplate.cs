using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;

using AdamsLair.WinForms.Drawing;
using ButtonState = AdamsLair.WinForms.Drawing.ButtonState;

namespace AdamsLair.WinForms.PropertyEditing.Templates
{
	public class ComboBoxEditorTemplate : EditorTemplate, IPopupControlHost
	{
		private const string ClipboardDataFormat = "ComboBoxEditorTemplateData";

		private	object				selectedObject	= null;
		private	string				selectedObjStr	= "";
		private	bool				pressed			= false;
		private	DateTime			mouseClosed		= DateTime.MinValue;
		private	int					dropdownHeight	= 100;
		private	ComboBoxDropDown	dropdown		= null;
		private	List<object>		dropdownItems	= new List<object>();
        private PopupControl		popupControl	= new PopupControl();

		public object SelectedObject
		{
			get { return this.selectedObject; }
			set
			{
				if (IsDropDownOpened) return; // Don't override while the user is selecting
				string lastObjStr = this.selectedObjStr;

				this.selectedObject = value;
				this.selectedObjStr = DefaultValueStringGenerator(this.selectedObject);
				if (this.dropdown != null) this.dropdown.SelectedItem = this.selectedObject;

				if (lastObjStr != this.selectedObjStr) EmitInvalidate();
			}
		}
		public bool IsDropDownOpened
		{
			get { return this.dropdown != null; }
		}
		public object DropDownHoveredObject
		{
			get
			{
				if (this.dropdown.HoveredIndex == -1) return null;
				return this.dropdown.Items[this.dropdown.HoveredIndex];
			}
		}
		public int DropDownHeight
		{
			get { return this.dropdownHeight; }
			set { this.dropdownHeight = value; }
		}
		public IEnumerable<object> DropDownItems
		{
			get { return this.dropdownItems; }
			set
			{
				this.dropdownItems = value.ToList();
				if (this.dropdown != null)
				{
					this.dropdown.Items.Clear();
					this.dropdown.Items.AddRange(this.dropdownItems.ToArray());
				}
			}
		}
		
		public ComboBoxEditorTemplate(PropertyEditor parent) : base(parent)
		{
			this.popupControl.PopupControlHost = this;
			this.popupControl.Closed += popupControl_Closed;
		}

		public void OnPaint(PaintEventArgs e, bool enabled, bool multiple)
		{
			ButtonState comboState = ButtonState.Normal;
			if (!enabled || ReadOnly)
				comboState = ButtonState.Disabled;
			else if (this.pressed || IsDropDownOpened || (this.focused && (Control.ModifierKeys & Keys.Control) == Keys.Control))
				comboState = ButtonState.Pressed;
			else if (this.hovered || this.focused)
				comboState = ButtonState.Hot;

			ControlRenderer.DrawComboBox(e.Graphics, this.rect, comboState, this.selectedObjStr);
		}
		public override void OnLostFocus(EventArgs e)
		{
			if (this.focused) EmitEditingFinished(this.selectedObject, FinishReason.LostFocus);			
			base.OnLostFocus(e);
		}
		public override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			if (ReadOnly) this.hovered = false;
		}
		public void OnMouseDown(MouseEventArgs e)
		{
			if (this.rect.Contains(e.Location))
			{
				if (this.hovered && (e.Button & MouseButtons.Left) != MouseButtons.None && (DateTime.Now - this.mouseClosed).TotalMilliseconds > 200)
				{
					this.pressed = true;
					EmitInvalidate();
				}
			}
		}
		public void OnMouseUp(MouseEventArgs e)
		{
			if (this.pressed && (e.Button & MouseButtons.Left) != MouseButtons.None)
			{
				if (this.hovered) ShowDropDown();
				this.pressed = false;
				EmitInvalidate();
			}
		}
		public void OnKeyUp(KeyEventArgs e)
		{
			if (e.KeyCode == Keys.ControlKey)
				EmitInvalidate();
		}
		public void OnKeyDown(KeyEventArgs e)
		{
			if (e.KeyCode == Keys.ControlKey)
				EmitInvalidate();

			if (e.KeyCode == Keys.Return || e.KeyCode == Keys.Right)
			{
				ShowDropDown();
				e.Handled = true;
			}
			else if (e.KeyCode == Keys.Down && e.Control)
			{
				ShowDropDown();
				//int index = this.dropdownItems.IndexOf(this.selectedObject);
				//this.selectedObject = this.dropdownItems[(index + 1) % this.dropdownItems.Count];
				//this.EmitEdited();
				e.Handled = true;
			}
			else if (e.KeyCode == Keys.Up && e.Control)
			{
				ShowDropDown();
				//int index = this.dropdownItems.IndexOf(this.selectedObject);
				//this.selectedObject = this.dropdownItems[(index + this.dropdownItems.Count - 1) % this.dropdownItems.Count];
				//this.EmitEdited();
				e.Handled = true;
			}
			else if (e.Control && e.KeyCode == Keys.C)
			{
				if (this.selectedObject != null)
				{
					DataObject data = new DataObject();
					data.SetText(this.selectedObjStr);
					data.SetData(ClipboardDataFormat, this.selectedObject);
					Clipboard.SetDataObject(data);
				}
				else
					Clipboard.Clear();
				e.Handled = true;
			}
			else if (e.Control && e.KeyCode == Keys.V)
			{
				bool success = false;
				if (Clipboard.ContainsData(ClipboardDataFormat) || Clipboard.ContainsText())
				{
					object pasteObjProxy = null;
					if (Clipboard.ContainsData(ClipboardDataFormat))
					{
						object pasteObj = Clipboard.GetData(ClipboardDataFormat);
						pasteObjProxy = this.dropdownItems.FirstOrDefault(obj => object.Equals(obj, pasteObj));
					}
					else if (Clipboard.ContainsText())
					{
						string pasteObj = Clipboard.GetText();
						pasteObjProxy = this.dropdownItems.FirstOrDefault(obj => obj != null && obj.ToString() == pasteObj);
					}
					if (pasteObjProxy != null)
					{
						if (this.selectedObject != pasteObjProxy)
						{
							this.selectedObject = pasteObjProxy;
							this.selectedObjStr = DefaultValueStringGenerator(this.selectedObject);
							EmitInvalidate();
							EmitEdited(this.selectedObject);
						}
						success = true;
					}
				}
				if (!success) System.Media.SystemSounds.Beep.Play();
				e.Handled = true;
			}
		}

		public void ShowDropDown()
		{
			if (this.dropdown != null) return;
			if (ReadOnly) return;
			PropertyGrid parentGrid = Parent.ParentGrid;

			this.dropdown = new ComboBoxDropDown(this.dropdownItems);
			this.dropdown.SelectedItem = this.selectedObject;

			Size dropDownSize = new Size(
				this.rect.Width, 
				Math.Min(this.dropdownHeight, this.dropdown.PreferredHeight));
			
			Point dropDownLoc = new Point(
				this.rect.X + parentGrid.AutoScrollPosition.X,
				this.rect.Y + parentGrid.AutoScrollPosition.Y);
			dropDownLoc.Y += this.rect.Height + 1;
			
			this.dropdown.Location = dropDownLoc;
			this.dropdown.Size = dropDownSize;
			this.dropdown.AcceptSelection += dropdown_AcceptSelection;
			this.dropdown.RequestClose += dropdown_RequestClose;

			this.popupControl.Show(parentGrid, this.dropdown, dropDownLoc.X, dropDownLoc.Y, dropDownSize.Width, dropDownSize.Height, PopupResizeMode.None);

			EmitInvalidate();
		}
		public void HideDropDown()
		{
			if (this.popupControl.Visible)
				this.popupControl.Hide();

			if (this.dropdown != null)
			{
				if (!this.dropdown.Disposing && !this.dropdown.IsDisposed)
					this.dropdown.Dispose();
				this.dropdown = null;
			}

			EmitInvalidate();
		}
		private void dropdown_AcceptSelection(object sender, EventArgs e)
		{
			if (this.selectedObject != this.dropdown.SelectedItem)
			{
				this.selectedObject = this.dropdown.SelectedItem;
				this.selectedObjStr = DefaultValueStringGenerator(this.selectedObject);
				EmitInvalidate();
				EmitEdited(this.selectedObject);
			}
		}
		private void dropdown_RequestClose(object sender, EventArgs e)
		{
			HideDropDown();
		}
		private void popupControl_Closed(object sender, ToolStripDropDownClosedEventArgs e)
		{
			HideDropDown();
			this.mouseClosed = DateTime.Now;
		}

		protected string DefaultValueStringGenerator(object obj)
		{
			return this.selectedObject != null ? this.selectedObject.ToString() : "";
		}
	}
}
