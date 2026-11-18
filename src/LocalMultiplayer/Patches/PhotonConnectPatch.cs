using com.quackandcheese.LocalMultiplayer.Helpers;
using HarmonyLib;
using Photon.Pun;
using Photon.Realtime;

namespace com.quackandcheese.LocalMultiplayer.Patches;

// Since PEAK's console/crossplay update, Portningsbolaget.Photon.SteamPhotonAuthManager logs in with
// AuthType=Steam + a Steam ticket, and NetworkingUtilities.LoadUserID is only used for on-prem builds.
// A self-made Photon app has no Steam auth provider ("Authentication type not supported (none configured)"),
// so right before every connect we point Photon at the configured App IDs and switch to AuthType None with
// a per-instance UserId (real SteamID for the host, spoof SteamID for the joining instance).
[HarmonyPatch]
internal static class PhotonConnectPatch
{
    [HarmonyPatch(typeof(PhotonNetwork), nameof(PhotonNetwork.ConnectUsingSettings), typeof(AppSettings), typeof(bool))]
    [HarmonyPrefix]
    private static void ConnectUsingSettingsPrefix(AppSettings appSettings)
    {
        if (!PhotonNetworkHelper.HasCustomAppIds)
        {
            return;
        }

        PhotonNetworkHelper.SetPhotonServerSettings();
        if (appSettings != null)
        {
            appSettings.AppIdRealtime = ConfigManager.Photon_AppIdRealtime.Value;
            appSettings.AppIdVoice = ConfigManager.Photon_AppIdVoice.Value;
        }

        PhotonNetworkHelper.ApplyLocalAuthValues("ConnectUsingSettings");
    }

    [HarmonyPatch(typeof(PhotonNetwork), nameof(PhotonNetwork.ConnectToRegion))]
    [HarmonyPrefix]
    private static void ConnectToRegionPrefix(string region)
    {
        if (!PhotonNetworkHelper.HasCustomAppIds)
        {
            return;
        }

        PhotonNetworkHelper.SetPhotonServerSettings();
        PhotonNetworkHelper.ApplyLocalAuthValues($"ConnectToRegion({region})");
    }
}
