using System;
using System.Collections.Generic;
using UnityEngine;

public class ResourceServer : MonoBehaviour
{
    [SerializeField] private LevelScaner _levelScaner;

    private Queue<Resource> _freeResource = new();
    private List<int> _collectedResourcesIndex = new();

    public event Action AddedFreeResource;

    public int FreeResourcesCount => _freeResource.Count;

    private void OnEnable()
    {
        _levelScaner.FindedResource += AddFreeResource;
    }

    private void OnDisable()
    {
        _levelScaner.FindedResource -= AddFreeResource;
    }

    public bool TryGetFreeResourse(out Resource resource)
    {
        resource = null;

        if (_freeResource.Count == 0)
            return false;

        resource = _freeResource.Dequeue();

        if (_collectedResourcesIndex.Contains(resource.Index))
            return false;

        _collectedResourcesIndex.Add(resource.Index);

        return true;
    }

    private void AddFreeResource(Resource resource)
    {
        _freeResource.Enqueue(resource);
        AddedFreeResource?.Invoke();
    }
}