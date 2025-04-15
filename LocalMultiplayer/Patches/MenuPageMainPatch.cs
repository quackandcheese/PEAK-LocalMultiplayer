using com.github.zehsteam.LocalMultiplayer.Helpers;
using HarmonyLib;
using Steamworks;
using Steamworks.Data;

namespace com.github.zehsteam.LocalMultiplayer.Patches;

[HarmonyPatch(typeof(MenuPageMain))]
internal static class MenuPageMainPatch
{
    [HarmonyPatch(nameof(MenuPageMain.Start))]
    [HarmonyPostfix]
    private static void StartPatch()
    {
        SteamAccountManager.UnassignSpoofAccount();
    }

    [HarmonyPatch(nameof(MenuPageMain.ButtonEventJoinGame))]
    [HarmonyPrefix]
    private static bool ButtonEventJoinGamePatch()
    {
        SteamAccountManager.AssignSpoofAccount();

        PhotonNetworkHelper.SetPhotonServerSettings();

        SteamManager.instance?.OnGameLobbyJoinRequested(new Lobby(GlobalSaveHelper.SteamLobbyId.Value), SteamClient.SteamId);

        return false;
    }
}
