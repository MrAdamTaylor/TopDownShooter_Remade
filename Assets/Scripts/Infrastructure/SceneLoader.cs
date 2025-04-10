using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader
{
    private const string STANDART_SCENES_PATH = "Scenes/";
    
    private string _nextSceneName;
    private List<Action> _preloadActions;

    public SceneLoader(List<Action> actions)
    {
        _preloadActions = actions;
    }

    public async UniTaskVoid LoadByConfig(LevelConfig levelConfig)
    {
        _nextSceneName = levelConfig.SceneName;
        await LoadOpeariont();
    }

    private async Task LoadOpeariont()
    {
        ExecutePreloadActions();
        await Resources.UnloadUnusedAssets();
        var op = SceneManager.LoadSceneAsync(STANDART_SCENES_PATH+_nextSceneName);
        await op.ToUniTask();
    }

    private void ExecutePreloadActions()
    {
        for (int i = 0; i < _preloadActions.Count; i++)
        {
            _preloadActions[i].Invoke();
        }
    }

    public async UniTaskVoid LoadByName(string payload)
    {
        _nextSceneName = payload;
        await LoadOpeariont();
    }
}