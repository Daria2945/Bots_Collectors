using System;
using UnityEngine;

public class ResourceBalance : MonoBehaviour
{
    private int _value;

    public event Action<int> ValueChanged;

    private void Start()
    {
        ValueChanged?.Invoke(_value);
    }

    public void Add()
    {
        _value++;
        ValueChanged?.Invoke(_value);
    }

    public bool TryRemove(int resourceCount)
    {
        if(resourceCount < 0)
            return false;

        if(resourceCount > _value)
            return false;

        _value -= resourceCount;
        ValueChanged?.Invoke(_value);

        return true;
    }
}