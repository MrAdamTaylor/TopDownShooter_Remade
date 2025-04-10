using UnityEngine;

public class LoadLevelState : IState
{
    private readonly GameStateMachine _stateMachine;
    public LoadLevelState(GameStateMachine stateMachine)
    {
        Debug.Log($"<color=green> Welcome LoadLevelState </color>");
        _stateMachine = stateMachine;
    }

    public void Exit()
    {
        
    }

    public void Enter()
    {
        Debug.Log($"<color=yellow>LoadLevelState to GameLoopState</color>");
        _stateMachine.Enter<GameLoopState>();
    }
}