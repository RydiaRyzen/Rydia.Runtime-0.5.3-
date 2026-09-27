using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;

namespace Rydia.IO
{

    /// <summary>
    /// 埋め込みリソースを表すクラスです。
    /// </summary>
    /// <typeparam name="TTypeInAssetsAssembly">
    /// 埋め込みリソースを含むアセンブリ内の型です。
    /// </typeparam>
    public class EmbeddedResources<TTypeInAssetsAssembly>
    {

        private Assembly _assembly = null;
        private string _prefix;

        public EmbeddedResources()
        {
            Debug.Assert(this._assembly == null);
            if (this._assembly == null)
                this._assembly = Assembly.GetAssembly(typeof(TTypeInAssetsAssembly));
            Debug.Assert(this._assembly != null);
            this._prefix = this._assembly.GetName().Name + ".";
        }

        /// <summary>
        /// 指定されたファイル名の埋め込みリソースを開き、
        /// <see cref="Stream"/> を返します。
        /// </summary>
        /// <param name="filename">開く埋め込みリソースのファイル名です。</param>
        /// <returns>
        /// 埋め込みリソースを読み取るための <see cref="Stream"/> を返します。
        /// リソースが見つからない場合は <see langword="null"/> を返します。
        /// </returns>
        public Stream? Open(string filename)
        {
            Debug.Assert(this._assembly != null, "Did you forget to call Initialize()?");
            var names = this._assembly.GetManifestResourceNames();
            var modules = this._assembly.GetLoadedModules();
            return this._assembly.GetManifestResourceStream(this._prefix + filename); //(_prefix + filename);
        }

        /// <summary>
        /// 指定されたファイル名の埋め込みリソースを開き、
        /// バイト配列として返します。
        /// </summary>
        /// <param name="filename">読み込む埋め込みリソースのファイル名です。</param>
        /// <returns>
        /// 埋め込みリソースの内容を格納したバイト配列を返します。
        /// </returns>
        public byte[] Load(string filename)
        {
            Stream stream = Open(filename);
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            return bytes;
        }

        /// <summary>
        /// 指定されたファイル名の埋め込みリソースを開き、
        /// UTF-8 エンコードされた文字列として返します。
        /// </summary>
        /// <param name="filename">読み込む埋め込みリソースのファイル名です。</param>
        /// <returns>
        /// 埋め込みリソースの内容を UTF-8 でデコードした文字列を返します。
        /// </returns>
        public string LoadText(string filename)
        {
            return LoadText(filename, Encoding.UTF8);
        }

        /// <summary>
        /// 指定されたファイル名の埋め込みリソースを開き、
        /// 指定されたエンコードで文字列として返します。
        /// </summary>
        /// <param name="filename">読み込む埋め込みリソースのファイル名です。</param>
        /// <param name="encoding">文字列のデコードに使用するエンコードです。</param>
        /// <returns>
        /// 埋め込みリソースの内容を指定されたエンコードでデコードした文字列を返します。
        /// </returns>
        public string LoadText(string filename, Encoding encoding)
        {
            var buffer = Load(filename);
            return encoding.GetString(buffer);
        }

    }

}

