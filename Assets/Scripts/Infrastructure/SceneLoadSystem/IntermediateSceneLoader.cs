using UnityEngine;

public class IntermediateSceneLoader : MonoBehaviour
{
    [SerializeField] private SceneLoadPopup _sceneLoadPopup;
    private static AppRoot _appRoot;
    private static SceneLoader _sceneLoader;
    
    private void Awake()
    {
        // если уже готов — сразу забираем
        if (AppRoot.SceneLoader != null)
        {
            _sceneLoader = AppRoot.SceneLoader;
            OnSceneLoaderReady();
        }
        else
        {
            // если нет — подписываемся
            AppRoot.OnSceneLoaderCreated += HandleSceneLoaderReady;
        }
    }

    private void OnDestroy()
    {
        AppRoot.OnSceneLoaderCreated -= HandleSceneLoaderReady;
    }

    private void HandleSceneLoaderReady(SceneLoader loader)
    {
        _sceneLoader = loader;
        OnSceneLoaderReady();
    }

    private void OnSceneLoaderReady()
    {
        Debug.Log("SceneLoader готов, можно использовать");
        if(_sceneLoader.SceneName == null)
            _sceneLoader.LoadByName(Common_App_Root_Constants.MAIN_MENU_SCENE_NAME);
        else
        {
            _sceneLoader.LoadOpeariont();
        }
    }
}
