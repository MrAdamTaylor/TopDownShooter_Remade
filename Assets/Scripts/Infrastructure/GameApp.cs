using UnityEngine;

public class GameApp
{
    private GameStateMachine _gameStateMachine;
    private bool _isBootstraped;
    public GameApp()
    {
        Debug.Log("Game App Created");
        if (_gameStateMachine == null)
        {
            _gameStateMachine = new GameStateMachine(new SceneLoader());
        }
    }

    public void StartGame()
    {
        if (_isBootstraped)
        {
            _gameStateMachine.Enter<LoadLevelState>();
        }
        else
        {
            _gameStateMachine.Enter<BootstrapState>();
            _isBootstraped = true;
        }
    }
}

public class SceneLoader
{
    
    
    
}




