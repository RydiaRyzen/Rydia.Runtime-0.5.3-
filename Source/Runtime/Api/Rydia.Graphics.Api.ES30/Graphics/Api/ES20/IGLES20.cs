using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using Rydia.Drawing;

namespace Rydia.Graphics.Api.ES20
{

    public interface IGLES20
    {

        #region Errors

        /// <summary>
        /// Returns the last error that happened.
        /// </summary>
        /// <remarks>
        /// You usually don't need to call this method yourself since all OpenGL calls are error checked automatically when compiling in DEBUG mode with Assertions enabled.
        ///
        /// <para> <b>OpenGL API</b>: glGetError</para>
        /// </remarks>
        /// <returns>The last error that happened.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        ErrorCode GetError();

        #endregion

        #region Coordinate Transformations

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
        /// <param name="left">X-coordinate of the lower left corner of the viewport rectangle, in pixels.</param>
        /// <param name="bottom">Y-coordinate of the lower left corner of the viewport rectangle, in pixels.</param>
        /// <param name="width">width of the viewport.</param>
        /// <param name="height">height of the viewport.</param>
        /// <exception cref="GLException">InvalidValue if either <paramref name="width"/> or <paramref name="height"/> is negative.</exception>
        /// <seealso cref="GetViewport"/>
        /// <seealso cref="GetMaxViewportDimensions"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void Viewport(int left, int bottom, int width, int height);

        /// <summary>
        /// Returns the current viewport.
        /// </summary>
        /// <param name="left">Is set to the left coordinate of the viewport.</param>
        /// <param name="bottom">Is set to the bottom coordinate of the viewport.</param>
        /// <param name="width">Is set to the width of the viewport.</param>
        /// <param name="height">Is set to the height of the viewport.</param>
        /// <remarks>
        /// Initially the <paramref name="left"/> and <paramref name="bottom"/> window coordinates are both set to 0, 
        /// and <paramref name="width"/> and <paramref name="height"/> are set to the width and height of the window into which the GL will do its rendering.
        ///
        /// <para><b>OpenGL API</b>: glGetIntegerv(GL_VIEWPORT)</para>
        /// </remarks>
        /// <seealso cref="Viewport(int, int)"/>
        /// <seealso cref="Viewport(int, int, int, int)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void GetViewport(out int left, out int bottom, out int width, out int height);

        /// <summary>
        /// Returns the maximum supported width and height of the viewport.
        /// </summary>
        /// <param name="maxWidth">Is set to the maximum width of the viewport.</param>
        /// <param name="maxHeight">Is set to the maximum height of the viewport.</param>
        /// <remarks>
        /// These must be at least as large as the visible dimensions of the display being rendered to.
        ///
        /// <para><b>OpenGL API</b>: glGetIntegerv(GL_MAX_VIEWPORT_DIMS)</para>
        /// </remarks>
        /// <seealso cref="Viewport(int, int)"/>
        /// <seealso cref="Viewport(int, int, int, int)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void GetMaxViewportDimensions(out int maxWidth, out int maxHeight);

        /// <summary>
        /// Specify mapping of depth values from normalized device coordinates to window coordinates.
        /// </summary>
        /// <remarks>
        /// After clipping and division by W, depth coordinates range from -1 to 1, corresponding to the near and far clipping planes.
        /// This method specifies a linear mapping of the normalized depth coordinates in this range to window depth coordinates.
        /// Regardless of the actual depth buffer implementation, window coordinate depth values are treated as though they range from 0 through 1 (like color components).
        /// Thus, the values accepted by <c>DepthRange</c> are both clamped to this range before they are accepted.
        ///
        /// <para>The setting of (0, 1) maps the near plane to 0 and the far plane to 1.
        /// With this mapping, the depth buffer range is fully utilized.</para>
        ///
        /// <para><b>Note</b>: it is not necessary that <paramref name="nearVal"/> be less than <paramref name="farVal"/>.
        /// Reverse mappings such as <paramref name="nearVal"/> = 1, and <paramref name="farVal"/> = 0 are acceptable.</para>
        ///
        /// <para><b>OpenGL API</b>: glDepthRangef</para>
        /// </remarks>
        /// <param name="nearVal">specifies the mapping of the near clipping plane to window coordinates. The initial value is 0.</param>
        /// <param name="farVal">specifies the mapping of the far clipping plane to window coordinates. The initial value is 1.</param>
        /// <seealso cref="GetDepthRange"/>
        /// <seealso cref="DepthFunc"/>
        /// <seealso cref="PolygonOffset"/>
        /// <seealso cref="Viewport(int, int, int, int)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void DepthRange(float nearVal, float farVal);

        /// <summary>
        /// Gets the near and far clipping planes.
        /// </summary>
        /// <remarks>
        /// <b>OpenGL API</b>: glGetFloatv(GL_DEPTH_RANGE)
        /// </remarks>
        /// <param name="nearVal">is set to the mapping of the near clipping plane.</param>
        /// <param name="farVal">is set to the mapping of the far clipping plane.</param>
        /// <seealso cref="DepthRange"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void GetDepthRange(out float nearVal, out float farVal);

        #endregion

        #region Whole Framebuffer Operations

        /// <summary>
        /// Specify clear values used by <see cref="GLES2.Clear"/> to clear the color buffers.
        /// </summary>
        /// <remarks>
        /// All values are clamped to the range 0..1.
        ///
        /// <para><b>OpenGL API</b>: glClearColor</para>
        /// </remarks>
        /// <param name="red">red component.</param>
        /// <param name="green">green component.</param>
        /// <param name="blue">blue component.</param>
        /// <param name="alpha">alpha component.</param>
        /// <seealso cref="Clear"/>
        /// <seealso cref="GetClearColor"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void ClearColor(float red, float green, float blue, float alpha);

        /// <summary>
        /// Specify clear values used by <see cref="GLES2.Clear"/> to clear the color buffers.
        /// </summary>
        /// <remarks>
        /// The R, G, B and A values in AColor are clamped to the range 0..1.
        ///
        /// <para><b>OpenGL API</b>: glClearColor</para>
        /// </remarks>
        /// <param name="color">the clear color.</param>
        /// <seealso cref="Clear"/>
        /// <seealso cref="GetClearColor"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void ClearColor(Color4 color);

        /// <summary>
        /// Returns the current color used to clear the color buffers.
        /// </summary>
        /// <returns>The clear color</returns>
        /// <remarks>
        /// <b>OpenGL API</b>: glGetFloatv(GL_COLOR_CLEAR_VALUE)
        /// </remarks>
        /// <seealso cref="ClearColor(float, float, float, float)"/>
        /// <seealso cref="ClearColor(Color4)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Color4 GetClearColor();

        /// <summary>
        /// Enable and disable writing of frame buffer color components.
        /// </summary>
        /// <remarks>
        /// The initial values of the parameters are all True, indicating that the color components can be written.
        ///
        /// <para>This method specifies whether the individual color components in the frame buffer can or cannot be written.
        /// If <paramref name="red"/> is False, for example, no change is made to the red component of any pixel in any of the color buffers, regardless of the drawing operation attempted.</para>
        ///
        /// <para>Changes to individual bits of components cannot be controlled.
        /// Rather, changes are either enabled or disabled for entire color components.</para>
        ///
        /// <para><b>OpenGL API</b>: glColorMask</para>
        /// </remarks>
        /// <param name="red">whether red can or cannot be written into the frame buffer.</param>
        /// <param name="green">whether green can or cannot be written into the frame buffer.</param>
        /// <param name="blue">whether blue can or cannot be written into the frame buffer.</param>
        /// <param name="alpha">whether alpha can or cannot be written into the frame buffer.</param>
        /// <seealso cref="GetColorMask"/>
        /// <seealso cref="Clear"/>
        /// <seealso cref="DepthMask"/>
        /// <seealso cref="StencilMask"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void ColorMask(bool red, bool green, bool blue, bool alpha);

        /// <summary>
        /// Get whether writing of frame buffer color components is enabled.
        /// </summary>
        /// <remarks>
        /// <b>OpenGL API</b>: glGetIntegerv(GL_COLOR_WRITEMASK)
        /// </remarks>
        /// <param name="red">is set to True of red can be written. False otherwise.</param>
        /// <param name="green">is set to True of green can be written. False otherwise.</param>
        /// <param name="blue">is set to True of blue can be written. False otherwise.</param>
        /// <param name="alpha">is set to True of alpha can be written. False otherwise.</param>
        /// <seealso cref="ColorMask"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void GetColorMask(out bool red, out bool green, out bool blue, out bool alpha);

        /// <summary>
        /// Specify the clear value used by <see cref="GLES2.Clear"/> to clear the depth buffer.
        /// </summary>
        /// <remarks>
        /// <b>OpenGL API</b>: glClearDepthf/glClearDepth
        /// </remarks>
        /// <param name="depth">the depth value used when the depth buffer is cleared. The initial value is 1. 
        /// The value is clamped to the range 0..1.</param>
        /// <seealso cref="Clear"/>
        /// <seealso cref="GetClearDepth"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void ClearDepth(float depth);

        /// <summary>
        /// Gets the current clear depth value.
        /// </summary>
        /// <remarks>
        /// <b>OpenGL API</b>: glGetFloatv(GL_DEPTH_CLEAR_VALUE)
        /// </remarks>
        /// <returns>The current clear depth value.</returns>
        /// <seealso cref="ClearDepth"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        float GetClearDepth();

        /// <summary>
        /// Enable or disable writing into the depth buffer.
        /// </summary>
        /// <remarks>
        /// <b>OpenGL API</b>: glDepthMask
        /// </remarks>
        /// <param name="enable">specifies whether the depth buffer is enabled for writing. 
        /// If False, depth buffer writing is disabled. 
        /// Otherwise, it is enabled. Initially, depth buffer writing is enabled.</param>
        /// <seealso cref="GetDepthMask"/>
        /// <seealso cref="ColorMask"/>
        /// <seealso cref="DepthFunc"/>
        /// <seealso cref="DepthRange"/>
        /// <seealso cref="StencilMask"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void DepthMask(bool enable);

        /// <summary>
        /// Checks whether writing into the depth buffer is enabled.
        /// </summary>
        /// <remarks>
        /// <b>OpenGL API</b>: glGetIntegerv(GL_DEPTH_WRITEMASK)
        /// </remarks>
        /// <returns>True if writing into the depth buffer is enabled. False otherwise.</returns>
        /// <seealso cref="DepthMask"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        bool GetDepthMask();

        /// <summary>
        /// Specify the index value used by <see cref="GLES2.Clear"/> to clear the stencil buffer.
        /// </summary>
        /// <remarks>
        /// <b>OpenGL API</b>: glClearStencil
        /// </remarks>
        /// <param name="index">the index used when the stencil buffer is cleared. The initial value is 0. 
        /// Only the lowest <see cref="Framebuffer.StencilBits"/> bits of the index are used.</param>
        /// <seealso cref="Clear"/>
        /// <seealso cref="GetClearStencil"/>
        /// <seealso cref="Framebuffer.StencilBits"/>
        /// <seealso cref="StencilFunc"/>
        /// <seealso cref="StencilFuncSeparate"/>
        /// <seealso cref="StencilMask"/>
        /// <seealso cref="StencilMaskSeparate"/>
        /// <seealso cref="StencilOperation"/>
        /// <seealso cref="StencilOperationSeparate"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void ClearStencil(int index);

        /// <summary>
        /// Returns the index to which the stencil bitplanes are cleared.
        /// </summary>
        /// <remarks>
        /// The initial value is 0.
        ///
        /// <para><b>OpenGL API</b>: glGetIntegerv(GL_STENCIL_CLEAR_VALUE)</para>
        /// </remarks>
        /// <returns>The stencil index</returns>
        /// <seealso cref="ClearStencil"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        int GetClearStencil();

