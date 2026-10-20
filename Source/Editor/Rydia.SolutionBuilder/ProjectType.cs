using System;
using System.Collections.Generic;
using System.Text;

namespace Rydia.SolutionBuilder
{

    /// <summary>
    /// プロジェクトの種類を定義します。
    /// </summary>
    public enum ProjectType
    {
        /// <summary>
        /// クラスライブラリを表します。
        /// </summary>
        ClassLibrary,

        /// <summary>
        /// 共有プロジェクトを表します。
        /// </summary>
        Shared,

        /// <summary>
        /// Windows アプリケーションを表します。
        /// </summary>
        WindowsApplication
    }

}
