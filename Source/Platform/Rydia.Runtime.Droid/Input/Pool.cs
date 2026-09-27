using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rydia.Input
{
    public class Pool<T>
    {

        public interface PoolObjectFactory<T>
        {
            public T CreateObject();
        }

        private List<T> freeObjects;
        private PoolObjectFactory<T> factory;
        private int maxSize;

        public Pool(PoolObjectFactory<T> factory, int maxSize)
        {
            this.factory = factory;
            this.maxSize = maxSize;
            this.freeObjects = new List<T>(maxSize);
        }

        public T NewObject()
        {
            T @object = default;

            if (this.freeObjects.Count == 0)
                @object = this.factory.CreateObject();
            else
            {
                @object = this.freeObjects[this.freeObjects.Count - 1];
                this.freeObjects.Remove(@object);
            }
            return @object;
        }

        public void Free(T @object)
        {
            if (this.freeObjects.Count < this.maxSize)
                this.freeObjects.Add(@object);
        }

    }
}