        /// <summary>
        /// Control the front and back writing of individual bits in the stencil planes.
        /// </summary>
        /// <remarks>
        /// <c>StencilMask</c> controls the writing of individual bits in the stencil planes.
        /// The least significant n bits of AMask, where n is the number of bits in the stencil buffer, specify a mask.
        /// Where a 1 appears in the mask, it's possible to write to the corresponding bit in the stencil buffer.
        /// Where a 0 appears, the corresponding bit is write-protected.
        /// Initially, all bits are enabled for writing.
        ///
        /// <para>There can be two separate mask writemasks; one affects back-facing polygons, 
        /// and the other affects front-facing polygons as well as other non-polygon primitives.
        /// <c>StencilMask</c> sets both front and back stencil writemasks to the same values.
        /// Use <see cref="StencilMaskSeparate"/> to set front and back stencil writemasks to different values.</para>
        ///
        /// <para><b>Note</b>: <c>StencilMask</c> is the same as calling <see cref="GLES2.StencilMaskSeparate"/> with face set to FrontAndBack.</para>
        ///
        /// <para><b>OpenGL API</b>: glStencilMask</para>
        /// </remarks>
        /// <param name="mask">specifies a bit mask to enable and disable> writing of individual bits in the stencil planes. 
        /// Initially, the mask is all 1's.</param>
        /// <seealso cref="GetStencilWriteMask"/>
        /// <seealso cref="GetStencilBackWriteMask"/>
        /// <seealso cref="Framebuffer.StencilBits"/>
        /// <seealso cref="ColorMask"/>
        /// <seealso cref="DepthMask"/>
        /// <seealso cref="StencilFunc"/>
        /// <seealso cref="StencilFuncSeparate"/>
        /// <seealso cref="StencilMaskSeparate"/>
        /// <seealso cref="StencilOperation"/>
        /// <seealso cref="StencilOperationSeparate"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void StencilMask(uint mask);

        /// <summary>
        /// Control the front and/or back writing of individual bits in the stencil planes.
        /// </summary>
        /// <remarks>
        /// See <see cref="StencilMask"/> for more details.
        ///
        /// <para><b>OpenGL API</b>: glStencilMaskSeparate</para>
        /// </remarks>
        /// <param name="face">specifies whether front and/or back stencil writemask is updated.</param>
        /// <param name="mask">specifies a bit mask to enable and disable writing of individual bits in the stencil planes. 
        /// Initially, the mask is all 1's.</param>
        /// <seealso cref="GetStencilWriteMask"/>
        /// <seealso cref="GetStencilBackWriteMask"/>
        /// <seealso cref="Framebuffer.StencilBits"/>
        /// <seealso cref="ColorMask"/>
        /// <seealso cref="DepthMask"/>
        /// <seealso cref="StencilFunc"/>
        /// <seealso cref="StencilFuncSeparate"/>
        /// <seealso cref="StencilMask"/>
        /// <seealso cref="StencilOperation"/>
        /// <seealso cref="StencilOperationSeparate"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void StencilMaskSeparate(CullFaceMode face, uint mask);

        /// <summary>
        /// Get the mask that controls writing of the stencil bitplanes for front-facing polygons and non-polygons.
        /// </summary>
        /// <remarks>
        /// The initial value is all 1's.
        ///
        /// <para><b>OpenGL API</b>: glGetIntegerv(GL_STENCIL_WRITEMASK)</para>
        /// </remarks>
        /// <returns>The stencil write mask.</returns>
        /// <seealso cref="StencilMask"/>
        /// <seealso cref="StencilMaskSeparate"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        uint GetStencilWriteMask();

        /// <summary>
        /// Get the mask that controls writing of the stencil bitplanes for back-facing polygons.
        /// </summary>
        /// <remarks>
        /// The initial value is all 1's.
        ///
        /// <para><b>OpenGL API</b>: glGetIntegerv(GL_STENCIL_BACK_WRITEMASK)</para>
        /// </remarks>
        /// <returns>The stencil write mask.</returns>
        /// <seealso cref="StencilMask"/>
        /// <seealso cref="StencilMaskSeparate"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        uint GetStencilBackWriteMask();

        /// <summary>
        /// <c>Clear</c> buffers to preset values.
        /// </summary>
        /// <remarks>
        /// The value to which each buffer is cleared depends on the setting of the <c>clear</c> value for that buffer, 
        /// as set by <see cref="ClearColor(Color4)"/>, <see cref="ClearStencil"/> and <see cref="ClearDepth"/>.
        ///
        /// <para>The pixel ownership test, the scissor test, dithering, and the buffer writemasks affect the operation of <c>Clear</c>.
        /// The scissor box bounds the cleared region.
        /// Blend function, stenciling, fragment shading, and depth-buffering are ignored by <c>Clear</c>.</para>
        ///
        /// <para><b>OpenGL API</b>: glClear</para>
        /// </remarks>
        /// <param name="buffers">the buffers to be cleared.</param>
        /// <seealso cref="ClearColor(float, float, float, float)"/>
        /// <seealso cref="GetClearColor"/>
        /// <seealso cref="ClearStencil"/>
        /// <seealso cref="GetClearStencil"/>
        /// <seealso cref="ClearDepth(float)"/>
        /// <seealso cref="GetClearDepth"/>
        /// <seealso cref="ColorMask"/>
        /// <seealso cref="DepthMask"/>
        /// <seealso cref="Scissor"/>
        /// <seealso cref="StencilMask"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void Clear(ClearBufferMask buffers);

        #endregion

        #region Per-Fragment operations

        /// <summary>
        /// Specify pixel arithmetic using blend functions.
        /// </summary>
        /// <remarks>
        /// Pixels can be drawn using a function that blends the incoming (source) RGBA values with the RGBA values that are already in the frame buffer (the destination values).
        /// Blending is initially disabled.
        /// Use <see cref="Enable"/> and <see cref="Disable"/> with argument <see cref="EnableCap.Blend"/> to enable and disable blending.
        ///
        /// <para><c>BlendFunc</c> defines the operation of blending when it is enabled.
        /// ASrcFactor specifies which method is used to scale the source color components.
        /// ADstFactor specifies which method is used to scale the destination color components.
        /// See <see cref="BlendFactor"/> for a description of the possible operations.</para>
        ///
        /// <para><b>Note</b>: incoming (source) alpha is correctly thought of as a material opacity, ranging from 1.0, 
        /// representing complete opacity, to 0.0, representing complete transparency.</para>
        ///
        /// <para><b>Note</b>: transparency is best implemented using blend function (SrcAlpha, OneMinusSrcAlpha) with primitives sorted from farthest to nearest.
        /// Note that this transparency calculation does not require the presence of alpha bitplanes in the frame buffer.</para>
        ///
        /// <para><b>OpenGL API</b>: glBlendFunc</para>
        /// </remarks>
        /// <param name="srcFactor">specifies how the red, green, blue, and alpha source blending factors are computed. The initial value is One.</param>
        /// <param name="dstFactor">specifies how the red, green, blue, and alpha destination blending factors are computed. The initial value is Zero.</param>
        /// <seealso cref="GetBlendSrcRgb"/>
        /// <seealso cref="GetBlendSrcAlpha"/>
        /// <seealso cref="GetBlendDstRgb"/>
        /// <seealso cref="GetBlendDstAlpha"/>
        /// <seealso cref="BlendColor(Color4)"/>
        /// <seealso cref="BlendEquation"/>
        /// <seealso cref="BlendEquationSeparate"/>
        /// <seealso cref="Clear"/>
        /// <seealso cref="Enable"/>
        /// <seealso cref="StencilFunc"/>
        /// <seealso cref="BlendFuncSeparate"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void BlendFunc(BlendingFactorSrc srcFactor, BlendingFactorDest dstFactor);

        /// <summary>
        /// Specify pixel arithmetic for RGB and alpha components separately.
        /// </summary>
        /// <remarks>
        /// Pixels can be drawn using a function that blends the incoming (source) RGBA values with the RGBA values that are already in the frame buffer (the destination values).
        /// Blending is initially disabled.
        /// Use <see cref="Enable"/> and <see cref="Disable"/> with argument <see cref="EnableCap.Blend"/> to enable and disable blending.
        ///
        /// <para><c>BlendFuncSeparate</c> defines the operation of blending when it is enabled.
        /// ASrcRgb specifies which method is used to scale the source RGB-color components.
        /// ADstRGB specifies which method is used to scale the destination RGB-color components.
        /// Likewise, ASrcAlpha specifies which method is used to scale the source alpha color component, 
        /// and ADstAlpha specifies which method is used to scale the destination alpha component.
        /// See <see cref="BlendFactor"/> for a description of the possible operations.</para>
        ///
        /// <para><b>Note</b>: incoming (source) alpha is correctly thought of as a material opacity, ranging from 1.0, 
        /// representing complete opacity, to 0.0, representing complete transparency.</para>
        ///
        /// <para><b>OpenGL API</b>: glBlendFuncSeparate</para>
        /// </remarks>
        /// <param name="srcRgb">specifies how the red, green and blue source blending factors are computed. The initial value is One.</param>
        /// <param name="dstRgb">specifies how the red, green and blue destination blending factors are computed. The initial value is Zero.</param>
        /// <param name="srcAlpha">specifies how the alpha source blending factor is computed. The initial value is One.</param>
        /// <param name="dstAlpha">specifies how the alpha destination blending factor is computed. The initial value is Zero.</param>
        /// <seealso cref="GetBlendSrcRgb"/>
        /// <seealso cref="GetBlendSrcAlpha"/>
        /// <seealso cref="GetBlendDstRgb"/>
        /// <seealso cref="GetBlendDstAlpha"/>
        /// <seealso cref="BlendColor(Color4)"/>
        /// <seealso cref="BlendEquation"/>
        /// <seealso cref="BlendEquationSeparate"/>
        /// <seealso cref="Clear"/>
        /// <seealso cref="Enable"/>
        /// <seealso cref="StencilFunc"/>
        /// <seealso cref="BlendFunc"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void BlendFuncSeparate(
            BlendingFactorSrc srcRgb, BlendingFactorDest dstRgb,
            BlendingFactorSrc srcAlpha, BlendingFactorDest dstAlpha);

        /// <summary>
        /// Gets the current RGB source blend function.
        /// </summary>
        /// <remarks>
        /// <b>OpenGL API</b>: glGetIntegerv(GL_BLEND_SRC_RGB)
        /// </remarks>
        /// <returns>The blend function.</returns>
        /// <seealso cref="BlendFunc"/>
        /// <seealso cref="BlendFuncSeparate"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        BlendingFactorSrc GetBlendSrcRgb();

        /// <summary>
        /// Gets the current alpha source blend function.
        /// </summary>
        /// <remarks>
        /// <b>OpenGL API</b>: glGetIntegerv(GL_BLEND_SRC_ALPHA)
        /// </remarks>
        /// <returns>The blend function.</returns>
        /// <seealso cref="BlendFunc"/>
        /// <seealso cref="BlendFuncSeparate"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        BlendingFactorSrc GetBlendSrcAlpha();

        /// <summary>
        /// Gets the current RGB destination blend function.
        /// </summary>
        /// <remarks>
        /// <b>OpenGL API</b>: glGetIntegerv(GL_BLEND_DST_RGB)
        /// </remarks>
        /// <returns>The blend function.</returns>
        /// <seealso cref="BlendFunc"/>
        /// <seealso cref="BlendFuncSeparate"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        BlendingFactorDest GetBlendDstRgb();

        /// <summary>
        /// Gets the current alpha destination blend function.
        /// </summary>
        /// <remarks>
        /// <b>OpenGL API</b>: glGetIntegerv(GL_BLEND_DST_ALPHA)
        /// </remarks>
        /// <returns>The blend function.</returns>
        /// <seealso cref="BlendFunc"/>
        /// <seealso cref="BlendFuncSeparate"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        BlendingFactorDest GetBlendDstAlpha();

        /// <summary>
        /// Specify the equation used for both the RGB blend equation and the Alpha blend equation.
        /// </summary>
        /// <remarks>
        /// The blend equations determine how a new pixel (the 'source' color) is combined with a pixel already in the framebuffer (the 'destination' color).
        /// This function sets both the RGB blend equation and the alpha blend equation to a single equation.
        ///
        /// <para>These equations use the source and destination blend factors specified by either <see cref="BlendFunc"/> or <see cref="BlendFuncSeparate"/>.
        /// See <see cref="BlendFunc"/> or <see cref="BlendFuncSeparate"/> for a description of the various blend factors.</para>
        ///
        /// <para>See <see cref="BlendEquationMode"/> for a description of the available options.
        /// The results of these equations are clamped to the range 0..1.</para>
        ///
        /// <para>The Add equation is useful for anti-aliasing and transparency, among other things.</para>
        ///
        /// <para>Initially, both the RGB blend equation and the alpha blend equation are set to Add.</para>
        ///
        /// <para><b>OpenGL API</b>: glBlendEquation</para>
        /// </remarks>
        /// <param name="equation">specifies how source and destination colors are combined.</param>
        /// <seealso cref="GetBlendEquationRgb"/>
        /// <seealso cref="GetBlendEquationAlpha"/>
        /// <seealso cref="BlendColor(Color4)"/>
        /// <seealso cref="BlendEquationSeparate"/>
        /// <seealso cref="BlendFunc"/>
        /// <seealso cref="BlendFuncSeparate"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void BlendEquation(BlendEquationMode equation);

