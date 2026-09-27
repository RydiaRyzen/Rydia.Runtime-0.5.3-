using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Rydia.Graphics
{

    /// <summary>
    /// ネイティブメモリとの連携を行う機能を提供するクラスです
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class NativeBuffer<T> : DisposableBase
        where T : new()
    {

        private T[] _buffer;
        private GCHandle _handle;
        private IntPtr _address;
        private int _position;

        /// <summary>
        /// Gets the capacity.
        /// </summary>
        /// <value>
        /// The capacity.
        /// </value>
        public int Capacity
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the stride.
        /// </summary>
        /// <value>
        /// The stride.
        /// </value>
        public int Stride
        {
            get;
            private set;
        }

        /// <summary>
        /// 指定された<paramref name="index"/>に関連付けられた<see cref="T"/>を取得または設定します
        /// </summary>
        /// <param name="index">を表す数値</param>
        /// <returns></returns>
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
        /// Initializes a new instance of the <see cref="NativeBuffer{T}"/> class.
        /// </summary>
        /// <param name="data">を表す値</param>
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
        /// Initializes a new instance of the <see cref="NativeBuffer{T}"/> class.
        /// </summary>
        /// <param name="capacity">を表す数値</param>
        public NativeBuffer(int capacity)
        {
            Capacity = capacity;
            this._buffer = new T[capacity];
            this._handle = GCHandle.Alloc(this._buffer, GCHandleType.Pinned);
            this._address = this._handle.AddrOfPinnedObject();
            Stride = Marshal.SizeOf(typeof(T));
        }

        /// <summary>
        /// Disposings the specified disposing.
        /// </summary>
        /// <param name="disposing">if set to <c>true</c> [disposing].</param>
        protected override void Disposing(bool disposing)
        {
            this._handle.Free();
        }

        /// <summary>
        /// Positions the specified position.
        /// </summary>
        /// <param name="position">を表す数値</param>
        public void Position(int position)
        {
            this._position = position;
        }

        /// <summary>
        /// Puts the specified data.
        /// </summary>
        /// <param name="data">を表す値</param>
        /// <param name="offset">を表す数値</param>
        /// <param name="length">を表す数値</param>
        public void Put(T[] data, int offset, int length)
        {
            Array.Copy(data, offset, this._buffer, this._position, length);
            this._position += length;
        }

        /// <summary>
        /// Puts the specified data.
        /// </summary>
        /// <param name="data">を表す値</param>
        public void Put(T[] data)
        {
            Put(data, 0, data.Length);
        }

        /// <summary>
        /// Puts the specified data.
        /// </summary>
        /// <param name="data">を表す値</param>
        public void Put(T data)
        {
            Put(new T[] { data });
        }

        /// <summary>
        /// Limits this instance.
        /// </summary>
        /// <returns></returns>
        public int Limit()
        {
            return Capacity - this._position;
        }


        /// <summary>
        /// Performs an implicit conversion from <see cref="NativeBuffer{T}"/> to <see cref="IntPtr"/>.
        /// </summary>
        /// <param name="array">を表す値</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator IntPtr(NativeBuffer<T> array)
        {
            return IntPtr.Add(array._address, array._position * array.Stride);
        }

    }


}
