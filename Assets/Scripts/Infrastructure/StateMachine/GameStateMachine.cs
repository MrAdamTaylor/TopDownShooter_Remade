using System;
using System.Collections.Generic;

public class GameStateMachine : IGameStateMachine
{
    private Dictionary<Type, IExitableState> _states;
    private IExitableState _activeState;
    
    public GameStateMachine(SceneLoader sceneLoader)
    {
        _states = new Dictionary<Type, IExitableState>
        {
            [typeof(BootstrapState)] = new BootstrapState(this),
            [typeof(LoadLevelState)] = new LoadLevelState(this),
            [typeof(GameLoopState)] = new GameLoopState(this)
        };
    }

    public void Enter<TState>() where TState : class, IState
    {
        IState state = ChangeState<TState>();
        state.Enter();
    }
    
    private TState ChangeState<TState>() where TState : class, IExitableState
    {
        _activeState?.Exit();
      
        TState state = GetState<TState>();
        _activeState = state;
      
        return state;
    }
    
    private TState GetState<TState>() where TState : class, IExitableState => 
        _states[typeof(TState)] as TState;
}