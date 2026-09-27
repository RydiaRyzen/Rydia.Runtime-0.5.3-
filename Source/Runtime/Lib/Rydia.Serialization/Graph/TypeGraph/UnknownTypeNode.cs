using System;
using System.Reflection;
using Rydia.Serialization.Graph.ValueGraph;

namespace Rydia.Serialization.Graph.TypeGraph
{
    internal class UnknownTypeNode : ObjectTypeNode
    {
        public UnknownTypeNode(TypeNode parent, Type type) : base(parent, type)
        {
        }

        public UnknownTypeNode(TypeNode parent, Type parentType, MemberInfo memberInfo) : base(parent, parentType,
            memberInfo)
        {
        }

        internal override ValueNode CreateSerializerOverride(ValueNode parent)
        {
            return new UnknownValueNode(parent, Name, this);
        }
    }
}