using com.github.zehsteam.LocalMultiplayer.Helpers;
using HarmonyLib;
using Steamworks;
using Steamworks.Data;

namespace com.github.zehsteam.LocalMultiplayer.Patches;

[HarmonyPatch(typeof(MenuPageMain))]
internal static class MenuPageMainPatch
{
    [HarmonyPatch(nameof(MenuPageMain.ButtonEventJoinGame))]
    [HarmonyPrefix]
    private static bool ButtonEventJoinGamePatch()
    {
        PhotonNetworkHelper.SetPhotonServerSettings();

        if (TryGetSteamLobbyId(out ulong steamLobbyId))
        {
            SteamManager.instance?.OnGameLobbyJoinRequested(new Lobby(steamLobbyId), SteamClient.SteamId);
        }

        return false;
    }

    private static bool TryGetSteamLobbyId(out ulong steamLobbyId)
    {
        steamLobbyId = 0;

        if (!GlobalSaveHelper.TryLoad("SteamLobbyId", out string value))
        {
            return false;
        }

        if (!ulong.TryParse(value, out ulong result))
        {
            return false;
        }

        steamLobbyId = result;
        return true;
    }
}
