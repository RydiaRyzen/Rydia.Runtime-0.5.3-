using Rydia.Audio.Api.OpenAL;
using System;
using System.Runtime.InteropServices;

namespace Rydia.Audio.Api.OpenAL
{
    public interface IAL
    {

        string GetString(ALGetString param);

        string GetErrorString(ALError param);

        int[] GenBuffers(int n);

        int GenBuffer();

        void DeleteBuffers(int[] buffers);

        void DeleteBuffer(int buffer);

        void BufferData(int bid, ALFormat format, byte[] buffer, int freq);

        void BufferData(int bid, ALFormat format, IntPtr buffer, int size, int freq);

        int[] GenSources(int n);

        int GenSource();

        void GetSource(int sid, ALGetSourcei param, out int value);

        void GetSource(int sid, ALSourceb param, out bool value);

        void GetSource(int sid, ALSource3f param, out float value1, out float value2, out float value3);

        void GetSource(int sid, ALSourcef param, out float value);

        void Source(int sid, ALSource3i param, int value1, int value2, int value3);

        void Source(int sid, ALSourcei param, int value);

        void Source(int sid, ALSourceb param, bool value);

        void Source(int sid, ALSource3f param, float value1, float value2, float value3);

        void Source(int sid, ALSourcef param, float value);

        void SourcePlay(int[] sids);

        void SourcePlay(int sid);

        void SourcePause(int[] sids);

        void SourcePause(int sid);

        void SourceStop(int[] sids);

        void SourceStop(int sid);

        void SourceQueueBuffers(int sid, int[] bids);

        void SourceQueueBuffer(int source, int buffer);

        void SourceUnqueueBuffers(int sid, int[] bids);

        int SourceUnqueueBuffer(int sid);

        int[] SourceUnqueueBuffers(int sid, int numEntries);

        void SourceRewind(int[] sids);

        void SourceRewind(int sid);

        void DeleteSources(int[] sources);

        void DeleteSource(int sid);

        void GetListener(ALListenerfv param, float[] values);

        void GetListener(ALListenerf param, out float value);

        void GetListener(ALListener3f param, out float value1, out float value2, out float value3);

        void Listener(ALListenerf param, float value);

        void Listener(ALListener3f param, float value1, float value2, float value3);

        void Listener(ALListenerfv param, float[] values);

        int GetInteger(ALGetInteger param);

        float GetFloat(ALGetFloat param);

        ALError GetError();

        bool IsExtensionPresent([In] string extname);

        IntPtr GetProcAddress([In] string fname);

        int GetEnumValue([In] string ename);

        void GetBuffer(int bid, ALGetBufferi param, out int value);

        bool IsSource(int sid);

        bool IsBuffer(int bid);

        void DopplerFactor(float value);

        void SpeedOfSound(float value);

        void DistanceModel(ALDistanceModel distancemodel);

    }
}
