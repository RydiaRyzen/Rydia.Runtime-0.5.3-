using System;
using System.Collections.Generic;
using System.Text;

namespace Rydia.SolutionBuilder
{

    /// <summary>
    /// アセンブリ参照を表します。
    /// </summary>
    public sealed class AssemblyReference
    {

        /// <summary>
        /// アセンブリ名を取得します
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// DLL ファイルへのパスを取得します
        /// </summary>
        /// <remarks>
        /// プロジェクトファイルからの相対パス、または絶対パスを指定できます。
        /// </remarks>
        public string HintPath { get; }

        /// <summary>
        /// 指定したアセンブリ名と DLL ファイルへのパスを使用して、
        /// <see cref="AssemblyReference"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="name">アセンブリ名。</param>
        /// <param name="hintPath">DLL ファイルへのパス。相対パスまたは絶対パスを指定します。</param>
        /// <exception cref="ArgumentException">
        /// <paramref name="name"/> または <paramref name="hintPath"/> が
        /// <see langword="null"/>、空、または空白文字のみの場合にスローされます。
        /// </exception>
        public AssemblyReference(string name, string hintPath)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(name);
            ArgumentException.ThrowIfNullOrWhiteSpace(hintPath);

            Name = name;
            HintPath = hintPath;
        }

    }

}
