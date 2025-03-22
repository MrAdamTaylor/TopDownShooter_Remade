
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static class BundleConstants
    {
        
    }
    
    
    private BundleSystem _bundleSystem;
    private Dictionary<IUiWindow, GameObject> _uiPrefabs;

    public void Initialize(BundleSystem bundleSystem)
    {
        _bundleSystem = bundleSystem;
        _uiPrefabs = new Dictionary<IUiWindow, GameObject>();
    }
    
    
    public TUI GetOrCreate<TUI>(bool isShowed = false) where TUI: class, IUiWindow 
    {
        CheckingForPossibleErrors();
        var uiWindow = _uiPrefabs.Keys
            .FirstOrDefault(key => key is TUI);
        
        if (uiWindow != null)
        {
            return (TUI)uiWindow;
        }
        else
        {
            var resource = GetUIResource<TUI>();
            GameObject prefab = (GameObject)resource.CurrentType;
            GameObject instance = Instantiate(prefab,this.transform, false);
            instance.SetActive(isShowed);
            IUiWindow window = instance.GetComponent<IUiWindow>();
            window.Configure(resource);
            return window as TUI;
        }
    }

    private UIResource GetUIResource<TUI>() where TUI : class, IUiWindow
    {
        UIResource resource = _bundleSystem.GetResourceByType<TUI>();
        return resource;
    }


    public void Show<TUIWindow, TUConfig>() where TUIWindow : class, IUiWindow where TUConfig : UiConfig
    {
        TUIWindow uiWindow = GetOrCreate<TUIWindow>();
        UIResource resource = GetUIResource<TUIWindow>();
        if (resource.ApplyingConfigs.First() is not TUConfig)
        {
            throw new Exception($"Not found UI config in Serialized file");
        }
        
        uiWindow.Configure(resource);
        uiWindow.Show();
    }

    public void GetOrCreate(string bundleName)
    {
        Debug.Log($"Try Get or Create bundle name {bundleName}");

        CheckingForPossibleErrors(bundleName);
        
        UIResource resource = _bundleSystem.GetResource(bundleName);
        GameObject prefab = (GameObject)resource.CurrentType;
            
        GameObject instance = Instantiate(prefab, this.transform, false);
        Debug.Log($"Создание бандла прошло успешно!");
            
    }

    private void CheckingForPossibleErrors(string bundleName)
    {
        if (_bundleSystem == null)
        {
            throw new NullReferenceException("Bundle system is null");
        }
        
        if (_bundleSystem.IsEmpty)
        {
            throw new InvalidOperationException("Ресурсы не загружены. Словарь бандлов пуст.");
        }
    }

    private void CheckingForPossibleErrors()
    {
        if (_bundleSystem == null)
        {
            throw new NullReferenceException("Bundle system is null");
        }
        
        if (_bundleSystem.IsEmpty)
        {
            throw new InvalidOperationException("Ресурсы не загружены. Словарь бандлов пуст.");
        }
    }
}


