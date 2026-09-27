using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Rydia.Graphics.Api.ES20;
using Rydia.Runtime;
using TKES = OpenTK.Graphics.ES30;

namespace Rydia.Graphics.Api.ES30
{

    public class GLES30Bindings : GLES20Bindings, IGLES30
    {

        public object SyncRoot
        {
            get;
        } = new object();

        public void BeginQuery(QueryTarget target, int id)
        {
            TKES.GL.BeginQuery((TKES.QueryTarget)target, id);
        }

        public void BeginTransformFeedback(TransformFeedbackPrimitiveType primitiveMode)
        {
            TKES.GL.BeginTransformFeedback((TKES.TransformFeedbackPrimitiveType)primitiveMode);
        }

        public void BindBufferBase(BufferRangeTarget target, int index, int buffer)
        {
            TKES.GL.BindBufferBase((TKES.BufferRangeTarget)target, index, buffer);
        }

        public void BindBufferRange(BufferRangeTarget target, int index, int buffer, nint offset, int size)
        {
            TKES.GL.BindBufferRange((TKES.BufferRangeTarget)target, index, buffer, offset, size);
        }

        public void BindBufferRange(BufferRangeTarget target, int index, int buffer, nint offset, nint size)
        {
            TKES.GL.BindBufferRange((TKES.BufferRangeTarget)target, index, buffer, offset, size);
        }

        public void BindSampler(int unit, int sampler)
        {
            TKES.GL.BindSampler(unit, sampler);
        }

        public void BindTransformFeedback(TransformFeedbackTarget target, int id)
        {
            TKES.GL.BindTransformFeedback((TKES.TransformFeedbackTarget)target, id);
        }

        public void BindVertexArray(int array)
        {
            TKES.GL.BindVertexArray(array);
        }

        public void BlitFramebuffer(int srcX0, int srcY0, int srcX1, int srcY1, int dstX0, int dstY0, int dstX1, int dstY1, ClearBufferMask mask, BlitFramebufferFilter filter)
        {
            TKES.GL.BlitFramebuffer(srcX0, srcY0, srcX1, srcY1, dstX0, dstY0, dstX1, dstY1, (TKES.ClearBufferMask)mask, (TKES.BlitFramebufferFilter)filter);
        }

        public void ClearBuffer(ClearBufferCombined buffer, int drawbuffer, float depth, int stencil)
        {
            TKES.GL.ClearBuffer((TKES.ClearBufferCombined)buffer, drawbuffer, depth, stencil);
        }

        public void ClearBuffer(ClearBuffer buffer, int drawbuffer, float[] value)
        {
            TKES.GL.ClearBuffer((TKES.ClearBuffer)buffer, drawbuffer, value);
        }

        public void ClearBuffer(ClearBuffer buffer, int drawbuffer, ref float value)
        {
            TKES.GL.ClearBuffer((TKES.ClearBuffer)buffer, drawbuffer, ref value);
        }

        public unsafe void ClearBuffer(ClearBuffer buffer, int drawbuffer, float* value)
        {
            TKES.GL.ClearBuffer((TKES.ClearBuffer)buffer, drawbuffer, value);
        }

        public void ClearBuffer(ClearBuffer buffer, int drawbuffer, int[] value)
        {
            TKES.GL.ClearBuffer((TKES.ClearBuffer)buffer, drawbuffer, value);
        }

        public void ClearBuffer(ClearBuffer buffer, int drawbuffer, ref int value)
        {
            TKES.GL.ClearBuffer((TKES.ClearBuffer)buffer, drawbuffer, ref value);
        }

        public unsafe void ClearBuffer(ClearBuffer buffer, int drawbuffer, int* value)
        {
            TKES.GL.ClearBuffer((TKES.ClearBuffer)buffer, drawbuffer, value);
        }

        public WaitSyncStatus ClientWaitSync(nint sync, ClientWaitSyncFlags flags, long timeout)
        {
#if ANDROID
            return (WaitSyncStatus)TKES.GL.ClientWaitSync(sync, (int)flags, (long)timeout);
#else
            return (WaitSyncStatus)TKES.GL.ClientWaitSync(sync, (TKES.ClientWaitSyncFlags)flags, timeout);
#endif
        }

        public void CompressedTexImage3D(TextureTarget3d target, int level, CompressedInternalFormat internalformat, int width, int height, int depth, int border, int imageSize, nint data)
        {
#if ANDROID
            TKES.GL.CompressedTexImage3D((TKES.TextureTarget3D)target, level, (TKES.CompressedInternalFormat)internalformat, width, height, depth, border, imageSize, data);
#else
            TKES.GL.CompressedTexImage3D((TKES.TextureTarget3d)target, level, (TKES.CompressedInternalFormat)internalformat, width, height, depth, border, imageSize, data);
#endif
        }

        public void CompressedTexImage3D<T8>(TextureTarget3d target, int level, CompressedInternalFormat internalformat, int width, int height, int depth, int border, int imageSize, [In, Out] T8[] data) where T8 : struct
        {
#if ANDROID
            TKES.GL.CompressedTexImage3D((TKES.TextureTarget3D)target, level, (TKES.CompressedInternalFormat)internalformat, width, height, depth, border, imageSize, data);
#else
            TKES.GL.CompressedTexImage3D((TKES.TextureTarget3d)target, level, (TKES.CompressedInternalFormat)internalformat, width, height, depth, border, imageSize, data);
#endif
        }

        public void CompressedTexImage3D<T8>(TextureTarget3d target, int level, CompressedInternalFormat internalformat, int width, int height, int depth, int border, int imageSize, [In, Out] T8[,] data) where T8 : struct
        {
#if ANDROID
            TKES.GL.CompressedTexImage3D((TKES.TextureTarget3D)target, level, (TKES.CompressedInternalFormat)internalformat, width, height, depth, border, imageSize, data);
#else
            TKES.GL.CompressedTexImage3D((TKES.TextureTarget3d)target, level, (TKES.CompressedInternalFormat)internalformat, width, height, depth, border, imageSize, data);
#endif
        }

        public void CompressedTexImage3D<T8>(TextureTarget3d target, int level, CompressedInternalFormat internalformat, int width, int height, int depth, int border, int imageSize, [In, Out] T8[,,] data) where T8 : struct
        {
#if ANDROID
            TKES.GL.CompressedTexImage3D((TKES.TextureTarget3D)target, level, (TKES.CompressedInternalFormat)internalformat, width, height, depth, border, imageSize, data);
#else
            TKES.GL.CompressedTexImage3D((TKES.TextureTarget3d)target, level, (TKES.CompressedInternalFormat)internalformat, width, height, depth, border, imageSize, data);
#endif
        }

        public void CompressedTexImage3D<T8>(TextureTarget3d target, int level, CompressedInternalFormat internalformat, int width, int height, int depth, int border, int imageSize, [In, Out] ref T8 data) where T8 : struct
        {
#if ANDROID
            TKES.GL.CompressedTexImage3D((TKES.TextureTarget3D)target, level, (TKES.CompressedInternalFormat)internalformat, width, height, depth, border, imageSize, ref data);
#else
            TKES.GL.CompressedTexImage3D((TKES.TextureTarget3d)target, level, (TKES.CompressedInternalFormat)internalformat, width, height, depth, border, imageSize, ref data);
#endif
        }

        public void CompressedTexSubImage3D(TextureTarget3d target, int level, int xoffset, int yoffset, int zoffset, int width, int height, int depth, PixelFormat format, int imageSize, nint data)
        {
#if ANDROID
            TKES.GL.CompressedTexSubImage3D((TKES.TextureTarget3D)target, level, xoffset, yoffset, zoffset, width, height, depth, (TKES.CompressedInternalFormat)format, imageSize, data);
#else
            TKES.GL.CompressedTexSubImage3D((TKES.TextureTarget3d)target, level, xoffset, yoffset, zoffset, width, height, depth, (TKES.PixelFormat)format, imageSize, data);
#endif
        }

