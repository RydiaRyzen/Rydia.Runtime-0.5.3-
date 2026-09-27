using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace Rydia.Diagnostics
{

    /// <summary>
    /// 発信者情報を表します
    /// </summary>
    public class CallerInfo
    {

        /// <summary>
        /// ファイルパスを取得します
        /// </summary>
        public string FilePath
        {
            get;
        }

        /// <summary>
        /// 行数を取得します
        /// </summary>
        public int LineNumber
        {
            get;
        }

        /// <summary>
        /// メンバー名を取得します
        /// </summary>
        public string MemberName
        {
            get;
        }

        /// <summary>
        /// クラス名を取得します
        /// </summary>
        public string ClassName
        {
            get;
        }

        private CallerInfo(string className, string filePath, int lineNumber, string memberName)
        {
            FilePath = filePath;
            LineNumber = lineNumber;
            MemberName = memberName;
            ClassName = className;
        }

        /// <summary>
        /// このメソッドを呼び出したコードの発信者情報を取得します
        /// </summary>
        /// <param name="filePath">指定しません</param>
        /// <param name="lineNumber">指定しません</param>
        /// <param name="methodName">指定しません</param>
        /// <returns></returns>
        public static CallerInfo Get<T>(
                [CallerFilePath] string filePath = "",
                [CallerLineNumber] int lineNumber = 0,
                [CallerMemberName] string methodName = "")
            where T : class
        {
            return new CallerInfo(typeof(T).Name, filePath, lineNumber, methodName);
        }

        /// <summary>
        /// パラメータから<see cref="CallerInfo"/>を抽出します
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>呼び出し元情報。呼び出し元情報がない場合は null を返します。</returns>
        public static CallerInfo Extract(object[] parameters)
		{
			return parameters.Extract<CallerInfo>();
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            var result = string.Format("{0}, {1}, {2}, {3}", ClassName, LineNumber, MemberName, FilePath);
            return result;
        }

    }
}
