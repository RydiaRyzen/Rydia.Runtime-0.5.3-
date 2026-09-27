using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

// Although not entirely equal, Generic Operators in Rydia are heavily inspired by the ones implemented in 
// MiscUtil by Jon Skeet, see here: http://www.yoda.arachsys.com/csharp/miscutil/usage/genericoperators.html

namespace Rydia
{
    /// <summary>
    /// 汎用型に対する数学演算を提供するクラスです。
    /// 演算は各型が最初に使用された時点で動的に解決されます。
    /// </summary>
    public static class GenericOperator
    {

        /// <summary>
        /// 2 つの値を加算します。
        /// </summary>
        /// <typeparam name="T">演算結果の型です。</typeparam>
        /// <typeparam name="U">2 番目のオペランドの型です。</typeparam>
        /// <param name="first">最初のオペランドです。</param>
        /// <param name="second">2 番目のオペランドです。</param>
        public static T Add<T, U>(T first, U second)
        {
            return DualType<T, U>.Add(first, second);
        }

        /// <summary>
        /// 2 つの値を減算します。
        /// </summary>
        /// <typeparam name="T">演算結果の型です。</typeparam>
        /// <typeparam name="U">2 番目のオペランドの型です。</typeparam>
        /// <param name="first">最初のオペランドです。</param>
        /// <param name="second">2 番目のオペランドです。</param>
        public static T Subtract<T, U>(T first, U second)
        {
            return DualType<T, U>.Subtract(first, second);
        }

        /// <summary>
        /// 2 つの値を乗算します。
        /// </summary>
        /// <typeparam name="T">演算結果の型です。</typeparam>
        /// <typeparam name="U">2 番目のオペランドの型です。</typeparam>
        /// <param name="first">最初のオペランドです。</param>
        /// <param name="second">2 番目のオペランドです。</param>
        public static T Multiply<T, U>(T first, U second)
        {
            return DualType<T, U>.Multiply(first, second);
        }

        /// <summary>
        /// 2 つの値を除算します。
        /// </summary>
        /// <typeparam name="T">演算結果の型です。</typeparam>
        /// <typeparam name="U">2 番目のオペランドの型です。</typeparam>
        /// <param name="first">最初のオペランドです。</param>
        /// <param name="second">2 番目のオペランドです。</param>
        public static T Divide<T, U>(T first, U second)
        {
            return DualType<T, U>.Divide(first, second);
        }

        /// <summary>
        /// 2 つの値の剰余を計算します。
        /// </summary>
        /// <typeparam name="T">オペランドの型です。</typeparam>
        /// <param name="first">最初のオペランドです。</param>
        /// <param name="second">2 番目のオペランドです。</param>
        public static T Modulo<T>(T first, T second)
        {
            return SingleType<T>.Modulo(first, second);
        }

        /// <summary>
        /// 指定された値の符号を反転します。
        /// </summary>
        /// <typeparam name="T">値の型です。</typeparam>
        /// <param name="value">符号を反転する値です。</param>
        public static T Negate<T>(T value)
        {
            return SingleType<T>.Negate(value);
        }

        /// <summary>
        /// 指定された値の絶対値を計算します。
        /// </summary>
        /// <typeparam name="T">値の型です。</typeparam>
        /// <param name="value">絶対値を計算する値です。</param>
        public static T Abs<T>(T value)
        {
            return SingleType<T>.Abs(value);
        }

        /// <summary>
        /// 2 つの値に対してビットごとの OR 演算を実行します。
        /// </summary>
        /// <typeparam name="T">値の型です。</typeparam>
        /// <param name="first">最初のオペランドです。</param>
        /// <param name="second">2 番目のオペランドです。</param>
        public static T Or<T>(T first, T second)
        {
            return SingleType<T>.Or(first, second);
        }