        public void CompressedTexSubImage3D<T10>(TextureTarget3d target, int level, int xoffset, int yoffset, int zoffset, int width, int height, int depth, PixelFormat format, int imageSize, [In, Out] T10[] data) where T10 : struct
        {
#if ANDROID
            TKES.GL.CompressedTexSubImage3D((TKES.TextureTarget3D)target, level, xoffset, yoffset, zoffset, width, height, depth, (TKES.CompressedInternalFormat)format, imageSize, data);
#else
            TKES.GL.CompressedTexSubImage3D((TKES.TextureTarget3d)target, level, xoffset, yoffset, zoffset, width, height, depth, (TKES.PixelFormat)format, imageSize, data);
#endif
        }

        public void CompressedTexSubImage3D<T10>(TextureTarget3d target, int level, int xoffset, int yoffset, int zoffset, int width, int height, int depth, PixelFormat format, int imageSize, [In, Out] T10[,] data) where T10 : struct
        {
#if ANDROID
            TKES.GL.CompressedTexSubImage3D((TKES.TextureTarget3D)target, level, xoffset, yoffset, zoffset, width, height, depth, (TKES.CompressedInternalFormat)format, imageSize, data);
#else
            TKES.GL.CompressedTexSubImage3D((TKES.TextureTarget3d)target, level, xoffset, yoffset, zoffset, width, height, depth, (TKES.PixelFormat)format, imageSize, data);
#endif
        }

        public void CompressedTexSubImage3D<T10>(TextureTarget3d target, int level, int xoffset, int yoffset, int zoffset, int width, int height, int depth, PixelFormat format, int imageSize, [In, Out] T10[,,] data) where T10 : struct
        {
#if ANDROID
            TKES.GL.CompressedTexSubImage3D((TKES.TextureTarget3D)target, level, xoffset, yoffset, zoffset, width, height, depth, (TKES.CompressedInternalFormat)format, imageSize, data);
#else
            TKES.GL.CompressedTexSubImage3D((TKES.TextureTarget3d)target, level, xoffset, yoffset, zoffset, width, height, depth, (TKES.PixelFormat)format, imageSize, data);
#endif
        }

        public void CompressedTexSubImage3D<T10>(TextureTarget3d target, int level, int xoffset, int yoffset, int zoffset, int width, int height, int depth, PixelFormat format, int imageSize, [In, Out] ref T10 data) where T10 : struct
        {
#if ANDROID
            TKES.GL.CompressedTexSubImage3D((TKES.TextureTarget3D)target, level, xoffset, yoffset, zoffset, width, height, depth, (TKES.CompressedInternalFormat)format, imageSize, ref data);
#else
            TKES.GL.CompressedTexSubImage3D((TKES.TextureTarget3d)target, level, xoffset, yoffset, zoffset, width, height, depth, (TKES.PixelFormat)format, imageSize, ref data);
#endif
        }

        public void CopyBufferSubData(BufferTarget readTarget, BufferTarget writeTarget, nint readOffset, nint writeOffset, int size)
        {
            TKES.GL.CopyBufferSubData((TKES.BufferTarget)readTarget, (TKES.BufferTarget)writeTarget, readOffset, writeOffset, size);
        }

        public void CopyBufferSubData(BufferTarget readTarget, BufferTarget writeTarget, nint readOffset, nint writeOffset, nint size)
        {
            TKES.GL.CopyBufferSubData((TKES.BufferTarget)readTarget, (TKES.BufferTarget)writeTarget, readOffset, writeOffset, size);
        }

        public void CopyTexSubImage3D(TextureTarget3d target, int level, int xoffset, int yoffset, int zoffset, int x, int y, int width, int height)
        {
#if ANDROID
            TKES.GL.CopyTexSubImage3D((TKES.TextureTarget3D)target, level, xoffset, yoffset, zoffset, x, y, width, height);
#else
            TKES.GL.CopyTexSubImage3D((TKES.TextureTarget3d)target, level, xoffset, yoffset, zoffset, x, y, width, height);
#endif
        }

        public void DeleteQueries(int n, int[] ids)
        {
            TKES.GL.DeleteQueries(n, ids);
        }

        public void DeleteQueries(int n, ref int ids)
        {
            TKES.GL.DeleteQueries(n, ref ids);
        }

        public unsafe void DeleteQueries(int n, int* ids)
        {
            TKES.GL.DeleteQueries(n, ids);
        }

        public void DeleteQuery(int ids)
        {
#if ANDROID
            TKES.GL.DeleteQueries(1, ref ids);
#else
            TKES.GL.DeleteQuery(ids);
#endif
        }

        public void DeleteSampler(int samplers)
        {
            TKES.GL.DeleteSamplers(1, ref samplers);
        }

        public void DeleteSamplers(int count, int[] samplers)
        {
            TKES.GL.DeleteSamplers(count, samplers);
        }

        public void DeleteSamplers(int count, ref int samplers)
        {
            TKES.GL.DeleteSamplers(count, ref samplers);
        }

        public unsafe void DeleteSamplers(int count, int* samplers)
        {
            TKES.GL.DeleteSamplers(count, samplers);
        }

        public void DeleteSync(nint sync)
        {
            TKES.GL.DeleteSync(sync);
        }

        public void DeleteTransformFeedback(int ids)
        {
#if ANDROID
            TKES.GL.DeleteTransformFeedback(1, ref ids);
#else
            TKES.GL.DeleteTransformFeedback(ids);
#endif
        }

        public void DeleteTransformFeedbacks(int n, int[] ids)
        {
#if ANDROID
            TKES.GL.DeleteTransformFeedback(n, ids);
#else
            TKES.GL.DeleteTransformFeedbacks(n, ids);
#endif
        }

        public void DeleteTransformFeedbacks(int n, ref int ids)
        {
#if ANDROID
            TKES.GL.DeleteTransformFeedback(n, ref ids);
#else
            TKES.GL.DeleteTransformFeedbacks(n, ref ids);
#endif
        }

        public unsafe void DeleteTransformFeedbacks(int n, int* ids)
        {
#if ANDROID
            TKES.GL.DeleteTransformFeedback(n, ids);
#else
            TKES.GL.DeleteTransformFeedbacks(n, ids);
#endif
        }

        public void DeleteVertexArray(int arrays)
        {
#if ANDROID
            TKES.GL.DeleteVertexArrays(1, ref arrays);
#else
            TKES.GL.DeleteVertexArray(arrays);
#endif
        }

        public void DeleteVertexArrays(int n, int[] arrays)
        {
            TKES.GL.DeleteVertexArrays(n, arrays);
        }

        public void DeleteVertexArrays(int n, ref int arrays)
        {
            TKES.GL.DeleteVertexArrays(n, ref arrays);
        }

        public unsafe void DeleteVertexArrays(int n, int* arrays)
        {
            TKES.GL.DeleteVertexArrays(n, arrays);
        }

        public void DrawArraysInstanced(PrimitiveType mode, int first, int count, int instancecount)
        {
            TKES.GL.DrawArraysInstanced((TKES.PrimitiveType)mode, first, count, instancecount);
        }

        public void DrawBuffers(int n, DrawBufferMode[] bufs)
        {
            var a = Array.ConvertAll(bufs, item => (TKES.DrawBufferMode)item);
            TKES.GL.DrawBuffers(n, a);
        }

