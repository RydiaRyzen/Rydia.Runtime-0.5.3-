using System;
using System.Reflection;
using Rydia.Serialization.Graph.ValueGraph;

namespace Rydia.Serialization.Graph.TypeGraph
{
    internal class StreamTypeNode : TypeNode
    {
        public StreamTypeNode(TypeNode parent, Type type) : base(parent, type)
        {
        }

        public StreamTypeNode(TypeNode parent, Type parentType, MemberInfo memberInfo) : base(parent, parentType,
            memberInfo)
        {
        }

        internal override ValueNode CreateSerializerOverride(ValueNode parent)
        {
            return new StreamValueNode(parent, Name, this);
        }
    }
}