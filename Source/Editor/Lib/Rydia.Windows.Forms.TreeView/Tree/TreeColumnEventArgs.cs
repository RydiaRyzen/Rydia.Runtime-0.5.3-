using System;
using System.Collections.Generic;
using System.Text;

namespace Aga.Controls.Tree
{
	public class TreeColumnEventArgs: EventArgs
	{
		private TreeColumn _column;
		public TreeColumn Column
		{
			get { return this._column; }
		}

		public TreeColumnEventArgs(TreeColumn column)
		{
			this._column = column;
		}
	}
}
