using HarmonyLib;

namespace com.github.zehsteam.LocalMultiplayer.Patches;

[HarmonyPatch(typeof(InputManager))]
internal static class InputManagerPatch
{
    [HarmonyPatch(nameof(InputManager.SaveDefaultKeyBindings))]
    [HarmonyPrefix]
    private static bool SaveDefaultKeyBindingsPatch()
    {
        if (!SteamAccountManager.IsUsingSpoofAccount)
        {
            return true;
        }

        return false;
    }

    [HarmonyPatch(nameof(InputManager.SaveCurrentKeyBindings))]
    [HarmonyPrefix]
    private static bool SaveCurrentKeyBindingsPatch()
    {
        if (!SteamAccountManager.IsUsingSpoofAccount)
        {
            return true;
        }

        return false;
    }
}