        /// <summary>
        /// Set the RGB blend equation and the alpha blend equation separately.
        /// </summary>
        /// <remarks>
        /// The blend equations determine how a new pixel (the 'source' color) is combined with a pixel already in the framebuffer (the 'destination' color).
        /// This function specifies one blend equation for the RGB-color components and one blend equation for the alpha component.
        ///
        /// <para>These equations use the source and destination blend factors specified by either <see cref="BlendFunc"/> or <see cref="BlendFuncSeparate"/>.
        /// See <see cref="BlendFunc"/> or <see cref="BlendFuncSeparate"/> for a description of the various blend factors.</para>
        ///
        /// <para>See <see cref="BlendEquationMode"/> for a description of the available options.
        /// The results of these equations are clamped to the range 0..1.</para>
        ///
        /// <para>The Add equation is useful for anti-aliasing and transparency, among other things.</para>
        ///
        /// <para>Initially, both the RGB blend equation and the alpha blend equation are set to Add.</para>
        ///
        /// <para><b>OpenGL API</b>: glBlendEquation</para>
        /// </remarks>
        /// <param name="equationRgb">specifies how the red, green, and blue components of the source and destination colors are combined.</param>
        /// <param name="equationAlpha">specifies how the alpha component of the source and destination colors are combined</param>
        /// <seealso cref="GetBlendEquationRgb"/>
        /// <seealso cref="GetBlendEquationAlpha"/>
        /// <seealso cref="BlendColor(Color4)"/>
        /// <seealso cref="BlendEquation"/>
        /// <seealso cref="BlendFunc"/>
        /// <seealso cref="BlendFuncSeparate"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void BlendEquationSeparate(BlendEquationMode equationRgb, BlendEquationMode equationAlpha);

        /// <summary>
        /// Gets the current RGB blend equation.
        /// </summary>
        /// <remarks>
        /// <b>OpenGL API</b>: glGetIntegerv(GL_BLEND_EQUATION_RGB)
        /// </remarks>
        /// <returns>The blend equation.</returns>
        /// <seealso cref="BlendEquation"/>
        /// <seealso cref="BlendEquationSeparate"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        BlendEquationMode GetBlendEquationRgb();

        /// <summary>
        /// Gets the current alpha blend equation.
        /// </summary>
        /// <remarks>
        /// <b>OpenGL API</b>: glGetIntegerv(GL_BLEND_EQUATION_ALPHA)
        /// </remarks>
        /// <returns>The blend equation.</returns>
        /// <seealso cref="BlendEquation"/>
        /// <seealso cref="BlendEquationSeparate"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        BlendEquationMode GetBlendEquationAlpha();

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
        /// <param name="red">red component.</param>
        /// <param name="green">green component.</param>
        /// <param name="blue">blue component.</param>
        /// <param name="alpha">alpha component.</param>
        /// <seealso cref="GetBlendColor"/>
        /// <seealso cref="BlendEquation"/>
        /// <seealso cref="BlendFunc"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void BlendColor(float red, float green, float blue, float alpha);

        /// <summary>
        /// Gets the current blend color.
        /// </summary>
        /// <remarks>
        /// <b>OpenGL API</b>: glGetFloatv(GL_BLEND_COLOR)
        /// </remarks>
        /// <returns>The blend color.</returns>
        /// <seealso cref="BlendColor(Color4)"/>
        /// <seealso cref="BlendColor(float, float, float, float)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Color4 GetBlendColor();

        /// <summary>
        /// Set front and back function and reference value for stencil testing.
        /// </summary>
        /// <remarks>
        /// Stenciling, like depth-buffering, enables and disables drawing on a per-pixel basis.
        /// Stencil planes are first drawn into using GL drawing primitives, then geometry and images are rendered using the stencil planes to mask out portions of the screen.
        /// Stenciling is typically used in multi-pass rendering algorithms to achieve special effects, such as decals, outlining, 
        /// and constructive solid geometry rendering.
        ///
        /// <para>The stencil test conditionally eliminates a pixel based on the outcome of a comparison between the reference value and the value in the stencil buffer.
        /// To enable and disable the test, call <see cref="GLES2.Enable"/> and <see cref="GLES2.Disable"/> with argument <see cref="EnableCap.StencilTest"/>.
        /// To specify actions based on the outcome of the stencil test, call <see cref="GLES2.StencilOperation"/> or <see cref="GLES2.StencilOperationSeparate"/>.</para>
        ///
        /// <para>There can be two separate sets of <paramref name="func"/>, <paramref name="reference"/>, and <paramref name="mask"/> parameters; 
        /// one affects back-facing polygons, and the other affects front-facing polygons as well as other non-polygon primitives.
        /// <c>StencilFunc</c> sets both front and back stencil state to the same values.
        /// Use <see cref="StencilFuncSeparate"/> to set front and back stencil state to different values.</para>
        ///
        /// <para><paramref name="func"/> is an enum that determines the stencil comparison function.
        /// It accepts one of eight values (see <see cref="CompareFunc"/>).</para>
        ///
        /// <para><paramref name="reference"/> is an integer reference value that is used in the stencil comparison.
        /// It is clamped to the range [0, 2n - 1], where n is the number of bitplanes in the stencil buffer.</para>
        ///
        /// <para><paramref name="mask"/> is bitwise ANDed with both the reference value and the stored stencil value, 
        /// with the ANDed values participating in the comparison.</para>
        ///
        /// <para><b>Note</b>: initially, the stencil test is disabled.
        /// If there is no stencil buffer, no stencil modification can occur and it is as if the stencil test always passes.</para>
        ///
        /// <para><b>Note</b>: <c>StencilFunc</c> is the same as calling <see cref="StencilFuncSeparate"/> with <c>face</c> set to <see cref="Face.FrontAndBack"/>.</para>
        ///
        /// <para><b>OpenGL API</b>: glStencilFunc</para>
        /// </remarks>
        /// <param name="func">the test function. Initial value is <see cref="CompareFunc.Always"/>.</param>
        /// <param name="reference">(optional) value that specifies the reference value for the stencil test. 
        /// <c>reference</c> is clamped to the range [0, 2n - 1], where n is the number of bitplanes in the stencil buffer. 
        /// The initial value is 0.</param>
        /// <param name="mask">(optional) value that specifies a mask that is ANDed with both the reference value and the stored stencil value when the test is done.
        /// The initial value is all 1's.</param>
        /// <seealso cref="GetStencilFunc"/>
        /// <seealso cref="GetStencilValueMask"/>
        /// <seealso cref="GetStencilRef"/>
        /// <seealso cref="GetStencilBackFunc"/>
        /// <seealso cref="GetStencilBackValueMask"/>
        /// <seealso cref="GetStencilBackRef"/>
        /// <seealso cref="Framebuffer.StencilBits"/>
        /// <seealso cref="BlendFunc"/>
        /// <seealso cref="DepthFunc"/>
        /// <seealso cref="Enable"/>
        /// <seealso cref="StencilFuncSeparate"/>
        /// <seealso cref="StencilMask"/>
        /// <seealso cref="StencilMaskSeparate"/>
        /// <seealso cref="StencilOperation"/>
        /// <seealso cref="StencilOperationSeparate"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void StencilFunc(StencilFunction func, int reference = 0, uint mask = 0xffffffff);

