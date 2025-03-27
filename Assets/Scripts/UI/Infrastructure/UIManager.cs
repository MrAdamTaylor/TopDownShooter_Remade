using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private UIBundleSystem _uiBundleSystem;
    private Dictionary<IUiWindow, GameObject> _uiPrefabs;
    private UIConfigurator _uiConfigurator;

    public void Initialize(UIBundleSystem uiBundleSystem)
    {
        _uiBundleSystem = uiBundleSystem;
        _uiPrefabs = new Dictionary<IUiWindow, GameObject>();
        _uiConfigurator = new UIConfigurator(this);
    }
    
    
    private TUI GetOrCreate<TUI>(bool isShowed = false) where TUI: class, IUiWindow 
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
            _uiPrefabs.Add(window, prefab);
            return window as TUI;
        }
    }

    private UIResource GetUIResource<TUI>() where TUI : class, IUiWindow
    {
        UIResource resource = _uiBundleSystem.GetResourceByType<TUI>();
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
        _uiConfigurator.Configure(uiWindow, resource);
        uiWindow.Show();
    }

    private void CheckingForPossibleErrors()
    {
        if (_uiBundleSystem == null)
        {
            throw new NullReferenceException("Bundle system is null");
        }
        
        if (_uiBundleSystem.IsEmpty)
        {
            throw new InvalidOperationException("Ресурсы не загружены. Словарь бандлов пуст.");
        }
    }
}