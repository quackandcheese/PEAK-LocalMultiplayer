/*using HarmonyLib;

namespace com.quackandcheese.LocalMultiplayer.Patches;

// dont need to worry about this because you cant change keybinds in PEAK lol

[HarmonyPatch(typeof(InputManager))]
internal static class InputManagerPatch
{
    [HarmonyPatch(nameof(InputManager.SaveDefaultKeyBindings))]
    [HarmonyPrefix]
    private static bool SaveDefaultKeyBindingsPatch()
    {
        if (SteamAccountManager.IsUsingSpoofAccount)
        {
            return false;
        }

        return true;
    }

    [HarmonyPatch(nameof(InputManager.SaveCurrentKeyBindings))]
    [HarmonyPrefix]
    private static bool SaveCurrentKeyBindingsPatch()
    {
        if (SteamAccountManager.IsUsingSpoofAccount)
        {
            return false;
        }

        return true;
    }
}
*/