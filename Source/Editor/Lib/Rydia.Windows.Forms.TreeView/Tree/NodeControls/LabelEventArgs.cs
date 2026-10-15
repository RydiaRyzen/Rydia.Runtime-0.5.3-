using System;
using System.Collections.Generic;
using System.Text;

namespace Aga.Controls.Tree.NodeControls
{
	public class LabelEventArgs : EventArgs
	{
		private object _subject;
		public object Subject
		{
			get { return this._subject; }
		}

		private string _oldLabel;
		public string OldLabel
		{
			get { return this._oldLabel; }
		}

		private string _newLabel;
		public string NewLabel
		{
			get { return this._newLabel; }
		}

		public LabelEventArgs(object subject, string oldLabel, string newLabel)
		{
			this._subject = subject;
			this._oldLabel = oldLabel;
			this._newLabel = newLabel;
		}
	}
}
