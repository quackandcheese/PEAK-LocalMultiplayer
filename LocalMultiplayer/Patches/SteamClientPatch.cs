using HarmonyLib;
using Steamworks;

namespace com.github.zehsteam.LocalMultiplayer.Patches;

internal static class SteamClientPatch
{
    public static void PatchAll(Harmony harmony)
    {
        if (harmony == null)
        {
            Logger.LogError($"SteamClientPatch: Failed to apply patches. Harmony is null.");
            return;
        }

        ApplyNamePatch(harmony);
        ApplySteamIdPatch(harmony);
    }

    private static void ApplyNamePatch(Harmony harmony)
    {
        var originalMethod = AccessTools.PropertyGetter(typeof(SteamClient), nameof(SteamClient.Name));
        var replacementMethod = AccessTools.Method(typeof(SteamClientPatch), nameof(NamePatch));

        if (originalMethod == null || replacementMethod == null)
        {
            Logger.LogError($"SteamClientPatch: Failed to apply {nameof(SteamClient.Name)} patch. Required methods not found.");
            return;
        }

        harmony.Patch(originalMethod, prefix: new HarmonyMethod(replacementMethod));
    }

    private static void ApplySteamIdPatch(Harmony harmony)
    {
        var originalMethod = AccessTools.PropertyGetter(typeof(SteamClient), nameof(SteamClient.SteamId));
        var replacementMethod = AccessTools.Method(typeof(SteamClientPatch), nameof(SteamIdPatch));

        if (originalMethod == null || replacementMethod == null)
        {
            Logger.LogError($"SteamClientPatch: Failed to apply {nameof(SteamClient.SteamId)} patch. Required methods not found.");
            return;
        }

        harmony.Patch(originalMethod, prefix: new HarmonyMethod(replacementMethod));
    }

    private static bool NamePatch(ref string __result)
    {
        if (!SteamAccountManager.IsUsingSpoofAccount)
        {
            return true;
        }

        __result = SteamAccountManager.SpoofAccount.Username;
        return false;
    }

    private static bool SteamIdPatch(ref SteamId __result)
    {
        if (!SteamAccountManager.IsUsingSpoofAccount)
        {
            return true;
        }

        __result = SteamAccountManager.SpoofAccount.SteamId;
        return false;
    }
}
