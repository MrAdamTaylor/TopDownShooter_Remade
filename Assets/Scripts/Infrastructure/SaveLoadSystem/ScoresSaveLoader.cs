using UnityEngine;

public class ScoresSaveLoader : ISaveLoader
{
    private const string PLAYER_SCORES_KEY = "PlayerScores";
    
    
    ScoreStorage _scoreStorage;
    

    public void SaveGame(GameContext gameContext)
    {
        _scoreStorage = gameContext.GetScoreSotrage();
        PlayerPrefs.SetInt(PLAYER_SCORES_KEY,_scoreStorage.Score);
        Debug.Log($"Saved Score: {_scoreStorage.Score}");
    }

    public void LoadGame(GameContext gameContext)
    {
        _scoreStorage = gameContext.GetScoreSotrage();
        if (PlayerPrefs.HasKey(PLAYER_SCORES_KEY))
        {
            var value = PlayerPrefs.GetInt(PLAYER_SCORES_KEY);
            _scoreStorage.SetupScore(value);
            Debug.Log($"Load Score: {_scoreStorage.Score}");
        }
    }
}