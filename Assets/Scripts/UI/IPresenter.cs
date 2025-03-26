using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public interface IPresenter
{
    
}

public interface IMainMenuPresenter : IPresenter
{
    Sprite BackgroundImage { get;}
    void LaunchGame();
    void ShowAuthors();
}

public class MainMenuPresenter : IMainMenuPresenter
{
    private Sprite _backgroundImage;
    public Sprite BackgroundImage => _backgroundImage;


    private UIManager _uiManager;
    
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
        GameApp gameApp = new GameApp();
    }

    public void ShowAuthors()
    {
        _uiManager.Show<AuthorsMenu, GameAuthorsConfig>();
    }
}

public class GameApp
{
    public GameApp()
    {
        Debug.Log("Game App Created");
    }
}