using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Rydia.Graphics.Api.ES20;

using Format = Rydia.Graphics.Api.ES20.RenderbufferInternalFormat;

namespace Rydia.Graphics.Backend
{
    /// <summary>
    /// A renderbuffer.
    /// </summary>
    /// <remarks>
    /// Serves as storage for a <see cref="NativeFramebuffer"/>.
    /// </remarks>
    public class NativeRenderbuffer : GLObject
    {

        internal NativeRenderbuffer(int handle)
        {
            Handle = handle;
        }

        /// <summary>
        /// Get the renderbuffer that is currently bound.
        /// </summary>
        /// <remarks>
        /// <b>OpenGL API</b>: glGetProgramiv(GL_RENDERBUFFER_BINDING)
        /// </remarks>
        /// <value>The currently bound renderbuffer, or null if there is no renderbuffer bound.</value>
        /// <seealso cref="Bind"/>
        /// <seealso cref="Unbind"/>
        /// <seealso cref="IsBound"/>
        public static NativeRenderbuffer Current
        {
            get
            {
                GL.GetInteger(GetPName.RenderbufferBinding, out int value);
                GL.CheckError("Renderbuffer");
                if (value == 0)
                    return null;
                return new NativeRenderbuffer(value);
            }
        }

        /// <summary>
        /// Get the largest renderbuffer width and height that the "GL can handle.
        /// </summary>
        /// <remarks>
        /// The value must be at least 1.
        ///
        /// <para><b>OpenGL API</b>: glGetIntegerv(GL_MAX_RENDERBUFFER_SIZE)</para>
        /// </remarks>
        /// <seealso cref="Storage"/>
        public static int MaxRenderbufferSize
        {
            get
            {
                GL.GetInteger(GetPName.MaxRenderbufferSize, out int value);
                GL.CheckError("Renderbuffer");
                return value;
            }
        }

