using System;
using Rydia.Serialization.Graph.ValueGraph;

namespace Rydia.Serialization.Graph
{
    internal interface IBinding
    {
        bool IsConst { get; }
        object ConstValue { get; }
        object GetValue(ValueNode target);
        void Bind(ValueNode target, Func<object> callback);
    }
}