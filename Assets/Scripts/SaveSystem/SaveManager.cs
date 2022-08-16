using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveManager : Singleton<SaveManager>
{
    private string SavePath => $"{Application.persistentDataPath}/save.txt";

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        Load();
    }

    private void OnValidate()
    {
        Load();
    }

    public void Save()
    {
        var state = LoadFile();
        CaptureState(state);
        SaveFile(state);
        Debug.Log("Saved");
    }

    public void Load()
    {
        var state = LoadFile();
        RestoreState(state);
        Debug.Log("Loaded");
    }

    private Dictionary<string, object> LoadFile()
    {
        if (!File.Exists(SavePath))
        {
            return new Dictionary<string, object>();
        }

        using (FileStream stream = File.Open(SavePath, FileMode.Open))
        {
            var formatter = new BinaryFormatter();
            return (Dictionary<string, object>)formatter.Deserialize(stream);
        }
    }

    private void SaveFile(object state)
    {
        using (var stream = File.Open(SavePath, FileMode.Create))
        {
            var formatter = new BinaryFormatter();
            formatter.Serialize(stream, state);
            stream.Close();
        }
    }

    private void CaptureState(Dictionary<string, object> state)
    {
        foreach (var saveable in FindObjectsOfType<SaveableEntities>())
        {
            state[saveable.Id] = saveable.CaptureState();
        }
    }

    private void RestoreState(Dictionary<string, object> state)
    {
        foreach (var saveable in FindObjectsOfType<SaveableEntities>())
        {
            if (state.TryGetValue(saveable.Id, out object value))
            {
                saveable.RestoreState(value);
            }
        }
    }

    private void OnApplicationQuit()
    {
        Save();
    }
}
