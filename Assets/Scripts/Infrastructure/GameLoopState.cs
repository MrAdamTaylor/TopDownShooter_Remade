using System;
using UnityEngine;

public class GameLoopState : IState
{
    private readonly object _stateMachine;

    public GameLoopState(GameStateMachine stateMachine)
    {
        Debug.Log($"<color=green> GameLoop State </color>");
        _stateMachine = stateMachine;
    }

    public void Exit()
    {
        throw new NotImplementedException();
    }

    public void Enter()
    {
        
    }
}