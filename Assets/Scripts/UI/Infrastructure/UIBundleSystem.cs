using System.Collections.Generic;
using UnityEditor;

public class UIBundleSystem
{
    public bool IsEmpty => _resourcesMap == null || _resourcesMap.Count == 0;

    public Dictionary<string, UIResource> GetMap() => _resourcesMap;
    
    private readonly Dictionary<string, UIResource> _resourcesMap;
    

    public UIBundleSystem(Dictionary<string, UIResource> resourcesMap)
    {
        _resourcesMap = resourcesMap;
    }

    public UIResource GetResourceByType<TUI>() where TUI : class, IUiWindow
    {
        string typeName = typeof(TUI).Name;
        return _resourcesMap.TryGetValue(typeName, out var value) ? value : null;
    }
    
}