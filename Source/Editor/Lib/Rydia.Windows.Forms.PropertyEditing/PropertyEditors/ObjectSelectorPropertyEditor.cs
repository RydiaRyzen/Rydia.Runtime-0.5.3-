using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;

using AdamsLair.WinForms.Drawing;
using AdamsLair.WinForms.PropertyEditing.Templates;

namespace AdamsLair.WinForms.PropertyEditing.Editors
{
	public class ObjectSelectorPropertyEditor : PropertyEditor, IPopupControlHost
	{
		private	ComboBoxEditorTemplate	objSelector	= null;
		private object	val			= null;
		private	bool	valMultiple	= false;

		public override object DisplayedValue
		{
			get { return this.val; }
		}
		public IEnumerable<ObjectItem> Items
		{
			get { return this.objSelector.DropDownItems.Cast<ObjectItem>(); }
			set 
			{
				this.objSelector.DropDownItems = value;
				UpdateEditorValue();
			}
		}
		public bool IsDropDownOpened
		{
			get { return this.objSelector.IsDropDownOpened; }
		}
		public ObjectItem DropDownHoveredObject
		{
			get { return this.objSelector.DropDownHoveredObject as ObjectItem; }
		}
		

		public ObjectSelectorPropertyEditor()
		{
			this.objSelector = new ComboBoxEditorTemplate(this);
			this.objSelector.Invalidate += objSelector_Invalidate;
			this.objSelector.Edited += objSelector_Edited;

			//this.Height = 18;
		}
		protected override void OnParentEditorChanged()
		{
			base.OnParentEditorChanged();
			Height = 5 + (int)Math.Round((float)ControlRenderer.FontRegular.Height);
		}
		
		public void ShowDropDown()
		{
			this.objSelector.ShowDropDown();
		}
		public void HideDropDown()
		{
			this.objSelector.HideDropDown();
		}
		protected override void OnGetValue()
		{
			base.OnGetValue();
			BeginUpdate();
			object[] values = GetValue().ToArray();

			// Apply values to editors
			if (!values.Any())
				this.val = null;
			else
			{
				this.val = values.Where(o => o != null).FirstOrDefault();
				this.valMultiple = values.Any(o => o == null) || !values.All(o => object.Equals(o, this.val));
			}

			UpdateEditorValue();
			EndUpdate();
		}

		private void UpdateEditorValue()
		{
			this.objSelector.SelectedObject = this.objSelector.DropDownItems.Cast<ObjectItem>().FirstOrDefault(i => object.Equals(i.Value, this.val));
		}

		protected internal override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			this.objSelector.OnPaint(e, Enabled, this.valMultiple);
		}
		protected internal override void OnGotFocus(EventArgs e)
		{
			base.OnGotFocus(e);
			this.objSelector.OnGotFocus(e);
		}
		protected internal override void OnLostFocus(EventArgs e)
		{
			base.OnLostFocus(e);
			this.objSelector.OnLostFocus(e);
		}
		protected internal override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			this.objSelector.OnMouseMove(e);
		}
		protected internal override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
			this.objSelector.OnMouseLeave(e);
		}
		protected internal override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			this.objSelector.OnMouseDown(e);
		}
		protected internal override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			this.objSelector.OnMouseUp(e);
		}
		protected internal override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);
			this.objSelector.OnKeyDown(e);
		}
		protected internal override void OnKeyUp(KeyEventArgs e)
		{
			base.OnKeyUp(e);
			this.objSelector.OnKeyUp(e);
		}

		protected override void UpdateGeometry()
		{
			base.UpdateGeometry();
			this.objSelector.Rect = new Rectangle(
				ClientRectangle.X + 1,
				ClientRectangle.Y + 1,
				ClientRectangle.Width - 2,
				ClientRectangle.Height - 1);
		}
		protected internal override void OnReadOnlyChanged()
		{
			base.OnReadOnlyChanged();
			this.objSelector.ReadOnly = ReadOnly;
		}

		private void objSelector_Invalidate(object sender, EventArgs e)
		{
			Invalidate();
		}
		private void objSelector_Edited(object sender, EventArgs e)
		{
			if (IsUpdating) return;
			if (Disposed) return;

			this.val = (this.objSelector.SelectedObject as ObjectItem).Value;
			Invalidate();
			PerformSetValue();
			PerformGetValue();
			OnEditingFinished(FinishReason.LeapValue);
		}
	}
}
