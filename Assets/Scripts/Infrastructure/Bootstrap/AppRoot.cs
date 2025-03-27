using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AppRoot : MonoBehaviour
{
    static class BootConstants
    {
        public const string PATH_TO_BOOT = "Prefabs/Bootstrap/AppController";
        public const string PATH_TO_UI_MANAGER = "Prefabs/UI/UiWindowManager";
        public const string RESOURCE_MAP_PATH = "ResourcesMapUI";
        public const string CLONE_LITERAL = "(Clone)";
        public const string CANVAS_NAME = "Canvas";
        public const string MAIN_MENU_SCENE_NAME = "MainMenu";
        public const string MAIN_MENU_PREFAB_NAME = "MainMenu";
    }


    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void Init()
    {
        CreateAppRoot();
        CreateUIManager();
    }

    private static void CreateUIManager()
    {
        GameObject canvas = Instantiate(Resources.Load(BootConstants.PATH_TO_UI_MANAGER)) as GameObject;
        canvas.name = canvas.name.Replace(BootConstants.CLONE_LITERAL, string.Empty);
        TextAsset jsonFile = Resources.Load<TextAsset>(BootConstants.RESOURCE_MAP_PATH);
        if (jsonFile == null)
        {
            throw new Exception("UI Resource Map not found in Resources!");
            return;
        }
        
        var resourceMap = LoadUiResources(jsonFile);
        UIBundleSystem uiBundleSystem = new UIBundleSystem(resourceMap);
        var manager = CreateUIManager(canvas, uiBundleSystem);
            
        if (SceneManager.GetActiveScene().name == BootConstants.MAIN_MENU_SCENE_NAME)
        {
            manager.Show<MainMenu, MainMenuConfig>();
        }
    }

    private static Dictionary<string, UIResource> LoadUiResources(TextAsset jsonFile)
    {
        Dictionary<string, UIResource> resourceMap = JsonConvert.DeserializeObject<Dictionary<string, UIResource>>(jsonFile.text);
        foreach (UIResource resource in resourceMap.Values)
        {
            resource.LoadAsset();
        }

        return resourceMap;
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