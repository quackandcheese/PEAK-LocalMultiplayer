using BepInEx;
using BepInEx.Configuration;
using com.github.zehsteam.LocalMultiplayer.Patches;
using HarmonyLib;

namespace com.github.zehsteam.LocalMultiplayer;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
internal class Plugin : BaseUnityPlugin
{
    private readonly Harmony _harmony = new Harmony(MyPluginInfo.PLUGIN_GUID);

    internal static Plugin Instance { get; private set; }

    internal static new ConfigFile Config { get; private set; }

    #pragma warning disable IDE0051 // Remove unused private members
    private void Awake()
    #pragma warning restore IDE0051 // Remove unused private members
    {
        Instance = this;

        LocalMultiplayer.Logger.Initialize(BepInEx.Logging.Logger.CreateLogSource(MyPluginInfo.PLUGIN_GUID));
        LocalMultiplayer.Logger.LogInfo($"{MyPluginInfo.PLUGIN_NAME} has awoken!");

        Config = Utils.CreateGlobalConfigFile(this);

        _harmony.PatchAll(typeof(NetworkConnectPatch));
        _harmony.PatchAll(typeof(SteamManagerPatch));
        _harmony.PatchAll(typeof(MenuPageMainPatch));

        ConfigManager.Initialize(Config);
    }
}
