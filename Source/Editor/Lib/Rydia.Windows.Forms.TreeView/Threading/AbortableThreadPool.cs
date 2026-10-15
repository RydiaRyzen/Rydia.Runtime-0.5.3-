// Stephen Toub
// stoub@microsoft.com

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Aga.Controls.Threading
{
	public class AbortableThreadPool
	{
		private LinkedList<WorkItem> _callbacks = new LinkedList<WorkItem>();
		private Dictionary<WorkItem, Thread> _threads = new Dictionary<WorkItem, Thread>();

		public WorkItem QueueUserWorkItem(WaitCallback callback)
		{
			return QueueUserWorkItem(callback, null);
		}

		public WorkItem QueueUserWorkItem(WaitCallback callback, object state)
		{
			if (callback == null) throw new ArgumentNullException("callback");

			WorkItem item = new WorkItem(callback, state, ExecutionContext.Capture());
			lock (this._callbacks)
			{
				this._callbacks.AddLast(item);
			}
			ThreadPool.QueueUserWorkItem(new WaitCallback(HandleItem));
			return item;
		}

		private void HandleItem(object ignored)
		{
			WorkItem item = null;
			try
			{
				lock (this._callbacks)
				{
					if (this._callbacks.Count > 0)
					{
						item = this._callbacks.First.Value;
						this._callbacks.RemoveFirst();
					}
					if (item == null)
						return;
					this._threads.Add(item, Thread.CurrentThread);

				}
				ExecutionContext.Run(item.Context,
					delegate { item.Callback(item.State); }, null);
			}
			finally
			{
				lock (this._callbacks)
				{
					if (item != null)
						this._threads.Remove(item);
				}
			}
		}

		public bool IsMyThread(Thread thread)
		{
			lock (this._callbacks)
			{
				foreach (Thread t in this._threads.Values)
				{
					if (t == thread)
						return true;
				}
				return false;
			}
		}

		public WorkItemStatus Cancel(WorkItem item, bool allowAbort)
		{
			if (item == null)
				throw new ArgumentNullException("item");
			lock (this._callbacks)
			{
				LinkedListNode<WorkItem> node = this._callbacks.Find(item);
				if (node != null)
				{
					this._callbacks.Remove(node);
					return WorkItemStatus.Queued;
				}
				else if (this._threads.ContainsKey(item))
				{
					if (allowAbort)
					{
						this._threads[item].Abort();
						this._threads.Remove(item);
						return WorkItemStatus.Aborted;
					}
					else
						return WorkItemStatus.Executing;
				}
				else
					return WorkItemStatus.Completed;
			}
		}

		public void CancelAll(bool allowAbort)
		{
			lock (this._callbacks)
			{
				this._callbacks.Clear();
				if (allowAbort)
				{
					foreach (Thread t in this._threads.Values)
						t.Abort();
				}
			}
		}
	}
}