        public void DrawBuffers(int n, ref DrawBufferMode bufs)
        {
            var bufs2 = (TKES.DrawBufferMode)bufs;
            TKES.GL.DrawBuffers(n, ref bufs2);
            bufs = (DrawBufferMode)bufs2;
        }

        public unsafe void DrawBuffers(int n, DrawBufferMode* bufs)
        {
            TKES.GL.DrawBuffers(n, (TKES.DrawBufferMode*)bufs);
        }

        public void DrawElementsInstanced(PrimitiveType mode, int count, DrawElementsType type, nint indices, int instancecount)
        {
            TKES.GL.DrawElementsInstanced((TKES.PrimitiveType)mode, count, (TKES.DrawElementsType)type, indices, instancecount);
        }

        public void DrawElementsInstanced<T3>(PrimitiveType mode, int count, DrawElementsType type, [In, Out] T3[] indices, int instancecount) where T3 : struct
        {
            TKES.GL.DrawElementsInstanced((TKES.PrimitiveType)mode, count, (TKES.DrawElementsType)type, indices, instancecount);
        }

        public void DrawElementsInstanced<T3>(PrimitiveType mode, int count, DrawElementsType type, [In, Out] T3[,] indices, int instancecount) where T3 : struct
        {
            TKES.GL.DrawElementsInstanced((TKES.PrimitiveType)mode, count, (TKES.DrawElementsType)type, indices, instancecount);
        }

        public void DrawElementsInstanced<T3>(PrimitiveType mode, int count, DrawElementsType type, [In, Out] T3[,,] indices, int instancecount) where T3 : struct
        {
            TKES.GL.DrawElementsInstanced((TKES.PrimitiveType)mode, count, (TKES.DrawElementsType)type, indices, instancecount);
        }

        public void DrawElementsInstanced<T3>(PrimitiveType mode, int count, DrawElementsType type, [In, Out] ref T3 indices, int instancecount) where T3 : struct
        {
            TKES.GL.DrawElementsInstanced((TKES.PrimitiveType)mode, count, (TKES.DrawElementsType)type, ref indices, instancecount);
        }

        public void DrawRangeElements(PrimitiveType mode, int start, int end, int count, DrawElementsType type, nint indices)
        {
            TKES.GL.DrawRangeElements((TKES.PrimitiveType)mode, start, end, count, (TKES.DrawElementsType)type, indices);
        }

        public void DrawRangeElements<T5>(PrimitiveType mode, int start, int end, int count, DrawElementsType type, [In, Out] T5[] indices) where T5 : struct
        {
            TKES.GL.DrawRangeElements((TKES.PrimitiveType)mode, start, end, count, (TKES.DrawElementsType)type, indices);
        }

        public void DrawRangeElements<T5>(PrimitiveType mode, int start, int end, int count, DrawElementsType type, [In, Out] T5[,] indices) where T5 : struct
        {
            TKES.GL.DrawRangeElements((TKES.PrimitiveType)mode, start, end, count, (TKES.DrawElementsType)type, indices);
        }

        public void DrawRangeElements<T5>(PrimitiveType mode, int start, int end, int count, DrawElementsType type, [In, Out] T5[,,] indices) where T5 : struct
        {
            TKES.GL.DrawRangeElements((TKES.PrimitiveType)mode, start, end, count, (TKES.DrawElementsType)type, indices);
        }

        public void DrawRangeElements<T5>(PrimitiveType mode, int start, int end, int count, DrawElementsType type, [In, Out] ref T5 indices) where T5 : struct
        {
            TKES.GL.DrawRangeElements((TKES.PrimitiveType)mode, start, end, count, (TKES.DrawElementsType)type, ref indices);
        }

        public void EndQuery(QueryTarget target)
        {
            TKES.GL.EndQuery((TKES.QueryTarget)target);
        }

        public void EndTransformFeedback()
        {
            TKES.GL.EndTransformFeedback();
        }

        public nint FenceSync(SyncCondition condition, WaitSyncFlags flags)
        {
            return TKES.GL.FenceSync((TKES.SyncCondition)condition, (TKES.WaitSyncFlags)flags);
        }

        public void FlushMappedBufferRange(BufferTarget target, nint offset, int length)
        {
            TKES.GL.FlushMappedBufferRange((TKES.BufferTarget)target, offset, length);
        }

        public void FlushMappedBufferRange(BufferTarget target, nint offset, nint length)
        {
            TKES.GL.FlushMappedBufferRange((TKES.BufferTarget)target, offset, length);
        }

        public void FramebufferTextureLayer(FramebufferTarget target, FramebufferAttachment attachment, int texture, int level, int layer)
        {
            TKES.GL.FramebufferTextureLayer((TKES.FramebufferTarget)target, (TKES.FramebufferAttachment)attachment, texture, level, layer);
        }


        public void GenQueries(int n, [Out] int[] ids)
        {
            TKES.GL.GenQueries(n, ids);
        }

        public void GenQueries(int n, out int ids)
        {
            TKES.GL.GenQueries(n, out ids);
        }

        public unsafe void GenQueries(int n, [Out] int* ids)
        {
            TKES.GL.GenQueries(n, ids);
        }

        public int GenQuery()
        {
#if ANDROID
            int id;
            TKES.GL.GenQueries(1, out id);
            return id;
#else
            return TKES.GL.GenQuery();
#endif
        }

        public int GenSampler()
        {
#if ANDROID
            int sampler;
            TKES.GL.GenSamplers(1, out sampler);
            return sampler;
#else
            return TKES.GL.GenSampler();
#endif
        }

        public void GenSamplers(int count, [Out] int[] samplers)
        {
            TKES.GL.GenSamplers(count, samplers);
        }

        public void GenSamplers(int count, out int samplers)
        {
            TKES.GL.GenSamplers(count, out samplers);
        }

        public unsafe void GenSamplers(int count, [Out] int* samplers)
        {
            TKES.GL.GenSamplers(count, samplers);
        }

        public int GenTransformFeedback()
        {
#if ANDROID
            int id;
            TKES.GL.GenTransformFeedback(1, out id);
            return id;
#else
            return TKES.GL.GenTransformFeedback();
#endif
        }

        public void GenTransformFeedbacks(int n, [Out] int[] ids)
        {
#if ANDROID
            TKES.GL.GenTransformFeedback(n, ids);
#else
            TKES.GL.GenTransformFeedbacks(n, ids);
#endif
        }

        public void GenTransformFeedbacks(int n, out int ids)
        {
#if ANDROID
            TKES.GL.GenTransformFeedback(n, out ids);
#else
            TKES.GL.GenTransformFeedbacks(n, out ids);
#endif
        }

        public unsafe void GenTransformFeedbacks(int n, [Out] int* ids)
        {
#if ANDROID
            TKES.GL.GenTransformFeedback(n, ids);
#else
            TKES.GL.GenTransformFeedbacks(n, ids);
#endif
        }

        public int GenVertexArray()
        {
#if ANDROID
            int array;
            TKES.GL.GenVertexArrays(1, out array);
            return array;
#else
            return TKES.GL.GenVertexArray();
#endif
        }

        public void GenVertexArrays(int n, [Out] int[] arrays)
        {
            TKES.GL.GenVertexArrays(n, arrays);
        }

        public void GenVertexArrays(int n, out int arrays)
        {
            TKES.GL.GenVertexArrays(n, out arrays);
        }

        public unsafe void GenVertexArrays(int n, [Out] int* arrays)
        {
            TKES.GL.GenVertexArrays(n, arrays);
        }

        public void GetActiveUniformBlock(int program, int uniformBlockIndex, ActiveUniformBlockParameter pname, [Out] int[] @params)
        {
            TKES.GL.GetActiveUniformBlock(program, uniformBlockIndex, (TKES.ActiveUniformBlockParameter)pname, @params);
        }

