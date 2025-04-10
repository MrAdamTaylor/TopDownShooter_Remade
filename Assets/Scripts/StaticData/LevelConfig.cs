using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Level", menuName = "Configs/LevelConfig")]
public class LevelConfig : ScriptableObject
{
    public string SceneName;
    
    public List<SpawnerConfigs> SpawnerConfigsList;
}