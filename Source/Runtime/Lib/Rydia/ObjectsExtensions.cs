using System;
using System.Collections.Generic;
using System.Text;

namespace Rydia
{
    public static class ObjectsExtensions
    {

        /// <summary>
        /// パラメーターから指定された型を抽出します。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameters">を表す値</param>
        /// <returns></returns>
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
