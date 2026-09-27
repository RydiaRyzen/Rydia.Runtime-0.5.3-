using System;
using System.Collections.Concurrent;
using Rydia.Serialization.Graph.TypeGraph;

namespace Rydia.Serialization.Graph
{
    internal class GraphGenerator
    {
        private readonly ConcurrentDictionary<Type, RootTypeNode> _graphCache =
            new ConcurrentDictionary<Type, RootTypeNode>();

        public RootTypeNode GenerateGraph(Type valueType)
        {
            return this._graphCache.GetOrAdd(valueType, type => new RootTypeNode(type));
        }
    }
}