        /// <summary>
        /// 2 つの値に対してビットごとの AND 演算を実行します。
        /// </summary>
        /// <typeparam name="T">値の型です。</typeparam>
        /// <param name="first">最初のオペランドです。</param>
        /// <param name="second">2 番目のオペランドです。</param>
        public static T And<T>(T first, T second)
        {
            return SingleType<T>.And(first, second);
        }

        /// <summary>
        /// 2 つの値に対してビットごとの排他的 OR 演算を実行します。
        /// </summary>
        /// <typeparam name="T">値の型です。</typeparam>
        /// <param name="first">最初のオペランドです。</param>
        /// <param name="second">2 番目のオペランドです。</param>
        public static T Xor<T>(T first, T second)
        {
            return SingleType<T>.Xor(first, second);
        }

        /// <summary>
        /// 指定された値に対してビットごとの NOT 演算を実行します。
        /// </summary>
        /// <typeparam name="T">値の型です。</typeparam>
        /// <param name="value">演算対象の値です。</param>
        public static T Not<T>(T value)
        {
            return SingleType<T>.Not(value);
        }

        /// <summary>
        /// 2 つの値が等しいかどうかを判定します。
        /// </summary>
        /// <typeparam name="T">値の型です。</typeparam>
        /// <param name="first">最初の値です。</param>
        /// <param name="second">2 番目の値です。</param>
        /// <returns>2 つの値が等しい場合は <see langword="true"/>、それ以外の場合は <see langword="false"/> です。</returns>
        public static bool Equal<T>(T first, T second)
        {
            return SingleType<T>.Equal(first, second);
        }

        /// <summary>
        /// 最初の値が 2 番目の値より大きいかどうかを判定します。
        /// </summary>
        /// <typeparam name="T">値の型です。</typeparam>
        /// <param name="first">最初の値です。</param>
        /// <param name="second">2 番目の値です。</param>
        /// <returns>最初の値が 2 番目の値より大きい場合は <see langword="true"/>、それ以外の場合は <see langword="false"/> です。</returns>
        public static bool GreaterThan<T>(T first, T second)
        {
            return SingleType<T>.GreaterThan(first, second);
        }

        /// <summary>
        /// 最初の値が 2 番目の値以上かどうかを判定します。
        /// </summary>
        /// <typeparam name="T">値の型です。</typeparam>
        /// <param name="first">最初の値です。</param>
        /// <param name="second">2 番目の値です。</param>
        /// <returns>最初の値が 2 番目の値以上の場合は <see langword="true"/>、それ以外の場合は <see langword="false"/> です。</returns>
        public static bool GreaterThanOrEqual<T>(T first, T second)
        {
            return SingleType<T>.GreaterThanOrEqual(first, second);
        }

        /// <summary>
        /// 最初の値が 2 番目の値より小さいかどうかを判定します。
        /// </summary>
        /// <typeparam name="T">値の型です。</typeparam>
        /// <param name="first">最初の値です。</param>
        /// <param name="second">2 番目の値です。</param>
        /// <returns>最初の値が 2 番目の値より小さい場合は <see langword="true"/>、それ以外の場合は <see langword="false"/> です。</returns>
        public static bool LessThan<T>(T first, T second)
        {
            return SingleType<T>.LessThan(first, second);
        }

        /// <summary>
        /// 最初の値が 2 番目の値以下かどうかを判定します。
        /// </summary>
        /// <typeparam name="T">値の型です。</typeparam>
        /// <param name="first">最初の値です。</param>
        /// <param name="second">2 番目の値です。</param>
        /// <returns>最初の値が 2 番目の値以下の場合は <see langword="true"/>、それ以外の場合は <see langword="false"/> です。</returns>
        public static bool LessThanOrEqual<T>(T first, T second)
        {
            return SingleType<T>.LessThanOrEqual(first, second);
        }

        /// <summary>
        /// 指定された値を別の型に変換します。
        /// </summary>
        /// <typeparam name="T">変換元の型です。</typeparam>
        /// <typeparam name="U">変換先の型です。</typeparam>
        /// <param name="value">変換する値です。</param>
        /// <returns>変換された値です。</returns>
        public static U Convert<T, U>(T value)
        {
            return DualType<T, U>.Convert(value);
        }

