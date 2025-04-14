using HarmonyLib;

namespace com.github.zehsteam.LocalMultiplayer.Patches;

[HarmonyPatch(typeof(DataDirector))]
internal static class DataDirectorPatch
{
    [HarmonyPatch(nameof(DataDirector.ColorSetBody))]
    [HarmonyPrefix]
    private static bool ColorSetBodyPatch(int colorID)
    {
        if (!SteamAccountManager.UseSpoofAccount || SteamAccountManager.SpoofAccount == default)
        {
            return true;
        }

        SteamAccountManager.SetSpoofAccountColor(colorID);
        return false;
    }

    [HarmonyPatch(nameof(DataDirector.ColorGetBody))]
    [HarmonyPrefix]
    private static bool ColorGetBodyPatch(ref int __result)
    {
        if (!SteamAccountManager.UseSpoofAccount || SteamAccountManager.SpoofAccount == default)
        {
            return true;
        }

        __result = SteamAccountManager.SpoofAccount.ColorId;
        return false;
    }
}
