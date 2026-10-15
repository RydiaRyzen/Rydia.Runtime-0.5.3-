using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Reflection;
using Rydia.Windows.Forms.Core.Properties;

namespace AdamsLair.WinForms.Properties
{
	public static class ResourcesCache
	{
		public readonly	static Bitmap ImageAdd					= CoreResources.ImageAdd;
		public readonly	static Bitmap ImageDelete				= CoreResources.ImageDelete;
		public readonly	static Bitmap NumberGripIcon			= CoreResources.NumberGripIcon;
		public readonly	static Bitmap DropDownIcon				= CoreResources.DropDownIcon;
		public readonly	static Bitmap ExpandNodeClosedDisabled	= CoreResources.ExpandNodeClosedDisabled;
		public readonly	static Bitmap ExpandNodeClosedNormal	= CoreResources.ExpandNodeClosedNormal;
		public readonly	static Bitmap ExpandNodeClosedHot		= CoreResources.ExpandNodeClosedHot;
		public readonly	static Bitmap ExpandNodeClosedPressed	= CoreResources.ExpandNodeClosedPressed;
		public readonly	static Bitmap ExpandNodeOpenedDisabled	= CoreResources.ExpandNodeOpenedDisabled;
		public readonly	static Bitmap ExpandNodeOpenedNormal	= CoreResources.ExpandNodeOpenedNormal;
		public readonly	static Bitmap ExpandNodeOpenedHot		= CoreResources.ExpandNodeOpenedHot;
		public readonly	static Bitmap ExpandNodeOpenedPressed	= CoreResources.ExpandNodeOpenedPressed;
		public readonly	static Bitmap ArrowDown					= CoreResources.ArrowDown;
		public readonly	static Bitmap ArrowDownLeft				= CoreResources.ArrowDownLeft;
		public readonly	static Bitmap ArrowDownRight			= CoreResources.ArrowDownRight;
		public readonly	static Bitmap ArrowUp					= CoreResources.ArrowUp;
		public readonly	static Bitmap ArrowUpLeft				= CoreResources.ArrowUpLeft;
		public readonly	static Bitmap ArrowUpRight				= CoreResources.ArrowUpRight;
		public readonly	static Bitmap ArrowLeft					= CoreResources.ArrowLeft;
		public readonly	static Bitmap ArrowRight				= CoreResources.ArrowRight;

		public readonly static Font DefaultFont = SystemFonts.DefaultFont;
		public readonly static Font DefaultFontSmall = new Font(DefaultFont.FontFamily, DefaultFont.Size * 0.9f, DefaultFont.Unit);
		public readonly static Font DefaultFontBold = new Font(DefaultFont, FontStyle.Bold);
		public readonly static Font DefaultFontBoldSmall = new Font(DefaultFontSmall, FontStyle.Bold);
	}
}
