namespace com.github.zehsteam.LocalMultiplayer.Objects;

internal class JsonSaveValue<T>(JsonSave jsonSave, string key, T defaultValue = default)
{
    public JsonSave JsonSave { get; private set; } = jsonSave;
    public string Key { get; private set; } = key;
    public T DefaultValue { get; private set; } = defaultValue;

    public T Value
    {
        get => Load();
        set => Save(value);
    }

    public bool HasValue => TryLoad(out T _);

    public T Load()
    {
        return JsonSave.Load(Key, DefaultValue);
    }

    public bool TryLoad(out T value)
    {
        return JsonSave.TryLoad(Key, out value);
    }

    public void Save(T value)
    {
        JsonSave.Save(Key, value);
    }
}
