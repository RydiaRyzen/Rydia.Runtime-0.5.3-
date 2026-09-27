using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rydia.Graphics.Api.ES20;
using Rydia.Graphics.Api.ES30;

namespace Rydia.Graphics.Backend
{


    public abstract class GLObject : DisposableBase, IEquatable<GLObject>
    {

        //Buffer
        //VertexArray
        //Shader
        //Program
        //Texture2D
        //Framebuffer
        //





        public static IGLES30 GL
        {
            get
            {
                return RuntimeHost.GLES30;
            }
        }

        protected int _Handle;

        /// <summary>
        /// Internal OpenGL handle to the object
        /// </summary>
        public int Handle
        {
            get
            {
                return this._Handle;
            }
            protected set
            {
                this._Handle = value;
            }
        }

        protected override sealed void Disposing(bool disposing)
        {
            if (Handle != 0)
            {
                DisposeHandle();
                Handle = 0;
            }
        }

        /// <summary>
        /// Abstract method that must be overridden to dispose of the internal OpenGL <see cref="Handle"/>.
        /// </summary>
        protected abstract void DisposeHandle();

        #region Equality

        public bool Equals(GLObject other)
        {
            if (other == null)
                return false;
            return Handle == other.Handle;
        }

        /// <summary>
        /// Checks if this object matches another object.
        /// </summary>
        /// <remarks>
        /// Two OpenGL objects are considered equal if their internal <see cref="Handle"/> properties are equal.
        /// </remarks>
        /// <param name="obj">The object to compare to</param>
        /// <returns>Whether this object matches <paramref name="obj"/></returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (obj is GLObject)
                return Equals((GLObject)obj);
            return base.Equals(obj);
        }

        /// <summary>
        /// Returns a hash code for this object
        /// </summary>
        /// <remarks>
        /// The hash code is equal to the internal <see cref="Handle"/> property of this object.
        /// </remarks>
        /// <returns>The hash code</returns>
        public override int GetHashCode()
        {
            return Handle;
        }

        #endregion

    }

}
