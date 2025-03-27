using UnityEditor;
using UnityEngine;

public class ScoreStorage
{
    public int Score => _score;

    private static int _score = 0;

    public ScoreStorage(int getInt)
    {
        _score = getInt;
    }

    public void SetupScore(int score)
    {
        _score = score;
    }

    [MenuItem("Tools/AddScores(100)")]
    public static void AddScores()
    {
        _score += 100;
    }

    [MenuItem("Tools/RemoveScores(100)")]
    public static void RemoveScores()
    {
        if(_score + 100 >= 0)
            _score -= 100;
        
    }
    
    
}