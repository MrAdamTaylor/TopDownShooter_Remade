using System.IO;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using Application = UnityEngine.Device.Application;

public static class Loader
{
    private const string RESOURCE_NAME = "/Resources";
    private const string BUNDLES_NAME = "/Bundles";
    private const string UI_BUNDLES_NAME = "ResourcesMapUI.txt";

    
    public static async UniTask<T> LoadUI<T>() 
    {
        string path = GetUIBundlePath();
        CheckFilePath(path);
        var result = await ReadFileByPath<T>(path);
        return result;
    }

    private static async UniTask<T> ReadFileByPath<T>(string path)
    {
        await UniTask.SwitchToThreadPool();
        using var reader = new StreamReader(path);
        using var jsonReader = new JsonTextReader(reader);
        var serializer = new JsonSerializer();
        T result = serializer.Deserialize<T>(jsonReader);
        await UniTask.SwitchToMainThread();
        return result;
    }

    private static string GetUIBundlePath()
    {
        string filePath = Path.Combine(Application.dataPath+BUNDLES_NAME, UI_BUNDLES_NAME);
        return filePath;
    }

    private static void CheckFilePath(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException("File not found: " + filePath);
        }
        else
        {
            Debug.Log($"<color=green> {filePath} </color>");
        }
    }

}