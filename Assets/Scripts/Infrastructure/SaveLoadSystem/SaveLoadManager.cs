using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    private ISaveLoader _saveLoader;
    private GameContext _gameContext;

    private void Awake()
    {
        _saveLoader = new ScoresSaveLoader();
        _gameContext = new GameContext();
    }


    public void LoadGame()
    {
        if (_saveLoader != null)
        {
            _saveLoader.LoadGame(_gameContext);
        }
    }

    public void SaveGame()
    {
        if (_saveLoader != null)
        {
            _saveLoader.SaveGame(_gameContext);
        }
    }

}
