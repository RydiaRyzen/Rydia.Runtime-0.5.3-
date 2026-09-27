using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rydia;
using Rydia.Diagnostics;

namespace Rydia.Diagnostics
{


    /// <summary>
    /// 複数のロガーをグループとして管理するロガーです。
    /// </summary>
    public class GroupLogger : LoggerShelf
    {

        private Dictionary<string, GroupLoggerItem> loggers = new Dictionary<string, GroupLoggerItem>();

        /// <summary>
        /// ログのプレフィックスに使用する文字列の最大長を取得または設定します。
        /// </summary>
        public int PrefixLength
        {
            get;
            set;
        }

        /// <summary>
        /// <see cref="GroupLogger"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public GroupLogger()
        {
            PrefixLength = -1;
        }

        /// <summary>
        /// 指定された名前のロガーを取得します。
        /// ロガーが存在しない場合は、新しく作成して登録します。
        /// </summary>
        /// <param name="name">取得するロガーの名前です。</param>
        /// <returns>指定された名前の <see cref="ILogger"/> を返します。</returns>
        public ILogger GetLogger(string name)
        {
            if (!this.loggers.TryGetValue(name, out GroupLoggerItem logger))
            {
                logger = new GroupLoggerItem(this, name);
                var tempPrefixLength = Math.Max(PrefixLength, name.Length);
                if (tempPrefixLength != PrefixLength)
                {
                    PrefixLength = tempPrefixLength;
                    foreach (var kvp in this.loggers)
                    {
                        kvp.Value.SetPrefixLength(PrefixLength);
                    }
                    logger.SetPrefixLength(PrefixLength);
                }
                this.loggers.Add(name, logger);
            }
            return logger;
        }

        /// <summary>
        /// <see cref="GroupLogger"/> に所属する個別のロガーを表します。
        /// </summary>
        private class GroupLoggerItem : Logger
        {

            private string prefix;

            /// <summary>
            /// このロガーを管理している親の <see cref="GroupLogger"/> を取得します。
            /// </summary>
            public GroupLogger Parent
            {
                get;
                private set;
            }

            /// <summary>
            /// 指定された親ロガーと名前を使用して
            /// <see cref="GroupLoggerItem"/> クラスの新しいインスタンスを初期化します。
            /// </summary>
            /// <param name="parent">このロガーを管理する親の <see cref="GroupLogger"/> です。</param>
            /// <param name="name">ロガーの名前です。</param>
            public GroupLoggerItem(GroupLogger parent, string name)
                : base(name)
            {
                Shelf = Parent = parent;
            }

            /// <summary>
            /// ログ出力時に使用するプレフィックスを取得します。
            /// </summary>
            public override string Prefix
            {
                get
                {
                    return this.prefix;
                }
            }

            /// <summary>
            /// ログ出力時に使用するプレフィックスの幅を設定します。
            /// </summary>
            /// <param name="length">プレフィックスの幅です。</param>
            internal void SetPrefixLength(int length)
            {
                this.prefix = $"[{Module.PadRight(length)}]";
            }

        }


    }
}
