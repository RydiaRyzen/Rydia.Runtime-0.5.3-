using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Rydia.Diagnostics
{

	/// <summary>
	/// このクラスは、例外のいくつかのプロパティを格納するために使用されます。
	/// シリアライズ可能です。
	/// </summary>
	public sealed class ExceptionInfo
	{

		/// <summary>
		///   <see cref="ExceptionInfo"/>を初期化します
		/// </summary>
		public ExceptionInfo()
		{
		}

		/// <summary>
		/// <see cref = "Exception" />クラスから<see cref = "ExceptionInfo" />クラスの新しいインスタンスを初期化します。
		/// </summary>
		/// <param name="exception">このインスタンスのプロパティを初期化するために使用される例外です。</param>
		public ExceptionInfo(Exception exception)
		{
			if (exception == null)
				throw new ArgumentNullException("exception");
			BaseException = exception;
			BaseType = exception.GetType();
			Message = exception.Message;
			StackTrace = exception.StackTrace;
			TypeFullName = BaseType.FullName;
			TypeName = BaseType.Name;
			InnerException = exception.InnerException != null ? new ExceptionInfo(exception.InnerException) : null;
		}

		/// <summary>
		/// 元となる例外情報を取得します
		/// </summary>
		public Exception BaseException
		{
			get;
		}

		/// <summary>
		/// 元となる例外情報の型を取得します
		/// </summary>
		public Type BaseType
		{
			get;
		}

		/// <summary>
		/// 例外のメッセージを取得または設定します。
		/// </summary>
		public string Message { get; set; }

		/// <summary>
		/// 例外のスタックトレースを取得または設定します。
		/// </summary>
		public string StackTrace { get; set; }

		/// <summary>
		/// 例外の種類の完全な名前を取得または設定します。
		/// 例外タイプの<see cref = "Type.FullName" />プロパティに対応する必要があります。
		/// </summary>
		public string TypeFullName { get; set; }

		/// <summary>
		/// 例外の種類の名前を取得または設定します。
		/// 例外タイプの<see cref = "Type.Name" />プロパティに対応する必要があります。
		/// </summary>
		public string TypeName { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="ExceptionInfo"/> of the inner exception.
		/// </summary>
		public ExceptionInfo InnerException { get; set; }

		/// <summary>
		/// 現在のオブジェクトを表す文字列を返します。
		/// </summary>
		/// <returns>
		/// 現在のオブジェクトを表す文字列。
		/// </returns>
		public override string ToString()
		{
			var sb = new StringBuilder();
			sb.AppendLine(Message);
			if (StackTrace != null)
				sb.AppendLine(StackTrace);
			if (InnerException != null)
				sb.AppendFormat("Inner exception: {0}{1}", InnerException, Environment.NewLine);
			return sb.ToString();
		}

		/// <summary>
		///   <see cref="ExceptionInfo"/>から<see cref="string"/>への暗黙的なキャストを実装します
		/// </summary>
		/// <param name="info">info</param>
		/// <returns></returns>
		public static implicit operator string(ExceptionInfo info)
		{
			return info.ToString();
		}

        /// <summary>
        /// 指定されたパラメーターから例外情報を抽出します
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>A caller info or null if there is no caller information available.</returns>
        public static ExceptionInfo Extract(object[] parameters)
		{
			return parameters.Extract<ExceptionInfo>();
		}

	}


}
