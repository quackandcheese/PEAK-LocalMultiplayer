using BepInEx.Configuration;

namespace com.github.quackandcheese.LocalMultiplayer;

internal static class ConfigManager
{
    public static ConfigFile ConfigFile { get; private set; }

    // General
    public static ConfigEntry<bool> ExtendedLogging { get; private set; }

    // Photon
    public static ConfigEntry<string> Photon_AppIdRealtime { get; private set; }
    public static ConfigEntry<string> Photon_AppIdVoice { get; private set; }

    public static void Initialize(ConfigFile configFile)
    {
        ConfigFile = configFile;
        BindConfigs();
    }

    private static void BindConfigs()
    {
        // General
        ExtendedLogging = ConfigFile.Bind("General", "ExtendedLogging", defaultValue: false, "Enable extended logging.");

        // Photon
        Photon_AppIdRealtime = ConfigFile.Bind("Photon", "AppIdRealtime", defaultValue: "", "The App ID of your Photon Pun application.");
        Photon_AppIdVoice =    ConfigFile.Bind("Photon", "AppIdVoice",    defaultValue: "", "The App ID of your Photon Voice application.");
    }
}
