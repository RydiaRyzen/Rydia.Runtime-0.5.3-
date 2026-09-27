using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml.Schema;
using Rydia.Drawing;

namespace Rydia.Graphics.Api.ES20
{


    public static class IGLES20Extensions
    {
        
        public static int GetInteger(this IGLES20 es, GetPName pName)
        {
            es.GetInteger(pName, out int  value);
            return value;
        }

        public static void GenBuffer(this IGLES20 es, out int handle)
        {
            var temp = new int[1];
            es.GenBuffers(1, temp);
            handle = temp[0];
        }

        public static void DeleteBuffer(this IGLES20 es, ref int handle)
        {
            var temp = new int[] { handle };
            es.DeleteBuffers(1, temp);
            handle = 0;
        }

        public static void GenFramebuffer(this IGLES20 es, out int handle)
        {
            var temp = new int[1];
            es.GenFramebuffers(1, temp);
            handle = temp[0];
        }

        public static void GenRenderbuffer(this IGLES20 es, out int handle)
        {
            var temp = new int[1];
            es.GenRenderbuffers(1, temp);
            handle = temp[0];
        }

        public static void DeleteFramebuffer(this IGLES20 es, ref int handle)
        {
            var temp = new int[] { handle };
            es.DeleteFramebuffers(1, temp);
            handle = 0;
        }

        public static void DeleteRenderbuffers(this IGLES20 es, ref int handles)
        {
            var temp = new int[] { handles};
            es.DeleteRenderbuffers(1, temp);
            handles = 0;
        }

        /// <summary>
        /// Set the blend color.
        /// </summary>
        /// <remarks>
        /// The blend color may be used to calculate the source and destination blending factors.
        /// The color components are clamped to the range 0..1 before being stored.
        /// See <see cref="BlendFunc"/> for a complete description of the blending operations.
        /// Initially the blend color is set to (0, 0, 0, 0).
        ///
        /// <para><b>OpenGL API</b>: glBlendColor</para>
        /// </remarks>
        /// <param name="color">the blend color.</param>
        /// <seealso cref="GetBlendColor"/>
        /// <seealso cref="BlendEquation"/>
        /// <seealso cref="BlendFunc"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BlendColor(this IGLES20 es, Color4 color)
        {
            es.BlendColor(color.Rf, color.Gf, color.Bf, color.Af);
        }

        /// <summary>
        /// Set the viewport.
        /// </summary>
        /// <remarks>
        /// When a GL context is first attached to a window, width and height are set to the dimensions of that window.
        /// 
        /// <para>This method specifies the affine transformation of X and Y from normalized device coordinates to window coordinates.</para>
        /// 
        /// <para>Viewport width and height are silently clamped to a range that depends on the implementation.
        /// To query this range, call <see cref="GetMaxViewportDimensions"/>.</para>
        /// 
        /// <para><b>OpenGL API</b>: glViewport</para>
        /// </remarks>
        /// <param name="width">width of the viewport.</param>
        /// <param name="height">height of the viewport.</param>
        /// <exception cref="GLException">InvalidValue if either <paramref name="width"/> or <paramref name="height"/> is negative.</exception>
        /// <seealso cref="GetViewport"/>
        /// <seealso cref="GetMaxViewportDimensions"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Viewport(this IGLES20 es, int width, int height)
        {
            es.Viewport(0, 0, width, height);
        }

        [Conditional("DEBUG")]
        public static void CheckError(this IGLES20 es, object instance, [CallerMemberName] string memberName = null)
        {
            Debug.Assert(instance != null);
            ErrorCode error = es.GetError();
            if (error != ErrorCode.NoError)
                throw new GLException(error, instance.ToString() + "." + memberName);
        }

    }

}
