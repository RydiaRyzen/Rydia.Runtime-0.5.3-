using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;

using AdamsLair.WinForms.Drawing;

namespace AdamsLair.WinForms.PropertyEditing.Editors
{
	public class BoolPropertyEditor : PropertyEditor
	{
		private	CheckState	state	= CheckState.Unchecked;
		private	bool		hovered	= false;
		private	bool		pressed	= false;

		public override object DisplayedValue
		{
			get { return Convert.ChangeType(this.state == CheckState.Checked, EditedType); }
		}

		public BoolPropertyEditor()
		{
			//this.Height = 16;
		}
		protected override void OnParentEditorChanged()
		{
			base.OnParentEditorChanged();
			Height = Math.Max(13, 3 + (int)Math.Round((float)ControlRenderer.FontRegular.Height));
		}

		protected override void OnGetValue()
		{
			base.OnGetValue();
			BeginUpdate();
			CheckState lastState = this.state;
			object[] values = GetValue().ToArray();

			// Apply values to editors
			if (!values.Any())
				this.state = CheckState.Unchecked;
			else
			{
				int trueCount = values.Count(o => o != null && (bool)Convert.ToBoolean(o));
				if (!ReadOnly && (values.Any(o => o == null) || (trueCount > 0 && trueCount < values.Count())))
					this.state = CheckState.Indeterminate;
				else
					this.state = trueCount > 0 ? CheckState.Checked : CheckState.Unchecked;
			}

			EndUpdate();
			if (this.state != lastState) Invalidate();
		}
		protected override void OnSetValue()
		{
			if (this.state == CheckState.Indeterminate) return;
			base.OnSetValue();
		}

		protected void ToggleState()
		{
			if (this.state == CheckState.Checked)
				this.state = CheckState.Unchecked;
			else
				this.state = CheckState.Checked;

			Invalidate();
			PerformSetValue();
			PerformGetValue();
			OnEditingFinished(FinishReason.LeapValue);
		}
		protected void SetState(bool value)
		{
			this.state = value ? CheckState.Checked : CheckState.Unchecked;

			Invalidate();
			PerformSetValue();
			PerformGetValue();
			OnEditingFinished(FinishReason.LeapValue);
		}

		protected internal override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			
			CheckBoxState boxState = CheckBoxState.UncheckedNormal;
			bool boxEnabled = Enabled && !ReadOnly;
			bool hot = this.hovered | Focused;
			if (this.state == CheckState.Checked)
			{
				if (!boxEnabled)		boxState = CheckBoxState.CheckedDisabled;
				else if (this.pressed)	boxState = CheckBoxState.CheckedPressed;
				else if (hot)			boxState = CheckBoxState.CheckedHot;
				else					boxState = CheckBoxState.CheckedNormal;				
			}
			else if (this.state == CheckState.Unchecked)
			{
				if (!boxEnabled)		boxState = CheckBoxState.UncheckedDisabled;
				else if (this.pressed)	boxState = CheckBoxState.UncheckedPressed;
				else if (hot)			boxState = CheckBoxState.UncheckedHot;
				else					boxState = CheckBoxState.UncheckedNormal;	
			}
			else if (this.state == CheckState.Indeterminate)
			{
				if (!boxEnabled)		boxState = CheckBoxState.MixedDisabled;
				else if (this.pressed)	boxState = CheckBoxState.MixedPressed;
				else if (hot)			boxState = CheckBoxState.MixedHot;
				else					boxState = CheckBoxState.MixedNormal;	
			}
			
			Size boxSize = ControlRenderer.CheckBoxSize;
			Point boxLoc = new Point(
				ClientRectangle.X + 2,
				ClientRectangle.Y + ClientRectangle.Height / 2 - boxSize.Height / 2);
			ControlRenderer.DrawCheckBox(e.Graphics, boxLoc, boxState);
		}
		protected internal override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			bool lastHovered = this.hovered;
			this.hovered = !ReadOnly && ClientRectangle.Contains(e.Location);
			if (lastHovered != this.hovered) Invalidate();
		}
		protected internal override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
			if (this.hovered) Invalidate();
			this.hovered = false;
			this.pressed = false;
		}
		protected internal override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			if (this.hovered && (e.Button & MouseButtons.Left) != MouseButtons.None)
			{
				if (!this.pressed) Invalidate();
				this.pressed = true;
			}
		}
		protected internal override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			if ((e.Button & MouseButtons.Left) != MouseButtons.None)
			{
				if (this.hovered && this.pressed) ToggleState();
				if (this.pressed) Invalidate();
				this.pressed = false;
			}
		}
		protected internal override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);
			if (e.KeyCode == Keys.Return)
			{
				ToggleState();
				e.Handled = true;
			}
			else if (e.KeyCode == Keys.Delete)
			{
				SetState(false);
				e.Handled = true;
			}
			else if (e.Control && e.KeyCode == Keys.C)
			{
				Clipboard.SetText((this.state == CheckState.Checked).ToString());
				e.Handled = true;
			}
			else if (e.Control && e.KeyCode == Keys.V)
			{
				bool temp;
				if (bool.TryParse(Clipboard.GetText(), out temp))
					SetState(temp);
				e.Handled = true;
			}
		}
	}
}
