using System;

namespace Rydia.Serialization
{
    [Flags]
    public enum SerializationOptions
    {
        None = 0,
        ThrowOnEndOfStream =     0b0001,
        AllowIncompleteObjects = 0b0010
    }
}
