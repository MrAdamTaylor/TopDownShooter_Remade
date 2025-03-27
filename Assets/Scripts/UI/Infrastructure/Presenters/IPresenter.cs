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