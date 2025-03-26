using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour, IPresentableUiWindow<IPresenter>
{
    [SerializeField] private  Image _backgroundImage;
    
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _showAuthorsButton;

    private IMainMenuPresenter _presenter;
    
    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void SetUpPresenter(IPresenter presenter)
    {
        if (presenter is not IMainMenuPresenter mainMenuPresenter)
        {
            throw new InvalidDataException($"{nameof(mainMenuPresenter)} must be {nameof(IMainMenuPresenter)}");
        }

        _presenter = mainMenuPresenter;
        _backgroundImage.sprite = mainMenuPresenter.BackgroundImage;
        _startButton.onClick.AddListener(LaunchGame);
        _showAuthorsButton.onClick.AddListener(ShowAuthors);
    }


    private void LaunchGame()
    {
        _presenter.LaunchGame();
    }

    private void ShowAuthors()
    {
        _presenter.ShowAuthors();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
        _startButton.onClick.RemoveListener(LaunchGame);
        _showAuthorsButton.onClick.RemoveListener(ShowAuthors);
        
    }
}