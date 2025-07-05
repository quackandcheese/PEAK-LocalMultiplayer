using com.quackandcheese.LocalMultiplayer;
using HarmonyLib;
using Zorro.Settings;

namespace com.quackandcheese.LocalMultiplayer.Patches;

[HarmonyPatch(typeof(SettingsHandler))]
internal static class SettingsHandlerPatch
{
    [HarmonyPatch(nameof(SettingsHandler.SaveSetting))]
    [HarmonyPrefix]
    private static bool SaveSettingPatch(ref Setting setting)
    {
        if (!SteamAccountManager.IsUsingSpoofAccount)
        {
            return true;
        }

        return false;
    }

/*    [HarmonyPatch(nameof(CharacterCustomization.SetCustomizationData))]
    [HarmonyPrefix]
    private static bool SetCustomizationDataPatch(ref CharacterCustomizationData customizationData, ref Photon.Realtime.Player player)
    {
        if (!SteamAccountManager.IsUsingSpoofAccount)
        {
            return true;
        }

        SteamAccountManager.SetCurrentSpoofAccountColor(colorID);
        return false;
    }*/

/*    [HarmonyPatch(nameof(DataDirector.ColorGetBody))]
    [HarmonyPrefix]
    private static bool ColorGetBodyPatch(ref int __result)
    {
        if (!SteamAccountManager.IsUsingSpoofAccount)
        {
            return true;
        }

        __result = SteamAccountManager.SpoofAccount.ColorId;
        return false;
    }*/
}
