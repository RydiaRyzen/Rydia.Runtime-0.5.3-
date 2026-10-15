using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Aga.Controls.Tree.NodeControls
{
	public class EditEventArgs : NodeEventArgs
	{
		private Control _control;
		public Control Control
		{
			get { return this._control; }
		}

		public EditEventArgs(TreeNodeAdv node, Control control)
			: base(node)
		{
			this._control = control;
		}
	}
}
