using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

using AdamsLair.WinForms.Drawing;

namespace AdamsLair.WinForms.ColorControls
{
	public class ColorShowBox : Control
	{
		private	ControlRenderer	renderer	= new ControlRenderer();
		private	Color			upperColor	= Color.Transparent;
		private	Color			lowerColor	= Color.Transparent;


		public event EventHandler UpperClick = null;
		public event EventHandler LowerClick = null;

		
		public ControlRenderer Renderer
		{
			get { return this.renderer; }
		}
		public Rectangle ColorAreaRectangle
		{
			get { return new Rectangle(
				ClientRectangle.X + 2,
				ClientRectangle.Y + 2,
				ClientRectangle.Width - 4,
				ClientRectangle.Height - 4); }
		}
		public Color Color
		{
			get { return this.upperColor; }
			set { this.upperColor = this.lowerColor = value; Invalidate(); }
		}
		public Color UpperColor
		{
			get { return this.upperColor; }
			set { this.upperColor = value; Invalidate(); }
		}
		public Color LowerColor
		{
			get { return this.lowerColor; }
			set { this.lowerColor = value; Invalidate(); }
		}


		public ColorShowBox()
		{
			DoubleBuffered = true;
			SetStyle(ControlStyles.UserPaint, true);
			SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			SetStyle(ControlStyles.Opaque, true);
			SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
			SetStyle(ControlStyles.ResizeRedraw, true);
		}
		
		protected void OnUpperClick()
		{
			if (UpperClick != null)
				UpperClick(this, null);
		}
		protected void OnLowerClick()
		{
			if (LowerClick != null)
				LowerClick(this, null);
		}

		protected override void OnMouseClick(MouseEventArgs e)
		{
			base.OnMouseClick(e);
			if (e.Y > (ClientRectangle.Top + ClientRectangle.Bottom) / 2)
				OnLowerClick();
			else
				OnUpperClick();
		}
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			Rectangle colorArea = ColorAreaRectangle;

			if (this.lowerColor.A < 255 || this.upperColor.A < 255)
				e.Graphics.FillRectangle(new HatchBrush(HatchStyle.LargeCheckerBoard, this.renderer.ColorLightBackground, this.renderer.ColorDarkBackground), colorArea);

			e.Graphics.FillRectangle(new SolidBrush(this.upperColor),
				colorArea.X,
				colorArea.Y,
				colorArea.Width,
				colorArea.Height / 2 + 1);
			e.Graphics.FillRectangle(new SolidBrush(this.lowerColor),
				colorArea.X,
				colorArea.Y + colorArea.Height / 2 + 1,
				colorArea.Width,
				colorArea.Height / 2);

			this.renderer.DrawBorder(e.Graphics, ClientRectangle, Drawing.BorderStyle.ContentBox, BorderState.Normal);
		}
	}
}
