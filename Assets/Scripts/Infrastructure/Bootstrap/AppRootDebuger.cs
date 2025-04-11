using UnityEditor;
using UnityEngine;

public class AppRootDebuger : MonoBehaviour
{
    static UIBundleSystem _uiBundleSystem;
    
    public void ConstructBundle(UIBundleSystem uiBundleSystem)
    {
        _uiBundleSystem = uiBundleSystem;
    }


    [MenuItem("Tools/Debug/Check UI Bundles (Runtime)")]
    public static void CheckBundles()
    {
        var map = _uiBundleSystem.GetMap(); // нужно добавить геттер
        foreach (var kv in map)
        {
            Debug.Log($"[UIBundle] {kv.Key} → {(kv.Value != null ? "OK" : "NULL")}");
        }
    }
}