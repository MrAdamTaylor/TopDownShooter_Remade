using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MainMenuPresenter : IMainMenuPresenter
{
    private Sprite _backgroundImage;
    public Sprite BackgroundImage => _backgroundImage;


    private UIManager _uiManager;
    private GameApp _gameApp;
    
    public MainMenuPresenter(UIManager uiManager, List<UiConfig> configs)
    {
        _uiManager = uiManager;
        MainMenuConfig menuConfig;
        if (configs.Any(x => x is not MainMenuConfig))
        {
            throw new Exception($"Config does not match type {typeof(MainMenuConfig).Name}");
        }
        else
        {
            menuConfig = (MainMenuConfig)configs.First();
        }

        _backgroundImage = menuConfig._backgroundImage;
    }

    public void LaunchGame()
    {
        _uiManager.Show<TestMenu, TestMenuConfig>();
        if (_gameApp == null)
        {
            _gameApp = new GameApp();
        }

        _gameApp.StartGame();
    }

    public void ShowAuthors()
    {
        _uiManager.Show<AuthorsMenu, GameAuthorsConfig>();
    }
}

[CreateAssetMenu(fileName = "TestMenuConfig", menuName = "UiConfig/TestMenu")]
public class TestMenuConfig : UiConfig
{
    
}