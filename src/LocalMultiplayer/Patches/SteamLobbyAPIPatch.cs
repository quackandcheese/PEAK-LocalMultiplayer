using HarmonyLib;
using Peak.Network;

namespace LocalMultiplayer.Patches;

[HarmonyPatch(typeof(SteamLobbyAPI))]
internal static class SteamLobbyAPIPatch
{
    [HarmonyPatch(nameof(SteamLobbyAPI.PlayerIsInLobby))]
    [HarmonyPrefix]
    private static bool PlayerIsInLobbyPatch(ref bool __result)
    {
        __result = true;
        return false;
    }
}
