using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rydia.SolutionBuilder
{

    /// <summary>
    /// ソースコードファイルの基底クラスです。
    /// </summary>
    public abstract class SourceFile
    {

        /// <summary>
        /// ソースコードファイルの名前を取得します。
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// クラスの名前空間を取得または設定します。
        /// </summary>
        public string? Namespace { get; set; }

        /// <summary>
        /// 指定した名前と名前空間で <see cref="SourceFile"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="name">ソースコードファイルの名前。</param>
        /// <param name="namespace">クラスの名前空間。名前空間を指定しない場合は <see langword="null"/> を指定します。</param>
        protected SourceFile(string name, string? @namespace)
        {
            Name = name;
            Namespace = @namespace;
        }

        /// <summary>
        /// ソースコードを生成します。
        /// </summary>
        /// <returns>生成されたソースコード。</returns>
        public abstract string Generate();

    }

}
