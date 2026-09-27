using System;
using Rydia.Serialization.Graph.TypeGraph;

namespace Rydia.Serialization.Graph.ValueGraph
{
    internal class UnknownValueNode : ObjectValueNode
    {
        private object _cachedValue;
        private Type _valueType;

        public UnknownValueNode(ValueNode parent, string name, TypeNode typeNode) : base(parent, name, typeNode)
        {
        }

        public override object Value
        {
            get
            {
                /* For creating serialization contexts quickly */
                if (this._cachedValue != null)
                {
                    return this._cachedValue;
                }

                return GetValue(child => child.Value);
            }

            set
            {
                if (value == null)
                {
                    return;
                }

                this._valueType = value.GetType();

                if (this._valueType == typeof(object))
                {
                    throw new InvalidOperationException("Unable to serialize object.");
                }

                /* Create graph as if parent were creating it */
                var unknownTypeGraph = new RootTypeNode(TypeNode.Parent, this._valueType);
                var unknownSerializer = (RootValueNode) unknownTypeGraph.CreateSerializer(Parent);
                unknownSerializer.EndiannessCallback = GetFieldEndianness;
                unknownSerializer.EncodingCallback = GetFieldEncoding;
                unknownSerializer.Value = value;
                Children.Add(unknownSerializer.Child);

                this._cachedValue = value;
            }
        }

        protected override Type GetValueTypeOverride()
        {
            return this._valueType;
        }
    }
}