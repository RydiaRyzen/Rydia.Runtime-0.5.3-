using System;

namespace Rydia.Serialization
{
    public interface ISubtypeFactory
    {
        bool TryGetKey(Type valueType, out object key);
        bool TryGetType(object key, out Type type);
    }
}