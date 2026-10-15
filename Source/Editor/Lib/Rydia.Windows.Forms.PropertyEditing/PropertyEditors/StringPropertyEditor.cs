using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;

using AdamsLair.WinForms.Drawing;
using AdamsLair.WinForms.PropertyEditing.Templates;

namespace AdamsLair.WinForms.PropertyEditing.Editors
{
	public class StringPropertyEditor : PropertyEditor
	{
		private	StringEditorTemplate	stringEditor	= null;
		private string	val				= null;
		private	bool	valMultiple		= false;

		public override object DisplayedValue
		{
			get { return Convert.ChangeType(this.val, EditedType); }
		}
		

		public StringPropertyEditor()
		{
			this.stringEditor = new StringEditorTemplate(this);
			this.stringEditor.Invalidate += stringEditor_Invalidate;
			this.stringEditor.Edited += stringEditor_Edited;
			this.stringEditor.EditingFinished += stringEditor_EditingFinished;

			//this.Height = 18;
		}
		protected override void OnParentEditorChanged()
		{
			base.OnParentEditorChanged();
			Height = 5 + (int)Math.Round((float)ControlRenderer.FontRegular.Height);
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
				this.val = (string)values.First();
				this.valMultiple = values.Any(o => o == null || (string)o != this.val);
			}

			this.stringEditor.Text = this.val;
			EndUpdate();
		}

		protected internal override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			this.stringEditor.OnPaint(e, Enabled, this.valMultiple);
		}
		protected internal override void OnGotFocus(EventArgs e)
		{
			base.OnGotFocus(e);
			this.stringEditor.OnGotFocus(e);
			this.stringEditor.Select();
		}
		protected internal override void OnLostFocus(EventArgs e)
		{
			base.OnLostFocus(e);
			this.stringEditor.OnLostFocus(e);
		}
		protected internal override void OnKeyPress(KeyPressEventArgs e)
		{
			base.OnKeyPress(e);
			this.stringEditor.OnKeyPress(e);
		}
		protected internal override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);
			this.stringEditor.OnKeyDown(e);
		}
		protected internal override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			this.stringEditor.OnMouseMove(e);
		}
		protected internal override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
			this.stringEditor.OnMouseLeave(e);
		}
		protected internal override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			this.stringEditor.OnMouseDown(e);
		}
		protected internal override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			this.stringEditor.OnMouseUp(e);
		}
		protected internal override void OnMouseDoubleClick(MouseEventArgs e)
		{
			base.OnMouseDoubleClick(e);
			this.stringEditor.OnMouseDoubleClick(e);
		}

		protected override void UpdateGeometry()
		{
			base.UpdateGeometry();
			this.stringEditor.Rect = new Rectangle(
				ClientRectangle.X + 1,
				ClientRectangle.Y + 1,
				ClientRectangle.Width - 2,
				ClientRectangle.Height - 1);
		}
		protected internal override void OnReadOnlyChanged()
		{
			base.OnReadOnlyChanged();
			this.stringEditor.ReadOnly = ReadOnly;
		}

		private void stringEditor_Invalidate(object sender, EventArgs e)
		{
			Invalidate();
		}
		private void stringEditor_Edited(object sender, EventArgs e)
		{
			if (IsUpdating) return;
			if (Disposed) return;

			this.val = this.stringEditor.Text;
			Invalidate();
			PerformSetValue();
			PerformGetValue();
		}
		private void stringEditor_EditingFinished(object sender, PropertyEditingFinishedEventArgs e)
		{
			OnEditingFinished(e.Reason);
		}
	}
}
