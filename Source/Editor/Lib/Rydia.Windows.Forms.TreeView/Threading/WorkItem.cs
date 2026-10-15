using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Aga.Controls.Threading
{
	public sealed class WorkItem
	{
		private WaitCallback _callback;
		private object _state;
		private ExecutionContext _ctx;

		internal WorkItem(WaitCallback wc, object state, ExecutionContext ctx)
		{
			this._callback = wc;
			this._state = state;
			this._ctx = ctx;
		}

		internal WaitCallback Callback
		{
			get
			{
				return this._callback;
			}
		}

		internal object State
		{
			get
			{
				return this._state;
			}
		}

		internal ExecutionContext Context
		{
			get
			{
				return this._ctx;
			}
		}
	}
}