        public void GetActiveUniformBlock(int program, int uniformBlockIndex, ActiveUniformBlockParameter pname, out int @params)
        {
            TKES.GL.GetActiveUniformBlock(program, uniformBlockIndex, (TKES.ActiveUniformBlockParameter)pname, out @params);
        }

        public unsafe void GetActiveUniformBlock(int program, int uniformBlockIndex, ActiveUniformBlockParameter pname, [Out] int* @params)
        {
            TKES.GL.GetActiveUniformBlock(program, uniformBlockIndex, (TKES.ActiveUniformBlockParameter)pname, @params);
        }

        public void GetActiveUniformBlockName(int program, int uniformBlockIndex, int bufSize, out int length, [Out] StringBuilder uniformBlockName)
        {
            TKES.GL.GetActiveUniformBlockName(program, uniformBlockIndex, bufSize, out length, uniformBlockName);
        }

        public unsafe void GetActiveUniformBlockName(int program, int uniformBlockIndex, int bufSize, [Out] int* length, [Out] StringBuilder uniformBlockName)
        {
            TKES.GL.GetActiveUniformBlockName(program, uniformBlockIndex, bufSize, length, uniformBlockName);
        }

        public void GetActiveUniforms(int program, int uniformCount, int[] uniformIndices, ActiveUniformParameter pname, [Out] int[] @params)
        {
            TKES.GL.GetActiveUniforms(program, uniformCount, uniformIndices, (TKES.ActiveUniformParameter)pname, @params);
        }

        public void GetActiveUniforms(int program, int uniformCount, ref int uniformIndices, ActiveUniformParameter pname, out int @params)
        {
#if ANDROID
            TKES.GL.GetActiveUniforms(program, uniformCount, out uniformIndices, (TKES.ActiveUniformParameter)pname, out @params);
#else
            TKES.GL.GetActiveUniforms(program, uniformCount, ref uniformIndices, (TKES.ActiveUniformParameter)pname, out @params);
#endif
        }

        public unsafe void GetActiveUniforms(int program, int uniformCount, int* uniformIndices, ActiveUniformParameter pname, [Out] int* @params)
        {
            TKES.GL.GetActiveUniforms(program, uniformCount, uniformIndices, (TKES.ActiveUniformParameter)pname, @params);
        }


        public void GetBufferParameter(BufferTarget target, BufferParameterName pname, [Out] long[] @params)
        {
            TKES.GL.GetBufferParameter((TKES.BufferTarget)target, (TKES.BufferParameterName)pname, @params);
        }

        public void GetBufferParameter(BufferTarget target, BufferParameterName pname, out long @params)
        {
            TKES.GL.GetBufferParameter((TKES.BufferTarget)target, (TKES.BufferParameterName)pname, out @params);
        }

        public unsafe void GetBufferParameter(BufferTarget target, BufferParameterName pname, [Out] long* @params)
        {
            TKES.GL.GetBufferParameter((TKES.BufferTarget)target, (TKES.BufferParameterName)pname, @params);
        }

        public void GetBufferPointer(BufferTarget target, BufferPointer pname, [Out] nint @params)
        {
            TKES.GL.GetBufferPointer((TKES.BufferTarget)target, (TKES.BufferPointer)pname, @params);
        }

        public void GetBufferPointer<T2>(BufferTarget target, BufferPointer pname, [In, Out] T2[] @params) where T2 : struct
        {
            TKES.GL.GetBufferPointer((TKES.BufferTarget)target, (TKES.BufferPointer)pname, @params);
        }

        public void GetBufferPointer<T2>(BufferTarget target, BufferPointer pname, [In, Out] T2[,] @params) where T2 : struct
        {
            TKES.GL.GetBufferPointer((TKES.BufferTarget)target, (TKES.BufferPointer)pname, @params);
        }

        public void GetBufferPointer<T2>(BufferTarget target, BufferPointer pname, [In, Out] T2[,,] @params) where T2 : struct
        {
            TKES.GL.GetBufferPointer((TKES.BufferTarget)target, (TKES.BufferPointer)pname, @params);
        }

        public void GetBufferPointer<T2>(BufferTarget target, BufferPointer pname, [In, Out] ref T2 @params) where T2 : struct
        {
            TKES.GL.GetBufferPointer((TKES.BufferTarget)target, (TKES.BufferPointer)pname, ref @params);
        }

        public int GetFragDataLocation(int program, string name)
        {
#if ANDROID
            var builder = new StringBuilder(name);
            return TKES.GL.GetFragDataLocation(program, builder);
#else
            return TKES.GL.GetFragDataLocation(program, name);
#endif
        }

        public void GetInteger(GetIndexedPName target, int index, [Out] int[] data)
        {
            TKES.GL.GetInteger((TKES.GetIndexedPName)target, index, data);
        }

        public void GetInteger(GetIndexedPName target, int index, out int data)
        {
            TKES.GL.GetInteger((TKES.GetIndexedPName)target, index, out data);
        }

        public unsafe void GetInteger(GetIndexedPName target, int index, [Out] int* data)
        {
            TKES.GL.GetInteger((TKES.GetIndexedPName)target, index, data);
        }

        public void GetInteger64(GetIndexedPName target, int index, [Out] long[] data)
        {
#if ANDROID
            TKES.GL.GetInteger64((TKES.All)target, index, data);
#else
            TKES.GL.GetInteger64((TKES.GetIndexedPName)target, index, data);
#endif
        }

        public void GetInteger64(GetIndexedPName target, int index, out long data)
        {
#if ANDROID
            TKES.GL.GetInteger64((TKES.All)target, index, out data);
#else
            TKES.GL.GetInteger64((TKES.GetIndexedPName)target, index, out data);
#endif
        }

        public unsafe void GetInteger64(GetIndexedPName target, int index, [Out] long* data)
        {
#if ANDROID
            TKES.GL.GetInteger64((TKES.All)target, index, data);
#else
            TKES.GL.GetInteger64((TKES.GetIndexedPName)target, index, data);
#endif
        }

        public long GetInteger64(GetPName pname)
        {
#if ANDROID
            TKES.GL.GetInteger64((TKES.All)pname, out long result);
            return result;
#else
            return TKES.GL.GetInteger64((TKES.GetPName)pname);
#endif
        }

        public void GetInteger64(GetPName pname, [Out] long[] data)
        {
            TKES.GL.GetInteger64((TKES.GetPName)pname, data);
        }

        public void GetInteger64(GetPName pname, out long data)
        {
            TKES.GL.GetInteger64((TKES.GetPName)pname, out data);
        }

        public unsafe void GetInteger64(GetPName pname, [Out] long* data)
        {
            TKES.GL.GetInteger64((TKES.GetPName)pname, data);
        }

        public void GetInternalformat(ImageTarget target, SizedInternalFormat internalformat, InternalFormatParameter pname, int bufSize, [Out] int[] @params)
        {
            TKES.GL.GetInternalformat((TKES.ImageTarget)target, (TKES.SizedInternalFormat)internalformat, (TKES.InternalFormatParameter)pname, bufSize, @params);
        }

        public void GetInternalformat(ImageTarget target, SizedInternalFormat internalformat, InternalFormatParameter pname, int bufSize, out int @params)
        {
            TKES.GL.GetInternalformat((TKES.ImageTarget)target, (TKES.SizedInternalFormat)internalformat, (TKES.InternalFormatParameter)pname, bufSize, out @params);
        }

