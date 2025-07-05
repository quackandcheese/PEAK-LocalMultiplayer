using Photon.Pun;
using Photon.Realtime;

namespace com.github.quackandcheese.LocalMultiplayer.Helpers;

internal static class PhotonNetworkHelper
{
    public static void SetPhotonServerSettings()
    {
        AppSettings appSettings = PhotonNetwork.PhotonServerSettings.AppSettings;
        appSettings.AppIdRealtime = ConfigManager.Photon_AppIdRealtime.Value;
        appSettings.AppIdVoice = ConfigManager.Photon_AppIdVoice.Value;
    }
}
