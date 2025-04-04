using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AppRoot : MonoBehaviour
{
    static class BootConstants
    {
        public const string PATH_TO_BOOT = "Prefabs/Bootstrap/AppController";
        public const string PATH_TO_UI_MANAGER = "Prefabs/UI/UiWindowManager";
        public const string CLONE_LITERAL = "(Clone)";
        public const string MAIN_MENU_SCENE_NAME = "MainMenu";
    }

    private static UIBundleSystem _uiBundleSystem;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void Init()
    {
        CreateAppRoot();
        CreateUIManager();
    }

    private static async void CreateUIManager()
    {
        GameObject canvas = Instantiate(Resources.Load(BootConstants.PATH_TO_UI_MANAGER)) as GameObject;
        canvas.name = canvas.name.Replace(BootConstants.CLONE_LITERAL, string.Empty);
        await LoadUiResources();
        var manager = CreateUIManager(canvas, _uiBundleSystem);
            
        if (SceneManager.GetActiveScene().name == BootConstants.MAIN_MENU_SCENE_NAME)
        {
            manager.Show<MainMenu, MainMenuConfig>();
        }
    }

    private  static async UniTask  LoadUiResources()
    {
        Dictionary<string, UIResource> resourceMap = await Loader.LoadUI<Dictionary<string, UIResource>>();
        _uiBundleSystem = new UIBundleSystem(resourceMap);
        foreach (UIResource resource in resourceMap.Values)
        {
            resource.LoadAsset();
        }
    }

    private static UIManager CreateUIManager(GameObject canvas, UIBundleSystem uiBundleSystem)
    {
        UIManager manager = canvas.GetComponent<UIManager>();
        manager.Initialize(uiBundleSystem);
        return manager;
    }

    private static void CreateAppRoot()
    {
        var appRoot = Instantiate(Resources.Load(BootConstants.PATH_TO_BOOT)) as GameObject;
        if (appRoot != null)
        {
            appRoot.name = appRoot.name.Replace(BootConstants.CLONE_LITERAL, string.Empty);
            DontDestroyOnLoad(appRoot);
        }
    }
}