        public unsafe void GetInternalformat(ImageTarget target, SizedInternalFormat internalformat, InternalFormatParameter pname, int bufSize, [Out] int* @params)
        {
            TKES.GL.GetInternalformat((TKES.ImageTarget)target, (TKES.SizedInternalFormat)internalformat, (TKES.InternalFormatParameter)pname, bufSize, @params);
        }

        public void GetProgramBinary(int program, int bufSize, out int length, out All binaryFormat, [Out] nint binary)
        {
            TKES.GL.GetProgramBinary(program, bufSize, out length, out TKES.All binaryFormat2, binary);
            binaryFormat = (All)binaryFormat2;
        }

        public void GetProgramBinary<T4>(int program, int bufSize, out int length, out All binaryFormat, [In, Out] T4[] binary) where T4 : struct
        {
            TKES.GL.GetProgramBinary(program, bufSize, out length, out TKES.All binaryFormat2, binary);
            binaryFormat = (All)binaryFormat2;
        }

        public void GetProgramBinary<T4>(int program, int bufSize, out int length, out All binaryFormat, [In, Out] T4[,] binary) where T4 : struct
        {
            TKES.GL.GetProgramBinary(program, bufSize, out length, out TKES.All binaryFormat2, binary);
            binaryFormat = (All)binaryFormat2;
        }

        public void GetProgramBinary<T4>(int program, int bufSize, out int length, out All binaryFormat, [In, Out] T4[,,] binary) where T4 : struct
        {
            TKES.GL.GetProgramBinary(program, bufSize, out length, out TKES.All binaryFormat2, binary);
            binaryFormat = (All)binaryFormat2;
        }

        public void GetProgramBinary<T4>(int program, int bufSize, out int length, out All binaryFormat, [In, Out] ref T4 binary) where T4 : struct
        {
            TKES.GL.GetProgramBinary(program, bufSize, out length, out TKES.All binaryFormat2, ref binary);
            binaryFormat = (All)binaryFormat2;
        }

        public unsafe void GetProgramBinary(int program, int bufSize, [Out] int* length, [Out] All* binaryFormat, [Out] nint binary)
        {
            TKES.GL.GetProgramBinary(program, bufSize, length, (TKES.All*)binaryFormat, binary);
        }

        public unsafe void GetProgramBinary<T4>(int program, int bufSize, [Out] int* length, [Out] All* binaryFormat, [In, Out] T4[] binary) where T4 : struct
        {
            TKES.GL.GetProgramBinary(program, bufSize, length, (TKES.All*)binaryFormat, binary);
        }

        public unsafe void GetProgramBinary<T4>(int program, int bufSize, [Out] int* length, [Out] All* binaryFormat, [In, Out] T4[,] binary) where T4 : struct
        {
            TKES.GL.GetProgramBinary(program, bufSize, length, (TKES.All*)binaryFormat, binary);
        }

        public unsafe void GetProgramBinary<T4>(int program, int bufSize, [Out] int* length, [Out] All* binaryFormat, [In, Out] T4[,,] binary) where T4 : struct
        {
            TKES.GL.GetProgramBinary(program, bufSize, length, (TKES.All*)binaryFormat, binary);
        }

        public unsafe void GetProgramBinary<T4>(int program, int bufSize, [Out] int* length, [Out] All* binaryFormat, [In, Out] ref T4 binary) where T4 : struct
        {
            TKES.GL.GetProgramBinary(program, bufSize, length, (TKES.All*)binaryFormat, ref binary);
        }

        public void GetQuery(QueryTarget target, GetQueryParam pname, [Out] int[] @params)
        {
            TKES.GL.GetQuery((TKES.QueryTarget)target, (TKES.GetQueryParam)pname, @params);
        }

        public void GetQuery(QueryTarget target, GetQueryParam pname, out int @params)
        {
            TKES.GL.GetQuery((TKES.QueryTarget)target, (TKES.GetQueryParam)pname, out @params);
        }

        public unsafe void GetQuery(QueryTarget target, GetQueryParam pname, [Out] int* @params)
        {
            TKES.GL.GetQuery((TKES.QueryTarget)target, (TKES.GetQueryParam)pname, @params);
        }

        public void GetQueryObject(int id, GetQueryObjectParam pname, [Out] int[] @params)
        {
            TKES.GL.GetQueryObject(id, (TKES.GetQueryObjectParam)pname, @params);
        }

        public void GetQueryObject(int id, GetQueryObjectParam pname, out int @params)
        {
            TKES.GL.GetQueryObject(id, (TKES.GetQueryObjectParam)pname, out @params);
        }

        public unsafe void GetQueryObject(int id, GetQueryObjectParam pname, [Out] int* @params)
        {
            TKES.GL.GetQueryObject(id, (TKES.GetQueryObjectParam)pname, @params);
        }

        public void GetSamplerParameter(int sampler, SamplerParameterName pname, [Out] float[] @params)
        {
            TKES.GL.GetSamplerParameter(sampler, (TKES.SamplerParameterName)pname, @params);
        }

        public void GetSamplerParameter(int sampler, SamplerParameterName pname, out float @params)
        {
            TKES.GL.GetSamplerParameter(sampler, (TKES.SamplerParameterName)pname, out @params);
        }

        public unsafe void GetSamplerParameter(int sampler, SamplerParameterName pname, [Out] float* @params)
        {
            TKES.GL.GetSamplerParameter(sampler, (TKES.SamplerParameterName)pname, @params);
        }

        public void GetSamplerParameter(int sampler, SamplerParameterName pname, [Out] int[] @params)
        {
            TKES.GL.GetSamplerParameter(sampler, (TKES.SamplerParameterName)pname, @params);
        }

        public void GetSamplerParameter(int sampler, SamplerParameterName pname, out int @params)
        {
            TKES.GL.GetSamplerParameter(sampler, (TKES.SamplerParameterName)pname, out @params);
        }

        public unsafe void GetSamplerParameter(int sampler, SamplerParameterName pname, [Out] int* @params)
        {
            TKES.GL.GetSamplerParameter(sampler, (TKES.SamplerParameterName)pname, @params);
        }

        public string GetString(StringNameIndexed name, int index)
        {
#if ANDROID
            return TKES.GL.GetString((TKES.All)name, index);
#else
            return TKES.GL.GetString((TKES.StringNameIndexed)name, index);
#endif
        }

        public void GetSync(nint sync, SyncParameterName pname, int bufSize, out int length, out int values)
        {
            TKES.GL.GetSync(sync, (TKES.SyncParameterName)pname, bufSize, out length, out values);
        }

        public unsafe void GetSync(nint sync, SyncParameterName pname, int bufSize, [Out] int* length, [Out] int* values)
        {
            TKES.GL.GetSync(sync, (TKES.SyncParameterName)pname, bufSize, length, values);
        }

        public void GetTransformFeedbackVarying(int program, int index, int bufSize, out int length, out int size, out TransformFeedbackType type, [Out] StringBuilder name)
        {
            TKES.GL.GetTransformFeedbackVarying(program, index, bufSize, out length, out size, out TKES.TransformFeedbackType type2, name);
            type = (TransformFeedbackType)type2;
        }

        public unsafe void GetTransformFeedbackVarying(int program, int index, int bufSize, [Out] int* length, [Out] int* size, [Out] TransformFeedbackType* type, [Out] StringBuilder name)
        {
            TKES.GL.GetTransformFeedbackVarying(program, index, bufSize, length, size, (TKES.TransformFeedbackType*)type, name);
        }

        public int GetUniformBlockIndex(int program, string uniformBlockName)
        {
#if ANDROID
            var builder = new StringBuilder(uniformBlockName);
            return TKES.GL.GetUniformBlockIndex(program, builder);
#else
            return TKES.GL.GetUniformBlockIndex(program, uniformBlockName);
#endif
        }

