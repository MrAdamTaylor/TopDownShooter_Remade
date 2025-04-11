using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChecker : MonoBehaviour
{
    
    
    
    UIManager _uiManager;
    
    public void Construct(UIManager uiManager)
    {
        _uiManager = uiManager;
        CheckMainMenu(SceneManager.GetActiveScene());
        
    }
    
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded; // Подписка на событие загрузки сцены
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // Отписка при отключении компонента
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        CheckMainMenu(scene);
    }

    private void CheckMainMenu(Scene scene)
    {
        if (scene.name == Common_App_Root_Constants.MAIN_MENU_SCENE_NAME && _uiManager != null)
        {
            _uiManager.Show<MainMenu, MainMenuConfig>();
        }
    }
}

public static class Common_App_Root_Constants
{
    public const string MAIN_MENU_SCENE_NAME = "MainMenu";
}