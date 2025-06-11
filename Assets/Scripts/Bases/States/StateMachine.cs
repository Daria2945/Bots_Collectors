using System;
using System.Collections.Generic;

public class StateMachine
{
    private Dictionary<Type, StateBase> _states = new();
    private StateBase _currentState;

    public void AddState(StateBase state)
    {
        var key = state.GetType();

        if (_states.ContainsKey(key))
            return;

        _states.Add(key, state);
    }

    public void Update()=>
        _currentState?.Update();

    public void CollectResource()=>
        _currentState?.CollectResource();

    public void SetState<T>() where T : StateBase
    {
        var type = typeof(T);

        if (_currentState != null && _currentState.GetType() == type)
            return;

        if (_states.TryGetValue(type, out StateBase newState))
        {
            _currentState = newState;
        }
    }
}