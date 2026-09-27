using Android.Runtime;

namespace Rydia.Runtime.Droid.App
{
    [Activity(Label = "@string/app_name", MainLauncher = true)]
    public class MainActivity : GLActivity
    {

        public MainActivity()
            : base(new E23ALSinewaveTestApp())
        {
            // 既定のデバイスを使用
            AudioOptions.DeviceName = null;
            // サンプリング周波数を44.1kHzに設定
            AudioOptions.Frequency = 44100;
            // モノラル音源の数を16に設定
            AudioOptions.MonoSources = 16;
            // ステレオ音源の数を16に設定
            AudioOptions.StereoSources = 16;
            // 更新レートを60に設定
            AudioOptions.Refresh = 60;
            // クロック同期を無効に設定
            AudioOptions.Sync = false;
        }
    }
}