using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rydia.Diagnostics
{
    public class FileLogListener : TextWriterLogListener
    {

        public static string LogfileDirName
        {
            get;
            set;
        }

        public static string DefaultLogfileDirName
        {
            get;
            private set;
        }

        static FileLogListener()
        {
            LogfileDirName = DefaultLogfileDirName = "Logs";
        }

        public FileLogListener(string path)
            : base(GetWriter(path))
        {

        }

        private static StreamWriter GetWriter(string path)
        {
            var prevLogfile = new FileInfo(path);
            var prevDir = prevLogfile.Directory;
            if(!prevDir.Exists)
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

        public void Close(LoggerShelf shelf)
        {
            shelf.RemoveListener(this);
            Target.Flush();
            Target.Close();
        }

    }

}
