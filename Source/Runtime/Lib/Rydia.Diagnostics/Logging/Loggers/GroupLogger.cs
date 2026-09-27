using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rydia;
using Rydia.Diagnostics;

namespace Rydia.Diagnostics
{


    public class GroupLogger : LoggerShelf
    {

        private Dictionary<string, GroupLoggerItem> loggers = new Dictionary<string, GroupLoggerItem>();
        
        public int PrefixLength
        {
            get;
            set;
        }

        public GroupLogger()
        {
            PrefixLength = -1;
        }

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

        private class GroupLoggerItem : Logger
        {

            private string prefix;

            public GroupLogger Parent
            {
                get;
                private set;
            }

            public GroupLoggerItem(GroupLogger parent, string name)
                : base(name)
            {
                Shelf = Parent = parent;
            }

            public override string Prefix
            {
                get
                {
                    return this.prefix;
                }
            }

            internal void SetPrefixLength(int length)
            {
                this.prefix = $"[{Module.PadRight(length)}]";
            }

        }


    }
}
