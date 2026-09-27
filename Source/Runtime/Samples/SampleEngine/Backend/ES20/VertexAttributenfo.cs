using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rydia.Graphics.Api.ES20;

namespace Rydia.Graphics.Backend
{

    /// <summary>
    /// Information about an attribute as returned by <see cref="NativeShaderProgram.GetAttributeInfo(int)"/>.
    /// </summary>
    public struct VertexAttributenfo
    {
        /// <summary>
        /// The data type of the attribute variable.
        /// </summary>
        public AttributeDataType DataType { get; }

        /// <summary>
        /// The size of the attribute variable, in units of type <see cref="DataType"/>.
        /// </summary>            
        public int Size { get; }

        /// <summary>
        /// The name of the attribute variable.
        /// </summary>
        public string Name { get; }

        internal VertexAttributenfo(AttributeDataType dataType, int size, string name)
        {
            DataType = dataType;
            Size = size;
            Name = name;
        }
    }

}
