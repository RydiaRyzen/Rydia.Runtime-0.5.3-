using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rydia.Diagnostics
{
    /// <summary>
    /// ファイルにログメッセージを書き込むログリスナーです。
    /// </summary>
    public class FileLogListener : TextWriterLogListener
    {

        /// <summary>
        /// ログファイルを格納するディレクトリ名を取得または設定します。
        /// </summary>
        public static string LogfileDirName
        {
            get;
            set;
        }

        /// <summary>
        /// ログファイルを格納する既定のディレクトリ名を取得します。
        /// </summary>
        public static string DefaultLogfileDirName
        {
            get;
            private set;
        }

        static FileLogListener()
        {
            LogfileDirName = DefaultLogfileDirName = "Logs";
        }

        /// <summary>
        /// 指定されたパスのログファイルにログを書き込む
        /// <see cref="FileLogListener"/> の新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="path">ログファイルのパスです。</param>
        public FileLogListener(string path)
            : base(GetWriter(path))
        {

        }

        /// <summary>
        /// 指定されたパスのログファイルを書き込むための
        /// <see cref="StreamWriter"/> を作成します。
        /// </summary>
        /// <param name="path">ログファイルのパスです。</param>
        /// <returns>
        /// ログファイルへの書き込みに使用する <see cref="StreamWriter"/> を返します。
        /// </returns>
        private static StreamWriter GetWriter(string path)
        {
            var prevLogfile = new FileInfo(path);
            var prevDir = prevLogfile.Directory;
            if (!prevDir.Exists)
                prevDir.Create();
            if (prevLogfile.Exists)
            {
                string timestampToken = prevLogfile.LastWriteTimeUtc.ToString("yyyy-MM-dd-T-HH-mm-ss");
                var temp = Path.GetFileNameWithoutExtension(path);
                string prevLogfileName = string.Format(temp + "_{0}.txt", timestampToken);
                string prevLogFilePath = Path.Combine(prevDir.FullName, prevLogfileName);

                prevLogfile.MoveTo(prevLogFilePath);
            }
            var result = new StreamWriter(path);
            result.AutoFlush = true;
            return result;
        }

        /// <summary>
        /// ログリスナーを閉じ、ログファイルへの書き込みを終了します。
        /// </summary>
        /// <param name="shelf">
        /// このログリスナーが登録されている <see cref="LoggerShelf"/> です。
        /// </param>
        public void Close(LoggerShelf shelf)
        {
            shelf.RemoveListener(this);
            Target.Flush();
            Target.Close();
        }

    }

}
