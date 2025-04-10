using System;
using UnityEngine;

public class BootstrapState : IState
{
    private readonly GameStateMachine _stateMachine;

    public BootstrapState(GameStateMachine stateMachine, LevelPackConfig levelPackConfig)
    {
        Debug.Log($"<color=green> BootstrapState Instantiate </color>");
        _stateMachine = stateMachine;
    }

    public void Exit()
    {
        
    }

    public void Enter()
    {
        Debug.Log($"<color=yellow>Bootstrap State to Load State</color>");
        _stateMachine.Enter<LoadLevelState>();
    }
}