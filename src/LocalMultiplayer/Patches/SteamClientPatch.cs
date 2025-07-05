using com.github.quackandcheese.LocalMultiplayer;
using HarmonyLib;
using Steamworks;

namespace com.github.quackandcheese.LocalMultiplayer.Patches;

[HarmonyPatch]
internal static class SteamClientPatch
{
    [HarmonyPatch(typeof(SteamFriends), nameof(SteamFriends.GetPersonaName))]
    [HarmonyPrefix]
    private static bool GetPersonaNamePatch(ref string __result)
    {
        if (!SteamAccountManager.IsUsingSpoofAccount)
        {
            return true;
        }

        __result = SteamAccountManager.SpoofAccount.Username;
        return false;
    }

    [HarmonyPatch(typeof(SteamUser), nameof(SteamUser.GetSteamID))]
    [HarmonyPrefix]
    private static bool GetSteamIDPatch(ref CSteamID __result)
    {
        if (!SteamAccountManager.IsUsingSpoofAccount)
        {
            return true;
        }

        __result = new CSteamID(SteamAccountManager.SpoofAccount.SteamId);
        return false;
    }
}
