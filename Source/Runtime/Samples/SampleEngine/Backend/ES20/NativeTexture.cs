using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Rydia.Graphics.Api.ES20;

using TKPixelFormat = Rydia.Graphics.Api.ES20.PixelFormat;

namespace Rydia.Graphics.Backend
{
    /// <summary>
    /// A texture
    /// </summary>
    public sealed class NativeTexture : GLObject
    {

        private TextureTargetType _type;

        /// <summary>
        /// Checks if this texture is currently bound.
        /// </summary>
        /// <value>True if this is the currently bound texture, False otherwise.</value>
        public bool IsBound
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                GL.GetInteger(this._type.GetTargetBinding(), out int currentlyBoundTexture);
                GL.CheckError(this);
                return (currentlyBoundTexture == Handle);
            }
        }

        /// <summary>
        /// Gets or sets the minification filter for this texture.
        /// </summary>
        /// <remarks>
        /// The texture minifying function is used whenever the pixel being textured maps to an area greater than one texture element.
        /// There are six defined minifying functions.
        /// Two of them use the nearest one or nearest four texture elements to compute the texture value.
        /// The other four use mipmaps.
        ///
        /// <para>A mipmap is an ordered set of arrays representing the same image at progressively lower resolutions.
        /// If the texture has dimensions W × H, there are Floor(Log2(Max(W, H)) + 1) mipmap levels.
        /// The first mipmap level is the original texture, with dimensions W × H.
        /// Each subsequent mipmap level has half the dimensions of the previous level, until the final mipmap is reached, 
        /// which has dimension 1 × 1.</para>
        ///
        /// <para>To define the mipmap levels, call <see cref="Upload{T}(GLES2.PixelFormat, int, int, T[], int, GLES2.PixelDataType, int)"/>, 
        /// <see cref="CompressedTexImage2D"/> or <see cref="CopyTexImage2D"/> with 
        /// the <c>level</c> argument indicating the order of the mipmaps.
        /// Level 0 is the original texture; level Floor(Log2(Max(W, H))) is the final 1 × 1 mipmap.</para>
        ///
        /// <para>As more texture elements are sampled in the minification process, fewer aliasing artifacts will be apparent.
        /// While the Nearest and Linear minification functions can be faster than the other four, 
        /// they sample only one or four texture elements to determine the texture value of the pixel being rendered 
        /// and can produce moire patterns or ragged transitions.
        /// The initial value of the minification filter is NearestMipmapLinear.</para>
        ///
        /// <para><b>Note</b>: in DEBUG mode with assertions enabled, an error will be logged to the debug console if this texture is not bound.</para>
        ///
        /// <para><b>OpenGL API</b>: glTexParameteri(GL_TEXTURE_MIN_FILTER), glGetTexParameteriv(GL_TEXTURE_MIN_FILTER)</para>
        /// </remarks>
        /// <seealso cref="Bind"/>
        /// <seealso cref="Unbind"/>
        /// <seealso cref="BindToTextureUnit"/>
        /// <seealso cref="UnbindFromTextureUnit"/>
        /// <seealso cref="Upload{T}(GLES2.PixelFormat, int, int, T[], int, GLES2.PixelDataType, int)"/>
        /// <seealso cref="SubUpload{T}(GLES2.PixelFormat, int, int, int, int, T[], int, GLES2.PixelDataType, int)"/>
        /// <seealso cref="CopyTexImage2D"/>
        /// <seealso cref="CopyTexSubImage2D"/>
        /// <seealso cref="GLES2.PixelStore"/>
        /// <seealso cref="MagnificationFilter"/>
        public TextureMinFilter MinificationFilter
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                this._type.CheckBinding(Handle, this);
                GL.GetTexParameter(this._type, GetTextureParameterName.TextureMinFilter, out int value);
                GL.CheckError(this);
                return (TextureMinFilter)value;
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                this._type.CheckBinding(Handle, this);
                GL.TexParameter(this._type, TextureParameterName.TextureMinFilter, (int)value);
                GL.CheckError(this);
            }
        }

        /// <summary>
        /// Gets or sets the magnification filter for this texture.
        /// </summary>
        /// <remarks>
        /// The texture magnification function is used when the pixel being textured maps to an area less than or equal to one texture element.
        /// It sets the texture magnification function to either Nearest or Linear.
        /// Nearest is generally faster than Linear, but it can produce textured images with sharper edges because the transition between texture elements is not as smooth.
        /// The initial value of the magnification filter is Linear.
        ///
        /// <para><b>Note</b>: in DEBUG mode with assertions enabled, an error will be logged to the debug console if this texture is not bound.</para>
        ///
        /// <para><b>OpenGL API</b>: glTexParameteri(GL_TEXTURE_MAG_FILTER), glGetTexParameteriv(GL_TEXTURE_MAG_FILTER)</para>
        /// </remarks>
        /// <seealso cref="Bind"/>
        /// <seealso cref="Unbind"/>
        /// <seealso cref="BindToTextureUnit"/>
        /// <seealso cref="UnbindFromTextureUnit"/>
        /// <seealso cref="Upload{T}(GLES2.PixelFormat, int, int, T[], int, GLES2.PixelDataType, int)"/>
        /// <seealso cref="SubUpload{T}(GLES2.PixelFormat, int, int, int, int, T[], int, GLES2.PixelDataType, int)"/>
        /// <seealso cref="CopyTexImage2D"/>
        /// <seealso cref="CopyTexSubImage2D"/>
        /// <seealso cref="GLES2.PixelStore"/>
        /// <seealso cref="MinificationFilter"/>
        public TextureMagFilter MagnificationFilter
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                this._type.CheckBinding(Handle, this);
                GL.GetTexParameter(this._type, GetTextureParameterName.TextureMagFilter, out int value);
                GL.CheckError(this);
                return (TextureMagFilter)value;
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                this._type.CheckBinding(Handle, this);
                GL.TexParameter(this._type, TextureParameterName.TextureMagFilter, (int)value);
                GL.CheckError(this);
            }
        }

        /// <summary>
        /// Gets or sets the wrap mode for texture coordinate S (horizontal).
        /// </summary>
        /// <remarks>
        /// See <see cref="TextureWrapMode"/> for options and their effects.
        ///
        /// <para><b>Note</b>: in DEBUG mode with assertions enabled, an error will be logged to the debug console if this texture is not bound.</para>
        ///
        /// <para><b>OpenGL API</b>: glTexParameteri(GL_TEXTURE_WRAP_S), glGetTexParameteriv(GL_TEXTURE_WRAP_S)</para>
        /// </remarks>
        /// <seealso cref="Bind"/>
        /// <seealso cref="Unbind"/>
        /// <seealso cref="BindToTextureUnit"/>
        /// <seealso cref="UnbindFromTextureUnit"/>
        /// <seealso cref="WrapT"/>
        public TextureWrapMode WrapS
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                this._type.CheckBinding(Handle, this);
                GL.GetTexParameter(this._type, GetTextureParameterName.TextureWrapS, out int value);
                GL.CheckError(this);
                return (TextureWrapMode)value;
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                this._type.CheckBinding(Handle, this);
                GL.TexParameter(this._type, TextureParameterName.TextureWrapS, (int)value);
                GL.CheckError(this);
            }
        }

        /// <summary>
        /// Gets or sets the wrap mode for texture coordinate T (vertical).
        /// </summary>
        /// <remarks>
        /// See <see cref="TextureWrapMode"/> for options and their effects.
        ///
        /// <para><b>Note</b>: in DEBUG mode with assertions enabled, an error will be logged to the debug console if this texture is not bound.</para>
        ///
        /// <para><b>OpenGL API</b>: glTexParameteri(GL_TEXTURE_WRAP_T), glGetTexParameteriv(GL_TEXTURE_WRAP_T)</para>
        /// </remarks>
        /// <seealso cref="Bind"/>
        /// <seealso cref="Unbind"/>
        /// <seealso cref="BindToTextureUnit"/>
        /// <seealso cref="UnbindFromTextureUnit"/>
        /// <seealso cref="WrapS"/>
        public TextureWrapMode WrapT
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                this._type.CheckBinding(Handle, this);
                GL.GetTexParameter(this._type, GetTextureParameterName.TextureWrapT, out int value);
                GL.CheckError(this);
                return (TextureWrapMode)value;
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                this._type.CheckBinding(Handle, this);
                GL.TexParameter(this._type, TextureParameterName.TextureWrapT, (int)value);
                GL.CheckError(this);
            }
        }

        /// <summary>
        /// Get the 2D texture that is currently bound for the active multitexture unit.
        /// </summary>
        /// <remarks>
        /// <b>OpenGL API</b>: glGetProgramiv(GL_TEXTURE_BINDING_2D)
        /// </remarks>
        /// <value>The currently bound 2D texture, or null if there is no 2D texture bound.</value>
        /// <seealso cref="Bind"/>
        /// <seealso cref="CurrentCubemapTexture"/>
        public static NativeTexture Current2DTexture
        {
            get
            {
                GL.GetInteger(GetPName.TextureBinding2D, out int value);
                GL.CheckError("Texture");
                if (value == 0)
                    return null;
                return new NativeTexture(value, TextureTargetType.TwoD);
            }
        }

        /// <summary>
        /// Get the cubemap texture that is currently bound for the active multitexture unit.
        /// </summary>
        /// <remarks>
        /// <b>OpenGL API</b>: glGetProgramiv(GL_TEXTURE_BINDING_CUBE_MAP)
        /// </remarks>
        /// <value>The currently bound cubemap texture, or null if there is no cubemap texture bound.</value>
        /// <seealso cref="Bind"/>
        /// <seealso cref="Current2DTexture"/>
        public static NativeTexture CurrentCubemapTexture
        {
            get
            {
                GL.GetInteger(GetPName.TextureBindingCubeMap, out int value);
                GL.CheckError("Texture");
                if (value == 0)
                    return null;
                return new NativeTexture(value, TextureTargetType.CubeMap);
            }
        }

        /// <summary>
        /// Gets the active texture unit.
        /// </summary>
        /// <remarks>
        /// <b>OpenGL API</b>: glGetIntegerv(GL_ACTIVE_TEXTURE)
        /// </remarks>
        /// <value>The index active multitexture unit, ranging from 0 to <see cref="MaxTextureUnits"/> - 1.</value>
        /// <seealso cref="BindToTextureUnit"/>
        /// <seealso cref="MaxTextureUnits"/>
        /// <seealso cref="MaxCombinedTextureUnits"/>
        public static int ActiveTextureUnit
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                GL.GetInteger(GetPName.ActiveTexture, out int value);
                GL.CheckError("Texture");
                return value;
            }
        }

        /// <summary>
        /// Gets the maximum supported texture image units that can be used to access texture maps from the fragment shader.
        /// </summary>
        /// <remarks>
        /// The value must be at least 8.
        ///
        /// <para><b>OpenGL API</b>: glGetIntegerv(GL_MAX_TEXTURE_IMAGE_UNITS)</para>
        /// </remarks>
        /// <seealso cref="BindToTextureUnit"/>
        /// <seealso cref="MaxCombinedTextureUnits"/>
        /// <seealso cref="MaxVertexTextureUnits"/>
        public static int MaxTextureUnits
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                GL.GetInteger(GetPName.MaxTextureImageUnits, out int value);
                GL.CheckError("Texture");
                return value;
            }
        }

        /// <summary>
        /// Gets the maximum supported texture image units that can be used to access texture maps from the vertex shader and the fragment processor combined.
        /// </summary>
        /// <remarks>
        /// If both the vertex shader and the fragment processing stage access the same texture image unit, 
        /// then that counts as using two texture image units against this limit.
        /// The value must be at least 8.
        ///
        /// <para><b>OpenGL API</b>: glGetIntegerv(GL_MAX_COMBINED_TEXTURE_IMAGE_UNITS)</para>
        /// </remarks>
        /// <seealso cref="BindToTextureUnit"/>
        /// <seealso cref="MaxTextureUnits"/>
        /// <seealso cref="MaxVertexTextureUnits"/>
        public static int MaxCombinedTextureUnits
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                GL.GetInteger(GetPName.MaxCombinedTextureImageUnits, out int value);
                GL.CheckError("Texture");
                return value;
            }
        }

        /// <summary>
        /// Gets the maximum supported texture image units that can be used to access texture maps from the vertex shader.
        /// </summary>
        /// <remarks>
        /// The value may be 0.
        ///
        /// <para><b>OpenGL API</b>: glGetIntegerv(GL_MAX_VERTEX_TEXTURE_IMAGE_UNITS)</para>
        /// </remarks>
        /// <seealso cref="BindToTextureUnit"/>
        /// <seealso cref="MaxTextureUnits"/>
        /// <seealso cref="MaxCombinedTextureUnits"/>
        public static int MaxVertexTextureUnits
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                GL.GetInteger(GetPName.MaxVertexTextureImageUnits, out int value);
                GL.CheckError("Texture");
                return value;
            }
        }

        /// <summary>
        /// Gets a rough estimate of the largest texture that the GL can handle.
        /// </summary>
        /// <remarks>
        /// The value must be at least 64.
        ///
        /// <para><b>OpenGL API</b>: glGetIntegerv(GL_MAX_TEXTURE_SIZE)</para>
        /// </remarks>
        /// <seealso cref="Upload{T}(GLES2.PixelFormat, int, int, T[], int, GLES2.PixelDataType, int)"/>
        public static int MaxTextureSize
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                GL.GetInteger(GetPName.MaxTextureSize, out int value);
                GL.CheckError("Texture");
                return value;
            }
        }

        /// <summary>
        /// Gets a rough estimate of the largest cube-map texture that the GL can handle.
        /// </summary>
        /// <remarks>
        /// The value must be at least 16.
        ///
        /// <para><b>OpenGL API</b>: glGetIntegerv(GL_MAX_CUBE_MAP_TEXTURE_SIZE)</para>
        /// </remarks>
        /// <seealso cref="Upload{T}(GLES2.PixelFormat, int, int, T[], int, GLES2.PixelDataType, int)"/>
        public static int MaxCubeMapTextureSize
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                GL.GetInteger(GetPName.MaxCubeMapTextureSize, out int value);
                GL.CheckError("Texture");
                return value;
            }
        }

        /// <summary>
        /// Gets a list of symbolic constants indicating which compressed texture formats are available.
        /// </summary>
        /// <remarks>
        /// May be empty.
        ///
        /// <para> <b>OpenGL API</b>: glGetIntegerv(GL_NUM_COMPRESSED_TEXTURE_FORMATS/GL_COMPRESSED_TEXTURE_FORMATS)</para>
        /// </remarks>
        /// <value>A list of compressed texture formats.</value>
        /// <seealso cref="CompressedTexImage2D"/>
        /// <seealso cref="CompressedTexSubImage2D"/>
        public static int[] CompressedTextureFormats
        {
            get
            {
                GL.GetInteger(GetPName.NumCompressedTextureFormats, out int count);
                GL.CheckError("Texture");
                int[] result = new int[count];

                if (count > 0)
                {
                    GL.GetInteger(GetPName.CompressedTextureFormats, result);
                }

                return result;
            }
        }

        /// <summary>
        /// Gets or sets an implementation-specific hint for generating mipmaps.
        /// </summary>
        /// <remarks>
        /// <b>Note</b>: this is a global setting that effects all textures.
        ///
        /// <para><b>OpenGL API</b>: glHint, glGetIntegerv(GL_GENERATE_MIPMAP_HINT)</para>
        /// </remarks>
        /// <seealso cref="GenerateMipmap"/>
        public static HintMode MipmapHint
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                GL.GetInteger(GetPName.GenerateMipmapHint, out int value);
                GL.CheckError("Texture");
                return (HintMode)value;
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                GL.Hint(HintTarget.GenerateMipmapHint, (HintMode)value);
                GL.CheckError("Texture");
            }
        }

        internal NativeTexture(int handle, TextureTargetType type)
        {
            Handle = handle;
            this._type = (TextureTargetType)type;
        }

        /// <summary>
        /// Creates a texture.
        /// </summary>
        /// <remarks>
        /// <b>OpenGL API</b>: glGenTextures
        /// </remarks>
        /// <param name="type">(optional) type of texture to create. Defaults to a 2D texture.</param>
        /// <seealso cref="Bind"/>
        /// <seealso cref="Upload{T}(GLES2.PixelFormat, int, int, T[], int, GLES2.PixelDataType, int)"/>
        /// <seealso cref="CopyTexImage2D"/>
        public NativeTexture(TextureTargetType type = TextureTargetType.TwoD)
        {
            GL.CheckError(this);
            Handle = GL.GenTexture();
            GL.CheckError(this);
            this._type = (TextureTargetType)type;
        }

        public void ActiveTexture(TextureUnit textureUnit)
        {
            GL.ActiveTexture(textureUnit);
        }

        /// <summary>
        /// Binds the texture.
        /// </summary>
        /// <remarks>
        /// Lets you create or use a named texture.
        /// Binds the texture name to the target of the current active texture unit.
        /// When a texture is bound to a target, the previous binding for that target is automatically broken.
        ///
        /// <para>When a texture is first bound, it assumes the specified target: 
        /// A first bound texture of type <see cref="TextureTargetType.TwoD"/> becomes a two-dimensional texture and a first bound texture of 
        /// type <see cref="TextureTargetType.CubeMap"/> becomes a cube-mapped texture.
        /// The state of a two-dimensional texture immediately after it is first bound is equivalent to the state of the default texture at GL initialization.</para>
        ///
        /// <para>While a texture is bound, GL operations on the target to which it is bound affect the bound texture, 
        /// and queries of the target to which it is bound return state from the bound texture.
        /// In effect, the texture targets become aliases for the textures currently bound to them, 
        /// and the texture name zero refers to the default textures that were bound to them at initialization.</para>
        ///
        /// <para>A texture binding remains active until a different texture is bound to the same target, 
        /// or until the bound texture is deleted.</para>
        ///
        /// <para>Once created, a named texture may be re-bound to its same original target as often as needed.
        /// It is usually much faster to use <c>Bind</c> to bind an existing named texture to one of the texture targets 
        /// than it is to reload the texture image using <see cref="Upload{T}(GLES2.PixelFormat, int, int, T[], int, GLES2.PixelDataType, int)"/>.</para>
        ///
        /// <para><b>OpenGL API</b>: glBindTexture</para>
        /// </remarks>
        /// <seealso cref="Unbind"/>
        /// <seealso cref="IsBound"/>
        /// <seealso cref="BindToTextureUnit"/>
        /// <seealso cref="UnbindFromTextureUnit"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Bind()
        {
            GL.BindTexture(this._type, Handle);
            GL.CheckError(this);
        }

        /// <summary>
        /// Unbinds the texture.
        /// </summary>
        /// <remarks>
        /// <b>OpenGL API</b>: glBindTexture
        /// </remarks>
        /// <seealso cref="Bind"/>
        /// <seealso cref="IsBound"/>
        /// <seealso cref="BindToTextureUnit"/>
        /// <seealso cref="UnbindFromTextureUnit"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Unbind()
        {
            GL.BindTexture(this._type, 0);
            GL.CheckError(this);
        }

        /// <summary>
        /// Actives a texture unit and binds this texture to that unit.
        /// </summary>
        /// <remarks>
        /// Once the texture unit is active, it binds the texture by calling <see cref="Bind"/>.
        ///
        /// <para><b>OpenGL API</b>: glActiveTexture, glBindTexture</para>
        /// </remarks>
        /// <param name="textureUnit">index of the texture unit to make active. 
        /// The number of texture units is implementation dependent, but must be at least 8.</param>
        /// <exception cref="GLException">InvalidEnum if <paramref name="textureUnit"/> is greater than the number of supported texture units.</exception>
        /// <seealso cref="Bind"/>
        /// <seealso cref="Unbind"/>
        /// <seealso cref="IsBound"/>
        /// <seealso cref="UnbindFromTextureUnit"/>
        /// <seealso cref="MaxTextureUnits"/>
        /// <seealso cref="MaxCombinedTextureUnits"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void BindToTextureUnit(int textureUnit)
        {
            GL.ActiveTexture((TextureUnit)((int)TextureUnit.Texture0 + textureUnit));
            GL.CheckError(this);
            GL.BindTexture(this._type, Handle);
            GL.CheckError(this);
        }

        /// <summary>
        /// Actives a texture unit and binds this texture to that unit.
        /// </summary>
        /// <remarks>
        /// Once the texture unit is active, it binds the texture by calling <see cref="Bind"/>.
        ///
        /// <para><b>OpenGL API</b>: glActiveTexture, glBindTexture</para>
        /// </remarks>
        /// <param name="textureUnit">index of the texture unit to make active. 
        /// The number of texture units is implementation dependent, but must be at least 8.</param>
        /// <exception cref="GLException">InvalidEnum if <paramref name="textureUnit"/> is greater than the number of supported texture units.</exception>
        /// <seealso cref="Bind"/>
        /// <seealso cref="Unbind"/>
        /// <seealso cref="IsBound"/>
        /// <seealso cref="UnbindFromTextureUnit"/>
        /// <seealso cref="MaxTextureUnits"/>
        /// <seealso cref="MaxCombinedTextureUnits"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void BindToTextureUnit(TextureUnit textureUnit)
        {
            GL.ActiveTexture(textureUnit);
            GL.CheckError(this);
            GL.BindTexture(this._type, Handle);
            GL.CheckError(this);
        }

        /// <summary>
        /// Actives a texture unit and unbinds this texture from that unit.
        /// </summary>
        /// <remarks>
        /// Once the texture unit is active, it unbinds the texture by calling <see cref="Unbind"/>.
        ///
        /// <para><b>OpenGL API</b>: glActiveTexture, glBindTexture</para>
        /// </remarks>
        /// <param name="textureUnit">index of the texture unit to make active. 
        /// The number of texture units is implementation dependent, but must be at least 8.</param>
        /// <exception cref="GLException">InvalidEnum if <paramref name="textureUnit"/> is greater than the number of supported texture units.</exception>
        /// <seealso cref="Bind"/>
        /// <seealso cref="Unbind"/>
        /// <seealso cref="IsBound"/>
        /// <seealso cref="BindToTextureUnit"/>
        /// <seealso cref="MaxTextureUnits"/>
        /// <seealso cref="MaxCombinedTextureUnits"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void UnbindFromTextureUnit(int textureUnit)
        {
            GL.ActiveTexture((TextureUnit)((int)TextureUnit.Texture0 + textureUnit));
            GL.CheckError(this);
            GL.BindTexture(this._type, 0);
            GL.CheckError(this);
        }

        /// <summary>
        /// Generate a complete set of mipmaps for this texture object.
        /// </summary>
        /// <remarks>
        /// Computes a complete set of mipmap arrays derived from the zero level array.
        /// Array levels up to and including the 1x1 dimension texture image are replaced with the derived arrays, 
        /// regardless of previous contents.
        /// The zero level texture image is left unchanged.
        ///
        /// <para>The internal formats of the derived mipmap arrays all match those of the zero level texture image.
        /// The dimensions of the derived arrays are computed by halving the width and height of the zero level texture image, 
        /// then in turn halving the dimensions of each array level until the 1x1 dimension texture image is reached.</para>
        ///
        /// <para>The contents of the derived arrays are computed by repeated filtered reduction of the zero level array.
        /// No particular filter algorithm is required, though a box filter is recommended.
        /// <see cref="MipmapHint"/> may be called to express a preference for speed or quality of filtering.</para>
        ///
        /// <para><b>Note</b>: in DEBUG mode with assertions enabled, an error will be logged to the debug console if this texture is not bound.</para>
        ///
        /// <para><b>OpenGL API</b>: glGenerateMipmap</para>
        /// </remarks>
        /// <exception cref="GLException">InvalidOperation if this is a cube map texture, but its six faces do not share indentical widths, heights, formats, and types.</exception>
        /// <exception cref="GLException">InvalidOperation if either the width or height of the zero level array is not a power of two.</exception>
        /// <exception cref="GLException">InvalidOperation if the zero level array is stored in a compressed internal format.</exception>
        /// <seealso cref="Bind"/>
        /// <seealso cref="Unbind"/>
        /// <seealso cref="NativeFramebuffer.AttachTexture"/>
        /// <seealso cref="MipmapHint"/>
        /// <seealso cref="Upload{T}(GLES2.PixelFormat, int, int, T[], int, GLES2.PixelDataType, int)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GenerateMipmap()
        {
            this._type.CheckBinding(Handle, this);
            GL.GenerateMipmap(this._type);
            GL.CheckError(this);
        }

        /// <summary>
        /// Reserve memory for a texture of given dimensions.
        /// </summary>
        /// <remarks>
        /// This creates the texture in memory, but does not set the image data yet.
        ///
        /// <para>Texturing maps a portion of a specified texture image onto each graphical primitive for which texturing is active.
        /// Texturing is active when the current fragment shader or vertex shader makes use of built-in texture lookup functions.</para>
        ///
        /// <para>To reserve memory for texture images, call <c>Reserve</c>.
        /// The arguments describe the parameters of the texture image, such as height, width, level-of-detail number (see <see cref="MinificationFilter"/>), and format.
        /// The other arguments describe how the image is represented in memory.</para>       
        ///
        /// <para><b>Note</b>: This method specifies a two-dimensional or cube-map texture for the current texture unit, specified with <see cref="BindToTextureUnit"/>.</para>
        ///
        /// <para><b>Note</b>: in DEBUG mode with assertions enabled, an error will be logged to the debug console if this texture is not bound.</para>
        ///
        /// <para><b>OpenGL API</b>: glTexImage2D</para>
        /// </remarks>
        /// <param name="format">the format of the texel data.</param>
        /// <param name="width">the width of the texture image. All implementations support 2D texture images that are at least 64 texels wide and cube-mapped texture images that are at least 16 texels wide.</param>
        /// <param name="height">the height of the texture image. All implementations support 2D texture images that are at least 64 texels high and cube-mapped texture images that are at least 16 texels high.</param>
        /// <param name="level">(optional) level-of-detail number if updating separate mipmap levels. Level 0 (default) is the base image level. Level N is the Nth mipmap reduction image.</param>
        /// <param name="type">(optional) data type the texel data. Defaults to UnsignedByte.</param>
        /// <param name="cubeTarget">(optional) if this texture is a cube texture, then this parameter specifies which of the 6 cube faces to update. This parameter is ignored for 2D textures.</param>
        /// <exception cref="GLException">InvalidValue if this is a cube map texture and the <paramref name="width"/> and <paramref name="height"/> parameters are not equal.</exception>
        /// <exception cref="GLException">InvalidValue if <paramref name="level"/> is less than 0 or greater than the maximum level.</exception>
        /// <exception cref="GLException">InvalidValue if <paramref name="width"/> or <paramref name="height"/> is less than 0 or greater than the maximum texture size.</exception>
        /// <exception cref="GLException">InvalidOperation if <paramref name="type"/> is UnsignedShort565 and <paramref name="format"/> is not Rgb.</exception>
        /// <exception cref="GLException">InvalidOperation if <paramref name="type"/> is UnsignedShort4444 or UnsignedShort5551 and <paramref name="format"/> is not Rgba.</exception>
        /// <seealso cref="BindToTextureUnit"/>
        /// <seealso cref="CompressedTexImage2D"/>
        /// <seealso cref="SubUpload{T}(GLES2.PixelFormat, int, int, int, int, T[], int, GLES2.PixelDataType, int)"/>
        /// <seealso cref="CompressedTexSubImage2D"/>
        /// <seealso cref="CopyTexImage2D"/>
        /// <seealso cref="CopyTexSubImage2D"/>
        /// <seealso cref="GLES2.PixelStore"/>
        /// <seealso cref="MaxTextureSize"/>
        /// <seealso cref="MaxCubeMapTextureSize"/>
        /// <seealso cref="Upload{T}(GLES2.PixelFormat, int, int, T[], int, GLES2.PixelDataType, int)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Reserve(PixelFormat format, int width, int height, int level = 0, PixelDataType type = PixelDataType.UnsignedByte, int cubeTarget = 0)
        {
            this._type.CheckBinding(Handle, this);
#pragma warning disable CS0618 // Type or member is obsolete
            GL.TexImage2D(GetTarget(cubeTarget), level,
                (PixelInternalFormat)format, width, height, 0,
                (TKPixelFormat)format, (PixelType)type, IntPtr.Zero);
#pragma warning restore CS0618 // Type or member is obsolete
            GL.CheckError(this);
        }

        /// <summary>
        /// Uploads an image to the texture.
        /// </summary>
        /// <remarks>
        /// This creates the texture in memory.
        ///
        /// <para>Texturing maps a portion of a specified texture image onto each graphical primitive for which texturing is active.
        /// Texturing is active when the current fragment shader or vertex shader makes use of built-in texture lookup functions.</para>
        ///
        /// <para>To define texture images, call <c>Upload</c>.
        /// The arguments describe the parameters of the texture image, such as height, width, level-of-detail number (see <see cref="MinificationFilter"/>), and format.
        /// The other arguments describe how the image is represented in memory.</para>
        ///
        /// <para>Data is read from <paramref name="data"/> as a sequence of unsigned bytes or shorts, depending on <paramref name="type"/>.
        /// Color components are converted to floating point based on the <paramref name="type"/>.</para>
        ///
        /// <para><paramref name="width"/> × <paramref name="height"/> texels are read from memory, starting at location <paramref name="data"/>.
        /// By default, these texels are taken from adjacent memory locations, except that after all width texels are read, 
        /// the read pointer is advanced to the next four-byte boundary.
        /// The four-byte row alignment is specified by <see cref="GLES2.PixelStore"/> with argument UnpackAlignment, 
        /// and it can be set to one, two, four, or eight bytes.</para>
        ///
        /// <para>The first element corresponds to the lower left corner of the texture image.
        /// Subsequent elements progress left-to-right through the remaining texels in the lowest row of the texture image, 
        /// and then in successively higher rows of the texture image.
        /// The final element corresponds to the upper right corner of the texture image.</para>
        ///
        /// <para><b>Note</b>: to reserve memory for the texture without specifying texture data, use <see cref="Reserve(GLES2.PixelFormat, int, int, int, GLES2.PixelDataType, int)"/> instead.
        /// You can then <c>upload</c> subtextures to initialize this texture memory.
        /// The image is undefined if the user tries to apply an uninitialized portion of the texture image to a primitive.</para>
        ///
        /// <para><b>Note</b>: This method specifies a two-dimensional or cube-map texture for the current texture unit, specified with <see cref="BindToTextureUnit"/>.</para>
        ///
        /// <para><b>Note</b>: in DEBUG mode with assertions enabled, an error will be logged to the debug console if this texture is not bound.</para>
        ///
        /// <para><b>OpenGL API</b>: glTexImage2D</para>
        /// </remarks>
        /// <typeparam name="T">the type of data in the <paramref name="data"/> array</typeparam>
        /// <param name="format">the format of the texel data.</param>
        /// <param name="width">the width of the texture image. All implementations support 2D texture images that are at least 64 texels wide and cube-mapped texture images that are at least 16 texels wide.</param>
        /// <param name="height">the height of the texture image. All implementations support 2D texture images that are at least 64 texels high and cube-mapped texture images that are at least 16 texels high.</param>
        /// <param name="data">one-dimensional array with the image data in memory.</param>
        /// <param name="level">(optional) level-of-detail number if updating separate mipmap levels. Level 0 (default) is the base image level. Level N is the Nth mipmap reduction image.</param>
        /// <param name="type">(optional) data type the texel data. Defaults to UnsignedByte.</param>
        /// <param name="cubeTarget">(optional) if this texture is a cube texture, then this parameter specifies which of the 6 cube faces to update. This parameter is ignored for 2D textures.</param>
        /// <exception cref="GLException">InvalidValue if this is a cube map texture and the <paramref name="width"/> and <paramref name="height"/> parameters are not equal.</exception>
        /// <exception cref="GLException">InvalidValue if <paramref name="level"/> is less than 0 or greater than the maximum level.</exception>
        /// <exception cref="GLException">InvalidValue if <paramref name="width"/> or <paramref name="height"/> is less than 0 or greater than the maximum texture size.</exception>
        /// <exception cref="GLException">InvalidOperation if <paramref name="type"/> is UnsignedShort565 and <paramref name="format"/> is not Rgb.</exception>
        /// <exception cref="GLException">InvalidOperation if <paramref name="type"/> is UnsignedShort4444 or UnsignedShort5551 and <paramref name="format"/> is not Rgba.</exception>
        /// <seealso cref="BindToTextureUnit"/>
        /// <seealso cref="CompressedTexImage2D"/>
        /// <seealso cref="SubUpload{T}(GLES2.PixelFormat, int, int, int, int, T[], int, GLES2.PixelDataType, int)"/>
        /// <seealso cref="CompressedTexSubImage2D"/>
        /// <seealso cref="CopyTexImage2D"/>
        /// <seealso cref="CopyTexSubImage2D"/>
        /// <seealso cref="GLES2.PixelStore"/>
        /// <seealso cref="MaxTextureSize"/>
        /// <seealso cref="MaxCubeMapTextureSize"/>
        /// <seealso cref="Reserve"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void TexImage2D<T>(PixelFormat format, int width, int height, T[] data, int level = 0,
            PixelDataType type = PixelDataType.UnsignedByte, int cubeTarget = 0)
            where T : struct
        {
            this._type.CheckBinding(Handle, this);
#pragma warning disable CS0618 // Type or member is obsolete
            GL.TexImage2D(GetTarget(cubeTarget), level, (PixelInternalFormat)format, width, height, 0,
                (TKPixelFormat)format, (PixelType)type, data);
#pragma warning restore CS0618 // Type or member is obsolete
            GL.CheckError(this);
        }

        /// <summary>
        /// Uploads an image to the texture.
        /// </summary>
        /// <remarks>
        /// This creates the texture in memory.
        ///
        /// <para>Texturing maps a portion of a specified texture image onto each graphical primitive for which texturing is active.
        /// Texturing is active when the current fragment shader or vertex shader makes use of built-in texture lookup functions.</para>
        ///
        /// <para>To define texture images, call <c>Upload</c>.
        /// The arguments describe the parameters of the texture image, such as height, width, level-of-detail number (see <see cref="MinificationFilter"/>), and format.
        /// The other arguments describe how the image is represented in memory.</para>
        ///
        /// <para>Data is read from <paramref name="data"/> as a sequence of unsigned bytes or shorts, depending on <paramref name="type"/>.
        /// Color components are converted to floating point based on the <paramref name="type"/>.</para>
        ///
        /// <para><paramref name="width"/> × <paramref name="height"/> texels are read from memory, starting at location <paramref name="data"/>.
        /// By default, these texels are taken from adjacent memory locations, except that after all width texels are read, 
        /// the read pointer is advanced to the next four-byte boundary.
        /// The four-byte row alignment is specified by <see cref="GLES2.PixelStore"/> with argument UnpackAlignment, 
        /// and it can be set to one, two, four, or eight bytes.</para>
        ///
        /// <para>The first element corresponds to the lower left corner of the texture image.
        /// Subsequent elements progress left-to-right through the remaining texels in the lowest row of the texture image, 
        /// and then in successively higher rows of the texture image.
        /// The final element corresponds to the upper right corner of the texture image.</para>
        ///
        /// <para><b>Note</b>: to reserve memory for the texture without specifying texture data, use <see cref="Reserve(GLES2.PixelFormat, int, int, int, GLES2.PixelDataType, int)"/> instead.
        /// You can then <c>upload</c> subtextures to initialize this texture memory.
        /// The image is undefined if the user tries to apply an uninitialized portion of the texture image to a primitive.</para>
        ///
        /// <para><b>Note</b>: This method specifies a two-dimensional or cube-map texture for the current texture unit, specified with <see cref="BindToTextureUnit"/>.</para>
        ///
        /// <para><b>Note</b>: in DEBUG mode with assertions enabled, an error will be logged to the debug console if this texture is not bound.</para>
        ///
        /// <para><b>OpenGL API</b>: glTexImage2D</para>
        /// </remarks>
        /// <typeparam name="T">the type of data in the <paramref name="data"/> array</typeparam>
        /// <param name="format">the format of the texel data.</param>
        /// <param name="width">the width of the texture image. All implementations support 2D texture images that are at least 64 texels wide and cube-mapped texture images that are at least 16 texels wide.</param>
        /// <param name="height">the height of the texture image. All implementations support 2D texture images that are at least 64 texels high and cube-mapped texture images that are at least 16 texels high.</param>
        /// <param name="data">two-dimensional array with the image data in memory.</param>
        /// <param name="level">(optional) level-of-detail number if updating separate mipmap levels. Level 0 (default) is the base image level. Level N is the Nth mipmap reduction image.</param>
        /// <param name="type">(optional) data type the texel data. Defaults to UnsignedByte.</param>
        /// <param name="cubeTarget">(optional) if this texture is a cube texture, then this parameter specifies which of the 6 cube faces to update. This parameter is ignored for 2D textures.</param>
        /// <exception cref="GLException">InvalidValue if this is a cube map texture and the <paramref name="width"/> and <paramref name="height"/> parameters are not equal.</exception>
        /// <exception cref="GLException">InvalidValue if <paramref name="level"/> is less than 0 or greater than the maximum level.</exception>
        /// <exception cref="GLException">InvalidValue if <paramref name="width"/> or <paramref name="height"/> is less than 0 or greater than the maximum texture size.</exception>
        /// <exception cref="GLException">InvalidOperation if <paramref name="type"/> is UnsignedShort565 and <paramref name="format"/> is not Rgb.</exception>
        /// <exception cref="GLException">InvalidOperation if <paramref name="type"/> is UnsignedShort4444 or UnsignedShort5551 and <paramref name="format"/> is not Rgba.</exception>
        /// <seealso cref="BindToTextureUnit"/>
        /// <seealso cref="CompressedTexImage2D"/>
        /// <seealso cref="SubUpload{T}(GLES2.PixelFormat, int, int, int, int, T[], int, GLES2.PixelDataType, int)"/>
        /// <seealso cref="CompressedTexSubImage2D"/>
        /// <seealso cref="CopyTexImage2D"/>
        /// <seealso cref="CopyTexSubImage2D"/>
        /// <seealso cref="GLES2.PixelStore"/>
        /// <seealso cref="MaxTextureSize"/>
        /// <seealso cref="MaxCubeMapTextureSize"/>
        /// <seealso cref="Reserve"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void TexImage2D<T>(PixelFormat format, int width, int height, T[,] data,
            int level = 0, PixelDataType type = PixelDataType.UnsignedByte, int cubeTarget = 0)
            where T : struct
        {
            this._type.CheckBinding(Handle, this);
#pragma warning disable CS0618 // Type or member is obsolete
            GL.TexImage2D(GetTarget(cubeTarget), level, (PixelInternalFormat)format, width, height, 0,
                (TKPixelFormat)format, (PixelType)type, data);
#pragma warning restore CS0618 // Type or member is obsolete
            GL.CheckError(this);
        }

        /// <summary>
        /// Uploads a part of an image to the texture.
        /// </summary>
        /// <remarks>
        /// This updates the texture in memory.
        ///
        /// <para>This method redefines a contiguous subregion of an existing two-dimensional texture image.
        /// The texels referenced by data replace the portion of the existing texture array with X indices <paramref name="xOffset"/> and <paramref name="xOffset"/> + <paramref name="width"/> - 1, 
        /// inclusive, and Y indices <paramref name="yOffset"/> and <paramref name="yOffset"/> + <paramref name="height"/> - 1, inclusive.
        /// This region may not include any texels outside the range of the texture array as it was originally specified.
        /// It is not an error to specify a subtexture with zero width or height, but such a specification has no effect.</para>
        ///
        /// <para><b>Note</b>: in DEBUG mode with assertions enabled, an error will be logged to the debug console if this texture is not bound.</para>
        ///
        /// <para><b>OpenGL API</b>: glTexSubImage2D</para>
        /// </remarks>
        /// <typeparam name="T">the type of data in the <paramref name="data"/> array</typeparam>
        /// <param name="format">the format of the texel data.</param>
        /// <param name="xOffset">texel offset in the X direction within the texture array.</param>
        /// <param name="yOffset">texel offset in the Y direction within the texture array.</param>
        /// <param name="width">the width of the texture subimage.</param>
        /// <param name="height">the height of the texture subimage.</param>
        /// <param name="data">one-dimensional array with the image data in memory.</param>
        /// <param name="level">(optional) level-of-detail number if updating separate mipmap levels. Level 0 (default) is the base image level. Level N is the Nth mipmap reduction image.</param>
        /// <param name="type">(optional) data type the texel data. Defaults to UnsignedByte.</param>
        /// <param name="cubeTarget">(optional) if this texture is a cube texture, then this parameter specifies which of the 6 cube faces to update. This parameter is ignored for 2D textures.</param>
        /// <exception cref="GLException">InvalidValue if <paramref name="level"/> is less than 0 or greater than the maximum level.</exception>
        /// <exception cref="GLException">InvalidValue if <paramref name="xOffset"/> &lt; 0 or <paramref name="xOffset"/> + <paramref name="width"/> is greater than the width of this texture.</exception>
        /// <exception cref="GLException">InvalidValue if <paramref name="yOffset"/> &lt; 0 or <paramref name="yOffset"/> + <paramref name="height"/> is greater than the height of this texture.</exception>
        /// <exception cref="GLException">InvalidValue if <paramref name="width"/> or <paramref name="height"/> is less than 0.</exception>
        /// <exception cref="GLException">InvalidOperation if the texture array has not been defined by a previous <see cref="Upload{T}(GLES2.PixelFormat, int, int, T[], int, GLES2.PixelDataType, int)"/>, 
        /// <see cref="Reserve"/> or <see cref="CopyTexImage2D"/> operation 
        /// whose format matches the <paramref name="format"/> parameter of this method.</exception>
        /// <exception cref="GLException">InvalidOperation if <paramref name="type"/> is UnsignedShort565 and <paramref name="format"/> is not Rgb.</exception>
        /// <exception cref="GLException">InvalidOperation if <paramref name="type"/> is UnsignedShort4444 or UnsignedShort5551 and <paramref name="format"/> is not Rgba.</exception>
        /// <seealso cref="BindToTextureUnit"/>
        /// <seealso cref="Reserve"/>
        /// <seealso cref="Upload{T}(GLES2.PixelFormat, int, int, T[], int, GLES2.PixelDataType, int)"/>
        /// <seealso cref="CompressedTexImage2D"/>
        /// <seealso cref="CompressedTexSubImage2D"/>
        /// <seealso cref="CopyTexImage2D"/>
        /// <seealso cref="CopyTexSubImage2D"/>
        /// <seealso cref="GLES2.PixelStore"/>
        /// <seealso cref="MaxTextureSize"/>
        /// <seealso cref="MaxCubeMapTextureSize"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void TexSubImage2D<T>(PixelFormat format, int xOffset, int yOffset, int width, int height, T[] data,
            int level = 0, PixelDataType type = PixelDataType.UnsignedByte, int cubeTarget = 0) where T : struct
        {
            this._type.CheckBinding(Handle, this);
#pragma warning disable CS0618 // Type or member is obsolete
            GL.TexSubImage2D(GetTarget(cubeTarget), level, xOffset, yOffset, width, height,
                (TKPixelFormat)format, (PixelType)type, data);
#pragma warning restore CS0618 // Type or member is obsolete
            GL.CheckError(this);
        }

        /// <summary>
        /// Uploads a part of an image to the texture.
        /// </summary>
        /// <remarks>
        /// This updates the texture in memory.
        ///
        /// <para>This method redefines a contiguous subregion of an existing two-dimensional texture image.
        /// The texels referenced by data replace the portion of the existing texture array with X indices <paramref name="xOffset"/> and <paramref name="xOffset"/> + <paramref name="width"/> - 1, 
        /// inclusive, and Y indices <paramref name="yOffset"/> and <paramref name="yOffset"/> + <paramref name="height"/> - 1, inclusive.
        /// This region may not include any texels outside the range of the texture array as it was originally specified.
        /// It is not an error to specify a subtexture with zero width or height, but such a specification has no effect.</para>
        ///
        /// <para><b>Note</b>: in DEBUG mode with assertions enabled, an error will be logged to the debug console if this texture is not bound.</para>
        ///
        /// <para><b>OpenGL API</b>: glTexSubImage2D</para>
        /// </remarks>
        /// <typeparam name="T">the type of data in the <paramref name="data"/> array</typeparam>
        /// <param name="format">the format of the texel data.</param>
        /// <param name="xOffset">texel offset in the X direction within the texture array.</param>
        /// <param name="yOffset">texel offset in the Y direction within the texture array.</param>
        /// <param name="width">the width of the texture subimage.</param>
        /// <param name="height">the height of the texture subimage.</param>
        /// <param name="data">two-dimensional array with the image data in memory.</param>
        /// <param name="level">(optional) level-of-detail number if updating separate mipmap levels. Level 0 (default) is the base image level. Level N is the Nth mipmap reduction image.</param>
        /// <param name="type">(optional) data type the texel data. Defaults to UnsignedByte.</param>
        /// <param name="cubeTarget">(optional) if this texture is a cube texture, then this parameter specifies which of the 6 cube faces to update. This parameter is ignored for 2D textures.</param>
        /// <exception cref="GLException">InvalidValue if <paramref name="level"/> is less than 0 or greater than the maximum level.</exception>
        /// <exception cref="GLException">InvalidValue if <paramref name="xOffset"/> &lt; 0 or <paramref name="xOffset"/> + <paramref name="width"/> is greater than the width of this texture.</exception>
        /// <exception cref="GLException">InvalidValue if <paramref name="yOffset"/> &lt; 0 or <paramref name="yOffset"/> + <paramref name="height"/> is greater than the height of this texture.</exception>
        /// <exception cref="GLException">InvalidValue if <paramref name="width"/> or <paramref name="height"/> is less than 0.</exception>
        /// <exception cref="GLException">InvalidOperation if the texture array has not been defined by a previous <see cref="Upload{T}(GLES2.PixelFormat, int, int, T[], int, GLES2.PixelDataType, int)"/>, 
        /// <see cref="Reserve"/> or <see cref="CopyTexImage2D"/> operation 
        /// whose format matches the <paramref name="format"/> parameter of this method.</exception>
        /// <exception cref="GLException">InvalidOperation if <paramref name="type"/> is UnsignedShort565 and <paramref name="format"/> is not Rgb.</exception>
        /// <exception cref="GLException">InvalidOperation if <paramref name="type"/> is UnsignedShort4444 or UnsignedShort5551 and <paramref name="format"/> is not Rgba.</exception>
        /// <seealso cref="BindToTextureUnit"/>
        /// <seealso cref="Reserve"/>
        /// <seealso cref="Upload{T}(GLES2.PixelFormat, int, int, T[], int, GLES2.PixelDataType, int)"/>
        /// <seealso cref="CompressedTexImage2D"/>
        /// <seealso cref="CompressedTexSubImage2D"/>
        /// <seealso cref="CopyTexImage2D"/>
        /// <seealso cref="CopyTexSubImage2D"/>
        /// <seealso cref="GLES2.PixelStore"/>
        /// <seealso cref="MaxTextureSize"/>
        /// <seealso cref="MaxCubeMapTextureSize"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void TexSubImage2D<T>(PixelFormat format, int xOffset, int yOffset, int width, int height, T[,] data,
            int level = 0, PixelDataType type = PixelDataType.UnsignedByte, int cubeTarget = 0) where T : struct
        {
            this._type.CheckBinding(Handle, this);
#pragma warning disable CS0618 // Type or member is obsolete
            GL.TexSubImage2D(GetTarget(cubeTarget), level, xOffset, yOffset, width, height, (TKPixelFormat)format, (PixelType)type, data);
#pragma warning restore CS0618 // Type or member is obsolete
            GL.CheckError(this);
        }

        /// <summary>
        /// Uploads a compressed image to the texture.
        /// </summary>
        /// <remarks>
        /// This creates the texture in memory.
        ///
        /// <para>Texturing maps a portion of a specified texture image onto each graphical primitive for which texturing is active.
        /// Texturing is active when the current fragment shader or vertex shader makes use of built-in texture lookup functions.</para>
        ///
        /// <para>This method defines a two-dimensional texture image or cube-map texture image using compressed image data from client memory.
        /// The texture image is decoded according to the extension specification defining the specified <paramref name="format"/>.
        /// OpenGL ES defines no specific compressed texture formats, but does provide a mechanism to obtain symbolic constants for such formats provided by extensions.
        /// The list of specific compressed texture formats supported can be obtained by <see cref="CompressedTextureFormats"/>.</para>
        ///
        /// <para><b>Note</b>: a GL implementation may choose to store the texture array at any internal resolution it chooses.</para>
        ///
        /// <para><b>Note</b>: this method specifies a two-dimensional or cube-map texture for the current texture unit, specified with <see cref="BindToTextureUnit"/>.</para>
        ///
        /// <para><b>Note</b>: in DEBUG mode with assertions enabled, an error will be logged to the debug console if this texture is not bound.</para>
        ///
        /// <para><b>Note</b>: Undefined results, including abnormal program termination, are generated if data is not encoded in a manner consistent with the extension specification defining the internal compression format.</para>
        /// 
        /// <para><b>OpenGL API</b>: glCompressedTexImage2D</para>
        /// </remarks>
        /// <typeparam name="T">the type of data in the <paramref name="data"/> array</typeparam>
        /// <param name="format">the compressed format of the texel data.</param>
        /// <param name="width">the width of the texture image. All implementations support 2D texture images that are at least 64 texels wide and cube-mapped texture images that are at least 16 texels wide.</param>
        /// <param name="height">the height of the texture image. All implementations support 2D texture images that are at least 64 texels high and cube-mapped texture images that are at least 16 texels high.</param>
        /// <param name="data">array containing the compressed image data in memory.</param>
        /// <param name="level">(optional) level-of-detail number if updating separate mipmap levels. Level 0 (default) is the base image level. Level N is the Nth mipmap reduction image.</param>
        /// <param name="cubeTarget">(optional) if this texture is a cube texture, then this parameter specifies which of the 6 cube faces to update. This parameter is ignored for 2D textures.</param>
        /// <exception cref="GLException">InvalidEnum if <paramref name="format"/> is not a supported format returned in <see cref="CompressedTextureFormats"/>.</exception>
        /// <exception cref="GLException">InvalidValue if <paramref name="level"/> is less than 0 or greater than the maximum level.</exception>
        /// <exception cref="GLException">InvalidValue if <paramref name="width"/> or <paramref name="height"/> is less than 0 or greater than the maximum texture size.</exception>
        /// <exception cref="GLException">InvalidValue if the length of <paramref name="data"/> is not consistent with the format, dimensions, and contents of the specified compressed image data.</exception>
        /// <exception cref="GLException">InvalidOperation if parameter combinations are not supported by the specific compressed internal format as specified in the specific texture compression extension.</exception>
        /// <seealso cref="BindToTextureUnit"/>
        /// <seealso cref="Reserve"/>
        /// <seealso cref="Upload{T}(GLES2.PixelFormat, int, int, T[], int, GLES2.PixelDataType, int)"/>
        /// <seealso cref="SubUpload{T}(GLES2.PixelFormat, int, int, int, int, T[], int, GLES2.PixelDataType, int)"/>
        /// <seealso cref="CompressedTexSubImage2D"/>
        /// <seealso cref="CopyTexImage2D"/>
        /// <seealso cref="CopyTexSubImage2D"/>
        /// <seealso cref="MaxTextureSize"/>
        /// <seealso cref="MaxCubeMapTextureSize"/>
        /// <seealso cref="CompressedTextureFormats"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void CompressedTexImage2D<T>(int format, int width, int height, T[] data, int level = 0, int cubeTarget = 0) where T : struct
        {
            this._type.CheckBinding(Handle, this);
#pragma warning disable CS0618 // Type or member is obsolete
            GL.CompressedTexImage2D(GetTarget(cubeTarget), level, (PixelInternalFormat)format, width, height, 0, data.Length, data);
#pragma warning restore CS0618 // Type or member is obsolete
            GL.CheckError(this);
        }

        /// <summary>
        /// Uploads a part of a compressed image to the texture.
        /// </summary>
        /// <remarks>
        /// This updates the texture in memory.
        ///
        /// <para>Texturing maps a portion of a specified texture image onto each graphical primitive for which texturing is active.
        /// Texturing is active when the current fragment shader or vertex shader makes use of built-in texture lookup functions.</para>
        ///
        /// <para>This method redefines a contiguous subregion of an existing two-dimensional texture image.
        /// The texels referenced by <paramref name="data"/> replace the portion of the existing texture array with X indices <paramref name="xOffset"/> and <paramref name="xOffset"/> + <paramref name="width"/> - 1, 
        /// and the Y indices <paramref name="yOffset"/> and <paramref name="yOffset"/> + <paramref name="height"/> - 1, inclusive.
        /// This region may not include any texels outside the range of the texture array as it was originally specified.
        /// It is not an error to specify a subtexture with width of 0, but such a specification has no effect.</para>
        ///
        /// <para><paramref name="format"/> must be the same extension-specified compressed-texture format previously specified by <see cref="CompressedTexImage2D"/>.</para>
        ///
        /// <para><b>Note</b>: this method specifies a two-dimensional or cube-map texture for the current texture unit, specified with <see cref="BindToTextureUnit"/>.</para>
        ///
        /// <para><b>Note</b>: in DEBUG mode with assertions enabled, an error will be logged to the debug console if this texture is not bound.</para>
        ///
        /// <para><b>Note</b>: Undefined results, including abnormal program termination, are generated if data is not encoded in a manner consistent with the extension specification defining the internal compression format.</para>
        ///
        /// <para><b>OpenGL API</b>: glCompressedTexSubImage2D</para>
        /// </remarks>
        /// <typeparam name="T">The type of data in the <paramref name="data"/> array.</typeparam>
        /// <param name="format">the compressed format of the texel data.</param>
        /// <param name="xOffset">texel offset in the X direction within the texture array.</param>
        /// <param name="yOffset">texel offset in the Y direction within the texture array.</param>
        /// <param name="width">the width of the texture subimage.</param>
        /// <param name="height">the height of the texture subimage.</param>
        /// <param name="data">array containing the compressed image data in memory.</param>
        /// <param name="level">(optional) level-of-detail number if updating separate mipmap levels. Level 0 (default) is the base image level. Level N is the Nth mipmap reduction image.</param>
        /// <param name="cubeTarget">(optional) if this texture is a cube texture, then this parameter specifies which of the 6 cube faces to update. This parameter is ignored for 2D textures.</param>
        /// <exception cref="GLException">InvalidEnum if <paramref name="format"/> is not a supported format returned in <see cref="CompressedTextureFormats"/>.</exception>
        /// <exception cref="GLException">InvalidValue if <paramref name="level"/> is less than 0 or greater than the maximum level.</exception>
        /// <exception cref="GLException">InvalidValue if <paramref name="xOffset"/> &lt; 0 or <paramref name="xOffset"/> + <paramref name="width"/> is greater than the width of this texture.</exception>
        /// <exception cref="GLException">InvalidValue if <paramref name="yOffset"/> &lt; 0 or <paramref name="yOffset"/> + <paramref name="height"/> is greater than the height of this texture.</exception>
        /// <exception cref="GLException">InvalidValue if <paramref name="width"/> or <paramref name="height"/> is less than 0.</exception>
        /// <exception cref="GLException">InvalidValue if the length of <paramref name="data"/> is not consistent with the format, dimensions, and contents of the specified compressed image data.</exception>
        /// <exception cref="GLException">InvalidOperation if the texture array has not been defined by a previous <see cref="CompressedTexImage2D"/> operation whose format matches the <paramref name="format"/> of this method.</exception>
        /// <exception cref="GLException">InvalidOperation if parameter combinations are not supported by the specific compressed internal format as specified in the specific texture compression extension.</exception>
        /// <seealso cref="BindToTextureUnit"/>
        /// <seealso cref="Reserve"/>
        /// <seealso cref="Upload{T}(GLES2.PixelFormat, int, int, T[], int, GLES2.PixelDataType, int)"/>
        /// <seealso cref="SubUpload{T}(GLES2.PixelFormat, int, int, int, int, T[], int, GLES2.PixelDataType, int)"/>
        /// <seealso cref="CompressedTexImage2D"/>
        /// <seealso cref="CopyTexImage2D"/>
        /// <seealso cref="CopyTexSubImage2D"/>
        /// <seealso cref="MaxTextureSize"/>
        /// <seealso cref="MaxCubeMapTextureSize"/>
        /// <seealso cref="CompressedTextureFormats"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void CompressedTexSubImage2D<T>(int format, int xOffset, int yOffset, int width, int height, T[] data, int level = 0, int cubeTarget = 0) where T : struct
        {
            this._type.CheckBinding(Handle, this);
#pragma warning disable CS0618 // Type or member is obsolete
            GL.CompressedTexSubImage2D(GetTarget(cubeTarget), level, xOffset, yOffset, width, height,
                (TKPixelFormat)format, data.Length, data);
#pragma warning restore CS0618 // Type or member is obsolete
            GL.CheckError(this);
        }

        /// <summary>
        /// Copies pixels from the current framebuffer into the texture.
        /// </summary>
        /// <remarks>
        /// This method defines a two-dimensional texture image or cube-map texture image with pixels from the current framebuffer
        /// (rather than from client memory, as is the case for <see cref="Upload{T}(GLES2.PixelFormat, int, int, T[], int, GLES2.PixelDataType, int)"/>).
        ///
        /// <para>The screen-aligned pixel rectangle with lower left corner at (<paramref name="left"/>, <paramref name="bottom"/>) 
        /// and with a width of <paramref name="width"/> and a height of <paramref name="height"/> 
        /// defines the texture array at the mipmap level specified by <paramref name="level"/>.
        /// <paramref name="format"/> specifies the internal format of the texture array.</para>
        ///
        /// <para>The pixels in the rectangle are processed exactly as if <see cref="NativeFramebuffer.ReadPixels"/> had been called 
        /// with format set to Rgba, but the process stops just after conversion of Rgba values.
        /// Subsequent processing is identical to that described for <see cref="Upload{T}(GLES2.PixelFormat, int, int, T[], int, GLES2.PixelDataType, int)"/>, 
        /// beginning with the clamping of the R, G, B, and A values to the range 0-1 and then conversion to the texture's internal format for storage in the texel array.</para>
        ///
        /// <para>The components required for <paramref name="format"/> must be a subset of those present in the framebuffer's format.
        /// For example, a Rgba framebuffer can be used to supply components for any format.
        /// However, a Rgb framebuffer can only be used to supply components for Rgb or Luminance base internal format textures, 
        /// not Alpha, LuminanceAlpha or Rgba textures.</para>
        ///
        /// <para>Pixel ordering is such that lower <paramref name="left"/> and <paramref name="bottom"/> screen coordinates correspond to lower S and T texture coordinates.</para>
        ///
        /// <para>If any of the pixels within the specified rectangle are outside the framebuffer associated with the current rendering context, 
        /// then the values obtained for those pixels are undefined.</para>
        ///
        /// <para><b>Note</b>: a GL implementation may choose to store the texture array at any internal resolution it chooses.</para>
        ///
        /// <para><b>Note</b>: an image with height or width of 0 indicates a NULL texture.</para>
        ///
        /// <para><b>Note</b>: This method specifies a two-dimensional or cube-map texture for the current texture unit, 
        /// specified with <see cref="BindToTextureUnit"/>.</para>
        ///
        /// <para><b>Note</b>: in DEBUG mode with assertions enabled, an error will be logged to the debug console if this texture is not bound.</para>
        ///
        /// <para><b>OpenGL API</b>: glCopyTexImage2D</para>
        /// </remarks>
        /// <param name="format">the format of the texel data.</param>
        /// <param name="left">window coordinate of the left corner of the rectangular region of pixels to be copied.</param>
        /// <param name="bottom">window coordinate of the bottom corner of the rectangular region of pixels to be copied.</param>
        /// <param name="width">the width of the texture image. All implementations support 2D texture images that are at least 64 texels wide and cube-mapped texture images that are at least 16 texels wide.</param>
        /// <param name="height">the height of the texture image. All implementations support 2D texture images that are at least 64 texels high and cube-mapped texture images that are at least 16 texels high.</param>
        /// <param name="level">(optional) level-of-detail number if updating separate mipmap levels. Level 0 (default) is the base image level. Level N is the Nth mipmap reduction image.</param>
        /// <param name="cubeTarget">(optional) if this texture is a cube texture, then this parameter specifies which of the 6 cube faces to update. This parameter is ignored for 2D textures.</param>
        /// <exception cref="GLException">InvalidValue if this is a cube map texture and the <paramref name="width"/> and <paramref name="height"/> parameters are not equal.</exception>
        /// <exception cref="GLException">InvalidValue if <paramref name="level"/> is less than 0 or greater than the maximum level.</exception>
        /// <exception cref="GLException">InvalidValue if <paramref name="width"/> or <paramref name="height"/> is less than 0 or greater than then maximum texture size.</exception>
        /// <exception cref="GLException">InvalidOperation if the currently bound framebuffer's format does not contain a superset of the components required by the base format of <paramref name="format"/>.</exception>
        /// <seealso cref="BindToTextureUnit"/>
        /// <seealso cref="NativeFramebuffer.CompletenessStatus"/>
        /// <seealso cref="Reserve"/>
        /// <seealso cref="Upload{T}(GLES2.PixelFormat, int, int, T[], int, GLES2.PixelDataType, int)"/>
        /// <seealso cref="CompressedTexImage2D"/>
        /// <seealso cref="SubUpload{T}(GLES2.PixelFormat, int, int, int, int, T[], int, GLES2.PixelDataType, int)"/>
        /// <seealso cref="CompressedTexSubImage2D"/>
        /// <seealso cref="CopyTexSubImage2D"/>
        /// <seealso cref="MaxTextureSize"/>
        /// <seealso cref="MaxCubeMapTextureSize"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void CopyTexImage2D(PixelFormat format, int left, int bottom, int width, int height, int level = 0, int cubeTarget = 0)
        {
            this._type.CheckBinding(Handle, this);
#pragma warning disable CS0618 // Type or member is obsolete
            GL.CopyTexImage2D(GetTarget(cubeTarget), level, (PixelInternalFormat)format, left, bottom, width, height, 0);
#pragma warning restore CS0618 // Type or member is obsolete
            GL.CheckError(this);
        }

        /// <summary>
        /// Copies pixels from a part of the current framebuffer into the texture.
        /// </summary>
        /// <remarks>
        /// This method replaces a rectangular portion of a two-dimensional texture image or cube-map texture image with pixels from the current framebuffer
        /// (rather than from client memory, as is the case for <see cref="SubUpload{T}(GLES2.PixelFormat, int, int, int, int, T[], int, GLES2.PixelDataType, int)"/>).
        ///
        /// <para>The screen-aligned pixel rectangle with lower left corner at <paramref name="left"/>, <paramref name="bottom"/> and with width <paramref name="width"/> and height <paramref name="height"/>
        /// replaces the portion of the texture array with X indices <paramref name="xOffset"/> through <paramref name="xOffset"/> + <paramref name="width"/> - 1, inclusive, 
        /// and Y indices <paramref name="yOffset"/> through <paramref name="yOffset"/> + <paramref name="height"/> - 1, inclusive, 
        /// at the mipmap level specified by <paramref name="level"/>.</para>
        ///
        /// <para>The pixels in the rectangle are processed exactly as if <see cref="NativeFramebuffer.ReadPixels"/> had been called with format set to Rgba, 
        /// but the process stops just after conversion of Rgba values.
        /// Subsequent processing is identical to that described for <see cref="SubUpload{T}(GLES2.PixelFormat, int, int, int, int, T[], int, GLES2.PixelDataType, int)"/>, 
        /// beginning with the clamping of the R, G, B, and A values to the range 0-1 and then 
        /// conversion to the texture's internal format for storage in the texel array.</para>
        ///
        /// <para>The destination rectangle in the texture array may not include any texels outside the texture array as it was originally specified.
        /// It is not an error to specify a subtexture with zero width or height, but such a specification has no effect.</para>
        ///
        /// <para>If any of the pixels within the specified rectangle are outside the framebuffer associated with the current rendering context, 
        /// then the values obtained for those pixels are undefined.</para>
        ///
        /// <para>No change is made to the internal format, width, or height parameters of the texture array or to texel values outside the specified subregion.</para>
        ///
        /// <para><b>Note</b>: in DEBUG mode with assertions enabled, an error will be logged to the debug console if this texture is not bound.</para>
        ///
        /// <para><b>OpenGL API</b>: glCopyTexSubImage2D</para>
        /// </remarks>
        /// <param name="xOffset">texel offset in the X direction within the texture array.</param>
        /// <param name="yOffset">texel offset in the Y direction within the texture array.</param>
        /// <param name="left">window coordinate of the left corner of the rectangular region of pixels to be copied.</param>
        /// <param name="bottom">window coordinate of the bottom corner of the rectangular region of pixels to be copied.</param>
        /// <param name="width">the width of the texture subimage.</param>
        /// <param name="height">the height of the texture subimage.</param>
        /// <param name="level">(optional) level-of-detail number if updating separate mipmap levels. Level 0 (default) is the base image level. Level N is the Nth mipmap reduction image.</param>
        /// <param name="cubeTarget">(optional) if this texture is a cube texture, then this parameter specifies which of the 6 cube faces to update. This parameter is ignored for 2D textures.</param>
        /// <exception cref="GLException">InvalidValue if <paramref name="level"/> is less than 0 or greater than the maximum level.</exception>
        /// <exception cref="GLException">InvalidValue if <paramref name="xOffset"/> &lt; 0 or <paramref name="xOffset"/> + <paramref name="width"/> is greater than the width of this texture.</exception>
        /// <exception cref="GLException">InvalidValue if <paramref name="yOffset"/> &lt; 0 or <paramref name="yOffset"/> + <paramref name="height"/> is greater than the height of this texture.</exception>
        /// <exception cref="GLException">InvalidValue if <paramref name="width"/> or <paramref name="height"/> is less than 0.</exception>
        /// <exception cref="GLException">InvalidOperation if the currently bound framebuffer's format does not contain a superset of the components required by the base format.</exception>
        /// <exception cref="GLException">InvalidFramebufferOperation if the currently bound framebuffer is not framebuffer complete.</exception>
        /// <seealso cref="BindToTextureUnit"/>
        /// <seealso cref="NativeFramebuffer.CompletenessStatus"/>
        /// <seealso cref="Reserve"/>
        /// <seealso cref="Upload{T}(GLES2.PixelFormat, int, int, T[], int, GLES2.PixelDataType, int)"/>
        /// <seealso cref="CompressedTexImage2D"/>
        /// <seealso cref="CompressedTexSubImage2D"/>
        /// <seealso cref="CopyTexImage2D"/>
        /// <seealso cref="MaxTextureSize"/>
        /// <seealso cref="MaxCubeMapTextureSize"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void CopyTexSubImage2D(int xOffset, int yOffset, int left, int bottom, int width, int height, int level = 0, int cubeTarget = 0)
        {
            this._type.CheckBinding(Handle, this);
#pragma warning disable CS0618 // Type or member is obsolete
            GL.CopyTexSubImage2D(GetTarget(cubeTarget), level, xOffset, yOffset, left, bottom, width, height);
#pragma warning restore CS0618 // Type or member is obsolete
            GL.CheckError(this);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal TextureTargetType GetTarget(int cubeTarget)
        {
            if (this._type == TextureTargetType.CubeMap)
                return (TextureTargetType)((int)TextureTarget2d.TextureCubeMapPositiveX + cubeTarget);
            return this._type;
        }

        /// <summary>
        /// Deletes the texture.
        /// </summary>
        /// <remarks>
        /// After a texture is deleted, it has no contents or dimensionality.
        /// If a texture that is currently bound is deleted, the binding reverts to 0 (the default texture).
        ///
        /// <para><b>OpenGL API</b>: glDeleteTextures</para>
        /// </remarks>
        /// <seealso cref="Bind"/>
        protected override void DisposeHandle()
        {
            GL.DeleteTexture(Handle);
            GL.CheckError(this);
        }

    }

}
