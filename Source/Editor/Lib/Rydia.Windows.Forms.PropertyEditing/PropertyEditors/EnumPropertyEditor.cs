using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;

using AdamsLair.WinForms.Drawing;
using AdamsLair.WinForms.PropertyEditing.Templates;

namespace AdamsLair.WinForms.PropertyEditing.Editors
{
	public class EnumPropertyEditor : PropertyEditor, IPopupControlHost
	{
		private	ComboBoxEditorTemplate	stringSelector	= null;
		private Enum	val				= null;
		private	bool	valMultiple		= false;

		public override object DisplayedValue
		{
			get { return this.val != null ? Convert.ChangeType(this.val, EditedType) : EditedType.GetDefaultInstanceOf(); }
		}
		public bool IsDropDownOpened
		{
			get { return this.stringSelector.IsDropDownOpened; }
		}
		public string DropDownHoveredName
		{
			get { return this.stringSelector.DropDownHoveredObject as string; }
		}
		

		public EnumPropertyEditor()
		{
			this.stringSelector = new ComboBoxEditorTemplate(this);
			this.stringSelector.Invalidate += stringSelector_Invalidate;
			this.stringSelector.Edited += stringSelector_Edited;

			//this.Height = 18;
		}
		protected override void OnParentEditorChanged()
		{
			base.OnParentEditorChanged();
			Height = 5 + (int)Math.Round((float)ControlRenderer.FontRegular.Height);
		}
		
		public void ShowDropDown()
		{
			this.stringSelector.ShowDropDown();
		}
		public void HideDropDown()
		{
			this.stringSelector.HideDropDown();
		}
		protected override void OnGetValue()
		{
			base.OnGetValue();
			BeginUpdate();
			Enum[] values = GetValue().Select(o => o is Enum ? (Enum)o : null).ToArray();
			IEnumerable<Enum> valuesNotNull = values.Where(o => o != null);

			// Apply values to editors
			if (!valuesNotNull.Any())
			{
				this.val = null;
			}
			else
			{
				Enum firstVal = valuesNotNull.First();

				this.val = firstVal;
				this.valMultiple = values.Any(o => o == null) || !values.All(o => Enum.Equals(o, firstVal));
			}

			this.stringSelector.SelectedObject = this.val != null ? this.val.ToString() : null;
			EndUpdate();
		}

		protected internal override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			this.stringSelector.OnPaint(e, Enabled, this.valMultiple);
		}
		protected internal override void OnGotFocus(EventArgs e)
		{
			base.OnGotFocus(e);
			this.stringSelector.OnGotFocus(e);
		}
		protected internal override void OnLostFocus(EventArgs e)
		{
			base.OnLostFocus(e);
			this.stringSelector.OnLostFocus(e);
		}
		protected internal override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			this.stringSelector.OnMouseMove(e);
		}
		protected internal override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
			this.stringSelector.OnMouseLeave(e);
		}
		protected internal override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			this.stringSelector.OnMouseDown(e);
		}
		protected internal override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			this.stringSelector.OnMouseUp(e);
		}
		protected internal override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);
			this.stringSelector.OnKeyDown(e);
		}
		protected internal override void OnKeyUp(KeyEventArgs e)
		{
			base.OnKeyUp(e);
			this.stringSelector.OnKeyUp(e);
		}

		protected override void UpdateGeometry()
		{
			base.UpdateGeometry();
			this.stringSelector.Rect = new Rectangle(
				ClientRectangle.X + 1,
				ClientRectangle.Y + 1,
				ClientRectangle.Width - 2,
				ClientRectangle.Height - 1);
		}
		protected internal override void OnReadOnlyChanged()
		{
			base.OnReadOnlyChanged();
			this.stringSelector.ReadOnly = ReadOnly;
		}
		protected override void OnEditedTypeChanged()
		{
			base.OnEditedTypeChanged();
			this.stringSelector.DropDownItems = Enum.GetNames(EditedType);
		}

		private void stringSelector_Invalidate(object sender, EventArgs e)
		{
			Invalidate();
		}
		private void stringSelector_Edited(object sender, EventArgs e)
		{
			if (IsUpdating) return;
			if (Disposed) return;

			object selection = this.stringSelector.SelectedObject;
			if (selection == null) return;

			this.val = (Enum)Enum.Parse(EditedType, selection.ToString());
			Invalidate();
			PerformSetValue();
			PerformGetValue();
			OnEditingFinished(FinishReason.LeapValue);
		}
	}
}