        public void GetUniformIndices(int program, int uniformCount, string[] uniformNames, [Out] int[] uniformIndices)
        {
#if ANDROID
            var builder = new StringBuilder();
            foreach (var name in uniformNames)
            {
                builder.Append(name);
                builder.Append('\0'); // ★重要：NUL 終端
            }
            TKES.GL.GetUniformIndices(program, uniformCount, builder, uniformIndices);
#else
            TKES.GL.GetUniformIndices(program, uniformCount, uniformNames, uniformIndices);
#endif
        }

        public void GetUniformIndices(int program, int uniformCount, string[] uniformNames, out int uniformIndices)
        {
#if ANDROID
            var builder = new StringBuilder();
            foreach (var name in uniformNames)
            {
                builder.Append(name);
                builder.Append('\0'); // ★重要：NUL 終端
            }
            TKES.GL.GetUniformIndices(program, uniformCount, builder, out uniformIndices);
#else
            TKES.GL.GetUniformIndices(program, uniformCount, uniformNames, out uniformIndices);
#endif
        }

        public unsafe void GetUniformIndices(int program, int uniformCount, string[] uniformNames, [Out] int* uniformIndices)
        {
#if ANDROID
            var builder = new StringBuilder();
            foreach (var name in uniformNames)
            {
                builder.Append(name);
                builder.Append('\0'); // ★重要：NUL 終端
            }
            TKES.GL.GetUniformIndices(program, uniformCount, builder, uniformIndices);
#else
            TKES.GL.GetUniformIndices(program, uniformCount, uniformNames, uniformIndices);
#endif
        }

        public void GetVertexAttribI(int index, All pname, out int @params)
        {
            TKES.GL.GetVertexAttribI(index, (TKES.All)pname, out @params);
        }

        public unsafe void GetVertexAttribI(int index, All pname, [Out] int* @params)
        {
            TKES.GL.GetVertexAttribI(index, (TKES.All)pname, @params);
        }

        public void InvalidateFramebuffer(FramebufferTarget target, int numAttachments, FramebufferAttachment[] attachments)
        {
            var att = Array.ConvertAll(attachments, x => (TKES.FramebufferAttachment)x);
            TKES.GL.InvalidateFramebuffer((TKES.FramebufferTarget)target, numAttachments, att);
        }

        public void InvalidateFramebuffer(FramebufferTarget target, int numAttachments, ref FramebufferAttachment attachments)
        {
            var attachments2 = (TKES.FramebufferAttachment)attachments;
            TKES.GL.InvalidateFramebuffer((TKES.FramebufferTarget)target, numAttachments, ref attachments2);
            attachments = (FramebufferAttachment)attachments2;
        }

        public unsafe void InvalidateFramebuffer(FramebufferTarget target, int numAttachments, FramebufferAttachment* attachments)
        {
            TKES.GL.InvalidateFramebuffer((TKES.FramebufferTarget)target, numAttachments, (TKES.FramebufferAttachment*)attachments);
        }

        public void InvalidateSubFramebuffer(FramebufferTarget target, int numAttachments, FramebufferAttachment[] attachments, int x, int y, int width, int height)
        {
            var att = Array.ConvertAll(attachments, x => (TKES.FramebufferAttachment)x);
            TKES.GL.InvalidateSubFramebuffer((TKES.FramebufferTarget)target, numAttachments, att, x, y, width, height);
        }

        public void InvalidateSubFramebuffer(FramebufferTarget target, int numAttachments, ref FramebufferAttachment attachments, int x, int y, int width, int height)
        {
            var attachments2 = (TKES.FramebufferAttachment)attachments;
            TKES.GL.InvalidateSubFramebuffer((TKES.FramebufferTarget)target, numAttachments, ref attachments2, x, y, width, height);
            attachments = (FramebufferAttachment)attachments2;
        }

        public unsafe void InvalidateSubFramebuffer(FramebufferTarget target, int numAttachments, FramebufferAttachment* attachments, int x, int y, int width, int height)
        {
            TKES.GL.InvalidateSubFramebuffer((TKES.FramebufferTarget)target, numAttachments, (TKES.FramebufferAttachment*)attachments, x, y, width, height);
        }

        public bool IsQuery(int id)
        {
            return TKES.GL.IsQuery(id);
        }

        public bool IsSampler(int sampler)
        {
            return TKES.GL.IsSampler(sampler);
        }

        public bool IsSync(nint sync)
        {
            return TKES.GL.IsSync(sync);
        }

        public bool IsTransformFeedback(int id)
        {
            return TKES.GL.IsTransformFeedback(id);
        }

        public bool IsVertexArray(int array)
        {
            return TKES.GL.IsVertexArray(array);
        }

        public nint MapBufferRange(BufferTarget target, nint offset, int length, BufferAccessMask access)
        {
            return TKES.GL.MapBufferRange((TKES.BufferTarget)target, offset, length, (TKES.BufferAccessMask)access);
        }

        public nint MapBufferRange(BufferTarget target, nint offset, nint length, BufferAccessMask access)
        {
            return TKES.GL.MapBufferRange((TKES.BufferTarget)target, offset, length, (TKES.BufferAccessMask)access);
        }

        public void PauseTransformFeedback()
        {
            TKES.GL.PauseTransformFeedback();
        }

        public void ProgramBinary(int program, All binaryFormat, nint binary, int length)
        {
            TKES.GL.ProgramBinary(program, (TKES.All)binaryFormat, binary, length);
        }

        public void ProgramBinary<T2>(int program, All binaryFormat, [In, Out] T2[] binary, int length) where T2 : struct
        {
            TKES.GL.ProgramBinary(program, (TKES.All)binaryFormat, binary, length);
        }

        public void ProgramBinary<T2>(int program, All binaryFormat, [In, Out] T2[,] binary, int length) where T2 : struct
        {
            TKES.GL.ProgramBinary(program, (TKES.All)binaryFormat, binary, length);
        }

        public void ProgramBinary<T2>(int program, All binaryFormat, [In, Out] T2[,,] binary, int length) where T2 : struct
        {
            TKES.GL.ProgramBinary(program, (TKES.All)binaryFormat, binary, length);
        }

        public void ProgramBinary<T2>(int program, All binaryFormat, [In, Out] ref T2 binary, int length) where T2 : struct
        {
            TKES.GL.ProgramBinary(program, (TKES.All)binaryFormat, ref binary, length);
        }

        public void ProgramParameter(int program, ProgramParameterName pname, int value)
        {
            TKES.GL.ProgramParameter(program, (TKES.ProgramParameterName)pname, value);
        }

        public void ReadBuffer(ReadBufferMode src)
        {
            TKES.GL.ReadBuffer((TKES.ReadBufferMode)src);
        }

        public void RenderbufferStorageMultisample(RenderbufferTarget target, int samples, RenderbufferInternalFormat internalformat, int width, int height)
        {
            TKES.GL.RenderbufferStorageMultisample((TKES.RenderbufferTarget)target, samples, (TKES.RenderbufferInternalFormat)internalformat, width, height);
        }

        public void ResumeTransformFeedback()
        {
            TKES.GL.ResumeTransformFeedback();
        }

        public void SamplerParameter(int sampler, SamplerParameterName pname, float param)
        {
            TKES.GL.SamplerParameter(sampler, (TKES.SamplerParameterName)pname, param);
        }

        public void SamplerParameter(int sampler, SamplerParameterName pname, float[] param)
        {
            TKES.GL.SamplerParameter(sampler, (TKES.SamplerParameterName)pname, param);
        }

        public unsafe void SamplerParameter(int sampler, SamplerParameterName pname, float* param)
        {
            TKES.GL.SamplerParameter(sampler, (TKES.SamplerParameterName)pname, param);
        }

