using System.Collections.Generic;

public class UIBundleSystem
{
    public bool IsEmpty => _resourcesMap == null || _resourcesMap.Count == 0;

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