using UnityEngine;

public class LoadLevelState : IPayloadedState
{
    private readonly GameStateMachine _stateMachine;
    
    private LevelConfig _levelConfig;
    private SceneLoader _sceneLoader;
    public LoadLevelState(GameStateMachine stateMachine, SceneLoader loader ,LevelConfig levelConfig)
    {
        Debug.Log($"<color=green> Welcome LoadLevelState </color>");
        _levelConfig = levelConfig;
        _sceneLoader = loader;
        _stateMachine = stateMachine;
    }

    public void Enter(string payload)
    {
        _sceneLoader.LoadByName(payload);
    }

    public void Exit()
    {
        
    }

    public void Enter(object payload)
    {
        if (payload is string levelName)
        {
            Debug.Log($"<color=yellow>LoadLevelState to GameLoopState</color>");
            _sceneLoader.LoadByName(levelName);
            _stateMachine.Enter<GameLoopState>();
        }
        else
        {
            _sceneLoader.LoadByConfig(_levelConfig);
            _stateMachine.Enter<GameLoopState>();
        }
    }
}