using com.github.quackandcheese.LocalMultiplayer.Helpers;
using HarmonyLib;
using Steamworks;
using Unity.Services.Lobbies.Models;

namespace com.github.quackandcheese.LocalMultiplayer.Patches;

[HarmonyPatch(typeof(MainMenu))]
internal static class MenuPageMainPatch
{
    [HarmonyPatch(nameof(MainMenu.Initialize))]
    [HarmonyPostfix]
    private static void InitializePatch()
    {
        SteamAccountManager.UnassignSpoofAccount();
    }

    [HarmonyPatch(nameof(MainMenu.PlaySoloClicked))]
    [HarmonyPrefix]
    private static bool PlaySoloClickedPatch(ref MainMenu __instance)
    {
        SteamAccountManager.AssignSpoofAccount();

        PhotonNetworkHelper.SetPhotonServerSettings();

/*        GameLobbyJoinRequested_t req = new GameLobbyJoinRequested_t()
        {
            m_steamIDLobby = (CSteamID)GlobalSaveHelper.SteamLobbyId.Value,
            m_steamIDFriend = SteamUser.GetSteamID()
        };
        GameHandler.GetService<SteamLobbyHandler>().OnLobbyJoinRequested(req);*/
        GameHandler.GetService<SteamLobbyHandler>().TryJoinLobby(new CSteamID(GlobalSaveHelper.SteamLobbyId.Value));

        return false;
    }
}
