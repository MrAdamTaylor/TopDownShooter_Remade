
using System;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static class BundleConstants
    {
        
    }
    
    
    private BundleSystem _bundleSystem;
    private Dictionary<string, GameObject> _uiPrefabs;

    public void Initialize(BundleSystem bundleSystem)
    {
        _bundleSystem = bundleSystem;
        _uiPrefabs = new Dictionary<string, GameObject>();
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
}
