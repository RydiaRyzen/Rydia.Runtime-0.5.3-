using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rydia.SolutionBuilder
{

    /// <summary>
    /// メソッドの定義を表します。
    /// </summary>
    public sealed class MethodDefinition
    {

        /// <summary>
        /// メソッド名を取得します。
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// メソッドの戻り値の型を取得します。
        /// </summary>
        public string ReturnType { get; }

        /// <summary>
        /// メソッド本体のソースコードを取得または設定します。
        /// </summary>
        public string Body { get; set; } = string.Empty;

        /// <summary>
        /// 指定したメソッド名と戻り値の型を使用して、
        /// <see cref="MethodDefinition"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="name">メソッド名。</param>
        /// <param name="returnType">メソッドの戻り値の型。省略した場合は <see langword="void"/> になります。</param>
        public MethodDefinition(
            string name,
            string returnType = "void")
        {
            Name = name;
            ReturnType = returnType;
        }
    }
}
