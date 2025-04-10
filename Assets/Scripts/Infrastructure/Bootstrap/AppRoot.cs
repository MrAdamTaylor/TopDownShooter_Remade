using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AppRoot : MonoBehaviour
{
    static class BootConstants
    {
        public const string PATH_TO_BOOT = "Prefabs/Bootstrap/AppController";
        public const string PATH_TO_UI_MANAGER = "Prefabs/UI/UiWindowManager";
        public const string CLONE_LITERAL = "(Clone)";
        
    }

    public static SceneLoader SceneLoader;
    
    private static List<Action> _actions = new();
    
    private static UIBundleSystem _uiBundleSystem;
    private static GameStateMachine _gameStateMachine;
    private static SceneChecker _sceneChecker;
    
    
    public static void AddGameApp(GameStateMachine gameStateMachine)
    {
        _gameStateMachine = gameStateMachine;
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Init()
    {
        CreateAppRoot();
        CreateUIManager();
        CreateSceneLoader();
        Debug.Log($"<color=cyan>LastAppRootCommand</color>");
    }

    private static void CreateSceneLoader()
    {
        SceneLoader = new SceneLoader(_actions);
    }

    private static async void CreateUIManager()
    {
        GameObject canvas = Instantiate(Resources.Load(BootConstants.PATH_TO_UI_MANAGER)) as GameObject;
        canvas.name = canvas.name.Replace(BootConstants.CLONE_LITERAL, string.Empty);
        await LoadUiResources();
        var manager = CreateUIManager(canvas, _uiBundleSystem);
        _sceneChecker.Construct(manager);
        _actions.Add(() => manager.ClearAllUI());
        
            
        /*if (SceneManager.GetActiveScene().name == BootConstants.MAIN_MENU_SCENE_NAME)
        {
            manager.Show<MainMenu, MainMenuConfig>();
        }*/
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
            _sceneChecker = appRoot.AddComponent<SceneChecker>();
            DontDestroyOnLoad(appRoot);
        }
    }
    
    
    [MenuItem("Tools/LoadMainMenu")]
    private static void LoadMainMenu()
    {
        _gameStateMachine.Enter<LoadLevelState, string>("MainMenu");
    }


}

public class SceneChecker : MonoBehaviour
{
    
    private const string MAIN_MENU_SCENE_NAME = "MainMenu";
    
    UIManager _uiManager;
    
    public void Construct(UIManager uiManager)
    {
        _uiManager = uiManager;
        _uiManager.Show<MainMenu,MainMenuConfig>();
        
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
        if (scene.name == MAIN_MENU_SCENE_NAME && _uiManager != null)
        {
            _uiManager.Show<MainMenu, MainMenuConfig>();
        }
    }
    
    /*void Start()
    {
        if (SceneManager.GetActiveScene().name == MAIN_MENU_SCENE_NAME && _uiManager != null)
        {
            _uiManager.Show<MainMenu, MainMenuConfig>();
        }
        
        Debug.Log($"<color=cyan>Scene Checker Started</color>");
    }*/
}