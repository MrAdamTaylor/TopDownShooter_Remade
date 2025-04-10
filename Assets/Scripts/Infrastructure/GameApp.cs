using UnityEngine;

public class GameApp
{
    public GameStateMachine GameStateMachine;
    private bool _isBootstraped;
    private LevelPackConfig _levelPackConfig;
    
    public GameApp(LevelPackConfig levelPackConfig)
    {
        Debug.Log("Game App Created");
        _levelPackConfig = levelPackConfig;
        GameStateMachine ??= new GameStateMachine(AppRoot.SceneLoader, levelPackConfig);
    }

    public void StartGame()
    {
        if (_isBootstraped)
        {
            GameStateMachine.Enter<LoadLevelState, string>(_levelPackConfig.DefaultFirstLevelConfig.SceneName);
        }
        else
        {
            GameStateMachine.Enter<BootstrapState>();
            _isBootstraped = true;
        }
    }
}