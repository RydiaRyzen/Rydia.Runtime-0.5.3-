using System;
using System.Collections.Generic;
using System.Text;

namespace Rydia
{
    /// <summary>
    /// オブジェクトに関する拡張メソッドを提供するクラスです。
    /// </summary>
    public static class ObjectsExtensions
    {

        /// <summary>
        /// 指定されたパラメーターから、指定された型のオブジェクトを抽出します。
        /// </summary>
        /// <typeparam name="T">抽出するオブジェクトの型です。</typeparam>
        /// <param name="parameters">検索対象となるオブジェクトの配列です。</param>
        /// <returns>
        /// 指定された型に一致するオブジェクトを返します。
        /// 一致するオブジェクトが存在しない場合は <see langword="null"/> を返します。
        /// </returns>
        public static T Extract<T>(this object[] parameters) where T : class
        {
            if (parameters == null || parameters.Length <= 0)
            {
                return null;
            }
            foreach (var param in parameters)
            {
                if (param is T)
                    return param as T;
            }
            return null;
        }

    }
}
