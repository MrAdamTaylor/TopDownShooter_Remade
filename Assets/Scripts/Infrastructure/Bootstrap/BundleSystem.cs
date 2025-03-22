using System.Collections.Generic;

public class BundleSystem
{
    public bool IsEmpty => _resourcesMap == null || _resourcesMap.Count == 0;

    private readonly Dictionary<string, UIResource> _resourcesMap;
    
    

    public BundleSystem(Dictionary<string, UIResource> resourcesMap)
    {
        _resourcesMap = resourcesMap;
    }
    
    public bool IsExistBundle(string key)
    {
        return _resourcesMap.ContainsKey(key);
    }

    public UIResource GetResource(string bundleName)
    {
        return _resourcesMap[bundleName];
    }

    public UIResource GetResourceByType<TUI>() where TUI : class, IUiWindow
    {
        string typeName = typeof(TUI).Name;
        return _resourcesMap.TryGetValue(typeName, out var value) ? value : null;
    }
}