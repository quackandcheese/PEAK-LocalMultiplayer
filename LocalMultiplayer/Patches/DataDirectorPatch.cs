using HarmonyLib;

namespace com.github.zehsteam.LocalMultiplayer.Patches;

[HarmonyPatch(typeof(DataDirector))]
internal static class DataDirectorPatch
{
    [HarmonyPatch(nameof(DataDirector.SaveSettings))]
    [HarmonyPrefix]
    private static bool SaveSettingsPatch()
    {
        if (!SteamAccountManager.IsUsingSpoofAccount)
        {
            return true;
        }

        return false;
    }

    [HarmonyPatch(nameof(DataDirector.ColorSetBody))]
    [HarmonyPrefix]
    private static bool ColorSetBodyPatch(int colorID)
    {
        if (!SteamAccountManager.IsUsingSpoofAccount)
        {
            return true;
        }

        SteamAccountManager.SetCurrentSpoofAccountColor(colorID);
        return false;
    }

    [HarmonyPatch(nameof(DataDirector.ColorGetBody))]
    [HarmonyPrefix]
    private static bool ColorGetBodyPatch(ref int __result)
    {
        if (!SteamAccountManager.IsUsingSpoofAccount)
        {
            return true;
        }

        __result = SteamAccountManager.SpoofAccount.ColorId;
        return false;
    }
}