        /// <summary>
        /// 2 つの値を指定された係数で線形補間します。
        /// </summary>
        /// <typeparam name="T">値の型です。</typeparam>
        /// <param name="first">補間の開始値です。</param>
        /// <param name="second">補間の終了値です。</param>
        /// <param name="factor">補間係数です。</param>
        /// <returns>2 つの値を指定された係数で線形補間した結果です。</returns>
        public static T Lerp<T>(T first, T second, float factor)
        {
            return SingleType<T>.Lerp(first, second, factor);
        }


        private static readonly Type[] SortedNumericPrimitives = new[]
        {
            typeof(double),
            typeof(float),
            typeof(long),
            typeof(ulong),
            typeof(int),
            typeof(uint),
            typeof(short),
            typeof(ushort),
            typeof(sbyte),
            typeof(byte),
            typeof(bool)
        };

        private static Type SelectIntermediateType<T, U>()
        {
            if (typeof(T) == typeof(U)) return typeof(T);

            bool firstInList = false;
            bool secondInList = false;
            Type favored = null;
            for (int i = 0; i < SortedNumericPrimitives.Length; i++)
            {
                if (SortedNumericPrimitives[i] == typeof(T))
                {
                    if (favored == null) favored = typeof(T);
                    firstInList = true;
                }
                if (SortedNumericPrimitives[i] == typeof(U))
                {
                    if (favored == null) favored = typeof(U);
                    secondInList = true;
                }
                if (firstInList && secondInList) return favored;
            }

            return typeof(T);
        }
        [System.Diagnostics.DebuggerStepThrough]
        private static Func<TParamA, TParamB, TParamC, TResult> CreateOperatorFunc<TParamA, TParamB, TParamC, TResult>(Func<Expression, Expression, Expression, Expression> mainExpressionConstruct, Type intermediateType = null, bool exceptionFallback = true)
        {
            try
            {
                ParameterExpression paramA = Expression.Parameter(typeof(TParamA));
                ParameterExpression paramB = Expression.Parameter(typeof(TParamB));
                ParameterExpression paramC = Expression.Parameter(typeof(TParamC));
                try
                {
                    return Expression.Lambda<Func<TParamA, TParamB, TParamC, TResult>>(mainExpressionConstruct(paramA, paramB, paramC), paramA, paramB, paramC).Compile();
                }
                catch (InvalidOperationException)
                {
                    Expression exprA;
                    Expression exprB;
                    Expression exprC;
                    Expression exprReturn;

                    if (typeof(TParamA) != intermediateType && intermediateType != null)
                        exprA = Expression.Convert(paramA, intermediateType);
                    else
                        exprA = paramA;

                    if (typeof(TParamB) != intermediateType && intermediateType != null)
                        exprB = Expression.Convert(paramB, intermediateType);
                    else
                        exprB = paramB;

                    if (typeof(TParamC) != intermediateType && intermediateType != null)
                        exprC = Expression.Convert(paramC, intermediateType);
                    else
                        exprC = paramC;

                    exprReturn = mainExpressionConstruct(exprA, exprB, exprC);

                    if (exprReturn.Type != typeof(TResult) && intermediateType != null)
                        exprReturn = Expression.ConvertChecked(exprReturn, typeof(TResult));

                    return Expression.Lambda<Func<TParamA, TParamB, TParamC, TResult>>(exprReturn, paramA, paramB, paramC).Compile();
                }
            }
            catch (Exception e)
            {
                if (exceptionFallback)
                    return delegate { throw new InvalidOperationException(e.Message); };
                else
                    return null;
            }
        }
        [System.Diagnostics.DebuggerStepThrough]
        private static Func<TParamA, TParamB, TResult> CreateOperatorFunc<TParamA, TParamB, TResult>(Func<Expression, Expression, Expression> mainExpressionConstruct, Type intermediateType = null, bool exceptionFallback = true)
        {
            try
            {
                ParameterExpression paramA = Expression.Parameter(typeof(TParamA));
                ParameterExpression paramB = Expression.Parameter(typeof(TParamB));
                try
                {
                    return Expression.Lambda<Func<TParamA, TParamB, TResult>>(mainExpressionConstruct(paramA, paramB), paramA, paramB).Compile();
                }
                catch (InvalidOperationException)
                {
                    Expression exprA;
                    Expression exprB;
                    Expression exprReturn;

                    if (typeof(TParamA) != intermediateType && intermediateType != null)
                        exprA = Expression.Convert(paramA, intermediateType);
                    else
                        exprA = paramA;

                    if (typeof(TParamB) != intermediateType && intermediateType != null)
                        exprB = Expression.Convert(paramB, intermediateType);
                    else
                        exprB = paramB;

                    exprReturn = mainExpressionConstruct(exprA, exprB);

                    if (intermediateType != null)
                        exprReturn = Expression.ConvertChecked(exprReturn, typeof(TResult));

                    return Expression.Lambda<Func<TParamA, TParamB, TResult>>(exprReturn, paramA, paramB).Compile();
                }
            }
            catch (Exception e)
            {
                if (exceptionFallback)
                    return delegate { throw new InvalidOperationException(e.Message); };
                else
                    return null;
            }
        }
        [System.Diagnostics.DebuggerStepThrough]
        private static Func<TParam, TResult> CreateOperatorFunc<TParam, TResult>(Func<Expression, Expression> mainExpressionConstruct, Type intermediateType = null, bool exceptionFallback = true)
        {
            try
            {
                ParameterExpression param = Expression.Parameter(typeof(TParam));
                try
                {
                    return Expression.Lambda<Func<TParam, TResult>>(mainExpressionConstruct(param), param).Compile();
                }
                catch (InvalidOperationException)
                {
                    Expression expr;
                    Expression exprReturn;

                    if (typeof(TParam) != intermediateType && intermediateType != null)
                        expr = Expression.ConvertChecked(param, intermediateType);
                    else
                        expr = param;

                    exprReturn = mainExpressionConstruct(expr);

                    if (intermediateType != null)
                        exprReturn = Expression.ConvertChecked(exprReturn, typeof(TResult));

                    return Expression.Lambda<Func<TParam, TResult>>(exprReturn, param).Compile();
                }
            }
            catch (Exception e)
            {
                if (exceptionFallback)
                    return delegate { throw new InvalidOperationException(e.Message); };
                else
                    return null;
            }
        }
        [System.Diagnostics.DebuggerStepThrough]
        private static Func<T, U> CreateNoOpFunc<T, U>()
        {
            try
            {
                ParameterExpression param = Expression.Parameter(typeof(T));
                return Expression.Lambda<Func<T, U>>(param, param).Compile();
            }
            catch (Exception e)
            {
                return delegate { throw new InvalidOperationException(e.Message); };
            }
        }

