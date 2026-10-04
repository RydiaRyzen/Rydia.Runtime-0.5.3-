using Rydia.Audio.Api.OpenAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKAudio = OpenTK.Audio.OpenAL;
using TKAL = OpenTK.Audio.OpenAL.AL;
using System.Runtime.InteropServices;

namespace Rydia.Runtime
{

	public class ALBindings : DisposableBase, IAL
	{

        static ALBindings()
        {
            // OpenAL の DLL をプラットフォームごとに解決
            NativeLibrary.SetDllImportResolver(typeof(TKAudio.Alc).Assembly, (name, assembly, path) =>
            {
                if (name == "openal32.dll")
                {
					var isAndroid = AppDomain.CurrentDomain.GetAssemblies().Any(assembly =>
					assembly.FullName.StartsWith("Mono.Android"));
#if __ANDROID__
                    if (isAndroid)
                        return NativeLibrary.Load("libopenal32.so");
#else
                    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                        return NativeLibrary.Load("openal32.dll");
#endif
                }

                return IntPtr.Zero;
            });
        }

        internal ALBindings()
		{

		}

		protected override void Disposing(bool disposing)
		{
			
		}

        /// <summary>
        /// This function retrieves an OpenAL string property.
        /// </summary>
        /// <param name="param">The property to be returned: Vendor, Version, Renderer and Extensions</param>
        /// <returns>Returns a pointer to a null-terminated string.</returns>
        public string GetString(ALGetString param)
		{
			var value = (TKAudio.ALGetString)param;
			return TKAL.Get(value);
		}

        /// <summary>
        /// This function retrieves an OpenAL string property.
        /// </summary>
        /// <param name="param">The human-readable errorstring to be returned.</param>
        /// <returns>Returns a pointer to a null-terminated string.</returns>
        public string GetErrorString(ALError param)
		{
			var value = (TKAudio.ALError)param;
			return TKAL.GetErrorString(value);
		}

        //
        // 概要:
        //     
        //
        // パラメーター:
        //   n:
        //     
        //
        // 戻り値:
        //            
        /// <summary>
        /// This function generates one or more buffers, which contain audio data (see AL.BufferData).
        /// References to buffers are uint values, which are used wherever a buffer reference
        /// is needed (in calls such as AL.DeleteBuffers, AL.Source with parameter ALSourcei,
        /// AL.SourceQueueBuffers, and AL.SourceUnqueueBuffers).
        /// </summary>
        /// <param name="n">The number of buffers to be generated.</param>
        /// <returns>Pointer to an array of uint values which will store the names of the new buffers.</returns>
        public int[] GenBuffers(int n)
		{
			return TKAL.GenBuffers(n);
		}
        
		/// <summary>
        /// This function generates one buffer only, which contain audio data (see AL.BufferData).
        /// References to buffers are uint values, which are used wherever a buffer reference
        /// is needed (in calls such as AL.DeleteBuffers, AL.Source with parameter ALSourcei,
        /// AL.SourceQueueBuffers, and AL.SourceUnqueueBuffers).
		/// </summary>
		/// <returns>Pointer to an uint value which will store the name of the new buffer.</returns>
        public int GenBuffer()
		{
			return TKAL.GenBuffer();
		}

        /// <summary>
        /// This function deletes one or more buffers, freeing the resources used by the
        /// buffer. Buffers which are attached to a source can not be deleted. See AL.Source
        /// (ALSourcei) and AL.SourceUnqueueBuffers for information on how to detach a buffer
        /// from a source.
		/// </summary>
		/// <param name="buffers">Pointer to an array of buffer names identifying the buffers to be deleted.</param>
        public void DeleteBuffers(int[] buffers)
		{
			TKAL.DeleteBuffers(buffers);
		}

		public void DeleteBuffer(int buffer)
		{
			TKAL.DeleteBuffer(buffer);
		}

		public unsafe void BufferData(int bid, ALFormat format, byte[] buffer, int freq)
		{
			if (buffer.Length == 0)
				return;
			fixed (byte* bufferPtr = buffer)
			{
				var value = (TKAudio.ALFormat)format;
				TKAL.BufferData((uint)bid, value, new IntPtr(bufferPtr), buffer.Length, freq);
			}
		}

		public int[] GenSources(int n)
		{
			return TKAL.GenSources(n);
		}

		public int GenSource()
		{
			return TKAL.GenSource();
		}

		public void GetSource(int sid, ALGetSourcei param, out int value)
		{
			var p = (TKAudio.ALGetSourcei)param;
			TKAL.GetSource(sid, p, out value);
		}

		public void GetSource(int sid, ALSourceb param, out bool value)
		{
			var p = (TKAudio.ALSourceb)param;
			TKAL.GetSource(sid, p, out value);
		}

		public void GetSource(int sid, ALSource3f param, out float value1, out float value2, out float value3)
		{
			var p = (TKAudio.ALSource3f)param;
			TKAL.GetSource(sid, p, out value1, out value2, out value3);
		}

		public void GetSource(int sid, ALSourcef param, out float value)
		{
			var p = (TKAudio.ALSourcef)param;
			TKAL.GetSource(sid, p, out value);
		}

		public void Source(int sid, ALSource3i param, int value1, int value2, int value3)
		{
			var p = (TKAudio.ALSource3i)param;
			TKAL.Source(sid, p, value1, value2, value3);
		}

		public void Source(int sid, ALSourcei param, int value)
		{
			var p = (TKAudio.ALSourcei)param;
			TKAL.Source(sid, p, value);
		}

