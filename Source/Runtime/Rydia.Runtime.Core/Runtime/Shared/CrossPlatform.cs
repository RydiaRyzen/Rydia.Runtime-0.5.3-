using System;
using System.Collections.Generic;
using System.Runtime.Versioning;
using System.Text;
using Rydia.Runtime.Desktop;
using Rydia.Runtime.Mobile;
using Rydia.Runtime.Shared;

namespace Rydia.Runtime
{

    /// <summary>
    /// デスクトップとモバイルのプラットフォームを共通のインターフェースで扱うための
    /// クロスプラットフォーム実装を提供します。
    /// </summary>
    public class CrossPlatform : IPlatform
    {

        #region IPlatform

        /// <summary>
        /// 実際に使用しているプラットフォームを取得します。
        /// </summary>
        public IPlatform Platform
        {
            get;
            private set;
        }

        /// <summary>
        /// 画面のスケールを取得します。
        /// </summary>
        public float ScreenScale
        {
            get
            {
                return Platform.ScreenScale;
            }
        }

        /// <summary>
        /// アプリケーションランナーを取得します。
        /// </summary>
        public AppRunner App
        {
            get
            {
                return Platform.App;
            }
        }

        /// <summary>
        /// メッセージダイアログを表示します。
        /// </summary>
        /// <param name="args">メッセージダイアログの表示内容を指定します。</param>
        /// <returns>ユーザーが選択したダイアログの結果を返します。</returns>
        public Rydia.Runtime.Shared.DialogResult ShowMessage(MessageDialogEventArgs args)
        {
            return Platform.ShowMessage(args);
        }

        #endregion

        /// <summary>
        /// <see cref="CrossPlatform"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="platform">使用するデスクトッププラットフォームを指定します。</param>
        public CrossPlatform(IDesktopPlatform platform)
        {
            Platform = platform;
        }

        /// <summary>
        /// <see cref="CrossPlatform"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="platform">使用するモバイルプラットフォームを指定します。</param>
        public CrossPlatform(IMobilePlatform platform)
        {
            Platform = platform;
        }

        /// <summary>
        /// 現在のプラットフォームが指定した型かどうかを判定します。
        /// </summary>
        /// <typeparam name="TPlatform">判定するプラットフォームの型を指定します。</typeparam>
        /// <returns>
        /// 現在のプラットフォームが <typeparamref name="TPlatform"/> 型の場合は <c>true</c>、
        /// それ以外の場合は <c>false</c> を返します。
        /// </returns>
        public bool Is<TPlatform>() where TPlatform : IPlatform
        {
            return Platform is TPlatform;
        }

        /// <summary>
        /// 現在のプラットフォームを指定した型として取得します。
        /// </summary>
        /// <typeparam name="TPlatform">取得するプラットフォームの型を指定します。</typeparam>
        /// <returns>指定した型に変換されたプラットフォームを返します。</returns>
        /// <exception cref="InvalidCastException">
        /// 現在のプラットフォームを指定した型に変換できない場合に発生します。
        /// </exception>
        public TPlatform As<TPlatform>()
            where TPlatform : IPlatform
        {
            return (TPlatform)Platform;
        }

        /// <summary>
        /// 現在のプラットフォームを指定した型として取得できるか判定し、
        /// 取得できる場合は <paramref name="platform"/> に設定します。
        /// </summary>
        /// <typeparam name="TPlatform">取得するプラットフォームの型を指定します。</typeparam>
        /// <param name="platform">
        /// 取得したプラットフォーム。指定した型に変換できない場合は既定値になります。
        /// </param>
        /// <returns>
        /// 指定した型に変換できる場合は <c>true</c>、それ以外の場合は <c>false</c> を返します。
        /// </returns>
        public bool IsAs<TPlatform>(out TPlatform platform)
            where TPlatform : IPlatform
        {
            if (Platform is TPlatform p)
            {
                platform = p;
                return true;
            }
            platform = default;
            return false;
        }

    }

}