        public void SamplerParameter(int sampler, SamplerParameterName pname, int param)
        {
            TKES.GL.SamplerParameter(sampler, (TKES.SamplerParameterName)pname, param);
        }

        public void SamplerParameter(int sampler, SamplerParameterName pname, int[] param)
        {
            TKES.GL.SamplerParameter(sampler, (TKES.SamplerParameterName)pname, param);
        }

        public unsafe void SamplerParameter(int sampler, SamplerParameterName pname, int* param)
        {
            TKES.GL.SamplerParameter(sampler, (TKES.SamplerParameterName)pname, param);
        }

        public void TexImage3D(TextureTarget3d target, int level, TextureComponentCount internalformat, int width, int height, int depth, int border, PixelFormat format, PixelType type, nint pixels)
        {
#if ANDROID
            TKES.GL.TexImage3D((TKES.TextureTarget3D)target, level, (TKES.TextureComponentCount)internalformat, width, height, depth, border, (TKES.PixelFormat)format, (TKES.PixelType)type, pixels);
#else
            TKES.GL.TexImage3D((TKES.TextureTarget3d)target, level, (TKES.TextureComponentCount)internalformat, width, height, depth, border, (TKES.PixelFormat)format, (TKES.PixelType)type, pixels);
#endif
        }

        public void TexImage3D<T9>(TextureTarget3d target, int level, TextureComponentCount internalformat, int width, int height, int depth, int border, PixelFormat format, PixelType type, [In, Out] T9[] pixels) where T9 : struct
        {
#if ANDROID
            TKES.GL.TexImage3D((TKES.TextureTarget3D)target, level, (TKES.TextureComponentCount)internalformat, width, height, depth, border, (TKES.PixelFormat)format, (TKES.PixelType)type, pixels);
#else
            TKES.GL.TexImage3D((TKES.TextureTarget3d)target, level, (TKES.TextureComponentCount)internalformat, width, height, depth, border, (TKES.PixelFormat)format, (TKES.PixelType)type, pixels);
#endif
        }

        public void TexImage3D<T9>(TextureTarget3d target, int level, TextureComponentCount internalformat, int width, int height, int depth, int border, PixelFormat format, PixelType type, [In, Out] T9[,] pixels) where T9 : struct
        {
#if ANDROID
            TKES.GL.TexImage3D((TKES.TextureTarget3D)target, level, (TKES.TextureComponentCount)internalformat, width, height, depth, border, (TKES.PixelFormat)format, (TKES.PixelType)type, pixels);
#else
            TKES.GL.TexImage3D((TKES.TextureTarget3d)target, level, (TKES.TextureComponentCount)internalformat, width, height, depth, border, (TKES.PixelFormat)format, (TKES.PixelType)type, pixels);
#endif
        }

        public void TexImage3D<T9>(TextureTarget3d target, int level, TextureComponentCount internalformat, int width, int height, int depth, int border, PixelFormat format, PixelType type, [In, Out] T9[,,] pixels) where T9 : struct
        {
#if ANDROID
            TKES.GL.TexImage3D((TKES.TextureTarget3D)target, level, (TKES.TextureComponentCount)internalformat, width, height, depth, border, (TKES.PixelFormat)format, (TKES.PixelType)type, pixels);
#else
            TKES.GL.TexImage3D((TKES.TextureTarget3d)target, level, (TKES.TextureComponentCount)internalformat, width, height, depth, border, (TKES.PixelFormat)format, (TKES.PixelType)type, pixels);
#endif
        }

        public void TexImage3D<T9>(TextureTarget3d target, int level, TextureComponentCount internalformat, int width, int height, int depth, int border, PixelFormat format, PixelType type, [In, Out] ref T9 pixels) where T9 : struct
        {
#if ANDROID
            TKES.GL.TexImage3D((TKES.TextureTarget3D)target, level, (TKES.TextureComponentCount)internalformat, width, height, depth, border, (TKES.PixelFormat)format, (TKES.PixelType)type, ref pixels);
#else
            TKES.GL.TexImage3D((TKES.TextureTarget3d)target, level, (TKES.TextureComponentCount)internalformat, width, height, depth, border, (TKES.PixelFormat)format, (TKES.PixelType)type, ref pixels);
#endif
        }

        public void TexStorage2D(TextureTarget2d target, int levels, SizedInternalFormat internalformat, int width, int height)
        {
#if ANDROID
            TKES.GL.TexStorage2D((TKES.TextureTarget2D)target, levels, (TKES.SizedInternalFormat)internalformat, width, height);
#else
            TKES.GL.TexStorage2D((TKES.TextureTarget2d)target, levels, (TKES.SizedInternalFormat)internalformat, width, height);
#endif
        }

        public void TexStorage3D(TextureTarget3d target, int levels, SizedInternalFormat internalformat, int width, int height, int depth)
        {
#if ANDROID
            TKES.GL.TexStorage3D((TKES.TextureTarget3D)target, levels, (TKES.SizedInternalFormat)internalformat, width, height, depth);
#else
            TKES.GL.TexStorage3D((TKES.TextureTarget3d)target, levels, (TKES.SizedInternalFormat)internalformat, width, height, depth);
#endif
        }

        public void TexSubImage3D(TextureTarget3d target, int level, int xoffset, int yoffset, int zoffset, int width, int height, int depth, PixelFormat format, PixelType type, nint pixels)
        {
#if ANDROID
            TKES.GL.TexSubImage3D((TKES.TextureTarget3D)target, level, xoffset, yoffset, zoffset, width, height, depth, (TKES.PixelFormat)format, (TKES.PixelType)type, pixels);
#else
            TKES.GL.TexSubImage3D((TKES.TextureTarget3d)target, level, xoffset, yoffset, zoffset, width, height, depth, (TKES.PixelFormat)format, (TKES.PixelType)type, pixels);
#endif
        }

        public void TexSubImage3D<T10>(TextureTarget3d target, int level, int xoffset, int yoffset, int zoffset, int width, int height, int depth, PixelFormat format, PixelType type, [In, Out] T10[] pixels) where T10 : struct
        {
#if ANDROID
            TKES.GL.TexSubImage3D((TKES.TextureTarget3D)target, level, xoffset, yoffset, zoffset, width, height, depth, (TKES.PixelFormat)format, (TKES.PixelType)type, pixels);
#else
            TKES.GL.TexSubImage3D((TKES.TextureTarget3d)target, level, xoffset, yoffset, zoffset, width, height, depth, (TKES.PixelFormat)format, (TKES.PixelType)type, pixels);
#endif
        }

        public void TexSubImage3D<T10>(TextureTarget3d target, int level, int xoffset, int yoffset, int zoffset, int width, int height, int depth, PixelFormat format, PixelType type, [In, Out] T10[,] pixels) where T10 : struct
        {
#if ANDROID
            TKES.GL.TexSubImage3D((TKES.TextureTarget3D)target, level, xoffset, yoffset, zoffset, width, height, depth, (TKES.PixelFormat)format, (TKES.PixelType)type, pixels);
#else
            TKES.GL.TexSubImage3D((TKES.TextureTarget3d)target, level, xoffset, yoffset, zoffset, width, height, depth, (TKES.PixelFormat)format, (TKES.PixelType)type, pixels);
#endif
        }

        public void TexSubImage3D<T10>(TextureTarget3d target, int level, int xoffset, int yoffset, int zoffset, int width, int height, int depth, PixelFormat format, PixelType type, [In, Out] T10[,,] pixels) where T10 : struct
        {
#if ANDROID
            TKES.GL.TexSubImage3D((TKES.TextureTarget3D)target, level, xoffset, yoffset, zoffset, width, height, depth, (TKES.PixelFormat)format, (TKES.PixelType)type, pixels);
#else
            TKES.GL.TexSubImage3D((TKES.TextureTarget3d)target, level, xoffset, yoffset, zoffset, width, height, depth, (TKES.PixelFormat)format, (TKES.PixelType)type, pixels);
#endif
        }

