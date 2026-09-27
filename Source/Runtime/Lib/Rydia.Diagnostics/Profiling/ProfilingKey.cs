using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rydia.Diagnostics.Profiling
{
    /// <summary>
    /// 
    /// </summary>
    public class ProfilingKey
    {

        private Dictionary<string, ProfilingKey> _Children = new Dictionary<string, ProfilingKey>();

        public IReadOnlyList<ProfilingKey> Children
        {
            get
            {
                return _Children.Values.ToList();
            }
        }

        public string Name
        {
            get;
            private set;
        }

        public ProfilingKey Parent
        {
            get;
            private set;
        }

        public ProfilingKey Root
        {
            get
            {
                return Parent == null ? this : Parent.Root;
            }
        }

        public string FullName
        {
            get
            {
                if (Parent == null)
                {
                    return Name;
                }
                return Parent.FullName + $@"\{Name}";
            }
        }

        public ProfilingKey(string name)
            : this(null, name)
        {
            
        }

        public ProfilingKey(ProfilingKey parent, string name)
        {
            Parent = parent;
            Name = name;
        }

        public ProfilingKey AddChild(string v)
        {
            if(!_Children.TryGetValue(v, out ProfilingKey value))
            {
                value = new ProfilingKey(this, v);
                _Children.Add(v, value);
            }
            return value;
        }

    }

}