        /// <summary>
        /// Checks if this renderbuffer is currently bound.
        /// </summary>
        /// <value>True if this is the currently bound renderbuffer, False otherwise.</value>
        public bool IsBound
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                GL.GetInteger(RenderbufferTarget.Renderbuffer.GetTargetBinding(), out int currentlyBoundBuffer);
                GL.CheckError(this);
                return (currentlyBoundBuffer == Handle);
            }
        }

        /// <summary>
        /// Gets the width of the renderbuffer.
        /// </summary>
        /// <remarks>
        /// <b>Note</b>: in DEBUG mode with assertions enabled, an error will be logged to the debug console if this renderbuffer is not bound.
        ///
        /// <para><b>OpenGL API</b>: glGetRenderbufferParameteriv(GL_RENDERBUFFER_WIDTH)</para>
        /// </remarks>
        /// <value>The width in pixels of the image of the renderbuffer.</value>
        /// <seealso cref="Bind"/>
        /// <seealso cref="Storage"/>
        /// <seealso cref="Height"/>
        /// <seealso cref="BufferFormat"/>
        /// <seealso cref="RedSize"/>
        /// <seealso cref="GreenSize"/>
        /// <seealso cref="BlueSize"/>
        /// <seealso cref="AlphaSize"/>
        /// <seealso cref="DepthSize"/>
        /// <seealso cref="StencilSize"/>
        public int Width
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                RenderbufferTarget.Renderbuffer.CheckBinding(Handle, this);
                GL.GetRenderbufferParameter(RenderbufferTarget.Renderbuffer, RenderbufferParameterName.RenderbufferWidth, out int value);
                GL.CheckError(this);
                return value;
            }
        }

        /// <summary>
        /// Gets the height of the renderbuffer.
        /// </summary>
        /// <remarks>
        /// <b>Note</b>: in DEBUG mode with assertions enabled, an error will be logged to the debug console if this renderbuffer is not bound.
        ///
        /// <para><b>OpenGL API</b>: glGetRenderbufferParameteriv(GL_RENDERBUFFER_HEIGHT)</para>
        /// </remarks>
        /// <value>The height in pixels of the image of the renderbuffer.</value>
        /// <seealso cref="Bind"/>
        /// <seealso cref="Storage"/>
        /// <seealso cref="Width"/>
        /// <seealso cref="BufferFormat"/>
        /// <seealso cref="RedSize"/>
        /// <seealso cref="GreenSize"/>
        /// <seealso cref="BlueSize"/>
        /// <seealso cref="AlphaSize"/>
        /// <seealso cref="DepthSize"/>
        /// <seealso cref="StencilSize"/>
        public int Height
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                RenderbufferTarget.Renderbuffer.CheckBinding(Handle, this);
                GL.GetRenderbufferParameter(RenderbufferTarget.Renderbuffer, RenderbufferParameterName.RenderbufferHeight, out int value);
                GL.CheckError(this);
                return value;
            }
        }

        /// <summary>
        /// Gets the format of the renderbuffer.
        /// </summary>
        /// <remarks>
        /// <b>Note</b>: in DEBUG mode with assertions enabled, an error will be logged to the debug console if this renderbuffer is not bound.
        ///
        /// <para><b>OpenGL API</b>: glGetRenderbufferParameteriv(GL_RENDERBUFFER_INTERNAL_FORMAT)</para>
        /// </remarks>
        /// <value>The color-renderable, depth-renderable, or stencil-renderable format of the renderbuffer</value>
        /// <seealso cref="Bind"/>
        /// <seealso cref="Storage"/>
        /// <seealso cref="Width"/>
        /// <seealso cref="Height"/>
        /// <seealso cref="RedSize"/>
        /// <seealso cref="GreenSize"/>
        /// <seealso cref="BlueSize"/>
        /// <seealso cref="AlphaSize"/>
        /// <seealso cref="DepthSize"/>
        /// <seealso cref="StencilSize"/>
        public Format BufferFormat
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                RenderbufferTarget.Renderbuffer.CheckBinding(Handle, this);
                GL.GetRenderbufferParameter(RenderbufferTarget.Renderbuffer, RenderbufferParameterName.RenderbufferInternalFormat, out int value);
                GL.CheckError(this);
                return (Format)value;
            }
        }

        /// <summary>
        /// Gets the resolution in bits for the red component.
        /// </summary>
        /// <remarks>
        /// <b>Note</b>: in DEBUG mode with assertions enabled, an error will be logged to the debug console if this renderbuffer is not bound.
        ///
        /// <para><b>OpenGL API</b>: glGetRenderbufferParameteriv(GL_RENDERBUFFER_RED_SIZE)</para>
        /// </remarks>
        /// <value>The resolution in bits for the red component of the image of the renderbuffer.</value>
        /// <seealso cref="Bind"/>
        /// <seealso cref="Storage"/>
        /// <seealso cref="Width"/>
        /// <seealso cref="Height"/>
        /// <seealso cref="BufferFormat"/>
        /// <seealso cref="GreenSize"/>
        /// <seealso cref="BlueSize"/>
        /// <seealso cref="AlphaSize"/>
        /// <seealso cref="DepthSize"/>
        /// <seealso cref="StencilSize"/>
        public int RedSize
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                RenderbufferTarget.Renderbuffer.CheckBinding(Handle, this);
                GL.GetRenderbufferParameter(RenderbufferTarget.Renderbuffer, RenderbufferParameterName.RenderbufferRedSize, out int value);
                GL.CheckError(this);
                return value;
            }
        }

        /// <summary>
        /// Gets the resolution in bits for the green component.
        /// </summary>
        /// <remarks>
        /// <b>Note</b>: in DEBUG mode with assertions enabled, an error will be logged to the debug console if this renderbuffer is not bound.
        ///
        /// <para><b>OpenGL API</b>: glGetRenderbufferParameteriv(GL_RENDERBUFFER_GREEN_SIZE)</para>
        /// </remarks>
        /// <value>The resolution in bits for the green component of the image of the renderbuffer.</value>
        /// <seealso cref="Bind"/>
        /// <seealso cref="Storage"/>
        /// <seealso cref="Width"/>
        /// <seealso cref="Height"/>
        /// <seealso cref="BufferFormat"/>
        /// <seealso cref="RedSize"/>
        /// <seealso cref="BlueSize"/>
        /// <seealso cref="AlphaSize"/>
        /// <seealso cref="DepthSize"/>
        /// <seealso cref="StencilSize"/>
        public int GreenSize
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                RenderbufferTarget.Renderbuffer.CheckBinding(Handle, this);
                GL.GetRenderbufferParameter(RenderbufferTarget.Renderbuffer, RenderbufferParameterName.RenderbufferGreenSize, out int value);
                GL.CheckError(this);
                return value;
            }
        }

        /// <summary>
        /// Gets the resolution in bits for the blue component.
        /// </summary>
        /// <remarks>
        /// <b>Note</b>: in DEBUG mode with assertions enabled, an error will be logged to the debug console if this renderbuffer is not bound.
        ///
        /// <para><b>OpenGL API</b>: glGetRenderbufferParameteriv(GL_RENDERBUFFER_BLUE_SIZE)</para>
        /// </remarks>
        /// <value>The resolution in bits for the blue component of the image of the renderbuffer.</value>
        /// <seealso cref="Bind"/>
        /// <seealso cref="Storage"/>
        /// <seealso cref="Width"/>
        /// <seealso cref="Height"/>
        /// <seealso cref="BufferFormat"/>
        /// <seealso cref="GreenSize"/>
        /// <seealso cref="RedSize"/>
        /// <seealso cref="AlphaSize"/>
        /// <seealso cref="DepthSize"/>
        /// <seealso cref="StencilSize"/>
        public int BlueSize
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                RenderbufferTarget.Renderbuffer.CheckBinding(Handle, this);
                GL.GetRenderbufferParameter(RenderbufferTarget.Renderbuffer, RenderbufferParameterName.RenderbufferBlueSize, out int value);
                GL.CheckError(this);
                return value;
            }
        }

        /// <summary>
        /// Gets the resolution in bits for the alpha component.
        /// </summary>
        /// <remarks>
        /// <b>Note</b>: in DEBUG mode with assertions enabled, an error will be logged to the debug console if this renderbuffer is not bound.
        ///
        /// <para><b>OpenGL API</b>: glGetRenderbufferParameteriv(GL_RENDERBUFFER_ALPHA_SIZE)</para>
        /// </remarks>
        /// <value>The resolution in bits for the alpha component of the image of the renderbuffer.</value>
        /// <seealso cref="Bind"/>
        /// <seealso cref="Storage"/>
        /// <seealso cref="Width"/>
        /// <seealso cref="Height"/>
        /// <seealso cref="BufferFormat"/>
        /// <seealso cref="GreenSize"/>
        /// <seealso cref="BlueSize"/>
        /// <seealso cref="RedSize"/>
        /// <seealso cref="DepthSize"/>
        public int AlphaSize
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                RenderbufferTarget.Renderbuffer.CheckBinding(Handle, this);
                GL.GetRenderbufferParameter(RenderbufferTarget.Renderbuffer, RenderbufferParameterName.RenderbufferAlphaSize, out int value);
                GL.CheckError(this);
                return value;
            }
        }

        /// <summary>
        /// Gets the resolution in bits for the depth component.
        /// </summary>
        /// <remarks>
        /// <b>Note</b>: in DEBUG mode with assertions enabled, an error will be logged to the debug console if this renderbuffer is not bound.
        ///
        /// <para><b>OpenGL API</b>: glGetRenderbufferParameteriv(GL_RENDERBUFFER_DEPTH_SIZE)</para>
        /// </remarks>
        /// <value>The resolution in bits for the depth component of the image of the renderbuffer.</value>
        /// <seealso cref="Bind"/>
        /// <seealso cref="Storage"/>
        /// <seealso cref="Width"/>
        /// <seealso cref="Height"/>
        /// <seealso cref="BufferFormat"/>
        /// <seealso cref="GreenSize"/>
        /// <seealso cref="BlueSize"/>
        /// <seealso cref="AlphaSize"/>
        /// <seealso cref="RedSize"/>
        /// <seealso cref="StencilSize"/>
        public int DepthSize
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                RenderbufferTarget.Renderbuffer.CheckBinding(Handle, this);
                GL.GetRenderbufferParameter(RenderbufferTarget.Renderbuffer, RenderbufferParameterName.RenderbufferDepthSize, out int value);
                GL.CheckError(this);
                return value;
            }
        }

        /// <summary>
        /// Gets the resolution in bits for the stencil component.
        /// </summary>
        /// <remarks>
        /// <b>Note</b>: in DEBUG mode with assertions enabled, an error will be logged to the debug console if this renderbuffer is not bound.
        ///
        /// <para><b>OpenGL API</b>: glGetRenderbufferParameteriv(GL_RENDERBUFFER_STENCIL_SIZE)</para>
        /// </remarks>
        /// <value>The resolution in bits for the stencil component of the image of the renderbuffer.</value>
        /// <seealso cref="Bind"/>
        /// <seealso cref="Storage"/>
        /// <seealso cref="Width"/>
        /// <seealso cref="Height"/>
        /// <seealso cref="BufferFormat"/>
        /// <seealso cref="GreenSize"/>
        /// <seealso cref="BlueSize"/>
        /// <seealso cref="AlphaSize"/>
        /// <seealso cref="DepthSize"/>
        /// <seealso cref="RedSize"/>
        public int StencilSize
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                RenderbufferTarget.Renderbuffer.CheckBinding(Handle, this);
                GL.GetRenderbufferParameter(RenderbufferTarget.Renderbuffer, RenderbufferParameterName.RenderbufferStencilSize, out int value);
                GL.CheckError(this);
                return value;
            }
        }

        /// <summary>
        /// Creates a renderbuffer.
        /// </summary>
        /// <remarks>
        /// No renderbuffer objects are associated with the returned renderbuffer object names until they are first bound by calling <see cref="Bind"/>.
        ///
        /// <para><b>OpenGL API</b>: glGenRenderbuffers</para>
        /// </remarks>
        /// <seealso cref="Bind"/>
        public NativeRenderbuffer()
        {
            GL.GenRenderbuffer(out this._Handle);
            GL.CheckError(this);
        }

        /// <summary>
        /// Binds the renderbuffer.
        /// </summary>
        /// <remarks>
        /// A renderbuffer is a data storage object containing a single image of a renderable internal format.
        /// A renderbuffer's image may be attached to a framebuffer object to use as a destination for rendering and as a source for reading.
        ///
        /// <para><c>Bind</c> lets you create or use a named renderbuffer object.
        /// When a renderbuffer object is bound, the previous binding is automatically broken.</para>
        ///
        /// <para>The state of a renderbuffer object immediately after it is first bound is a zero-sized memory buffer with format RGBA4 and zero-sized red, 
        /// green, blue, alpha, depth, and stencil pixel depths.</para>
        ///
        /// <para>A renderbuffer object binding created with <c>Bind</c> remains active until a different renderbuffer object name is bound, 
        /// or until the bound renderbuffer object is deleted.</para>
        ///
        /// <para><b>OpenGL API</b>: glBindRenderbuffer</para>
        /// </remarks>
        /// <seealso cref="NativeFramebuffer.AttachRenderbuffer"/>
        /// <seealso cref="Unbind"/>
        /// <seealso cref="IsBound"/>
        /// <seealso cref="Storage"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Bind()
        {
            GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer, Handle);
            GL.CheckError(this);
        }

        /// <summary>
        /// Unbinds the renderbuffer.
        /// </summary>
        /// <remarks>
        /// <b>OpenGL API</b>: glBindRenderbuffer
        /// </remarks>
        /// <seealso cref="Bind"/>
        /// <seealso cref="IsBound"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Unbind()
        {
            GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer, 0);
            GL.CheckError(this);
        }

        /// <summary>
        /// Create and initialize a renderbuffer object's data store.
        /// </summary>
        /// <remarks>
        /// This method establishes the data <c>storage</c>, format, and dimensions of a renderbuffer object's image.
        /// Any existing data store for the renderbuffer is deleted and the contents of the new data store are undefined.
        ///
        /// <para>An implementation may vary its allocation of internal component resolution based on any parameter, 
        /// but the allocation and chosen internal format must not be a function of any other state and cannot be changed once they are established.
        /// The actual resolution in bits of each component of the allocated image can be queried using the properties of this object.</para>
        ///
        /// <para><b>Note</b>: in DEBUG mode with assertions enabled, an error will be logged to the debug console if this renderbuffer is not bound.</para>
        ///
        /// <para><b>OpenGL API</b>: glRenderbufferStorage</para>
        /// </remarks>
        /// <param name="width">width of the renderbuffer in pixels</param>
        /// <param name="height">height of the renderbuffer in pixels</param>
        /// <param name="format">(optional) color-renderable, depth-renderable, or stencil- renderable format of the renderbuffer. Defaults to Rgba4.</param>
        /// <exception cref="GLException">InvalidValue if <paramref name="width"/> or <paramref name="height"/> is less than zero or greater than the maximum renderbuffer size (see <see cref="MaxRenderbufferSize"/>).</exception>
        /// <exception cref="GLException">OutOfMemory if the implementation is unable to create a data store with the requested <paramref name="width"/> and <paramref name="height"/>.</exception>
        /// <exception cref="GLException">InvalidOperation if no renderbuffer is bound.</exception>
        /// <seealso cref="Bind"/>
        /// <seealso cref="NativeFramebuffer.AttachRenderbuffer"/>
        /// <seealso cref="MaxRenderbufferSize"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Storage(int width, int height, Format format = Format.Rgba4)
        {
            RenderbufferTarget.Renderbuffer.CheckBinding(Handle, this);
            GL.RenderbufferStorage(RenderbufferTarget.Renderbuffer, (RenderbufferInternalFormat)format, width, height);
            GL.CheckError(this);
        }

        /// <summary>
        /// Deletes the renderbuffer.
        /// </summary>
        /// <remarks>
        /// After a renderbuffer object is deleted, it has no contents, and its name is free for reuse.
        ///
        /// <para>If a renderbuffer object that is currently bound is deleted, the binding reverts to 0 (the absence of any renderbuffer object).
        /// Additionally, special care must be taken when deleting a renderbuffer object if the image of the renderbuffer is attached to a framebuffer object.
        /// In this case, if the deleted renderbuffer object is attached to the currently bound framebuffer object, it is automatically detached.
        /// However, attachments to any other framebuffer objects are the responsibility of the application.</para>
        ///
        /// <para><b>OpenGL API</b>: glDeleteRenderbuffers</para>
        /// </remarks>
        /// <seealso cref="Bind"/>
        protected override void DisposeHandle()
        {
            GL.DeleteRenderbuffers(ref this._Handle);
        }
    }

}
