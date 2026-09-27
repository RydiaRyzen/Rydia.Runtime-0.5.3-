using System;
using System.Collections.Generic;
using System.Text;
using Rydia.Graphics.Api.ES20;

namespace Rydia.Graphics.Backend
{

    /// <summary>
    /// Information about a uniform as returned by <see cref="NativeShaderProgram.GetUniformInfo(int)"/>.
    /// </summary>
    public struct UniformInfo
    {
        /// <summary>
        /// The data type of the uniform variable.
        /// </summary>
        public UniformDataType DataType { get; }

        /// <summary>
        /// The size of the uniform variable. 
        /// For arrays, this is the length of the array.
        /// Otherwise, the value is 1.
        /// </summary>
        public int Size { get; }

        /// <summary>
        /// The name of the uniform variable.
        /// </summary>
        public string Name { get; }

        internal UniformInfo(UniformDataType dataType, int size, string name)
        {
            DataType = dataType;
            Size = size;
            Name = name;
        }
    }

}