        private static class SingleType<T>
        {
            public static readonly Func<T, T, T> Modulo;
            public static readonly Func<T, T> Negate;
            public static readonly Func<T, T> Abs;
            public static readonly Func<T, T, float, T> Lerp;

            public static readonly Func<T, T, T> Or;
            public static readonly Func<T, T, T> And;
            public static readonly Func<T, T, T> Xor;
            public static readonly Func<T, T> Not;

            public static readonly Func<T, T, bool> Equal;
            public static readonly Func<T, T, bool> GreaterThan;
            public static readonly Func<T, T, bool> GreaterThanOrEqual;
            public static readonly Func<T, T, bool> LessThan;
            public static readonly Func<T, T, bool> LessThanOrEqual;

            private static readonly Type FloatFactorIntermediateType;

            static SingleType()
            {
                Modulo = CreateOperatorFunc<T, T, T>(Expression.Modulo);
                Negate = CreateOperatorFunc<T, T>(Expression.NegateChecked);
                Abs = CreateOperatorFunc<T, T>(OperatorBodyAbs);

                Or = CreateOperatorFunc<T, T, T>(Expression.Or);
                And = CreateOperatorFunc<T, T, T>(Expression.And);
                Xor = CreateOperatorFunc<T, T, T>(Expression.ExclusiveOr);
                Not = CreateOperatorFunc<T, T>(Expression.Not);

                Equal = CreateOperatorFunc<T, T, bool>(Expression.Equal);
                GreaterThan = CreateOperatorFunc<T, T, bool>(Expression.GreaterThan);
                GreaterThanOrEqual = CreateOperatorFunc<T, T, bool>(Expression.GreaterThanOrEqual);
                LessThan = CreateOperatorFunc<T, T, bool>(Expression.LessThan);
                LessThanOrEqual = CreateOperatorFunc<T, T, bool>(Expression.LessThanOrEqual);

                {
                    FloatFactorIntermediateType = SelectIntermediateType<T, float>();
                    Func<T, T, float, T> temp;

                    // Try to create a Lerp term without casting the scale factor
                    temp = CreateOperatorFunc<T, T, float, T>(OperatorBodyLerp, FloatFactorIntermediateType, false);

                    // Doesn't work? Try with casting the scale factor to the intermediate Type then.
                    if (temp == null)
                    {
                        temp = CreateOperatorFunc<T, T, float, T>(OperatorBodyLerpCast, FloatFactorIntermediateType);
                    }

                    // Assign Lerp method;
                    Lerp = temp;
                }
            }

