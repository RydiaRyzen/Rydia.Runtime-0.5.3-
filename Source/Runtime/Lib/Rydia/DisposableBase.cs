using System;
using System.Collections.Generic;
using System.Text;

namespace Rydia
{

    /// <summary>
    /// 全ての破棄オブジェクトを表す既定クラスです
    /// </summary>
    /// <seealso cref="Rydia.IDisposableEx" />
    public abstract partial class DisposableBase : IDisposableEx
	{

        /// <summary>
        /// このオブジェクトが破棄されているかどうかを表す値を取得します
        /// </summary>
		public bool IsDisposed
		{
			get;
			private set;
		}

        /// <summary>
        ///   <see cref="DisposableBase"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
		protected DisposableBase()
		{

		}

		/// <summary>
		/// <see cref="DisposableBase"/> クラスのファイナライザです
		/// </summary>
		~DisposableBase()
		{
			// このコードを変更しないでください。クリーンアップ コードを 'Dispose(bool disposing)' メソッドに記述します
			Dispose(disposing: false);
		}

		/// <summary>
		/// Releases unmanaged and - optionally - managed resources.
		/// </summary>
		public void Dispose()
		{
			// このコードを変更しないでください。クリーンアップ コードを 'Dispose(bool disposing)' メソッドに記述します
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}

		protected void Dispose(bool disposing)
		{
			if (!IsDisposed)
			{
				// TODO: アンマネージド リソース (アンマネージド オブジェクト) を解放し、
				// TODO: ファイナライザーをオーバーライドします
				Disposing(disposing);
				IsDisposed = true;
				Disposed(disposing);
			}
		}

		/// <summary>
		/// 破棄処理中に呼び出されます
		/// </summary>
		protected abstract void Disposing(bool disposing);

		/// <summary>
		/// 破棄処理後に呼び出されます
		/// </summary>
		protected virtual void Disposed(bool disposing)
		{

		}

	}

}
