using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using com.quackandcheese.LocalMultiplayer.Patches;
using HarmonyLib;

namespace com.quackandcheese.LocalMultiplayer;

[BepInAutoPlugin]
public partial class Plugin : BaseUnityPlugin
{
    internal static ManualLogSource Log { get; private set; } = null!;
    internal static Plugin Instance { get; private set; }
    internal static new ConfigFile Config { get; private set; }
    internal static Harmony? Harmony { get; set; }

    private void Awake()
    {
        Instance = this;

        Log = Logger;

        Config = Utils.CreateGlobalConfigFile(this);

        Patch();

        ConfigManager.Initialize(Config);

        Log.LogInfo($"Plugin {Name} is loaded!");
    }
    internal void Patch()
    {
        Harmony ??= new Harmony(Info.Metadata.GUID);

        Log.LogDebug("Patching...");

        Harmony.PatchAll();

        Log.LogDebug("Finished patching!");
    }
}
