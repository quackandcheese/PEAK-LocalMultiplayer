using HarmonyLib;
using Photon.Pun;
using Steamworks;

namespace com.github.zehsteam.LocalMultiplayer.Patches;

[HarmonyPatch(typeof(PlayerAvatar))]
internal static class PlayerAvatarPatch
{
    [HarmonyPatch(nameof(PlayerAvatar.AddToStatsManager))]
    [HarmonyPrefix]
    private static void AddToStatsManagerPatch()
    {
        PhotonNetwork.NickName = SteamClient.Name;
    }
}