        public void TexSubImage3D<T10>(TextureTarget3d target, int level, int xoffset, int yoffset, int zoffset, int width, int height, int depth, PixelFormat format, PixelType type, [In, Out] ref T10 pixels) where T10 : struct
        {
#if ANDROID
            TKES.GL.TexSubImage3D((TKES.TextureTarget3D)target, level, xoffset, yoffset, zoffset, width, height, depth, (TKES.PixelFormat)format, (TKES.PixelType)type, ref pixels);
#else
            TKES.GL.TexSubImage3D((TKES.TextureTarget3d)target, level, xoffset, yoffset, zoffset, width, height, depth, (TKES.PixelFormat)format, (TKES.PixelType)type, ref pixels);
#endif
        }

        public void TransformFeedbackVaryings(int program, int count, string[] varyings, TransformFeedbackMode bufferMode)
        {
#if ANDROID
            var sb = new StringBuilder();
            foreach (var v in varyings)
            {
                sb.Append(v);
                sb.Append('\0');   // ★ 必須
            }

            // ★ ToString() して OK（このバインディングでは正解）
            TKES.GL.TransformFeedbackVaryings(
                program,
                count,
                sb.ToString(),
                (TKES.TransformFeedbackMode)bufferMode);
#else
            TKES.GL.TransformFeedbackVaryings(program, count, varyings, (TKES.TransformFeedbackMode)bufferMode);
#endif
        }

        public void UniformBlockBinding(int program, int uniformBlockIndex, int uniformBlockBinding)
        {
            TKES.GL.UniformBlockBinding(program, uniformBlockIndex, uniformBlockBinding);
        }

        public void UniformMatrix2x3(int location, int count, bool transpose, float[] value)
        {
            TKES.GL.UniformMatrix2x3(location, count, transpose, value);
        }

        public void UniformMatrix2x3(int location, int count, bool transpose, ref float value)
        {
            TKES.GL.UniformMatrix2x3(location, count, transpose, ref value);
        }

        public unsafe void UniformMatrix2x3(int location, int count, bool transpose, float* value)
        {
            TKES.GL.UniformMatrix2x3(location, count, transpose, value);
        }

        public void UniformMatrix2x4(int location, int count, bool transpose, float[] value)
        {
            TKES.GL.UniformMatrix2x4(location, count, transpose, value);
        }

        public void UniformMatrix2x4(int location, int count, bool transpose, ref float value)
        {
            TKES.GL.UniformMatrix2x4(location, count, transpose, ref value);
        }

        public unsafe void UniformMatrix2x4(int location, int count, bool transpose, float* value)
        {
            TKES.GL.UniformMatrix2x4(location, count, transpose, value);
        }

        public void UniformMatrix3x2(int location, int count, bool transpose, float[] value)
        {
            TKES.GL.UniformMatrix3x2(location, count, transpose, value);
        }

        public void UniformMatrix3x2(int location, int count, bool transpose, ref float value)
        {
            TKES.GL.UniformMatrix3x2(location, count, transpose, ref value);
        }

        public unsafe void UniformMatrix3x2(int location, int count, bool transpose, float* value)
        {
            TKES.GL.UniformMatrix3x2(location, count, transpose, value);
        }

        public void UniformMatrix3x4(int location, int count, bool transpose, float[] value)
        {
            TKES.GL.UniformMatrix3x4(location, count, transpose, value);
        }

        public void UniformMatrix3x4(int location, int count, bool transpose, ref float value)
        {
            TKES.GL.UniformMatrix3x4(location, count, transpose, ref value);
        }

        public unsafe void UniformMatrix3x4(int location, int count, bool transpose, float* value)
        {
            TKES.GL.UniformMatrix3x4(location, count, transpose, value);
        }

        public void UniformMatrix4x2(int location, int count, bool transpose, float[] value)
        {
            TKES.GL.UniformMatrix4x2(location, count, transpose, value);
        }

        public void UniformMatrix4x2(int location, int count, bool transpose, ref float value)
        {
            TKES.GL.UniformMatrix4x2(location, count, transpose, ref value);
        }

        public unsafe void UniformMatrix4x2(int location, int count, bool transpose, float* value)
        {
            TKES.GL.UniformMatrix4x2(location, count, transpose, value);
        }

        public void UniformMatrix4x3(int location, int count, bool transpose, float[] value)
        {
            TKES.GL.UniformMatrix4x3(location, count, transpose, value);
        }

        public void UniformMatrix4x3(int location, int count, bool transpose, ref float value)
        {
            TKES.GL.UniformMatrix4x3(location, count, transpose, ref value);
        }

        public unsafe void UniformMatrix4x3(int location, int count, bool transpose, float* value)
        {
            TKES.GL.UniformMatrix4x3(location, count, transpose, value);
        }

        public bool UnmapBuffer(BufferTarget target)
        {
            return TKES.GL.UnmapBuffer((TKES.BufferTarget)target);
        }

        public void VertexAttribDivisor(int index, int divisor)
        {
            TKES.GL.VertexAttribDivisor(index, divisor);
        }

        public void VertexAttribI4(int index, int x, int y, int z, int w)
        {
            TKES.GL.VertexAttribI4(index, x, y, z, w);
        }

        public void VertexAttribI4(int index, int[] v)
        {
            TKES.GL.VertexAttribI4(index, v);
        }

        public void VertexAttribI4(int index, ref int v)
        {
            TKES.GL.VertexAttribI4(index, ref v);
        }

        public unsafe void VertexAttribI4(int index, int* v)
        {
            TKES.GL.VertexAttribI4(index, v);
        }

        public void VertexAttribIPointer(int index, int size, VertexAttribIntegerType type, int stride, nint pointer)
        {
            TKES.GL.VertexAttribIPointer(index, size, (TKES.VertexAttribIntegerType)type, stride, pointer);
        }

        public void VertexAttribIPointer<T4>(int index, int size, VertexAttribIntegerType type, int stride, [In, Out] T4[] pointer) where T4 : struct
        {
            TKES.GL.VertexAttribIPointer(index, size, (TKES.VertexAttribIntegerType)type, stride, pointer);
        }

        public void VertexAttribIPointer<T4>(int index, int size, VertexAttribIntegerType type, int stride, [In, Out] T4[,] pointer) where T4 : struct
        {
            TKES.GL.VertexAttribIPointer(index, size, (TKES.VertexAttribIntegerType)type, stride, pointer);
        }

        public void VertexAttribIPointer<T4>(int index, int size, VertexAttribIntegerType type, int stride, [In, Out] T4[,,] pointer) where T4 : struct
        {
            TKES.GL.VertexAttribIPointer(index, size, (TKES.VertexAttribIntegerType)type, stride, pointer);
        }

        public void VertexAttribIPointer<T4>(int index, int size, VertexAttribIntegerType type, int stride, [In, Out] ref T4 pointer) where T4 : struct
        {
            TKES.GL.VertexAttribIPointer(index, size, (TKES.VertexAttribIntegerType)type, stride, ref pointer);
        }

        public void WaitSync(nint sync, WaitSyncFlags flags, long timeout)
        {
#if ANDROID
            TKES.GL.WaitSync(sync, (int)flags, timeout);
#else
            TKES.GL.WaitSync(sync, (TKES.WaitSyncFlags)flags, timeout);
#endif
        }

    }

}
