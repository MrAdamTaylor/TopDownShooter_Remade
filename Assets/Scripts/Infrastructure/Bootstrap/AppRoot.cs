using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppRoot : MonoBehaviour
{
    static class BootConstants
    {
        public static string PATH_TO_BOOT = "Prefabs/Bootstrap/AppController";

        public static string CLONE_LITERAL = "(Clone)";
        
    }


    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void Init()
    {
        var appRoot = Instantiate(Resources.Load(BootConstants.PATH_TO_BOOT)) as GameObject;
        appRoot.name = appRoot.name.Replace("(Clone)", string.Empty);
        DontDestroyOnLoad(appRoot);
        
    }
}
