using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Rydia.Graphics
{

    /// <summary>
    /// ネイティブメモリとの連携を行う機能を提供するクラスです。
    /// </summary>
    /// <typeparam name="T">バッファーに格納する要素の型です。</typeparam>
    public class NativeBuffer<T> : DisposableBase
        where T : new()
    {

        private T[] _buffer;
        private GCHandle _handle;
        private IntPtr _address;
        private int _position;

        /// <summary>
        /// バッファーの容量を取得します。
        /// </summary>
        public int Capacity
        {
            get;
            private set;
        }

        /// <summary>
        /// バッファー内の1要素あたりのサイズを取得します。
        /// </summary>
        public int Stride
        {
            get;
            private set;
        }

        /// <summary>
        /// 指定された<paramref name="index"/>に関連付けられた<see cref="T"/>を取得または設定します。
        /// </summary>
        /// <param name="index">要素のインデックスを表す数値です。</param>
        /// <returns>指定されたインデックスの要素を返します。</returns>
        public T this[int index]
        {
            get
            {
                return this._buffer[index];
            }
            set
            {
                this._buffer[index] = value;
            }
        }

        /// <summary>
        /// 指定されたデータを使用して<see cref="NativeBuffer{T}"/>の新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="data">バッファーに格納するデータを表す配列です。</param>
        public NativeBuffer(T[] data)
        {
            Capacity = data.Length;
            this._buffer = new T[Capacity];
            Array.Copy(data, this._buffer, Capacity);
            this._handle = GCHandle.Alloc(this._buffer, GCHandleType.Pinned);
            this._address = this._handle.AddrOfPinnedObject();
            Stride = Marshal.SizeOf(typeof(T));
        }

        /// <summary>
        /// 指定された容量を使用して<see cref="NativeBuffer{T}"/>の新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="capacity">バッファーの容量を表す数値です。</param>
        public NativeBuffer(int capacity)
        {
            Capacity = capacity;
            this._buffer = new T[capacity];
            this._handle = GCHandle.Alloc(this._buffer, GCHandleType.Pinned);
            this._address = this._handle.AddrOfPinnedObject();
            Stride = Marshal.SizeOf(typeof(T));
        }

        /// <summary>
        /// アンマネージドリソースを解放します。
        /// </summary>
        /// <param name="disposing">
        /// マネージドリソースも解放する場合は<see langword="true"/>、アンマネージドリソースのみを解放する場合は<see langword="false"/>です。
        /// </param>
        protected override void Disposing(bool disposing)
        {
            this._handle.Free();
        }

        /// <summary>
        /// 現在のバッファー位置を指定された位置に設定します。
        /// </summary>
        /// <param name="position">設定するバッファー位置を表す数値です。</param>
        public void Position(int position)
        {
            this._position = position;
        }

        /// <summary>
        /// 指定されたデータを現在のバッファー位置から書き込みます。
        /// </summary>
        /// <param name="data">書き込むデータを表す配列です。</param>
        /// <param name="offset">データの読み取りを開始する位置を表す数値です。</param>
        /// <param name="length">書き込む要素数を表す数値です。</param>
        public void Put(T[] data, int offset, int length)
        {
            Array.Copy(data, offset, this._buffer, this._position, length);
            this._position += length;
        }

        /// <summary>
        /// 指定されたデータを現在のバッファー位置から書き込みます。
        /// </summary>
        /// <param name="data">書き込むデータを表す配列です。</param>
        public void Put(T[] data)
        {
            Put(data, 0, data.Length);
        }

        /// <summary>
        /// 指定されたデータを現在のバッファー位置から書き込みます。
        /// </summary>
        /// <param name="data">書き込むデータを表す値です。</param>
        public void Put(T data)
        {
            Put(new T[] { data });
        }

        /// <summary>
        /// 現在のバッファー位置から終端までの残り容量を取得します。
        /// </summary>
        /// <returns>現在のバッファー位置から終端までの残り容量を返します。</returns>
        public int Limit()
        {
            return Capacity - this._position;
        }

        /// <summary>
        /// <see cref="NativeBuffer{T}"/> から <see cref="IntPtr"/> への
        /// 暗黙的なキャストを実装します。
        /// </summary>
        /// <param name="array">変換元のネイティブバッファーを表す値です。</param>
        /// <returns>
        /// 現在のバッファー位置を基準としたメモリアドレスを返します。
        /// </returns>
        public static implicit operator IntPtr(NativeBuffer<T> array)
        {
            return IntPtr.Add(array._address, array._position * array.Stride);
        }

    }


}