            [System.Diagnostics.DebuggerStepThrough]
            private static Expression OperatorBodyAbs(Expression body)
            {
                return Expression.Condition(Expression.LessThan(body, Expression.Constant(default(T), typeof(T))), Expression.NegateChecked(body), body);
            }
            [System.Diagnostics.DebuggerStepThrough]
            private static Expression OperatorBodyLerp(Expression left, Expression right, Expression factor)
            {
                return
                    Expression.AddChecked(
                        Expression.MultiplyChecked(left, Expression.SubtractChecked(Expression.Constant(1.0f), factor)),
                        Expression.MultiplyChecked(right, factor)
                    );
            }
            [System.Diagnostics.DebuggerStepThrough]
            private static Expression OperatorBodyLerpCast(Expression left, Expression right, Expression factor)
            {
                return
                    Expression.AddChecked(
                        Expression.MultiplyChecked(left, Expression.SubtractChecked(Expression.ConvertChecked(Expression.Constant(1.0f), FloatFactorIntermediateType), factor)),
                        Expression.MultiplyChecked(right, factor)
                    );
            }
        }
        private static class DualType<T, U>
        {
            public static readonly Func<T, U, T> Add;
            public static readonly Func<T, U, T> Subtract;
            public static readonly Func<T, U, T> Multiply;
            public static readonly Func<T, U, T> Divide;
            public static readonly Func<T, U> Convert;

            static DualType()
            {
                Type intermediateType = SelectIntermediateType<T, U>();

                Add = CreateOperatorFunc<T, U, T>(Expression.AddChecked, intermediateType);
                Subtract = CreateOperatorFunc<T, U, T>(Expression.SubtractChecked, intermediateType);
                Multiply = CreateOperatorFunc<T, U, T>(Expression.MultiplyChecked, intermediateType);
                Divide = CreateOperatorFunc<T, U, T>(Expression.Divide, intermediateType);
                Convert = (typeof(T) != typeof(U)) ? CreateOperatorFunc<T, U>(OperatorBodyConvert) : CreateNoOpFunc<T, U>();
            }

            [System.Diagnostics.DebuggerStepThrough]
            private static Expression OperatorBodyConvert(Expression body)
            {
                return Expression.ConvertChecked(body, typeof(U));
            }
        }
    }
}
