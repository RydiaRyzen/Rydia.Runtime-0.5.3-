using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;

using AdamsLair.WinForms.Drawing;
using AdamsLair.WinForms.PropertyEditing.Templates;

namespace AdamsLair.WinForms.PropertyEditing.Editors
{
	public class FlaggedEnumPropertyEditor : BitmaskPropertyEditor
	{
		public override object DisplayedValue
		{
			get { return Enum.ToObject(EditedType, Convert.ChangeType(BitmaskValue, Enum.GetUnderlyingType(EditedType))); }
		}
		protected override void OnEditedTypeChanged()
		{
			base.OnEditedTypeChanged();
			Items = Enum.GetNames(EditedType).Select(n => 
				new BitmaskItem((ulong)Convert.ToUInt64(Enum.Parse(EditedType, n)), n));
		}
	}
}