		public void Source(int sid, ALSourceb param, bool value)
		{
			var p = (TKAudio.ALSourceb)param;
			TKAL.Source(sid, p, value);
		}

		public void Source(int sid, ALSource3f param, float value1, float value2, float value3)
		{
			var p = (TKAudio.ALSource3f)param;
			TKAL.Source(sid, p, value1, value2, value3);
		}

		public void Source(int sid, ALSourcef param, float value)
		{
			var p = (TKAudio.ALSourcef)param;
			TKAL.Source(sid, p, value);
		}

		public void SourcePlay(int[] sids)
		{
			TKAL.SourcePlay(sids.Length, sids);
		}

		public void SourcePlay(int sid)
		{
			TKAL.SourcePlay(unchecked(sid));
		}

		public void SourcePause(int[] sids)
		{
			TKAL.SourcePause(sids.Length, sids);
		}

		public void SourcePause(int sid)
		{
			TKAL.SourcePause(unchecked(sid));
		}

		public void SourceStop(int[] sids)
		{
			TKAL.SourceStop(sids.Length, sids);
		}

		public void SourceStop(int sid)
		{
			TKAL.SourceStop(unchecked(sid));
		}

		public void SourceQueueBuffers(int sid, int[] bids)
		{
			TKAL.SourceQueueBuffers(sid, bids.Length, bids);
		}

		public void SourceQueueBuffer(int source, int buffer)
		{
			TKAL.SourceQueueBuffer(source, buffer);
		}

		public void SourceUnqueueBuffers(int sid, int[] bids)
		{
			TKAL.SourceUnqueueBuffers(sid, bids.Length, bids);
		}

		public int SourceUnqueueBuffer(int sid)
		{
			return TKAL.SourceUnqueueBuffer(sid);
		}

		public int[] SourceUnqueueBuffers(int sid, int numEntries)
		{
			return TKAL.SourceUnqueueBuffers(sid, numEntries);
		}

		public void SourceRewind(int[] sids)
		{
			TKAL.SourceRewind(sids.Length, sids);
		}

		public void SourceRewind(int sid)
		{
			TKAL.SourceRewind(unchecked(sid));
		}

		public void DeleteSources(int[] sources)
		{
			TKAL.DeleteSources(sources);
		}

		public void DeleteSource(int sid)
		{
			TKAL.DeleteSource(sid);
		}

		public void GetListener(ALListenerf param, out float value)
		{
			var p = (TKAudio.ALListenerf)param;
			TKAL.GetListener(p, out value);
		}

		public void GetListener(ALListener3f param, out float value1, out float value2, out float value3)
		{
			var p = (TKAudio.ALListener3f)param;
			TKAL.GetListener(p, out value1, out value2, out value3);
		}

		public unsafe void GetListener(ALListenerfv param, float[] values)
		{
			var p = (TKAudio.ALListenerfv)param;
			fixed (float* ptr = values)
			{
				TKAL.GetListener(p, ptr);
			}
		}

		public void Listener(ALListenerf param, float value)
		{
			var p = (TKAudio.ALListenerf)param;
			TKAL.Listener(p, value);
		}

		public void Listener(ALListener3f param, float value1, float value2, float value3)
		{
			var p = (TKAudio.ALListener3f)param;
			TKAL.Listener(p, value1, value2, value3);
		}

		public unsafe void Listener(ALListenerfv param, float[] values)
		{
			var p = (TKAudio.ALListenerfv)param;
			TKAL.Listener(p, ref values);
		}

		public int GetInteger(ALGetInteger param)
		{
			var p = (TKAudio.ALGetInteger)param;
			return TKAL.Get(p);
		}

		public float GetFloat(ALGetFloat param)
		{
			var p = (TKAudio.ALGetFloat)param;
			return TKAL.Get(p);
		}

		public ALError GetError()
		{
			var p = TKAL.GetError();
			return (ALError)p;
		}

		public bool IsExtensionPresent([In] string extname)
		{
			return TKAL.IsExtensionPresent(extname);
		}

		public IntPtr GetProcAddress([In] string fname)
		{
			return TKAL.GetProcAddress(fname);
		}

		public int GetEnumValue([In] string ename)
		{
			return TKAL.GetEnumValue(ename);
		}

		public void GetBuffer(int bid, ALGetBufferi param, out int value)
		{
			var p = (TKAudio.ALGetBufferi)param;
			TKAL.GetBuffer(unchecked(bid), p, out value);
		}

		public bool IsSource(int sid)
		{
			return TKAL.IsSource(unchecked(sid));
		}

		public bool IsBuffer(int bid)
		{
			return TKAL.IsBuffer(unchecked(bid));
		}

		public void DopplerFactor(float value)
		{
			TKAL.DopplerFactor(value);
		}

		public void DopplerVelocity(float value)
		{
			TKAL.DopplerVelocity(value);
		}

		public void SpeedOfSound(float value)
		{
			TKAL.SpeedOfSound(value);
		}

		public void DistanceModel(ALDistanceModel distancemodel)
		{
			var p = (TKAudio.ALDistanceModel)distancemodel;
			TKAL.DistanceModel(p);
		}

		public void BufferData(int bid, ALFormat format, IntPtr buffer, int size, int freq)
		{
			var p = (TKAudio.ALFormat)format;
			TKAL.BufferData(bid, p, buffer, size, freq);
		}
	}

}
