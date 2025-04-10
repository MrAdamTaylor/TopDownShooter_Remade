using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelPack", menuName = "Configs/LevelPack")]
public class LevelPackConfig : ScriptableObject
{
    public LevelConfig DefaultFirstLevelConfig;
    
    public List<LevelConfig> LevelConfigsList;
}
