using System;
using System.Collections.Generic;
using System.Text;

namespace Aga.Controls.Tree.NodeControls
{
	public class NodeControlValueEventArgs : NodeEventArgs
	{
		private object _value;
		public object Value
		{
			get { return this._value; }
			set { this._value = value; }
		}

		public NodeControlValueEventArgs(TreeNodeAdv node)
			:base(node)
		{
		}
	}
}
