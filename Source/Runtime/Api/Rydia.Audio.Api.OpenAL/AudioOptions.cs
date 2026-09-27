using System;
using System.Collections.Generic;
using System.Text;
using Rydia.Audio.Api.OpenAL.Alc;

namespace Rydia.Audio.Api.OpenAL
{


    public class AudioOptions
    {

        public string DeviceName
        {
            get;
            set;
        }

        public int Frequency
        {
            get;
            set;
        }

        public int MonoSources
        {
            get;
            set;
        }

        public int StereoSources
        {
            get;
            set;
        }

        public int Refresh
        {
            get;
            set;
        }

        public bool Sync
        {
            get;
            set;
        }

        /// <summary>
        /// 属性リストを取得します
        /// </summary>
        public AlcContextAttributes[] Argument
        {
            get
            {
                List<AlcContextAttributes> extra = new List<AlcContextAttributes>
                {
                    AlcContextAttributes.Frequency, (AlcContextAttributes)Frequency,
                    AlcContextAttributes.MonoSources, (AlcContextAttributes)MonoSources,
                    AlcContextAttributes.StereoSources, (AlcContextAttributes)StereoSources,
                    AlcContextAttributes.Refresh, (AlcContextAttributes)Refresh,
                    AlcContextAttributes.Sync, (AlcContextAttributes)(Sync == true ? 1 : 0)
                };
                return extra.ToArray();
            }
        }

        public AudioOptions()
        {
            // サンプリング周波数を44.1kHzに設定
            Frequency = 44100;
            // モノラル音源の数を16に設定
            MonoSources = 16;
            // ステレオ音源の数を16に設定
            StereoSources = 16;
            // 更新レートを60に設定
            Refresh = 60;
            // クロック同期を無効に設定
            Sync = false;

            DeviceName = null;
        }

    }

}
