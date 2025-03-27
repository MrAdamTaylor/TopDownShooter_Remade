using UnityEngine;

public class GameContext
{
    ScoreStorage _scoreStorage;
    
    public GameContext()
    {
        _scoreStorage = new ScoreStorage(PlayerPrefs.GetInt("PlayerScores"));
    }

    public ScoreStorage GetScoreSotrage()
    {
        return _scoreStorage;
    }
}