        /// <summary>
        /// Set front and/or back function and reference value for stencil testing.
        /// </summary>
        /// <remarks>
        /// See <see cref="StencilFunc"/> for more details.
        ///
        /// <para><b>OpenGL API</b>: glStencilFuncSeparate</para>
        /// </remarks>
        /// <param name="face">specifies whether front and/or back stencil state is updated.</param>
        /// <param name="func">the test function. Initial value is <see cref="CompareFunc.Always"/>.</param>
        /// <param name="reference">(optional) value that specifies the reference value for the stencil test. 
        /// <c>reference</c> is clamped to the range [0, 2n - 1], where n is the number of bitplanes in the stencil buffer. 
        /// The initial value is 0.</param>
        /// <param name="mask">(optional) value that specifies a mask that is ANDed with both the reference value and the stored stencil value when the test is done.
        /// The initial value is all 1's.</param>
        /// <seealso cref="GetStencilFunc"/>
        /// <seealso cref="GetStencilValueMask"/>
        /// <seealso cref="GetStencilRef"/>
        /// <seealso cref="GetStencilBackFunc"/>
        /// <seealso cref="GetStencilBackValueMask"/>
        /// <seealso cref="GetStencilBackRef"/>
        /// <seealso cref="Framebuffer.StencilBits"/>
        /// <seealso cref="BlendFunc"/>
        /// <seealso cref="DepthFunc"/>
        /// <seealso cref="Enable"/>
        /// <seealso cref="StencilFunc"/>
        /// <seealso cref="StencilMask"/>
        /// <seealso cref="StencilMaskSeparate"/>
        /// <seealso cref="StencilOperation"/>
        /// <seealso cref="StencilOperationSeparate"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void StencilFuncSeparate(CullFaceMode face,
            StencilFunction func, int reference = 0, uint mask = 0xffffffff);

        /// <summary>
        /// Get what function is used to compare the stencil reference value with the stencil buffer value for front-facing polygons and non-polygons.
        /// </summary>
        /// <remarks>
        /// The initial value is <see cref="CompareFunc.Always"/>.
        ///
        /// <para><b>OpenGL API</b>: glGetIntegerv(GL_STENCIL_FUNC)</para>
        /// </remarks>
        /// <returns>The compare function</returns>
        /// <seealso cref="StencilFunc"/>
        /// <seealso cref="StencilFuncSeparate"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        StencilFunction GetStencilFunc();

        /// <summary>
        /// Get the mask that is used to mask both the stencil reference value and the stencil buffer value before they are compared for front-facing polygons and non-polygons.
        /// </summary>
        /// <remarks>
        /// The initial value is all 1's.
        ///
        /// <para><b>OpenGL API</b>: glGetIntegerv(GL_STENCIL_VALUE_MASK)</para>
        /// </remarks>
        /// <returns>The mask value</returns>
        /// <seealso cref="StencilFunc"/>
        /// <seealso cref="StencilFuncSeparate"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        uint GetStencilValueMask();

        /// <summary>
        /// Get the reference value that is compared with the contents of the stencil buffer for front-facing polygons and non-polygons.
        /// </summary>
        /// <remarks>
        /// The initial value is 0.
        ///
        /// <para><b>OpenGL API</b>: glGetIntegerv(GL_STENCIL_REF)</para>
        /// </remarks>
        /// <returns>The reference value</returns>
        /// <seealso cref="StencilFunc"/>
        /// <seealso cref="StencilFuncSeparate"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        int GetStencilRef();

        /// <summary>
        /// Get what function is used for back-facing polygons to compare the stencil reference value with the stencil buffer value.
        /// </summary>
        /// <remarks>
        /// The initial value is <see cref="CompareFunc.Always"/>.
        ///
        /// <para><b>OpenGL API</b>: glGetIntegerv(GL_STENCIL_BACK_FUNC)</para>
        /// </remarks>
        /// <returns>The compare function</returns>
        /// <seealso cref="StencilFunc"/>
        /// <seealso cref="StencilFuncSeparate"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        StencilFunction GetStencilBackFunc();

        /// <summary>
        /// Get the mask that is used for back-facing polygons to mask both the stencil reference value and the stencil buffer value before they are compared.
        /// </summary>
        /// <remarks>
        /// The initial value is all 1's.
        ///
        /// <para><b>OpenGL API</b>: glGetIntegerv(GL_STENCIL_BACK_VALUE_MASK)</para>
        /// </remarks>
        /// <returns>The mask</returns>
        /// <seealso cref="StencilFunc"/>
        /// <seealso cref="StencilFuncSeparate"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        uint GetStencilBackValueMask();

        /// <summary>
        /// Get the reference value that is compared with the contents of the stencil buffer for back-facing polygons.
        /// </summary>
        /// <remarks>
        /// The initial value is 0.
        ///
        /// <para><b>OpenGL API</b>: glGetIntegerv(GL_STENCIL_REF)</para>
        /// </remarks>
        /// <returns>The reference value</returns>
        /// <seealso cref="StencilFunc"/>
        /// <seealso cref="StencilFuncSeparate"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        int GetStencilBackRef();

        /// <summary>
        /// Set front and back stencil test actions.
        /// </summary>
        /// <remarks>
        /// Stenciling, like depth-buffering, enables and disables drawing on a per-pixel basis.
        /// You draw into the stencil planes using GL drawing primitives, then render geometry and images, 
        /// using the stencil planes to mask out portions of the screen.
        /// Stenciling is typically used in multi-pass rendering algorithms to achieve special effects, 
        /// such as decals, outlining, and constructive solid geometry rendering.
        ///
        /// <para>The stencil test conditionally eliminates a pixel based on the outcome of a comparison between the value in the stencil buffer and a reference value.
        /// To enable and disable the test, call <see cref="GLES2.Enable"/> and <see cref="GLES2.Disable"/> with argument <see cref="EnableCap.StencilTest"/>; 
        /// to control it, call <see cref="GLES2.StencilFunc"/> or <see cref="GLES2.StencilFuncSeparate"/>.</para>
        ///
        /// <para>There can be two separate sets of <paramref name="stencilFail"/>, <paramref name="depthFail"/> and <paramref name="bothPass"/> parameters; 
        /// one affects back-facing polygons, and the other affects front-facing polygons as well as other non-polygon primitives.
        /// <c>StencilOperation</c> sets both front and back stencil state to the same values.
        /// Use <see cref="GLES2.StencilOperationSeparate"/> to set front and back stencil state to different values.</para>
        ///
        /// <para><c>StencilOperation</c> takes three arguments that indicate what happens to the stored stencil value while stenciling is enabled.
        /// If the stencil test fails, no change is made to the pixel's color or depth buffers, and <paramref name="stencilFail"/> specifies what happens to the stencil buffer contents.</para>
        ///
        /// <para>Stencil buffer values are treated as unsigned integers.
        /// When incremented and decremented, values are clamped to 0 and 2n - 1, where n is the value returned by <see cref="Framebuffer.StencilBits"/>.</para>
        ///
        /// <para>The other two arguments to <c>StencilOperation</c> specify stencil buffer actions that depend on whether subsequent depth buffer tests 
        /// succeed (<paramref name="bothPass"/>) or fail (<paramref name="depthFail"/>) (see <see cref="DepthFunc"/>).
        /// Note that <paramref name="depthFail"/> is ignored when there is no depth buffer, or when the depth buffer is not enabled.
        /// In these cases, <paramref name="stencilFail"/> and <paramref name="bothPass"/> specify stencil action when the stencil test fails and passes, respectively.</para>
        ///
        /// <para><b>Note</b>: initially the stencil test is disabled.
        /// If there is no stencil buffer, no stencil modification can occur and it is as if the stencil tests always pass, 
        /// regardless of any call to <c>StencilOperation</c>.</para>
        ///
        /// <para><b>Note</b>: <c>StencilOperation</c> is the same as calling <see cref="GLES2.StencilOperationSeparate"/> 
        /// with <c>face</c> set to <see cref="Face.FrontAndBack"/>.</para>
        ///
        /// <para><b>OpenGL API</b>: glStencilOp</para>
        /// </remarks>
        /// <param name="stencilFail">(optional) value that specifies the action to take when the stencil test fails. The initial value is Keep.</param>
        /// <param name="depthFail">(optional) value that specifies the action to take when the stencil test passes, but the depth test fails. The initial value is Keep.</param>
        /// <param name="bothPass">(optional) value that specifies the action to take when both the stencil test and depth test pass, 
        /// or when the stencil test passes and either there is no depth buffer or depth testing is not enabled. The initial value is Keep.</param>
        /// <seealso cref="GetStencilFail"/>
        /// <seealso cref="GetStencilPassDepthPass"/>
        /// <seealso cref="GetStencilPassDepthFail"/>
        /// <seealso cref="GetStencilBackFail"/>
        /// <seealso cref="GetStencilBackPassDepthPass"/>
        /// <seealso cref="GetStencilBackPassDepthFail"/>
        /// <seealso cref="Framebuffer.StencilBits"/>
        /// <seealso cref="BlendFunc"/>
        /// <seealso cref="DepthFunc"/>
        /// <seealso cref="Enable"/>
        /// <seealso cref="StencilFunc"/>
        /// <seealso cref="StencilFuncSeparate"/>
        /// <seealso cref="StencilMask"/>
        /// <seealso cref="StencilMaskSeparate"/>
        /// <seealso cref="StencilOperationSeparate"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void StencilOperation(
            StencilOp stencilFail = StencilOp.Keep,
            StencilOp depthFail = StencilOp.Keep,
            StencilOp bothPass = StencilOp.Keep);

        /// <summary>
        /// Set front and/or back stencil test actions.
        /// </summary>
        /// <remarks>
        /// See <see cref="StencilOperation"/> for more details.
        ///
        /// <para><b>OpenGL API</b>: glStencilOpSeparate</para>
        /// </remarks>
        /// <param name="face">specifies whether front and/or back stencil state is updated.</param>
        /// <param name="stencilFail">(optional) value that specifies the action to take when the stencil test fails. The initial value is Keep.</param>
        /// <param name="depthFail">(optional) value that specifies the action to take when the stencil test passes, but the depth test fails. The initial value is Keep.</param>
        /// <param name="bothPass">(optional) value that specifies the action to take when both the stencil test and depth test pass, 
        /// or when the stencil test passes and either there is no depth buffer or depth testing is not enabled. The initial value is Keep.</param>
        /// <seealso cref="GetStencilFail"/>
        /// <seealso cref="GetStencilPassDepthPass"/>
        /// <seealso cref="GetStencilPassDepthFail"/>
        /// <seealso cref="GetStencilBackFail"/>
        /// <seealso cref="GetStencilBackPassDepthPass"/>
        /// <seealso cref="GetStencilBackPassDepthFail"/>
        /// <seealso cref="Framebuffer.StencilBits"/>
        /// <seealso cref="BlendFunc"/>
        /// <seealso cref="DepthFunc"/>
        /// <seealso cref="Enable"/>
        /// <seealso cref="StencilFunc"/>
        /// <seealso cref="StencilFuncSeparate"/>
        /// <seealso cref="StencilMask"/>
        /// <seealso cref="StencilMaskSeparate"/>
        /// <seealso cref="StencilOperation"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void StencilOperationSeparate(CullFaceMode face,
            StencilOp stencilFail = StencilOp.Keep, StencilOp depthFail = StencilOp.Keep, StencilOp bothPass = StencilOp.Keep);

        /// <summary>
        /// Get what action is taken when the stencil test fails for front-facing polygons and non-polygons.
        /// </summary>
        /// <remarks>
        /// The initial value is Keep.
        ///
        /// <para><b>OpenGL API</b>: glGetIntegerv(GL_STENCIL_FAIL)</para>
        /// </remarks>
        /// <returns>The stencil operation</returns>
        /// <seealso cref="StencilOperation"/>
        /// <seealso cref="StencilOperationSeparate"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        StencilOp GetStencilFail();

        /// <summary>
        /// Get what action is taken when the stencil test passes and the depth test passes for front-facing polygons and non-polygons.
        /// </summary>
        /// <remarks>
        /// The initial value is Keep.
        ///
        /// <para><b>OpenGL API</b>: glGetIntegerv(GL_STENCIL_PASS_DEPTH_PASS)</para>
        /// </remarks>
        /// <returns>The stencil operation</returns>
        /// <seealso cref="StencilOperation"/>
        /// <seealso cref="StencilOperationSeparate"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        StencilOp GetStencilPassDepthPass();

        /// <summary>
        /// Get what action is taken when the stencil test passes, but the depth test fails for front-facing polygons and non-polygons.
        /// </summary>
        /// <remarks>
        /// The initial value is Keep.
        ///
        /// <para><b>OpenGL API</b>: glGetIntegerv(GL_STENCIL_PASS_DEPTH_FAIL)</para>
        /// </remarks>
        /// <returns>The stencil operation</returns>
        /// <seealso cref="StencilOperation"/>
        /// <seealso cref="StencilOperationSeparate"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        StencilOp GetStencilPassDepthFail();

        /// <summary>
        /// Get what action is taken for back-facing polygons when the stencil test fails.
        /// </summary>
        /// <remarks>
        /// The initial value is Keep.
        ///
        /// <para><b>OpenGL API</b>: glGetIntegerv(GL_STENCIL_BACK_FAIL)</para>
        /// </remarks>
        /// <returns>The stencil operation</returns>
        /// <seealso cref="StencilOperation"/>
        /// <seealso cref="StencilOperationSeparate"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        StencilOp GetStencilBackFail();

        /// <summary>
        /// Get what action is taken for back-facing polygons when the stencil test passes and the depth test passes.
        /// </summary>
        /// <remarks>
        /// The initial value is Keep.
        ///
        /// <para><b>OpenGL API</b>: glGetIntegerv(GL_STENCIL_BACK_PASS_DEPTH_PASS)</para>
        /// </remarks>
        /// <returns>The stencil operation</returns>
        /// <seealso cref="StencilOperation"/>
        /// <seealso cref="StencilOperationSeparate"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        StencilOp GetStencilBackPassDepthPass();

        /// <summary>
        /// Get what action is taken for back-facing polygons when the stencil test passes, but the depth test fails.
        /// </summary>
        /// <remarks>
        /// The initial value is Keep.
        ///
        /// <para><b>OpenGL API</b>: glGetIntegerv(GL_STENCIL_BACK_PASS_DEPTH_FAIL)</para>
        /// </remarks>
        /// <returns>The stencil operation</returns>
        /// <seealso cref="StencilOperation"/>
        /// <seealso cref="StencilOperationSeparate"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        StencilOp GetStencilBackPassDepthFail();

        /// <summary>
        /// Specify the value used for depth buffer comparisons.
        /// </summary>
        /// <remarks>
        /// This method specifies the function used to compare each incoming pixel depth value with the depth value present in the depth buffer.
        /// The comparison is performed only if depth testing is enabled.
        /// (See <see cref="GLES2.Enable"/> and <see cref="GLES2.Disable"/> of <see cref="EnableCap.DepthTest"/>.)
        ///
        /// <para>Initially, depth testing is disabled.
        /// If depth testing is disabled or no depth buffer exists, it is as if the depth test always passes.</para>
        ///
        /// <para><b>Note</b>: even if the depth buffer exists and the depth mask is non-zero, 
        /// the depth buffer is not updated if the depth test is disabled.</para>
        ///
        /// <para><b>OpenGL API</b>: glDepthFunc</para>
        /// </remarks>
        /// <param name="func">specifies the depth comparison function. The initial value is <see cref="CompareFunc.Less"/>.</param>
        /// <seealso cref="GetDepthFunc"/>
        /// <seealso cref="Enable"/>
        /// <seealso cref="DepthRange"/>
        /// <seealso cref="PolygonOffset"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void DepthFunc(DepthFunction func);

        /// <summary>
        /// Gets the current depth function.
        /// </summary>
        /// <remarks>
        /// <b>OpenGL API</b>: glGetIntegerv(GL_DEPTH_FUNC)
        /// </remarks>
        /// <returns>The current depth function.</returns>
        /// <seealso cref="DepthFunc"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        DepthFunction GetDepthFunc();

        /// <summary>
        /// Define the scissor box.
        /// </summary>
        /// <remarks>
        /// When a GL" context is first attached to a window, the width and height are set to the dimensions of that window.
        ///
        /// <para>This method defines a rectangle, called the scissor box, in window coordinates.
        /// The first two arguments, <paramref name="left"/> and <paramref name="bottom"/>, specify the lower left corner of the box.
        /// <paramref name="width"/> and <paramref name="height"/> specify the width and height of the box.</para>
        ///
        /// <para>To enable and disable the scissor test, call <see cref="Enable"/> and <see cref="Disable"/> with <see cref="EnableCap.ScissorTest"/>.
        /// The test is initially disabled.
        /// While the test is enabled, only pixels that lie within the scissor box can be modified by drawing commands.
        /// Window coordinates have integer values at the shared corners of frame buffer pixels.
        /// <c>Scissor(0, 0, 1, 1)</c> allows modification of only the lower left pixel in the window, 
        /// and <c>Scissor(0, 0, 0, 0)</c> doesn't allow modification of any pixels in the window.</para>
        ///
        /// <para>When the scissor test is disabled, it is as though the scissor box includes the entire window.</para>
        ///
        /// <para><b>OpenGL API</b>: glScissor</para>
        /// </remarks>
        /// <param name="left">X-coordinate of the lower left corner of the scissor box. The initial value is 0.</param>
        /// <param name="bottom">Y-coordinate of the lower left corner of the scissor box. The initial value is 0.</param>
        /// <param name="width">width of the scissor box.</param>
        /// <param name="height">height of the scissor box.</param>
        /// <exception cref="GLException">InvalidValue if either <paramref name="width"/> or <paramref name="height"/> is negative.</exception>
        /// <seealso cref="GetScissor"/>
        /// <seealso cref="Enable"/>
        /// <seealso cref="Viewport(int, int, int, int)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void Scissor(int left, int bottom, int width, int height);

        /// <summary>
        /// Get the current scissor box.
        /// </summary>
        /// <remarks>
        /// <b>OpenGL API</b>: glGetIntegerv(GL_SCISSOR_BOX)
        /// </remarks>
        /// <param name="left">is set to the X-coordinate of the lower left corner of the scissor box.</param>
        /// <param name="bottom">is set to the Y-coordinate of the lower left corner of the scissor box.</param>
        /// <param name="width">is set to the width of the scissor box.</param>
        /// <param name="height">is set to the height of the scissor box.</param>
        /// <seealso cref="Scissor"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void GetScissor(out int left, out int bottom, out int width, out int height);

        /// <summary>
        /// Specify multisample coverage parameters.
        /// </summary>
        /// <remarks>
        /// Multisampling samples a pixel multiple times at various implementation-dependent subpixel locations to generate antialiasing effects.
        /// Multisampling transparently antialiases points, lines, and polygons if it is enabled.
        ///
        /// <para><paramref name="value"/> is used in constructing a temporary mask used in determining which samples will be used in resolving the final fragment color.
        /// This mask is bitwise-anded with the coverage mask generated from the multisampling computation.
        /// If the <paramref name="invert"/> flag is set, the temporary mask is inverted (all bits flipped) and then the bitwise-and is computed.</para>
        ///
        /// <para>If an implementation does not have any multisample buffers available, or multisampling is disabled, 
        /// rasterization occurs with only a single sample computing a pixel's final RGB color.</para>
        ///
        /// <para>Provided an implementation supports multisample buffers, and multisampling is enabled, 
        /// then a pixel's final color is generated by combining several samples per pixel.
        /// Each sample contains color, depth, and stencil information, allowing those operations to be performed on each sample.</para>
        ///
        /// <para><b>OpenGL API</b>: glSampleCoverage</para>
        /// </remarks>
        /// <param name="value">sample coverage value. The value is clamped to the range 0..1. The initial value is 1.0.</param>
        /// <param name="invert">(optional) value representing if the coverage masks should be inverted. Defaults to False.</param>
        /// <seealso cref="GetSampleCoverageValue"/>
        /// <seealso cref="GetSampleCoverageInvert"/>
        /// <seealso cref="GetSampleAlphaToCoverage"/>
        /// <seealso cref="GetSampleCoverage"/>
        /// <seealso cref="Framebuffer.SampleBuffers"/>
        /// <seealso cref="Framebuffer.Samples"/>
        /// <seealso cref="Enable"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void SampleCoverage(float value, bool invert = false);

        /// <summary>
        /// Get the current sample coverage value.
        /// </summary>
        /// <remarks>
        /// <b>OpenGL API</b>: glGetFloatv(GL_SAMPLE_COVERAGE_VALUE)
        /// </remarks>
        /// <returns>The current sample coverage value.</returns>
        /// <seealso cref="SampleCoverage"/>
        /// <seealso cref="GetSampleCoverageInvert"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        float GetSampleCoverageValue();

        /// <summary>
        /// Get the current sample coverage invert flag.
        /// </summary>
        /// <remarks>
        /// <b>OpenGL API</b>: glGetIntegerv(GL_SAMPLE_COVERAGE_INVERT)
        /// </remarks>
        /// <returns>The current sample coverage invert flag.</returns>
        /// <seealso cref="SampleCoverage"/>
        /// <seealso cref="GetSampleCoverageValue"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        bool GetSampleCoverageInvert();

        /// <summary>
        /// Get a boolean value indicating if the fragment coverage value should be ANDed with a temporary coverage value based on the current sample coverage value.
        /// </summary>
        /// <remarks>
        /// The initial value is False.
        ///
        /// <para><b>OpenGL API</b>: glGetIntegerv(GL_SAMPLE_COVERAGE)</para>
        /// </remarks>
        /// <returns>The coverage flag</returns>
        /// <seealso cref="SampleCoverage"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        bool GetSampleCoverage();

        /// <summary>
        /// Get a boolean value indicating if the fragment coverage value should be ANDed with a temporary coverage value based on the fragment's alpha value.
        /// </summary>
        /// <remarks>
        /// The initial value is False.
        ///
        /// <para><b>OpenGL API</b>: glGetIntegerv(GL_SAMPLE_ALPHA_TO_COVERAGE)</para>
        /// </remarks>
        /// <returns>The coverage flag</returns>
        /// <seealso cref="SampleCoverage"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        bool GetSampleAlphaToCoverage();

        #endregion

        #region Vertex Arrays

        /// <summary>
        /// Render primitives from array data, <b>without</b> using indices.
        /// </summary>
        /// <remarks>
        /// DrawArrays specifies multiple geometric primitives with very few subroutine calls.
        /// Instead of calling a GL procedure to pass each individual vertex attribute, 
        /// you can use <see cref="VertexAttribute"/> to prespecify separate arrays of vertices, normals, 
        /// and colors and use them to construct a sequence of primitives with a single call to DrawArrays.
        ///
        /// <para>When DrawArrays is called, it uses <paramref name="count"/> sequential elements from each enabled array 
        /// to construct a sequence of geometric primitives, beginning with the first element.
        /// <paramref name="type"/> specifies what kind of primitives are constructed and how the array elements construct those primitives.</para>
        ///
        /// <para>To enable and disable generic vertex attribute array, call <see cref="VertexAttribute.Enable"/> and <see cref="VertexAttribute.Disable"/>.</para>
        ///
        /// <para><b>Note</b>: if the current program object, as set by <see cref="ShaderProgram.Use"/>, is invalid, rendering results are undefined.
        /// However, no error is generated for this case.</para>
        ///
        /// <para><b>OpenGL API</b>: glDrawArrays</para>
        /// </remarks>
        /// <param name="type">specifies what kind of primitives to render.</param>
        /// <param name="count">the number of elements to be rendered.</param>
        /// <exception cref="GLException">InvalidValue if <paramref name="count"/> is negative.</exception>
        /// <exception cref="GLException">InvalidFramebufferOperation if the currently bound framebuffer is not framebuffer complete.</exception>
        /// <seealso cref="Framebuffer.Status"/>
        /// <seealso cref="VertexAttribute"/>
        /// <seealso cref="VertexAttribute.Enable"/>
        /// <seealso cref="VertexAttribute.Disable"/>
        /// <seealso cref="DrawElements(PrimitiveType, ushort[], int, int)"/>
        /// <seealso cref="ShaderProgram.Use"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void DrawArrays(PrimitiveType type, int count);

        /// <summary>
        /// Render primitives from array data, <b>without</b> using indices.
        /// </summary>
        /// <remarks>
        /// DrawArrays specifies multiple geometric primitives with very few subroutine calls.
        /// Instead of calling a GL procedure to pass each individual vertex attribute, 
        /// you can use <see cref="VertexAttribute"/> to prespecify separate arrays of vertices, normals, 
        /// and colors and use them to construct a sequence of primitives with a single call to DrawArrays.
        ///
        /// <para>When DrawArrays is called, it uses <paramref name="count"/> sequential elements from each enabled array 
        /// to construct a sequence of geometric primitives, beginning with element <paramref name="first"/>.
        /// <paramref name="type"/> specifies what kind of primitives are constructed and how the array elements construct those primitives.</para>
        ///
        /// <para>To enable and disable generic vertex attribute array, call <see cref="VertexAttribute.Enable"/> and <see cref="VertexAttribute.Disable"/>.</para>
        ///
        /// <para><b>Note</b>: if the current program object, as set by <see cref="ShaderProgram.Use"/>, is invalid, rendering results are undefined.
        /// However, no error is generated for this case.</para>
        ///
        /// <para><b>OpenGL API</b>: glDrawArrays</para>
        /// </remarks>
        /// <param name="type">specifies what kind of primitives to render.</param>
        /// <param name="first">(optional) the starting index in the enabled arrays.</param>
        /// <param name="count">the number of elements to be rendered.</param>
        /// <exception cref="GLException">InvalidValue if <paramref name="count"/> is negative.</exception>
        /// <exception cref="GLException">InvalidFramebufferOperation if the currently bound framebuffer is not framebuffer complete.</exception>
        /// <seealso cref="Framebuffer.Status"/>
        /// <seealso cref="VertexAttribute"/>
        /// <seealso cref="VertexAttribute.Enable"/>
        /// <seealso cref="VertexAttribute.Disable"/>
        /// <seealso cref="DrawElements(PrimitiveType, ushort[], int, int)"/>
        /// <seealso cref="ShaderProgram.Use"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void DrawArrays(PrimitiveType type, int first, int count);

        /// <summary>
        /// Render primitives from array data, using supplied indices.
        /// </summary>
        /// <remarks>
        /// DrawElements specifies multiple geometric primitives with very few subroutine calls.
        /// Instead of calling a GL function to pass each vertex attribute, you can use <see cref="VertexAttribute"/> to prespecify separate arrays of vertex attributes 
        /// and use them to construct a sequence of primitives with a single call to DrawElements.
        ///
        /// <para>When DrawElements is called, it uses <paramref name="indices"/> to locate vertices in the vertex array.
        /// <paramref name="type"/> specifies what kind of primitives are constructed and how the array elements construct these primitives.
        /// If more than one array is enabled, each is used.</para>
        ///
        /// <para>To enable and disable a generic vertex attribute array, call <see cref="VertexAttribute.Enable"/> and <see cref="VertexAttribute.Disable"/>.</para>
        ///
        /// <para><b>Note</b>: if the current program object, as set by <see cref="ShaderProgram.Use"/>, is invalid, rendering results are undefined.
        /// However, no error is generated for this case.</para>
        ///
        /// <para> <b>OpenGL API</b>: glDrawElements</para>
        /// </remarks>
        /// <param name="type">specifies what kind of primitives to render.</param>
        /// <param name="indices">array of indices (of type byte).</param>
        /// <exception cref="GLException">InvalidFramebufferOperation if the currently bound framebuffer is not framebuffer complete.</exception>
        /// <seealso cref="Framebuffer.Status"/>
        /// <seealso cref="VertexAttribute"/>
        /// <seealso cref="VertexAttribute.Enable"/>
        /// <seealso cref="VertexAttribute.Disable"/>
        /// <seealso cref="DrawArrays(PrimitiveType, int, int)"/>
        /// <seealso cref="ShaderProgram.Use"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void DrawElements(PrimitiveType type, byte[] indices);

        /// <summary>
        /// Render primitives from array data, using supplied indices.
        /// </summary>
        /// <remarks>
        /// DrawElements specifies multiple geometric primitives with very few subroutine calls.
        /// Instead of calling a GL function to pass each vertex attribute, you can use <see cref="VertexAttribute"/> to prespecify separate arrays of vertex attributes 
        /// and use them to construct a sequence of primitives with a single call to DrawElements.
        ///
        /// <para>When DrawElements is called, it uses <paramref name="indices"/> to locate vertices in the vertex array.
        /// <paramref name="type"/> specifies what kind of primitives are constructed and how the array elements construct these primitives.
        /// If more than one array is enabled, each is used.</para>
        ///
        /// <para>To enable and disable a generic vertex attribute array, call <see cref="VertexAttribute.Enable"/> and <see cref="VertexAttribute.Disable"/>.</para>
        ///
        /// <para><b>Note</b>: if the current program object, as set by <see cref="ShaderProgram.Use"/>, is invalid, rendering results are undefined.
        /// However, no error is generated for this case.</para>
        ///
        /// <para> <b>OpenGL API</b>: glDrawElements</para>
        /// </remarks>
        /// <param name="type">specifies what kind of primitives to render.</param>
        /// <param name="indices">array of indices (of type byte).</param>
        /// <param name="first">index to the first index in <paramref name="indices"/> to use.</param>
        /// <param name="count">number of indices in <paramref name="indices"/> to use, starting at <paramref name="count"/>.</param>
        /// <exception cref="GLException">InvalidFramebufferOperation if the currently bound framebuffer is not framebuffer complete.</exception>
        /// <seealso cref="Framebuffer.Status"/>
        /// <seealso cref="VertexAttribute"/>
        /// <seealso cref="VertexAttribute.Enable"/>
        /// <seealso cref="VertexAttribute.Disable"/>
        /// <seealso cref="DrawArrays(PrimitiveType, int, int)"/>
        /// <seealso cref="ShaderProgram.Use"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void DrawElements(PrimitiveType type, byte[] indices, int first, int count);

        /// <summary>
        /// Render primitives from array data, using supplied indices.
        /// </summary>
        /// <remarks>
        /// DrawElements specifies multiple geometric primitives with very few subroutine calls.
        /// Instead of calling a GL function to pass each vertex attribute, you can use <see cref="VertexAttribute"/> to prespecify separate arrays of vertex attributes 
        /// and use them to construct a sequence of primitives with a single call to DrawElements.
        ///
        /// <para>When DrawElements is called, it uses <paramref name="indices"/> to locate vertices in the vertex array.
        /// <paramref name="type"/> specifies what kind of primitives are constructed and how the array elements construct these primitives.
        /// If more than one array is enabled, each is used.</para>
        ///
        /// <para>To enable and disable a generic vertex attribute array, call <see cref="VertexAttribute.Enable"/> and <see cref="VertexAttribute.Disable"/>.</para>
        ///
        /// <para><b>Note</b>: if the current program object, as set by <see cref="ShaderProgram.Use"/>, is invalid, rendering results are undefined.
        /// However, no error is generated for this case.</para>
        ///
        /// <para> <b>OpenGL API</b>: glDrawElements</para>
        /// </remarks>
        /// <param name="type">specifies what kind of primitives to render.</param>
        /// <param name="indices">array of indices (of type ushort).</param>
        /// <exception cref="GLException">InvalidFramebufferOperation if the currently bound framebuffer is not framebuffer complete.</exception>
        /// <seealso cref="Framebuffer.Status"/>
        /// <seealso cref="VertexAttribute"/>
        /// <seealso cref="VertexAttribute.Enable"/>
        /// <seealso cref="VertexAttribute.Disable"/>
        /// <seealso cref="DrawArrays(PrimitiveType, int, int)"/>
        /// <seealso cref="ShaderProgram.Use"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void DrawElements(PrimitiveType type, ushort[] indices);

        /// <summary>
        /// Render primitives from array data, using supplied indices.
        /// </summary>
        /// <remarks>
        /// DrawElements specifies multiple geometric primitives with very few subroutine calls.
        /// Instead of calling a GL function to pass each vertex attribute, you can use <see cref="VertexAttribute"/> to prespecify separate arrays of vertex attributes 
        /// and use them to construct a sequence of primitives with a single call to DrawElements.
        ///
        /// <para>When DrawElements is called, it uses <paramref name="indices"/> to locate vertices in the vertex array.
        /// <paramref name="type"/> specifies what kind of primitives are constructed and how the array elements construct these primitives.
        /// If more than one array is enabled, each is used.</para>
        ///
        /// <para>To enable and disable a generic vertex attribute array, call <see cref="VertexAttribute.Enable"/> and <see cref="VertexAttribute.Disable"/>.</para>
        ///
        /// <para><b>Note</b>: if the current program object, as set by <see cref="ShaderProgram.Use"/>, is invalid, rendering results are undefined.
        /// However, no error is generated for this case.</para>
        ///
        /// <para> <b>OpenGL API</b>: glDrawElements</para>
        /// </remarks>
        /// <param name="type">specifies what kind of primitives to render.</param>
        /// <param name="indices">array of indices (of type ushort).</param>
        /// <param name="first">index to the first index in <paramref name="indices"/> to use.</param>
        /// <param name="count">number of indices in <paramref name="indices"/> to use, starting at <paramref name="count"/>.</param>
        /// <exception cref="GLException">InvalidFramebufferOperation if the currently bound framebuffer is not framebuffer complete.</exception>
        /// <seealso cref="Framebuffer.Status"/>
        /// <seealso cref="VertexAttribute"/>
        /// <seealso cref="VertexAttribute.Enable"/>
        /// <seealso cref="VertexAttribute.Disable"/>
        /// <seealso cref="DrawArrays(PrimitiveType, int, int)"/>
        /// <seealso cref="ShaderProgram.Use"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void DrawElements(PrimitiveType type, ushort[] indices, int first, int count);

        /// <summary>
        /// Render primitives from array data, using indices from a bound index buffer.
        /// </summary>
        /// <remarks>
        /// DrawElements specifies multiple geometric primitives with very few subroutine calls.
        /// Instead of calling a GL function to pass each vertex attribute, you can use <see cref="VertexAttribute"/> to prespecify 
        /// separate arrays of vertex attributes and use them to construct a sequence of primitives with a single call to DrawElements.
        ///
        /// <para><paramref name="type"/> specifies what kind of primitives are constructed and how the array elements construct these primitives.
        /// If more than one array is enabled, each is used.</para>
        ///
        /// <para>To enable and disable a generic vertex attribute array, call <see cref="VertexAttribute.Enable"/> and <see cref="VertexAttribute.Disable"/>.</para>
        ///
        /// <para><b>Note</b>: if the current program object, as set by <see cref="ShaderProgram.Use"/>, is invalid, rendering results are undefined.
        /// However, no error is generated for this case.</para>
        ///
        /// <para> <b>OpenGL API</b>: glDrawElements</para>
        /// </remarks>
        /// <param name="type">specifies what kind of primitives to render.</param>
        /// <param name="count">the number of indices used to render.</param>
        /// <param name="indexType">the type of the indices in the bound index buffer. Only 8-bit (UnsignedByte) and 16-bit (UnsignedShort) indices are supported.</param>
        /// <exception cref="GLException">InvalidFramebufferOperation if the currently bound framebuffer is not framebuffer complete.</exception>
        /// <seealso cref="Framebuffer.Status"/>
        /// <seealso cref="VertexAttribute"/>
        /// <seealso cref="VertexAttribute.Enable"/>
        /// <seealso cref="VertexAttribute.Disable"/>
        /// <seealso cref="DrawArrays(PrimitiveType, int, int)"/>
        /// <seealso cref="ShaderProgram.Use"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void DrawElements(PrimitiveType type, int count, DrawElementsType indexType);

        /// <summary>
        /// [requires: v2.0 or ES_VERSION_2_0] Render primitives from array data
        /// </summary>
        /// <param name="type">Specifies what kind of primitives to render. Symbolic constants Points, LineStrip,
        /// LineLoop, Lines, TriangleStrip, TriangleFan, and Triangles are accepted.</param>
        /// <param name="count">Specifies the number of elements to be rendered.</param>
        /// <param name="elementsType">Specifies the type of the values in indices. Must be UnsignedByte or UnsignedShort.</param>
        /// <param name="indices">[length: count,type] Specifies a pointer to the location where the indices are stored.</param>
        void DrawElements(PrimitiveType type, int count, DrawElementsType elementsType, IntPtr indices);
        #endregion

        #region Rasterization

        /// <summary>
        /// Specify whether front- or back-facing polygons can be culled.
        /// </summary>
        /// <remarks>
        /// This method specifies whether front- or back-facing polygons are culled (as specified by <paramref name="mode"/>) when polygon culling is enabled.
        /// Polygon culling is initially disabled.
        /// To enable and disable polygon culling, call the <see cref="GLES2.Enable"/> and <see cref="GLES2.Disable"/> methods with the argument <see cref="EnableCap.CullFace"/>.
        ///
        /// <para><see cref="GLES2.FrontFace"/> specifies which of the clockwise and counterclockwise polygons are front-facing and back-facing.</para>
        ///
        /// <para><b>Note</b>: if mode is <see cref="Face.FrontAndBack"/>, no polygons are drawn, 
        /// but other primitives such as points and lines are drawn.</para>
        ///
        /// <para><b>OpenGL API</b>: glCullFace</para>
        /// </remarks>
        /// <param name="mode">whether front- or back-facing polygons are candidates for culling.</param>
        /// <seealso cref="GetCullFace"/>
        /// <seealso cref="Enable"/>
        /// <seealso cref="Disable"/>
        /// <seealso cref="IsEnabled"/>
        /// <seealso cref="FrontFace"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void CullFace(CullFaceMode mode);

        /// <summary>
        /// Gets the current cull face mode.
        /// </summary>
        /// <remarks>
        /// <b>OpenGL API</b>: glGetIntegerv(GL_CULL_FACE_MODE)
        /// </remarks>
        /// <returns>The current cull face mode.</returns>
        /// <seealso cref="CullFace"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        CullFaceMode GetCullFace();

        /// <summary>
        /// Define front- and back-facing polygons.
        /// </summary>
        /// <remarks>
        /// In a scene composed entirely of opaque closed surfaces, back-facing polygons are never visible.
        /// Eliminating these invisible polygons has the obvious benefit of speeding up the rendering of the image.
        /// To enable and disable elimination of back-facing polygons, call <see cref="GLES2.Enable"/> and <see cref="GLES2.Disable"/> with argument <see cref="EnableCap.CullFace"/>.
        ///
        /// <para>The projection of a polygon to window coordinates is said to have clockwise winding if an imaginary object following the path from its first vertex, 
        /// its second vertex, and so on, to its last vertex, and finally back to its first vertex, 
        /// moves in a clockwise direction about the interior of the polygon.
        /// The polygon's winding is said to be counterclockwise if the imaginary object following 
        /// the same path moves in a counterclockwise direction about the interior of the polygon.
        /// glFrontFace specifies whether polygons with clockwise winding in window coordinates, 
        /// or counterclockwise winding in window coordinates, are taken to be front-facing.
        /// Passing <see cref="FrontFaceDirection.CounterClockwise"/> selects counterclockwise polygons as front-facing; 
        /// <see cref="FrontFaceDirection.Clockwise"/> selects clockwise polygons as front-facing.
        /// By default, counterclockwise polygons are taken to be front-facing.</para>
        ///
        /// <para><b>OpenGL API</b>: glFrontFace</para>
        /// </remarks>
        /// <param name="orientation">the orientation of front-facing polygons. Initial value is <see cref="FrontFaceDirection.CounterClockwise"/>.</param>
        /// <seealso cref="GetFrontFace"/>
        /// <seealso cref="Enable"/>
        /// <seealso cref="Disable"/>
        /// <seealso cref="IsEnabled"/>
        /// <seealso cref="CullFace"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void FrontFace(FrontFaceDirection orientation);

        /// <summary>
        /// Gets the current front face orientation.
        /// </summary>
        /// <remarks>
        /// <b>OpenGL API</b>: glGetIntegerv(GL_FRONT_FACE)
        /// </remarks>
        /// <returns>The current front face orientation.</returns>
        /// <seealso cref="FrontFace"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        FrontFaceDirection GetFrontFace();

        /// <summary>
        /// Specify the width of rasterized lines.
        /// </summary>
        /// <remarks>
        /// The actual width is determined by rounding the supplied width to the nearest integer.
        /// (If the rounding results in the value 0, it is as if the line width were 1.)
        ///
        /// <para>There is a range of supported line widths.
        /// Only width 1 is guaranteed to be supported; others depend on the implementation.
        /// To query the range of supported widths, call <see cref="GetAliasedLineWidthRange"/>.</para>
        ///
        /// <para><b>Note</b>: the line width specified by <c>LineWidth</c> is always returned when <see cref="GetLineWidth"/> is queried.
        /// Clamping and rounding have no effect on the specified value.</para>
        ///
        /// <para><b>OpenGL API</b>: glLineWidth</para>
        /// </remarks>
        /// <param name="width">the width of rasterized lines. The initial value is 1.</param>
        /// <exception cref="GLException">InvalidValue if <paramref name="width"/> is less than or equal to 0.</exception>
        /// <seealso cref="GetLineWidth"/>
        /// <seealso cref="GetAliasedLineWidthRange"/>
        /// <seealso cref="Enable"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void LineWidth(float width);

        /// <summary>
        /// Get the current line width.
        /// </summary>
        /// <remarks>
        /// <b>OpenGL API</b>: glGetFloatv(GL_LINE_WIDTH)
        /// </remarks>
        /// <returns>The current line width.</returns>
        /// <seealso cref="LineWidth"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        float GetLineWidth();

        /// <summary>
        /// Set the scale and units used to calculate polygon depth values.
        /// </summary>
        /// <remarks>
        /// When <see cref="EnableCap.PolygonOffsetFill"/> is enabled, each fragment's depth value will be offset after it is 
        /// interpolated from the depth values of the appropriate vertices.
        /// The value of the offset is (<paramref name="factor"/> × DZ) + (r × <paramref name="units"/>), 
        /// where DZ is a measurement of the change in depth relative to the screen area of the polygon, 
        /// and r is the smallest value that is guaranteed to produce a resolvable offset for a given implementation.
        /// The offset is added before the depth test is performed and before the value is written into the depth buffer.
        ///
        /// <para><c>PolygonOffset</c> is useful for rendering hidden-line images, for applying decals to surfaces, 
        /// and for rendering solids with highlighted edges.</para>
        ///
        /// <para><b>OpenGL API</b>: glPolygonOffset</para>
        /// </remarks>
        /// <param name="factor">a scale factor that is used to create a variable depth offset for each polygon. The initial value is 0.</param>
        /// <param name="units">is multiplied by an implementation-specific value to create a constant depth offset. The initial value is 0.</param>
        /// <seealso cref="Enable"/>
        /// <seealso cref="Disable"/>
        /// <seealso cref="IsEnabled"/>
        /// <seealso cref="DepthFunc"/>
        /// <seealso cref="GetPolygonOffsetFactor"/>
        /// <seealso cref="GetPolygonOffsetUnits"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void PolygonOffset(float factor, float units);

        /// <summary>
        /// Gets the current polygon offset factor.
        /// </summary>
        /// <remarks>
        /// <b>OpenGL API</b>: glGetFloatv(GL_POLYGON_OFFSET_FACTOR)
        /// </remarks>
        /// <returns>The polygon offset factor.</returns>
        /// <seealso cref="PolygonOffset"/>
        /// <seealso cref="GetPolygonOffsetUnits"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        float GetPolygonOffsetFactor();

        /// <summary>
        /// Gets the current polygon offset units.
        /// </summary>
        /// <remarks>
        /// <b>OpenGL API</b>: glGetFloatv(GL_POLYGON_OFFSET_UNITS)
        /// </remarks>
        /// <returns>The polygon offset units.</returns>
        /// <seealso cref="PolygonOffset"/>
        /// <seealso cref="GetPolygonOffsetFactor"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        float GetPolygonOffsetUnits();

        /// <summary>
        /// Set pixel storage alignment.
        /// </summary>
        /// <remarks>
        /// This method sets pixel storage modes that affect the operation of subsequent <see cref="Framebuffer.ReadPixels"/> 
        /// as well as the unpacking of texture patterns (see <see cref="Texture.Upload{T}(PixelFormat, int, int, T[], int, PixelDataType, int)"/> 
        /// and <see cref="Texture.SubUpload{T}(PixelFormat, int, int, int, int, T[], int, PixelDataType, int)"/>).
        ///
        /// <para>The PackAlignment mode affects how pixel data is downloaded from the GPU into client memory.
        /// The UnpackAlignment mode affects how pixel data is uploaded from client memory to the GPU.</para>
        ///
        /// <para><b>OpenGL API</b>: glPixelStorei</para>
        /// </remarks>
        /// <param name="mode">the mode to set.</param>
        /// <param name="value">the alignment value to set for the mode.</param>
        /// <seealso cref="Framebuffer.ReadPixels"/>
        /// <seealso cref="Texture.Upload{T}(PixelFormat, int, int, T[], int, PixelDataType, int)"/>
        /// <seealso cref="Texture.SubUpload{T}(PixelFormat, int, int, int, int, T[], int, PixelDataType, int)"/>
        /// <seealso cref="GetPixelStore"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void PixelStore(PixelStoreParameter mode, PixelStoreValue value);

        /// <summary>
        /// Returns to current pixel storage alignment.
        /// </summary>
        /// <remarks>
        /// <b>OpenGL API</b>: glGetIntegerv(GL_PACK_ALIGNMENT/GL_UNPACK_ALIGNMENT)
        /// </remarks>
        /// <param name="mode">the mode for which to return the storage value.</param>
        /// <returns>The storage alignment for the given node.</returns>
        /// <seealso cref="PixelStore"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        PixelStoreValue GetPixelStore(PixelStoreParameter mode);

        /// <summary>
        /// Gets the smallest and largest supported widths for aliased lines.
        /// </summary>
        /// <remarks>
        /// The returned range always includes value 1.0.
        ///
        /// <para><b>OpenGL API</b>: glGetIntegerv(GL_ALIASED_LINE_WIDTH_RANGE)</para>
        /// </remarks>
        /// <param name="min">is set to the smallest supported line width.</param>
        /// <param name="max">is set to the largest supported line width.</param>
        /// <seealso cref="LineWidth"/>
        /// <seealso cref="GetLineWidth"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void GetAliasedLineWidthRange(out float min, out float max);

        /// <summary>
        /// Gets the smallest and largest supported sizes for aliased points.
        /// </summary>
        /// <remarks>
        /// The returned range always includes value 1.0.
        ///
        /// <para><b>OpenGL API</b>: glGetIntegerv(GL_ALIASED_POINT_SIZE_RANGE)</para>
        /// </remarks>
        /// <param name="min">is set to the smallest supported point size.</param>
        /// <param name="max">is set to the largest supported point size.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void GetAliasedPointSizeRange(out float min, out float max);

        #endregion

        #region State

        /// <summary>
        /// Enable a server-side GL capability.
        /// </summary>
        /// <remarks>
        /// Use <see cref="IsEnabled"/> to determine the current setting of any capability.
        /// The initial value for each capability with the exception of <see cref="EnableCap.Dither"/> is False.
        /// The initial value for <see cref="EnableCap.Dither"/> is True.
        ///
        /// <para><b>OpenGL API</b>: glEnable</para>
        /// </remarks>
        /// <param name="capability">the GL capability to enable.</param>
        /// <seealso cref="IsEnabled"/>
        /// <seealso cref="Texture.BindToTextureUnit"/>
        /// <seealso cref="BlendFunc"/>
        /// <seealso cref="CullFace"/>
        /// <seealso cref="DepthFunc"/>
        /// <seealso cref="DepthRange"/>
        /// <seealso cref="LineWidth"/>
        /// <seealso cref="PolygonOffset"/>
        /// <seealso cref="Scissor"/>
        /// <seealso cref="StencilFunc"/>
        /// <seealso cref="StencilOperation"/>
        /// <seealso cref="Texture"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void Enable(EnableCap capability);

        /// <summary>
        /// Disable a server-side GL capability.
        /// </summary>
        /// <remarks>
        /// Use <see cref="IsEnabled"/> to determine the current setting of any capability.
        /// The initial value for each capability with the exception of <see cref="EnableCap.Dither"/> is False.
        /// The initial value for <see cref="EnableCap.Dither"/> is True.
        ///
        /// <para><b>OpenGL API</b>: glDisable</para>
        /// </remarks>
        /// <param name="capability">the GL capability to disable.</param>
        /// <seealso cref="IsEnabled"/>
        /// <seealso cref="Texture.BindToTextureUnit"/>
        /// <seealso cref="BlendFunc"/>
        /// <seealso cref="CullFace"/>
        /// <seealso cref="DepthFunc"/>
        /// <seealso cref="DepthRange"/>
        /// <seealso cref="LineWidth"/>
        /// <seealso cref="PolygonOffset"/>
        /// <seealso cref="Scissor"/>
        /// <seealso cref="StencilFunc"/>
        /// <seealso cref="StencilOperation"/>
        /// <seealso cref="Texture"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void Disable(EnableCap capability);

        /// <summary>
        /// Checks if a server-side GL capability is enabled.
        /// </summary>
        /// <remarks>
        /// <b>OpenGL API</b>: glIsEnabled
        /// </remarks>
        /// <param name="capability">the GL capability to check.</param>
        /// <returns>True if <paramref name="capability"/> is currently enabled. False otherwise.</returns>
        /// <seealso cref="Enable"/>
        /// <seealso cref="Disable"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        bool IsEnabled(EnableCap capability);

        #endregion

        #region Special Functions

        /// <summary>
        /// Block until all GL execution is complete.
        /// </summary>
        /// <remarks>
        /// This method does not return until the effects of all previously called GL commands are complete.
        /// Such effects include all changes to GLstate, all changes to connection state, and all changes to the frame buffer contents.
        ///
        /// <para><b>Note</b>: <c>Finish</c> requires a round trip to the server.</para>
        ///
        /// <para><b>OpenGL API</b>: glFinish</para>
        /// </remarks>
        /// <seealso cref="Flush"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void Finish();

        /// <summary>
        /// Force execution of GL commands in finite time.
        /// </summary>
        /// <remarks>
        /// Different GL implementations buffer commands in several different locations, 
        /// including network buffers and the graphics accelerator itself.
        /// This method empties all of these buffers, causing all issued commands to be executed as quickly 
        /// as they are accepted by the actual rendering engine.
        /// Though this execution may not be completed in any particular time period, it does complete in finite time.
        ///
        /// <para>Because any GL program might be executed over a network, or on an accelerator that buffers commands, 
        /// all programs should call <c>Flush</c> whenever they count on having all of their previously issued commands completed.
        /// For example, call <c>Flush</c> before waiting for user input that depends on the generated image.</para>
        ///
        /// <para><b>Note</b>: <c>Flush</c> can return at any time.
        /// It does not wait until the execution of all previously issued GL commands is complete.</para>
        ///
        /// <para><b>OpenGL API</b>: glFlush</para>
        /// </remarks>
        /// <seealso cref="Finish"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void Flush();

        #endregion

        #region Information Functions

        /// <summary>
        /// Get an estimate of the number of bits of subpixel resolution that are used to position rasterized geometry in window coordinates.
        /// </summary>
        /// <remarks>
        /// The value must be at least 4.
        ///
        /// <para><b>OpenGL API</b>: glGetIntegerv(GL_SUBPIXEL_BITS)</para>
        /// </remarks>
        /// <returns>The number of subpixel bits</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        int GetSubpixelBits();

        /// <summary>
        /// Get the company responsible for this GL implementation.
        /// </summary>
        /// <remarks>
        /// This name does not change from release to release.
        ///
        /// <para>Because the GL does not include queries for the performance characteristics of an implementation, 
        /// some applications are written to recognize known platforms and modify their GL usage based on known 
        /// performance characteristics of these platforms.
        /// <c>GetVendor</c> and <see cref="GetRenderer"/> together uniquely specify a platform.
        /// They do not change from release to release and should be used by platform-recognition algorithms.</para>
        ///
        /// <para><b>OpenGL API</b>: glGetString(GL_VENDOR)</para>
        /// </remarks>
        /// <returns>The vendor</returns>
        /// <seealso cref="GetRenderer"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        string GetVendor();

        /// <summary>
        /// Get the name of the renderer.
        /// </summary>
        /// <remarks>
        /// This name is typically specific to a particular configuration of a hardware platform.
        /// It does not change from release to release.
        ///
        /// <para>Because the GL does not include queries for the performance characteristics of an implementation, 
        /// some applications are written to recognize known platforms and modify their GL usage based on known 
        /// performance characteristics of these platforms.
        /// <see cref="GetVendor"/> and <c>GetRenderer</c> together uniquely specify a platform.
        /// They do not change from release to release and should be used by platform-recognition algorithms.</para>
        ///
        /// <para><b>OpenGL API</b>: glGetString(GL_RENDERER)</para>
        /// </remarks>
        /// <returns>The renderer</returns>
        /// <seealso cref="GetVendor"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        string GetRenderer();

        /// <summary>
        /// Get a version or release number of the form OpenGL[space]ES[space][version number][space][vendor-specific information].
        /// </summary>
        /// <remarks>
        /// <b>OpenGL API</b>: glGetString(GL_VERSION)
        /// </remarks>
        /// <returns>The GL version</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        string GetVersion();

        /// <summary>
        /// Get a version or release number for the shading language of the form OpenGL[space]ES[space]GLSL[space]ES[space][version number][space][vendor-specific information].
        /// </summary>
        /// <remarks>
        /// <b>OpenGL API</b>: glGetString(GL_SHADING_LANGUAGE_VERSION)
        /// </remarks>
        /// <returns>The GLSL version</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        string GetShadingLanguageVersion();

        /// <summary>
        /// Get a space-separated list of supported extensions to GL.
        /// </summary>
        /// <remarks>
        /// Some applications want to make use of features that are not part of the standard GL.
        /// These features may be implemented as extensions to the standard GL.
        /// This method returns a space-separated list of supported GL extensions.
        /// (Extension names never contain a space character.)
        ///
        /// <para><b>OpenGL API</b>: glGetString(GL_EXTENSIONS)</para>
        /// </remarks>
        /// <returns>The supported extensions</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        string GetExtensions();

        #endregion

        #region DataBuffer

        void GetBufferParameter(BufferTarget type, BufferParameterName name, out int value);

        void GenBuffers(int n, int[] handle);

        /// <summary>
        /// [requires: v2.0 or ES_VERSION_2_0] Bind a named buffer object
        /// </summary>
        /// <param name="type">Specifies the target to which the buffer object is bound. The symbolic constant
        ///     must be ArrayBuffer, CopyReadBuffer, CopyWriteBuffer, ElementArrayBuffer, PixelPackBuffer,
        ///     PixelUnpackBuffer, TransformFeedbackBuffer, or UniformBuffer.</param>
        /// <param name="handle">Specifies the name of a buffer object.</param>
        void BindBuffer(BufferTarget type, int handle);

        /// <summary>
        /// [requires: v2.0 or ES_VERSION_2_0] Creates and initializes a buffer object's
        ///     data store
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type">Specifies the target buffer object. The symbolic constant must be ArrayBuffer,
        ///     CopyReadBuffer, CopyWriteBuffer, ElementArrayBuffer, PixelPackBuffer, PixelUnpackBuffer,
        ///     TransformFeedbackBuffer, or UniformBuffer.</param>
        /// <param name="ptr">Specifies the size in bytes of the buffer object's new data store.</param>
        /// <param name="data">[length: size] Specifies a pointer to data that will be copied into the data
        ///     store for initialization, or Null if no data is to be copied.</param>
        /// <param name="usage">Specifies the expected usage pattern of the data store. The symbolic constant
        ///     must be StreamDraw, StreamRead, StreamCopy, StaticDraw, StaticRead, StaticCopy,
        ///     DynamicDraw, DynamicRead, or DynamicCopy.</param>
        void BufferData<T>(BufferTarget type, IntPtr ptr, T[] data, BufferUsageHint usage)
             where T : struct;

        /// <summary>
        /// [requires: v2.0 or ES_VERSION_2_0] Updates a subset of a buffer object's data
        ///     store
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type">Specifies the target buffer object. The symbolic constant must be ArrayBuffer,
        ///     CopyReadBuffer, CopyWriteBuffer, ElementArrayBuffer, PixelPackBuffer, PixelUnpackBuffer,
        ///     TransformFeedbackBuffer, or UniformBuffer.</param>
        /// <param name="offset">Specifies the offset into the buffer object's data store where data replacement
        ///     will begin, measured in bytes.</param>
        /// <param name="ptr">Specifies the size in bytes of the data store region being replaced.</param>
        /// <param name="data">[length: size] Specifies a pointer to the new data that will be copied into the
        ///     data store.</param>
        void BufferSubData<T>(BufferTarget type, IntPtr offset, IntPtr ptr,  T[] data)
             where T : struct;

        void DeleteBuffers(int n, int[] handles);

        #endregion

        #region Renderbuffer

        void GetRenderbufferParameter(RenderbufferTarget target, RenderbufferParameterName name, out int value);

        void GenRenderbuffers(int n, int[] handles);

        /// <summary>
        /// [requires: v2.0 or ES_VERSION_2_0] Bind a renderbuffer to a renderbuffer target
        /// </summary>
        /// <param name="target">Specifies the renderbuffer target of the binding operation. target must be Renderbuffer.</param>
        /// <param name="Handle">Specifies the name of the renderbuffer object to bind.</param>
        void BindRenderbuffer(RenderbufferTarget target, int Handle);

        void RenderbufferStorage(RenderbufferTarget target, RenderbufferInternalFormat format, int width, int height);

        void DeleteRenderbuffers(int n, int[] handles);

        #endregion

        #region Texture

        void TexParameter(TextureTargetType targetType, TextureParameterName name, int value);

        void GetTexParameter(TextureTargetType target, GetTextureParameterName name, out int value);

        void Hint(HintTarget target, HintMode value);

        int GenTexture();

        /// <summary>
        /// [requires: v2.0 or ES_VERSION_2_0] Bind a named texture to a texturing target
        /// </summary>
        /// <param name="type">Specifies the target to which the texture is bound. Must be either Texture2D,
        ///     Texture3D, Texture2DArray, or TextureCubeMap,</param>
        /// <param name="Handle">Specifies the name of a texture.</param>
        void BindTexture(TextureTargetType type, int Handle);

        /// <summary>
        /// [requires: v2.0 or ES_VERSION_2_0] Select active texture unit
        /// </summary>
        /// <param name="textureUnit">Specifies which texture unit to make active. The number of texture units is implementation-dependent,
        ///     but must be at least 32. texture must be one of Texturei, where i ranges from
        ///     zero to the value of MaxCombinedTextureImageUnits minus one. The initial value
        ///     is Texture0.</param>
        void ActiveTexture(TextureUnit textureUnit);

        void GenerateMipmap(TextureTargetType type);

        void TexImage2D(TextureTargetType target, int level,
                PixelInternalFormat format, int width, int height, int border,
                PixelFormat pFormat, PixelType type, IntPtr ptr);

        void TexImage2D<T>(TextureTargetType target, int level,
                PixelInternalFormat format, int width, int height, int border,
                PixelFormat pFormat, PixelType type, T[] data)
            where T : struct;

        void TexImage2D<T>(TextureTargetType target, int level,
                PixelInternalFormat format, int width, int height, int border,
                PixelFormat pFormat, PixelType type, T[,] data)
            where T : struct;

        void TexSubImage2D<T>(TextureTargetType target, int level, int xOffset, int yOffset, int width, int height,
            PixelFormat format, PixelType type, T[] data)
            where T : struct;

        void TexSubImage2D<T>(TextureTargetType target, int level, int xOffset, int yOffset, int width, int height,
            PixelFormat format, PixelType type, T[,] data)
            where T : struct;

        void CompressedTexImage2D<T>(TextureTargetType targetType, int level, PixelInternalFormat format, int width, int height, int border,
            int length, T[] data)
            where T : struct;

        void CompressedTexImage2D(TextureTargetType targetType, int level, PixelInternalFormat format, int width, int height, int border,
            int length, IntPtr data);

        void CompressedTexSubImage2D<T>(TextureTargetType targetType, int level, int xOffset, int yOffset, int width, int height,
                PixelFormat format, int length, T[] data)
            where T : struct;

        void CopyTexImage2D(TextureTargetType targetType, int level, PixelInternalFormat format,
            int left, int bottom, int width, int height, int border);

        void CopyTexSubImage2D(TextureTargetType targetType, int level, int xOffset, int yOffset, int left, int bottom, int width, int height);

        void DeleteTexture(int handle);

        #endregion

        #region Framebuffer

        /// <summary>
        /// [requires: v2.0 or ES_VERSION_2_0] Check the completeness status of a framebuffer
        /// </summary>
        /// <param name="target">Specify the target of the framebuffer completeness check.</param>
        /// <returns></returns>
        FramebufferStatus CheckFramebufferStatus(FramebufferTarget target);

        void GenFramebuffers(int n, int[] handles);

        /// <summary>
        /// [requires: v2.0 or ES_VERSION_2_0] Bind a framebuffer to a framebuffer target
        /// </summary>
        /// <param name="target">Specifies the framebuffer target of the binding operation.</param>
        /// <param name="Handle">Specifies the name of the framebuffer object to bind.</param>
        void BindFramebuffer(FramebufferTarget target, int Handle);

        void FramebufferRenderbuffer(FramebufferTarget target, FramebufferAttachment attachment, 
            RenderbufferTarget renderbuffer, int handle);

        void FramebufferTexture2D(FramebufferTarget target, FramebufferAttachment attachment,
                TextureTarget2d textureTarget, int handle, int level);

        void GetFramebufferAttachmentParameter(FramebufferTarget target, FramebufferAttachment attachment,
                FramebufferParameterName parameterName, out int value);

        void ReadPixels<T>(int left, int bottom, int width, int height, PixelFormat format, PixelType dataType, T[] data)
            where T : struct;

        void DeleteFramebuffers(int n, int[] _handle);

        #endregion

        #region Shader

        void GetShader(int handle, ShaderParameter parameter, out int length);

        void GetShaderSource(int handle, int bufSize, out int length, StringBuilder sb);

        void ShaderSource(int handle, string value);

        int CreateShader(ShaderType type);

        void CompileShader(int handle);

        string GetShaderInfoLog(int handle);

        void ReleaseShaderCompiler();

        void DeleteShader(int handle);

        #endregion

        #region Program

        void GetProgram(int program, GetProgramParameterName pname, out int @params);

        int CreateProgram();

        void DetachShader(int program, int shader);

        void GetAttachedShaders(int program, int maxCount, out int count, [Out] int[] shaders);

        void LinkProgram(int program);

        string GetProgramInfoLog(int program);

        void ValidateProgram(int program);

        void UseProgram(int program);

        /// <summary>
        /// [requires: v2.0 or ES_VERSION_2_0] Attaches a shader object to a program object
        /// </summary>
        /// <param name="program">Specifies the program object to which a shader object will be attached.</param>
        /// <param name="shader">Specifies the shader object that is to be attached.</param>
        void AttachShader(int program, int shader);

        void GetActiveAttrib(int program, int index, int bufSize, out int length, out int size,
            out ActiveAttribType type, [Out] StringBuilder name);

        void GetActiveUniform(int program, int index, int bufSize, out int length, out int size,
            out ActiveUniformType type, [Out] StringBuilder name);

        void DeleteProgram(int program);

        #endregion

        #region Uniform

        int GetUniformLocation(int program, string name);

        void Uniform1(int location, float v0);

        void Uniform2(int location, float v0, float v1);

        void Uniform3(int location, float v0, float v1, float v2);

        void Uniform4(int location, float v0, float v1, float v2, float v3);

        void UniformMatrix2(int location, bool transpose, ref Matrix2 matrix);

        void UniformMatrix3(int location, bool transpose, ref Matrix3 matrix);

        void UniformMatrix4(int location, bool transpose, ref Matrix4 matrix);

        void UniformMatrix2(int location, int count, bool transpose, ref float value);

        void UniformMatrix3(int location, int count, bool transpose, ref float value);

        void UniformMatrix4(int location, int count, bool transpose, ref float value);

        void Uniform1(int location, int v0);

        void Uniform2(int location, int v0, int v1);

        void Uniform3(int location, int v0, int v1, int v2);

        void Uniform4(int location, int v0, int v1, int v2, int v3);

        void Uniform1(int location, int count, float[] vector);

        void Uniform1(int location, int count, int[] vector);

        void GetUniform(int program, int location, out float @params);

        void GetUniform(int program, int location, [Out] float[] @params);

        void GetUniform(int program, int location, out int @params);

        void GetUniform(int program, int location, [Out] int[] @params);

        void Uniform2(int location, int count, ref float value);

        void Uniform3(int location, int count, ref float value);

        void Uniform4(int location, int count, ref float value);




        #endregion

        #region VertexAttribute

        void GetVertexAttrib(Int32 index, VertexAttribParameter pname,
            [OutAttribute] out Int32 @params);

        void GetVertexAttrib(Int32 index, VertexAttribParameter pname,
            [OutAttribute] Single[] @params);

        void VertexAttribPointer(Int32 indx, Int32 size, VertexAttribPointerType type,
            bool normalized, Int32 stride, IntPtr ptr);

        void VertexAttribPointer<T5>(Int32 indx, Int32 size, VertexAttribPointerType type,
            bool normalized, Int32 stride, [InAttribute, OutAttribute] T5[] ptr)
             where T5 : struct;

        int GetAttribLocation(int program, string name);

        /// <summary>
        ///  [requires: v2.0 or ES_VERSION_2_0] Associates a generic vertex attribute index
        ///   with a named attribute variable
        /// </summary>
        /// <param name="program">Specifies the handle of the program object in which the association is to be
        //     made.</param>
        /// <param name="index">Specifies the index of the generic vertex attribute to be bound.</param>
        /// <param name="name">Specifies a null terminated string containing the name of the vertex shader attribute
        ///    variable to which index is to be bound.</param>
        void BindAttribLocation(Int32 program, Int32 index, String name);

        void EnableVertexAttribArray(Int32 index);

        void DisableVertexAttribArray(Int32 index);

        void VertexAttrib1(Int32 indx, Single x);

        void VertexAttrib2(Int32 indx, Single x, Single y);

        void VertexAttrib3(Int32 indx, Single x, Single y, Single z);

        void VertexAttrib4(Int32 indx, Single x, Single y, Single z, Single w);


        #endregion

        void GetInteger(GetPName pName, out int value);

        void GetInteger(GetPName pName, int[] values);
        string GetString(StringName vendor);
    }

}
