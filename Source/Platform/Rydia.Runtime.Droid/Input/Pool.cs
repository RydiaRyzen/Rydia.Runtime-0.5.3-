using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rydia.Input
{

    /// <summary>
    /// オブジェクトを再利用するためのオブジェクトプールです。
    /// </summary>
    /// <typeparam name="T">
    /// プールで管理するオブジェクトの型です。
    /// </typeparam>
    public class Pool<T>
    {

        /// <summary>
        /// プールで使用するオブジェクトを生成するためのファクトリです。
        /// </summary>
        public interface PoolObjectFactory<T>
        {

            /// <summary>
            /// 新しいオブジェクトを生成します。
            /// </summary>
            /// <returns>
            /// 生成されたオブジェクトです。
            /// </returns>
            public T CreateObject();

        }

        private List<T> freeObjects;
        private PoolObjectFactory<T> factory;
        private int maxSize;

        /// <summary>
        /// <see cref="Pool{T}"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="factory">
        /// オブジェクトを生成するためのファクトリです。
        /// </param>
        /// <param name="maxSize">
        /// プールに保持できるオブジェクトの最大数です。
        /// </param>
        public Pool(PoolObjectFactory<T> factory, int maxSize)
        {
            this.factory = factory;
            this.maxSize = maxSize;
            this.freeObjects = new List<T>(maxSize);
        }

        /// <summary>
        /// プールからオブジェクトを取得します。
        /// </summary>
        /// <returns>
        /// プールから取得したオブジェクトです。
        /// プールに利用可能なオブジェクトがない場合は、
        /// ファクトリを使用して新しいオブジェクトを生成します。
        /// </returns>
        public T NewObject()
        {
            T @object = default;

            if (this.freeObjects.Count == 0)
                @object = this.factory.CreateObject();
            else
            {
                int index = this.freeObjects.Count - 1;
                this.freeObjects.RemoveAt(index);
            }
            return @object;
        }

        /// <summary>
        /// オブジェクトをプールへ返却します。
        /// </summary>
        /// <param name="object">
        /// プールへ返却するオブジェクトです。
        /// </param>
        /// <remarks>
        /// プールが最大保持数に達している場合、
        /// オブジェクトは破棄されず、そのまま破棄対象となります。
        /// </remarks>
        public void Free(T @object)
        {
            if (this.freeObjects.Count < this.maxSize)
                this.freeObjects.Add(@object);
        }

    }
}
