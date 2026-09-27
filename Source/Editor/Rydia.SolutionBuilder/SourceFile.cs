using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rydia.SolutionBuilder
{

    public abstract class SourceFile
    {
        public string Name { get; }

        /// <summary>
        /// クラスの名前空間を取得または設定します。
        /// </summary>
        public string? Namespace { get; set; }

        protected SourceFile(string name, string? @namespace)
        {
            Name = name;
            Namespace = @namespace;
        }

        public abstract string Generate();
    }

}
