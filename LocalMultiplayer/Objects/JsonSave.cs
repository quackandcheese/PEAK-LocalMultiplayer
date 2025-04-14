using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Text;

namespace com.github.zehsteam.LocalMultiplayer.Objects;

internal class JsonSave
{
    public string DirectoryPath { get; private set; }
    public string FileName { get; private set; }
    public string FilePath => Path.Combine(DirectoryPath, FileName);

    private JObject _data;

    public JsonSave(string directoryPath, string fileName)
    {
        DirectoryPath = directoryPath;
        FileName = fileName;
        _data = ReadFile();
    }

    public bool KeyExists(string key)
    {
        if (_data == null)
        {
            Logger.LogError($"KeyExists: Data is null. Ensure the save file is properly loaded.");
            return false;
        }

        return _data.ContainsKey(key);
    }

    public T Load<T>(string key, T defaultValue = default)
    {
        if (TryLoad(key, out T value))
        {
            return value;
        }

        return defaultValue;
    }

    public bool TryLoad<T>(string key, out T value)
    {
        _data = ReadFile();

        value = default;

        if (_data == null)
        {
            Logger.LogError($"Load: Data is null. Returning default value for key: {key}.");
            return false;
        }

        if (_data.TryGetValue(key, out JToken jToken))
        {
            try
            {
                value = jToken.ToObject<T>();
                return true;
            }
            catch (JsonException ex)
            {
                Logger.LogError($"Load: JSON Conversion Error for key: {key}. {ex.Message}");
            }
            catch (ArgumentNullException ex)
            {
                Logger.LogError($"Load: Argument Null Error for key: {key}. {ex.Message}");
            }
            catch (Exception ex)
            {
                Logger.LogError($"Load: Unexpected Error for key: {key}. {ex.Message}");
            }

            return false;
        }

        Logger.LogWarning($"Load: Key '{key}' does not exist. Returning default value.", extended: true);
        return false;
    }

    public bool Save<T>(string key, T value)
    {
        _data = ReadFile();

        if (_data == null)
        {
            Logger.LogError($"Save: Data is null. Cannot save key: {key}.");
            return false;
        }

        try
        {
            JToken jToken = JToken.FromObject(value);

            if (_data.ContainsKey(key))
            {
                _data[key] = jToken;
            }
            else
            {
                _data.Add(key, jToken);
            }

            return WriteFile(_data);
        }
        catch (Exception ex)
        {
            Logger.LogError($"Save: Error saving key: {key}. {ex.Message}");
            return false;
        }
    }

    private JObject ReadFile()
    {
        try
        {
            if (!File.Exists(FilePath))
            {
                Logger.LogWarning($"ReadFile: Save file does not exist at \"{FilePath}\". Initializing with an empty file.", extended: true);
                return new JObject();
            }

            using FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read);
            using StreamReader reader = new StreamReader(fs, Encoding.UTF8);

            return JObject.Parse(reader.ReadToEnd());
        }
        catch (JsonException ex)
        {
            Logger.LogError($"ReadFile: JSON Parsing Error for file: \"{FilePath}\". {ex.Message}");
        }
        catch (Exception ex)
        {
            Logger.LogError($"ReadFile: Unexpected Error for file: \"{FilePath}\". {ex.Message}");
        }

        return new JObject();
    }

    private bool WriteFile(JObject data)
    {
        try
        {
            if (!Directory.Exists(DirectoryPath))
            {
                Directory.CreateDirectory(DirectoryPath);
            }

            File.WriteAllText(FilePath, data.ToString(), Encoding.UTF8);

            return true;
        }
        catch (Exception ex)
        {
            Logger.LogError($"WriteFile: Unexpected Error for file: \"{FilePath}\". {ex.Message}");
        }

        return false;
    }
}
