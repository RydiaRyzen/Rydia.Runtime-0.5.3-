using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;

using AdamsLair.WinForms.Drawing;
using AdamsLair.WinForms.PropertyEditing.Templates;

namespace AdamsLair.WinForms.PropertyEditing.Editors
{
	public class NumericPropertyEditor : PropertyEditor
	{
		private	NumericEditorTemplate	numEditor = null;
		private	decimal	val			= 0m;
		private	bool	valMultiple	= false;

		public override object DisplayedValue
		{
			get { return Convert.ChangeType(this.val, EditedType); }
		}
		public decimal Minimum
		{
			get { return this.numEditor.Minimum; }
			set
			{
				this.numEditor.Minimum = value;
				UpdateHeight();
			}
		}
		public decimal Maximum
		{
			get { return this.numEditor.Maximum; }
			set
			{
				this.numEditor.Maximum = value;
				UpdateHeight();
			}
		}
		public decimal ValueBarMinimum
		{
			get { return this.numEditor.ValueBarMinimum; }
			set { this.numEditor.ValueBarMinimum = value; }
		}
		public decimal ValueBarMaximum
		{
			get { return this.numEditor.ValueBarMaximum; }
			set { this.numEditor.ValueBarMaximum = value; }
		}
		public decimal Increment
		{
			get { return this.numEditor.Increment; }
			set
			{
				this.numEditor.Increment = value;
				UpdateHeight();
			}
		}
		public int DecimalPlaces
		{
			get { return this.numEditor.DecimalPlaces; }
			set { this.numEditor.DecimalPlaces = value; }
		}


		public NumericPropertyEditor()
		{
			this.numEditor = new NumericEditorTemplate(this);
			this.numEditor.Invalidate += numEditor_Invalidate;
			this.numEditor.Edited += numEditor_Edited;
			this.numEditor.EditingFinished += numEditor_EditingFinished;

			//this.Height = 18;
		}
		protected override void OnParentEditorChanged()
		{
			base.OnParentEditorChanged();
			UpdateHeight();
		}


		protected override void OnGetValue()
		{
			base.OnGetValue();
			BeginUpdate();
			object[] values = GetValue().ToArray();

			// Apply values to editors
			if (!values.Any())
				this.val = 0m;
			else
			{
				this.val = values.Any(o => o != null) ? values.Where(o => o != null).Average(o => SafeToDecimal(o)) : 0m;
				this.valMultiple = values.Any(o => o == null) || !values.All(o => SafeToDecimal(o) == this.val);
			}

			this.numEditor.Value = this.val;
			EndUpdate();
		}
		
