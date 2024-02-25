using System;
using System.Collections.Generic;

public class StateMachine
{
    private Dictionary<Type, IState> _states;
    private IState _currentState;

    public StateMachine() 
    {
        _states = new Dictionary<Type, IState>();
    }

    public void Add<T>(T state) where T : class, IState
    {
        var key = typeof(T);

        if (_states.ContainsKey(key)) 
            throw new Exception($"State: {key} already been added.");

        _states.Add(key, state);
    }

    public async void SwitchState<T>() where T : class, IState
    {
        var key = typeof(T);

        if (_states.TryGetValue(key, out var state))
        {
            await _currentState?.Exit();
            _currentState = state;
            await _currentState.Enter();
        }
        else
            throw new Exception($"State: {key} do not exist!");
    }

    public void Clear()
    {
        _states.Clear();
    }
}