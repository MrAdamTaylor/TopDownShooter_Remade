using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    private ISaveLoader _saveLoader;
    private GameContext _gameContext;
    private GameRepository _gameRepository;

    private void Awake()
    {
        _saveLoader = new ScoresSaveLoader();
        _gameContext = new GameContext();
        _gameRepository = new GameRepository();
    }


    public void LoadGame()
    {
        if (_saveLoader != null && _gameRepository != null && _gameContext != null)
        {
            _saveLoader.LoadGame(_gameRepository, _gameContext);
        }
    }

    public void SaveGame()
    {
        if (_saveLoader != null && _gameRepository != null && _gameContext != null)
        {
            _saveLoader.SaveGame(_gameRepository, _gameContext);
        }
    }

}
