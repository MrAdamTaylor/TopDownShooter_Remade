using UnityEngine;

public class ScoresSaveLoader : ISaveLoader
{
    private const string PLAYER_SCORES_KEY = "PlayerScores";
    
    
    ScoreStorage _scoreStorage;
    

    public void SaveGame(IGameRepository gameRepository, GameContext gameContext)
    {
        _scoreStorage = gameContext.GetScoreSotrage();
        gameRepository.SetData(_scoreStorage.Score);
        //PlayerPrefs.SetInt(PLAYER_SCORES_KEY,_scoreStorage.Score);
        Debug.Log($"Saved Score: {_scoreStorage.Score}");
    }

    public void LoadGame(IGameRepository gameRepository, GameContext gameContext)
    {
        _scoreStorage = gameContext.GetScoreSotrage();
        if (gameRepository.TryGetData(out int playerScore))
        {
            gameRepository.SetData(playerScore);
            //var value = PlayerPrefs.GetInt(PLAYER_SCORES_KEY);
            _scoreStorage.SetupScore(playerScore);
            Debug.Log($"Load Score: {_scoreStorage.Score}");
        }
    }
}