		protected internal override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			this.numEditor.OnPaint(e, Enabled, this.valMultiple);
		}
		protected internal override void OnGotFocus(EventArgs e)
		{
			base.OnGotFocus(e);
			this.numEditor.OnGotFocus(e);
			this.numEditor.Select();
		}
		protected internal override void OnLostFocus(EventArgs e)
		{
			base.OnLostFocus(e);
			this.numEditor.OnLostFocus(e);
		}
		protected internal override void OnKeyPress(KeyPressEventArgs e)
		{
			base.OnKeyPress(e);
			this.numEditor.OnKeyPress(e);
		}
		protected internal override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);
			this.numEditor.OnKeyDown(e);
		}
		protected internal override void OnKeyUp(KeyEventArgs e)
		{
			base.OnKeyUp(e);
			this.numEditor.OnKeyUp(e);
		}
		protected internal override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			this.numEditor.OnMouseMove(e);
		}
		protected internal override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
			this.numEditor.OnMouseLeave(e);
		}
		protected internal override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			this.numEditor.OnMouseDown(e);
		}
		protected internal override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			this.numEditor.OnMouseUp(e);
		}
		protected internal override void OnMouseDoubleClick(MouseEventArgs e)
		{
			base.OnMouseDoubleClick(e);
			this.numEditor.OnMouseDoubleClick(e);
		}

		private void UpdateHeight()
		{
			if (ControlRenderer == null) return;

			bool showMinMaxBar = false;
			if (ValueBarMinimum > decimal.MinValue / 2 && ValueBarMaximum < decimal.MaxValue / 2)
			{
				showMinMaxBar = (float)((ValueBarMaximum - ValueBarMinimum) / Increment) < 10000.0f;
			}

			int prefHeight = 5 + (int)Math.Round((float)ControlRenderer.FontRegular.Height);
			if (showMinMaxBar) prefHeight += 3;
			Height = prefHeight;
			this.numEditor.ShowMinMaxBar = showMinMaxBar;
		}
		protected override void UpdateGeometry()
		{
			base.UpdateGeometry();
			this.numEditor.Rect = new Rectangle(
				ClientRectangle.X + 1,
				ClientRectangle.Y + 1,
				ClientRectangle.Width - 2,
				ClientRectangle.Height - 1);
		}
		protected internal override void OnReadOnlyChanged()
		{
			base.OnReadOnlyChanged();
			this.numEditor.ReadOnly = ReadOnly;
		}
		protected override void OnEditedTypeChanged()
		{
			base.OnEditedTypeChanged();
			if (EditedType == typeof(byte))
			{
				this.numEditor.DecimalPlaces = 0;
				this.numEditor.Minimum = byte.MinValue;
				this.numEditor.Maximum = byte.MaxValue;
			}
			else if (EditedType == typeof(sbyte))
			{
				this.numEditor.DecimalPlaces = 0;
				this.numEditor.Minimum = sbyte.MinValue;
				this.numEditor.Maximum = sbyte.MaxValue;
			}
			else if (EditedType == typeof(short))
			{
				this.numEditor.DecimalPlaces = 0;
				this.numEditor.Minimum = short.MinValue;
				this.numEditor.Maximum = short.MaxValue;
			}
			else if (EditedType == typeof(ushort))
			{
				this.numEditor.DecimalPlaces = 0;
				this.numEditor.Minimum = ushort.MinValue;
				this.numEditor.Maximum = ushort.MaxValue;
			}
			else if (EditedType == typeof(int))
			{
				this.numEditor.DecimalPlaces = 0;
				this.numEditor.Minimum = int.MinValue;
				this.numEditor.Maximum = int.MaxValue;
			}
			else if (EditedType == typeof(uint))
			{
				this.numEditor.DecimalPlaces = 0;
				this.numEditor.Minimum = uint.MinValue;
				this.numEditor.Maximum = uint.MaxValue;
			}
			else if (EditedType == typeof(long))
			{
				this.numEditor.DecimalPlaces = 0;
				this.numEditor.Minimum = long.MinValue;
				this.numEditor.Maximum = long.MaxValue;
			}
			else if (EditedType == typeof(ulong))
			{
				this.numEditor.DecimalPlaces = 0;
				this.numEditor.Minimum = ulong.MinValue;
				this.numEditor.Maximum = ulong.MaxValue;
			}
			else if (EditedType == typeof(float))
			{
				this.numEditor.DecimalPlaces = 2;
				this.numEditor.Increment = 0.1m;
				this.numEditor.Minimum = decimal.MinValue;
				this.numEditor.Maximum = decimal.MaxValue;
			}
			else if (EditedType == typeof(double))
			{
				this.numEditor.DecimalPlaces = 2;
				this.numEditor.Minimum = decimal.MinValue;
				this.numEditor.Maximum = decimal.MaxValue;
			}
			else if (EditedType == typeof(decimal))
			{
				this.numEditor.DecimalPlaces = 2;
				this.numEditor.Minimum = decimal.MinValue;
				this.numEditor.Maximum = decimal.MaxValue;
			}
		}

		private void numEditor_Edited(object sender, EventArgs e)
		{
			if (IsUpdating) return;
			if (Disposed) return;

			this.val = this.numEditor.Value;
			PerformSetValue();
			PerformGetValue();
		}
		private void numEditor_Invalidate(object sender, EventArgs e)
		{
			Invalidate();
		}
		private void numEditor_EditingFinished(object sender, PropertyEditingFinishedEventArgs e)
		{
			OnEditingFinished(e.Reason);
			PerformGetValue();
		}

		private static decimal SafeToDecimal(object o)
		{
			double v = Convert.ToDouble(o);
			if (double.IsNaN(v))
				return decimal.Zero;
			else if (v <= (double)decimal.MinValue || double.IsNegativeInfinity(v))
				return decimal.MinValue;
			else if (v >= (double)decimal.MaxValue || double.IsPositiveInfinity(v))
				return decimal.MaxValue;
			else
				return (decimal)v;
		}
	}
}
