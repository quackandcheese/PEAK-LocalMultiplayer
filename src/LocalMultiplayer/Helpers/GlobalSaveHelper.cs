using com.github.quackandcheese.LocalMultiplayer.Objects;
using System.Collections.Generic;

namespace com.github.quackandcheese.LocalMultiplayer.Helpers;

internal static class GlobalSaveHelper
{
    public static JsonSave JsonSave {  get; private set; }

    public static JsonSaveValue<ulong> SteamLobbyId { get; private set; }
    public static JsonSaveValue<List<SteamAccount>> SpoofSteamAccounts { get; private set; }
    public static JsonSaveValue<List<SteamAccount>> SpoofSteamAccountsInUse { get; private set; }

    static GlobalSaveHelper()
    {
        JsonSave = new JsonSave(Utils.GetPluginPersistentDataPath(), "GlobalSave");

        SteamLobbyId =            new JsonSaveValue<ulong>(JsonSave,              key: "SteamLobbyId");
        SpoofSteamAccounts =      new JsonSaveValue<List<SteamAccount>>(JsonSave, key: "SpoofSteamAccounts",      defaultValue: []);
        SpoofSteamAccountsInUse = new JsonSaveValue<List<SteamAccount>>(JsonSave, key: "SpoofSteamAccountsInUse", defaultValue: []);
    }

    public static bool KeyExists(string key)
    {
        return JsonSave.KeyExists(key);
    }

    public static T Load<T>(string key, T defaultValue = default)
    {
        return JsonSave.Load(key, defaultValue);
    }

    public static bool TryLoad<T>(string key, out T value)
    {
        return JsonSave.TryLoad(key, out value);
    }

    public static bool Save<T>(string key, T value)
    {
        return JsonSave.Save(key, value);
    }
}
