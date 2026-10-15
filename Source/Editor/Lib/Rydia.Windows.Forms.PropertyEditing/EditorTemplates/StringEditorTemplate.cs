using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;

using AdamsLair.WinForms.Drawing;

namespace AdamsLair.WinForms.PropertyEditing.Templates
{
	public class StringEditorTemplate : EditorTemplate
	{
		private	string		text			= null;
		private	Timer		cursorTimer		= null;
		private	bool		cursorVisible	= false;
		private	int			cursorIndex		= 0;
		private	int			selectionLength	= 0;
		private int			scroll			= 0;
		private	bool		mouseSelect		= false;

		public string Text
		{
			get { return this.text; }
			set
			{
				if (Focused && !SelectedAll) return; // Don't override while the user is typing
				if (this.text != value)
				{
					int textLen = this.text != null ? this.text.Length : 0;
					bool allWasSelected = 
						!string.IsNullOrEmpty(this.text) && 
						Math.Min(this.cursorIndex, this.cursorIndex + this.selectionLength) <= 0 && 
						Math.Abs(this.selectionLength) >= textLen;

					this.text = value;
					textLen = this.text != null ? this.text.Length : 0;

					if (allWasSelected)
					{
						Select(); // Causes invalidate by itself
					}
					else
					{
						this.cursorIndex = Math.Min(this.cursorIndex, textLen);
						this.selectionLength = Math.Min(this.cursorIndex + this.selectionLength, textLen) - this.cursorIndex;
						this.selectionLength = Math.Max(this.cursorIndex + this.selectionLength, 0) - this.cursorIndex;
						EmitInvalidate();
					}
				}
			}
		}
		public string SelectedText
		{
			get
			{
				if (this.text == null) return null;
				int begin = Math.Min(this.cursorIndex, this.cursorIndex + this.selectionLength);
				return this.text.Substring(begin, Math.Abs(this.selectionLength));
			}
		}
		public int SelectionBegin
		{
			get { return Math.Min(this.cursorIndex, this.cursorIndex + this.selectionLength); }
		}
		public int SelectionLength
		{
			get { return Math.Abs(this.selectionLength); }
		}
		public bool SelectedAll
		{
			get
			{
				if (this.text == null) return true;
				return SelectionLength >= this.text.Length;
			}
		}

		public StringEditorTemplate(PropertyEditor parent) : base(parent) {}

		public void Select(int pos = 0, int length = -1)
		{
			if (pos == 0 && length == -1) length = this.text != null ? this.text.Length : 0;
			this.cursorIndex = pos + length;
			this.selectionLength = -length;
			EmitInvalidate();
		}
		public void Deselect()
		{
			this.selectionLength = 0;

			EmitInvalidate();
		}
		public void DeleteSelection()
		{
			if (this.selectionLength == 0) return;
			if (this.text == null) return;
			if (this.readOnly) return;

			int begin = Math.Min(this.cursorIndex, this.cursorIndex + this.selectionLength);
			this.text = this.text.Remove(begin, Math.Abs(this.selectionLength));
			this.selectionLength = 0;
			this.cursorIndex = begin;

			UpdateScroll();
			EmitInvalidate();
			EmitEdited(this.text);
		}
		public void InsertText(string insertText)
		{
			if (insertText == null) return;
			if (this.readOnly) return;

			if (this.text == null)
			{
				this.text = insertText;
				this.cursorIndex = 0;
			}
			else
			{
				int begin = Math.Min(this.cursorIndex, this.cursorIndex + this.selectionLength);
				this.text = 
					this.text.Substring(0, begin) + 
					insertText + 
					this.text.Substring(begin + Math.Abs(this.selectionLength), this.text.Length - begin - Math.Abs(this.selectionLength));
				this.cursorIndex = begin;
			}

			this.cursorIndex += insertText.Length;
			this.selectionLength = 0;

			UpdateScroll();
			EmitInvalidate();
			EmitEdited(this.text);
		}
		public void ShowCursor()
		{
			this.cursorTimer.Stop();
			this.cursorTimer.Start();
			this.cursorVisible = true;
			EmitInvalidate();
		}
		public void UpdateScroll()
		{
			int cursorPixelPos = ControlRenderer.GetCharPosTextField(
				this.rect,
				this.text,
				ControlRenderer.FontRegular,
				TextBoxStyle.Sunken,
				this.cursorIndex,
				this.scroll);
			if (cursorPixelPos - this.rect.X < 15 && this.scroll > 0)
			{
				this.scroll = Math.Max(this.scroll - (15 - cursorPixelPos + this.rect.X), 0);
				EmitInvalidate();
			}
			else if (cursorPixelPos - this.rect.X > this.rect.Width - 15)
			{
				this.scroll += (cursorPixelPos - this.rect.X) - (this.rect.Width - 15);
				EmitInvalidate();
			}
		}

