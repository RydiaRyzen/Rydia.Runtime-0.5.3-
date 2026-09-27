using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Rydia.Runtime
{
    /// <summary>
    /// コマンドライン引数を管理するクラスです
    /// </summary>
    public class CommandLineArgs
    {

        /// <summary>
        /// デバッグを有効にするコマンドライン引数を取得または設定します。
        /// </summary>
        public static string CmdArgDebug
        {
            get;
            set;
        }

        /// <summary>
        /// プロファイリングを有効にするコマンドライン引数を取得または設定します。
        /// </summary>
        public static string CmdArgProfiling
        {
            get;
            set;
        }

        /// <summary>
        /// <see cref="CommandLineArgs"/> の新しいインスタンスを初期化します。
        /// </summary>
        static CommandLineArgs()
        {
            CmdArgDebug = "debug";
            CmdArgProfiling = "profile";
            IsRunningDebugAssembly = GetIsRunningDebugAssembly();
        }

        /// <summary>
        /// <see cref="CommandLineArgs"/> の新しいインスタンスを初期化します。
        /// </summary>
        public CommandLineArgs()
        {
            var args = Environment.GetCommandLineArgs();
            if (!string.IsNullOrEmpty(CmdArgDebug))
            {
                IsDebug = args.Contains(CmdArgDebug);
            }
            if (!string.IsNullOrEmpty(CmdArgProfiling))
            {
                IsProfiling = args.Contains(CmdArgProfiling);
            }
            Args = args;
        }

        /// <summary>
        /// コマンドライン引数を格納している文字列配列を取得します。
        /// </summary>
        public string[] Args
        {
            get;
            private set;
        }

        /// <summary>
        /// コマンドラインにデバッグ指定があるかどうかを示す値を取得します。
        /// </summary>
        public bool IsDebug
        {
            get;
            private set;
        }

        /// <summary>
        /// コマンドラインにプロファイリング指定があるかどうかを示す値を取得します。
        /// </summary>
        public bool IsProfiling
        {
            get;
            private set;
        }

        /// <summary>
        /// 実行中のアセンブリがデバッグ用アセンブリかどうかを示す値を取得します。
        /// </summary>
        public static bool IsRunningDebugAssembly
        {
            get;
        }

        /// <summary>
        /// 実行中のアセンブリに <see cref="DebuggableAttribute"/> が設定され、
        /// 最適化が無効になっているかどうかを確認します。
        /// このメソッドは一度だけ呼び出されます。
        /// </summary>
        private static bool GetIsRunningDebugAssembly()
        {
            var entryAssembly = Assembly.GetEntryAssembly();
            if (entryAssembly != null)
            {
                var debuggableAttribute = entryAssembly.GetCustomAttributes<DebuggableAttribute>().FirstOrDefault();
                if (debuggableAttribute != null)
                {
                    return (debuggableAttribute.DebuggingFlags & DebuggableAttribute.DebuggingModes.DisableOptimizations) != 0;
                }
            }
            return false;
        }

    }
}
