using com.github.zehsteam.LocalMultiplayer.Helpers;
using HarmonyLib;

namespace com.github.zehsteam.LocalMultiplayer.Patches;

[HarmonyPatch(typeof(NetworkConnect))]
internal static class NetworkConnectPatch
{
    [HarmonyPatch(nameof(NetworkConnect.Start))]
    [HarmonyPostfix]
    private static void StartPatch()
    {
        PhotonNetworkHelper.SetPhotonServerSettings();
    }
}
