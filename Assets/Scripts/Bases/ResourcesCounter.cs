using System;
using UnityEngine;

public class ResourcesCounter : MonoBehaviour
{
    private int _resources;

    public event Action<int> ResourcesChanged;

    private void Start()
    {
        ResourcesChanged?.Invoke(_resources);
    }

    public void AddResource()
    {
        _resources++;
        ResourcesChanged?.Invoke(_resources);
    }
}