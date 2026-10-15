using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;

using AdamsLair.WinForms.Drawing;
using ButtonState = AdamsLair.WinForms.Drawing.ButtonState;

namespace AdamsLair.WinForms.PropertyEditing.Templates
{
	public class MultiComboBoxEditorTemplate : EditorTemplate, IPopupControlHost
	{
		private const string ClipboardDataFormat = "MultiComboBoxEditorTemplateData";

		private	List<object>			selectedObjects	= new List<object>();
		private	string					selectedObjStr	= "";
		private	bool					pressed			= false;
		private	DateTime				mouseClosed		= DateTime.MinValue;
		private	int						dropdownHeight	= 100;
		private	MultiComboBoxDropDown	dropdown		= null;
		private	List<object>			dropdownItems	= new List<object>();
        private PopupControl			popupControl	= new PopupControl();

		public IEnumerable<object> SelectedObjects
		{
			get { return this.selectedObjects; }
			set
			{
				if (IsDropDownOpened) return; // Don't override while the user is selecting
				string lastObjStr = this.selectedObjStr;

				this.selectedObjects = value.ToList();
				this.selectedObjStr = DefaultValueStringGenerator(this.selectedObjects);
				if (this.dropdown != null)
				{
					for (int i = 0; i < this.dropdown.Items.Count; i++)
						this.dropdown.SetItemChecked(i, value.Contains(this.dropdown.Items[i]));
				}

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
		
		public MultiComboBoxEditorTemplate(PropertyEditor parent) : base(parent)
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
			if (this.focused) EmitEditingFinished(this.selectedObjects, FinishReason.LostFocus);			
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

			if (e.KeyCode == Keys.Return || e.KeyCode == Keys.Right || (e.KeyCode == Keys.Down && e.Control))
			{
				ShowDropDown();
				e.Handled = true;
			}
			else if (e.Control && e.KeyCode == Keys.C)
			{
				DataObject data = new DataObject();
				data.SetText(this.selectedObjStr);
				data.SetData(ClipboardDataFormat, this.selectedObjects);
				Clipboard.SetDataObject(data);
				e.Handled = true;
			}
			else if (e.Control && e.KeyCode == Keys.V)
			{
				bool success = false;
				List<object> pasteObjProxy = null; 
				if (Clipboard.ContainsData(ClipboardDataFormat))
				{
					List<object> pasteObj = Clipboard.GetData(ClipboardDataFormat) as List<object>;
					pasteObjProxy = pasteObj.Select(p => this.dropdownItems.FirstOrDefault(obj => object.Equals(obj, p))).Where(o => o != null).ToList(); 
				}
				else if (Clipboard.ContainsText())
				{
					string[] pasteObj = Clipboard.GetText().Split(new [] { ", " }, StringSplitOptions.RemoveEmptyEntries);
					pasteObjProxy = pasteObj.Select(p => this.dropdownItems.FirstOrDefault(obj => obj != null && p == obj.ToString())).ToList(); 
				}
				if (pasteObjProxy != null && pasteObjProxy.Count > 0)
				{
					this.selectedObjects = pasteObjProxy;
					this.selectedObjStr = DefaultValueStringGenerator(this.selectedObjects);
					EmitInvalidate();
					EmitEdited(this.selectedObjects);
					success = true;
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

			this.dropdown = new MultiComboBoxDropDown(this.dropdownItems);
			for (int i = 0; i < this.dropdown.Items.Count; i++)
				this.dropdown.SetItemChecked(i, this.selectedObjects.Contains(this.dropdown.Items[i]));

			Size dropDownSize = new Size(
				this.rect.Width, 
				Math.Min(this.dropdownHeight, this.dropdown.PreferredHeight));
			
			Point dropDownLoc = new Point(
				this.rect.X + parentGrid.AutoScrollPosition.X,
				this.rect.Y + parentGrid.AutoScrollPosition.Y);
			dropDownLoc.Y += this.rect.Height + 1;

			this.dropdown.Location = dropDownLoc;
			this.dropdown.Size = dropDownSize;
			this.dropdown.RequestClose += dropdown_RequestClose;
			this.dropdown.AcceptSelection += dropdown_AcceptSelection;

			this.popupControl.Show(parentGrid, this.dropdown, dropDownLoc.X, dropDownLoc.Y, dropDownSize.Width, dropDownSize.Height, PopupResizeMode.None);

			EmitInvalidate();
		}
		public void HideDropDown()
		{
			if (this.dropdown == null) return;
			if (!this.dropdown.Disposing && !this.dropdown.IsDisposed)
				this.dropdown.Dispose();
			this.dropdown = null;

			EmitInvalidate();
		}
		private void dropdown_RequestClose(object sender, EventArgs e)
		{
			HideDropDown();
		}
		private void dropdown_AcceptSelection(object sender, EventArgs e)
		{
			if (this.selectedObjects.Any(o => !this.dropdown.CheckedItems.Contains(o)) ||
				this.dropdown.CheckedItems.Cast<object>().Any(o => !this.selectedObjects.Contains(o)))
			{
				this.selectedObjects = this.dropdown.CheckedItems.Cast<object>().ToList();
				this.selectedObjStr = DefaultValueStringGenerator(this.selectedObjects);
				EmitInvalidate();
				EmitEdited(this.selectedObjects);
			}
		}
		private void popupControl_Closed(object sender, ToolStripDropDownClosedEventArgs e)
		{
			HideDropDown();
			this.mouseClosed = DateTime.Now;
		}

		protected string DefaultValueStringGenerator(IEnumerable<object> objEnum)
		{
			string valueString = "";
			foreach (object obj in objEnum)
				valueString += (valueString.Length > 0 ? ", " : "") + obj.ToString();
			return valueString;
		}
	}
}
