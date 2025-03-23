using com.github.zehsteam.LocalMultiplayer.Objects;

namespace com.github.zehsteam.LocalMultiplayer.Helpers;

internal static class GlobalSaveHelper
{
    private static JsonSave _jsonSave;

    static GlobalSaveHelper()
    {
        _jsonSave = new JsonSave(Utils.GetPluginPersistentDataPath(), "GlobalSave");
    }

    public static bool KeyExists(string key)
    {
        return _jsonSave.KeyExists(key);
    }

    public static T Load<T>(string key, T defaultValue = default)
    {
        return _jsonSave.Load(key, defaultValue);
    }

    public static bool TryLoad<T>(string key, out T value)
    {
        return _jsonSave.TryLoad(key, out value);
    }

    public static bool Save<T>(string key, T value)
    {
        return _jsonSave.Save(key, value);
    }
}
