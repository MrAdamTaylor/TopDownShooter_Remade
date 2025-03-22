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
    
    public MainMenuPresenter(UIManager uiManager)
    {
        _uiManager = uiManager;
    }

    public void LaunchGame()
    {
        throw new System.NotImplementedException();
    }

    public void ShowAuthors()
    {
        _uiManager.Show<AuthorsMenu, GameAuthorsConfig>();
    }
}