using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Text;
using System.Threading;
using UnityEngine;

namespace com.quackandcheese.LocalMultiplayer.Objects;

internal class JsonSave : IDisposable
{
    public string DirectoryPath { get; private set; }
    public string FileName { get; private set; }
    public string FilePath => Path.Combine(DirectoryPath, FileName);

    private JObject _data;
    private readonly Mutex _mutex;

    private const int _mutexTimeoutMs = 5000;
    private bool _disposed;

    public JsonSave(string directoryPath, string fileName)
    {
        DirectoryPath = directoryPath;
        FileName = fileName;
        
        string mutexName = $"Global\\JsonSave_{fileName.Replace(Path.DirectorySeparatorChar, '_')}";
        _mutex = new Mutex(false, mutexName);

        RefreshData();

        Application.quitting += Dispose;
    }

    public bool KeyExists(string key)
    {
        RefreshData();
        return _data?.ContainsKey(key) ?? false;
    }

    public T Load<T>(string key, T defaultValue = default)
    {
        return TryLoad<T>(key, out var value) ? value : defaultValue;
    }

    public bool TryLoad<T>(string key, out T value)
    {
        value = default;
        RefreshData();

        if (_data == null)
        {
            Plugin.Log.LogError($"TryLoad: Data is null. Key: {key}");
            return false;
        }

        if (_data.TryGetValue(key, out JToken token))
        {
            try
            {
                value = token.ToObject<T>();
                return true;
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"TryLoad: Failed to deserialize key '{key}'. {ex.Message}");
            }
        }

        return false;
    }

    public bool Save<T>(string key, T value)
    {
        bool hasHandle = false;

        try
        {
            hasHandle = _mutex.WaitOne(_mutexTimeoutMs);

            if (!hasHandle)
            {
                Plugin.Log.LogWarning("Save: Could not acquire mutex.");
                return false;
            }

            RefreshData();

            if (_data == null)
            {
                _data = [];
            }

            _data[key] = JToken.FromObject(value);

            return WriteFile(_data);
        }
        catch (Exception ex)
        {
            Plugin.Log.LogError($"Save: Error saving key '{key}'. {ex.Message}");
            return false;
        }
        finally
        {
            if (hasHandle)
            {
                _mutex.ReleaseMutex();
            }
        }
    }

    private JObject ReadFile()
    {
        try
        {
            if (!File.Exists(FilePath))
            {
                Plugin.Log.LogWarning($"ReadFile: Save file not found at \"{FilePath}\". Creating new.");
                return [];
            }

            using var fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            using var reader = new StreamReader(fs, Encoding.UTF8);

            return JObject.Parse(reader.ReadToEnd());
        }
        catch (Exception ex)
        {
            Plugin.Log.LogError($"ReadFile: Failed to read file \"{FilePath}\". {ex.Message}");
            return [];
        }
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
            Plugin.Log.LogError($"WriteFile: Failed to write file \"{FilePath}\". {ex.Message}");
            return false;
        }
    }

    private void RefreshData()
    {
        _data = ReadFile();

        if (_data == null)
        {
            Plugin.Log.LogError("RefreshData: Data is null. Creating new.");
            _data = [];
        }
    }

    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        _mutex?.Dispose();
        _disposed = true;
    }
}
