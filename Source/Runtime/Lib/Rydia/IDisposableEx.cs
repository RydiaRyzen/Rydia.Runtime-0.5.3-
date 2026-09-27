using System;

namespace Rydia
{

    public interface IDisposableEx : IDisposable
	{

		/// <summary>
		/// このオブジェクトが破棄されているかどうかを表す値を取得します
		/// </summary>
		bool IsDisposed
		{
			get;
		}

	}

}
