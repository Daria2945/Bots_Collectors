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
}