		public void OnPaint(PaintEventArgs e, bool enabled, bool multiple)
		{
			TextBoxState textBoxState;

			if (!enabled)
				textBoxState = TextBoxState.Disabled;
			else if (this.focused)
				textBoxState = TextBoxState.Focus;
			else if (this.hovered)
				textBoxState = TextBoxState.Hot;
			else
				textBoxState = TextBoxState.Normal;

			if (this.readOnly)
				textBoxState |= TextBoxState.ReadOnlyFlag;

			ControlRenderer.DrawTextField(
				e.Graphics,
				this.rect,
				this.text,
				ControlRenderer.FontRegular,
				ControlRenderer.ColorText, 
				multiple ? ControlRenderer.ColorMultiple : ControlRenderer.ColorVeryLightBackground,
				textBoxState, 
				TextBoxStyle.Sunken,
				this.scroll,
				(this.selectionLength != 0 || this.cursorVisible) ? this.cursorIndex : -1,
				this.selectionLength);
		}

		public override void OnGotFocus(EventArgs e)
		{
			base.OnGotFocus(e);
			if (this.cursorTimer == null)
			{
				this.cursorTimer = new Timer();
				this.cursorTimer.Interval = 500;
				this.cursorTimer.Tick += cursorTimer_Tick;
				this.cursorTimer.Enabled = true;
				this.cursorVisible = true;
			}
		}
		public override void OnLostFocus(EventArgs e)
		{
			if (this.focused) EmitEditingFinished(this.text, FinishReason.LostFocus);			
			base.OnLostFocus(e);
			if (this.cursorTimer != null)
			{
				this.cursorTimer.Tick -= cursorTimer_Tick;
				this.cursorTimer.Dispose();
				this.cursorTimer = null;
				this.cursorVisible = false;
			}
			this.scroll = 0;
		}
		public void OnKeyPress(KeyPressEventArgs e)
		{
			if (char.IsControl(e.KeyChar)) return;
			InsertText(e.KeyChar.ToString());
			e.Handled = true;
		}
		public void OnKeyDown(KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				EmitEditingFinished(this.text, FinishReason.UserAccept);
				e.Handled = true;
			}
			else if (e.KeyCode == Keys.Delete)
			{
				if (!this.readOnly && this.selectionLength == 0 && this.text != null && this.cursorIndex < this.text.Length)
				{
					this.text = this.text.Remove(this.cursorIndex, 1);
					UpdateScroll();
					EmitInvalidate();
					EmitEdited(this.text);
				}
				else
					DeleteSelection();
				e.Handled = true;
			}
			else if (e.KeyCode == Keys.Back)
			{
				if (!this.readOnly && this.selectionLength == 0 && this.text != null && this.cursorIndex > 0)
				{
					this.text = this.text.Remove(this.cursorIndex - 1, 1);
					this.cursorIndex--;
					UpdateScroll();
					EmitInvalidate();
					EmitEdited(this.text);
				}
				else
					DeleteSelection();
				e.Handled = true;
			}
			else if (e.KeyCode == Keys.Left)
			{
				if (e.Shift)
				{
					if (this.cursorIndex > 0)
					{
						this.cursorIndex--;
						this.selectionLength = Math.Min(this.selectionLength + 1, this.text != null ? this.text.Length : 0);
						UpdateScroll();
						ShowCursor();
					}
				}
				else
				{
					if (this.selectionLength < 0) this.cursorIndex += this.selectionLength;
					this.cursorIndex = Math.Max(this.cursorIndex - 1, 0);
					this.selectionLength = 0;
					UpdateScroll();
					ShowCursor();
				}
				e.Handled = true;
			}
			else if (e.KeyCode == Keys.Right)
			{
				if (e.Shift)
				{
					if (this.cursorIndex < (this.text != null ? this.text.Length : 0))
					{
						this.cursorIndex++;
						this.selectionLength = Math.Max(this.selectionLength - 1, -this.cursorIndex);
						UpdateScroll();
						ShowCursor();
					}
				}
				else
				{
					if (this.selectionLength > 0) this.cursorIndex += this.selectionLength;
					this.cursorIndex = Math.Min(this.cursorIndex + 1, this.text != null ? this.text.Length : 0);
					this.selectionLength = 0;
					UpdateScroll();
					ShowCursor();
				}
				e.Handled = true;
			}
			else if (e.KeyCode == Keys.End)
			{
				if (e.Shift)
				{
					int oldSelEnd = this.cursorIndex + this.selectionLength;
					this.cursorIndex = (this.text != null ? this.text.Length : 0);
					this.selectionLength = oldSelEnd - this.cursorIndex;
					UpdateScroll();
					ShowCursor();
				}
				else
				{
					this.cursorIndex = (this.text != null ? this.text.Length : 0);
					this.selectionLength = 0;
					UpdateScroll();
					ShowCursor();
				}
				e.Handled = true;
			}
			else if (e.KeyCode == Keys.Home)
			{
				if (e.Shift)
				{
					int oldSelEnd = this.cursorIndex + this.selectionLength;
					this.cursorIndex = 0;
					this.selectionLength = oldSelEnd - this.cursorIndex;
					UpdateScroll();
					ShowCursor();
				}
				else
				{
					this.cursorIndex = 0;
					this.selectionLength = 0;
					UpdateScroll();
					ShowCursor();
				}
				e.Handled = true;
			}
			else if (e.KeyCode == Keys.A && e.Control)
			{
				Select();
				UpdateScroll();
				e.Handled = true;
			}
			else if (e.KeyCode == Keys.C && e.Control)
			{
				if (string.IsNullOrEmpty(SelectedText))
					Clipboard.Clear();
				else
					Clipboard.SetText(SelectedText);
				e.Handled = true;
			}
			else if (e.KeyCode == Keys.X && e.Control)
			{
				if (string.IsNullOrEmpty(SelectedText))
					Clipboard.Clear();
				else
					Clipboard.SetText(SelectedText);
				DeleteSelection();
				e.Handled = true;
			}
			else if (e.KeyCode == Keys.V && e.Control)
			{
				string newText = Clipboard.GetText();
				InsertText(newText);
				e.Handled = true;
			}
		}
		public void OnMouseDown(MouseEventArgs e)
		{
			if (!this.rect.Contains(e.Location)) return;

			// Pick char
			int pickedCharIndex;
			Point pickLoc = new Point(Math.Min(Math.Max(e.X, this.rect.X + 2), this.rect.Right - 2), this.rect.Y + this.rect.Height / 2);
			pickedCharIndex = ControlRenderer.PickCharTextField( 
				this.rect, 
				this.text,
				ControlRenderer.FontRegular,
				TextBoxStyle.Sunken,
				pickLoc,
				this.scroll);
			if (pickedCharIndex == -1) pickedCharIndex = this.text != null ? this.text.Length : 0;

			this.cursorIndex = pickedCharIndex;
			this.selectionLength = 0;
			UpdateScroll();
			EmitInvalidate();

			this.mouseSelect = true;
		}
		public void OnMouseUp(MouseEventArgs e)
		{
			this.mouseSelect = false;
		}
		public void OnMouseDoubleClick(MouseEventArgs e)
		{
			Select();
		}
		public override void OnMouseMove(MouseEventArgs e)
		{
			bool lastHovered = this.hovered;
			base.OnMouseMove(e);

			if (this.mouseSelect)
			{
				CurrentCursor = Cursors.IBeam;

				// Pick char
				int pickedCharIndex;
				Point pickLoc = new Point(Math.Min(Math.Max(e.X, this.rect.X + 2), this.rect.Right - 2), this.rect.Y + this.rect.Height / 2);
				pickedCharIndex = ControlRenderer.PickCharTextField(
					this.rect, 
					this.text,
					ControlRenderer.FontRegular,
					TextBoxStyle.Sunken,
					pickLoc,
					this.scroll);
				if (pickedCharIndex == -1) pickedCharIndex = this.text != null ? this.text.Length : 0;

				this.selectionLength = (this.cursorIndex + this.selectionLength) - pickedCharIndex;
				this.cursorIndex = pickedCharIndex;
				UpdateScroll();
				EmitInvalidate();
			}
			else if (this.hovered != lastHovered)
			{
				CurrentCursor = this.hovered && (Control.MouseButtons == MouseButtons.None) ? Cursors.IBeam : Cursors.Default;
			}
		}
		public override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
			CurrentCursor = Cursors.Default;
		}

		private void cursorTimer_Tick(object sender, EventArgs e)
		{
			if (this.selectionLength != 0) return;
			this.cursorVisible = !this.cursorVisible;
			EmitInvalidate();
		}
	}
}
