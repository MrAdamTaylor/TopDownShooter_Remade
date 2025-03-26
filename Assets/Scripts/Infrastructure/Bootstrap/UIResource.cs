using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

[Serializable]
public class UIResource
{
    public string Path;
    public string AssetName;
    public string AssetType;
    public List<string> ConfigsName;
    public Dictionary<string, string> ExtraInfo;
    
    [JsonIgnore] 
    public object CurrentType;

    [JsonIgnore] 
    public List<UiConfig> ApplyingConfigs = new();
    
    public void LoadAsset()
    {
        LoadGameObject();
        LoadConfigs();
    }

    private void LoadConfigs()
    {
        for (int i = 0; i < ConfigsName.Count; i++)
        {
            UiConfig config = Resources.Load<UiConfig>(ConfigsName[i]);
            if(config != null)
                ApplyingConfigs.Add(config);
        }
    }

    private void LoadGameObject()
    {
        Type type = Type.GetType(AssetType);  
        
        if (type == null)
        {
            throw new Exception($"The type was not serialized correctly: {AssetType}");
            return;
        }

        if (type == typeof(GameObject))
        {
            CurrentType = Resources.Load<GameObject>(Path);
            if (CurrentType == null)
            {
                Debug.LogError($"Failed to load GameObject from path: {Path}");
            }
        }
    }
}