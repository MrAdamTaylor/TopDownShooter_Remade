using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public interface IGameRepository
{
    T GetData<T>();
    bool TryGetData<T>(out T data);
    void SetData<T>(T data);
}

public class GameRepository : IGameRepository
{
    public const string KEY_SAVE = "GameState";
    private Dictionary<string, string> _gameState = new(); 
    
    public T GetData<T>()
    {
        var keyName = typeof(T).Name;
        var serializedData = _gameState[keyName];
        var data = JsonConvert.DeserializeObject<T>(serializedData);
        return data;
    }

    public bool TryGetData<T>(out T data)
    {
        var keyName = typeof(T).Name;
        if (_gameState.TryGetValue(keyName, out var serializedData))
        {
            data = JsonConvert.DeserializeObject<T>(serializedData);
            return true;
        }
        
        data = default;
        return false;
        
    }

    public void SetData<T>(T data)
    {
        var keyName = typeof(T).Name;
        var serializeData = JsonConvert.SerializeObject(data);
        _gameState[keyName] = serializeData;
    }

    public void LoadState()
    {
        if (PlayerPrefs.HasKey(KEY_SAVE)) ;
        {
            var data = PlayerPrefs.GetString(KEY_SAVE);
            _gameState = JsonConvert.DeserializeObject<Dictionary<string, string>>(data);
        }
    }

    public void SaveState()
    {
        var data = JsonConvert.SerializeObject(_gameState);
        PlayerPrefs.SetString(KEY_SAVE, data);
    }
}
