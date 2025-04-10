using System;
using System.Collections.Generic;

public class GameStateMachine : IGameStateMachine
{
    private Dictionary<Type, IExitableState> _states;
    private IExitableState _activeState;
    private LevelPackConfig _levelPackConfig;
    
    public GameStateMachine(SceneLoader sceneLoader, LevelPackConfig levelPackConfig)
    {
        _states = new Dictionary<Type, IExitableState>
        {
            [typeof(BootstrapState)] = new BootstrapState(this, levelPackConfig),
            [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader,levelPackConfig.DefaultFirstLevelConfig),
            [typeof(GameLoopState)] = new GameLoopState(this)
        };
    }

    public void Enter<TState>() where TState : class, IState
    {
        IState state = ChangeState<TState>();
        state.Enter();
    }
    
    public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState
    {
        TState state = ChangeState<TState>();
        state.Enter(payload);
    }
    
    public void Enter<TState>(object payload = null) where TState : class, IExitableState
    {
        var state = ChangeState<TState>();

        switch (state)
        {
            case IState simpleState when payload == null:
                simpleState.Enter();
                break;

            case IPayloadedState payloadedState when payload != null:
                payloadedState.Enter(payload);
                break;
            
            case IPayloadedState payloadedState when payload == null:
                payloadedState.Enter(null);
                break;

            default:
                throw new InvalidOperationException($"State {typeof(TState)} cannot be entered with the given payload.");
        }
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