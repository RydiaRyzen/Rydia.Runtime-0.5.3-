using Rydia.Audio.Api.OpenAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using TKAudio = OpenTK.Audio.OpenAL;
using TKALC = OpenTK.Audio.OpenAL.Alc;
using TK = OpenTK;
using Rydia.Audio.Api.OpenAL.Alc;
using System.Diagnostics;

namespace Rydia.Runtime
{

	public class AlcBindings : DisposableBase, IAlc
	{

		internal AlcBindings()
		{

		}

		protected override void Disposing(bool disposing)
		{
			
		}

		public IntPtr OpenDevice([In] string devicename)
		{
			return TKALC.OpenDevice(devicename);
		}

		public bool CloseDevice([In] IntPtr device)
		{
			return TKALC.CloseDevice(device);
		}

		public bool MakeContextCurrent(IntPtr context)
		{
			return TKALC.MakeContextCurrent(new TK.ContextHandle(context));
		}

		public IntPtr CreateContext(IntPtr device, int[] attribList)
		{
			return TKALC.CreateContext(device, attribList).Handle;
		}

		public IntPtr CreateContext(IntPtr device, AlcContextAttributes[] attribList)
		{
			var att = Array.ConvertAll(attribList, (s) =>
			{
				return (int)s;
			});
			return TKALC.CreateContext(device, att).Handle;
		}

		public IntPtr GetCurrentContext()
		{
			return TKALC.GetCurrentContext().Handle;
		}

		public void DestroyContext(IntPtr context)
		{
			TKALC.DestroyContext(new TK.ContextHandle(context));
		}

		public void ProcessContext(IntPtr context)
		{
			TKALC.ProcessContext(new TK.ContextHandle(context));
		}

		public void GetInteger(IntPtr device, AlcGetInteger param, int size, int[] data)
		{
			;
			var p = (TKAudio.AlcGetInteger)param;
			TKALC.GetInteger(device, p, size, data);
		}

		public string GetString(IntPtr device, AlcGetString param)
		{
			var p = (TKAudio.AlcGetString)param;
			var str = TKALC.GetString(device, p);

			Debug.Assert(str != null, nameof(str) + " != null");

			return str;
		}

		public string[] GetString(IntPtr device, AlcGetStringList param)
		{
			var p = (TKAudio.AlcGetStringList)param;
			var str = TKALC.GetString(device, p);

			return str.ToArray();
		}

		public int GetEnumValue([In] IntPtr device, [In] string enumname)
		{
			return TKALC.GetEnumValue(device, enumname);
		}

		public IntPtr GetProcAddress([In] IntPtr device, [In] string funcname)
		{
			return TKALC.GetProcAddress(device, funcname);
		}

		public bool IsExtensionPresent([In] IntPtr device, [In] string extensionName)
		{
			return TKALC.IsExtensionPresent(device, extensionName);
		}

		public AlcError GetError([In] IntPtr device)
		{
			var result = TKALC.GetError(device);
			return (AlcError)result;
		}

		public IntPtr GetContextsDevice(IntPtr context)
		{
			return TKALC.GetContextsDevice(new TK.ContextHandle(context));
		}

		public void SuspendContext(IntPtr context)
		{
			TKALC.SuspendContext(new TK.ContextHandle(context));
		}

	}

}
