using Rydia.Audio.Api.OpenAL.Alc;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace Rydia.Audio.Api.OpenAL
{

    /// <summary>
    /// このインターフェイスを実装しているクラスは○○の機能を提供します
    /// </summary>
    public interface IAlc
    {

        IntPtr OpenDevice([In] string devicename);

        bool CloseDevice([In] IntPtr device);

        bool MakeContextCurrent(IntPtr context);

        IntPtr CreateContext(IntPtr device, int[] attribList);

        IntPtr CreateContext(IntPtr device, AlcContextAttributes[] attribList);

        IntPtr GetCurrentContext();

        void DestroyContext(IntPtr context);

        void ProcessContext(IntPtr context);

        void GetInteger(IntPtr device, AlcGetInteger param, int size, int[] data);

        string GetString(IntPtr device, AlcGetString param);

        string[] GetString(IntPtr device, AlcGetStringList param);

        int GetEnumValue([In] IntPtr device, [In] string enumname);

        IntPtr GetProcAddress([In] IntPtr device, [In] string funcname);

        bool IsExtensionPresent([In] IntPtr device, [In] string extensionName);

        AlcError GetError([In] IntPtr device);

        IntPtr GetContextsDevice(IntPtr context);

        void SuspendContext(IntPtr context);

    }

}
