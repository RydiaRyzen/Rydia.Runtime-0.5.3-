using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rydia.SolutionBuilder
{

    /// <summary>
    /// C# プロパティの定義を表します。
    /// </summary>
    public sealed class PropertyDefinition
    {
        /// <summary>
        /// プロパティ名を取得します。
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// プロパティの型を取得します。
        /// </summary>
        public string Type { get; }

        /// <summary>
        /// <see cref="PropertyDefinition"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="name">
        /// プロパティ名。
        /// </param>
        /// <param name="type">
        /// プロパティの型。
        /// </param>
        public PropertyDefinition(
            string name,
            string type)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(name);
            ArgumentException.ThrowIfNullOrWhiteSpace(type);

            Name = name;
            Type = type;
        }
    }

}
