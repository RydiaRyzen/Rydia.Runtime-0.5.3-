using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rydia
{

	/// <summary>
	/// <see cref="System.Enum"/>の高速化メソッドを定義するクラスです
	/// </summary>
	/// <typeparam name="TEnum">The type of the enum.</typeparam>
	public static class Enum<TEnum> where TEnum : struct
	{

		private static readonly List<TEnum> s_Values = Enum.GetValues(typeof(TEnum)).Cast<TEnum>().ToList();
		private static readonly int s_Count = s_Values.Count;

		/// <summary>
		/// 指定された型の配列を<see cref="TEnum"/>の要素分作成します
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static T[] CreateArray<T>()
		{
			return new T[Count];
		}

		/// <summary>
		/// <see cref="TEnum"/>を反復処理するデリゲートを処理します
		/// </summary>
		/// <param name="action">実行する処理</param>
		public static void ForEach(Action<TEnum> action)
		{
			for (int i = 0; i < s_Count; ++i)
				action(s_Values[i]);
		}

		/// <summary>
		/// <see cref="TEnum"/>の要素を反復処理するオブジェクトを取得します
		/// </summary>
		public static IEnumerable<TEnum> Values
		{
			get
			{
				return new List<TEnum>(s_Values);
			}
		}

		/// <summary>
		/// この<see cref="TEnum"/>の要素数を取得します
		/// </summary>
		public static int Count
		{
			get
			{
				return s_Count;
			}
		}

	}

}
