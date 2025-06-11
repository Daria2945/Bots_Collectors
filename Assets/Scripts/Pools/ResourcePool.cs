using System.Collections.Generic;
using UnityEngine;

public class ResourcePool : MonoBehaviour
{
    [SerializeField] private Resource _prefab;

    private Queue<Resource> _resources = new();

    public Resource Get()
    {
        if (_resources.Count == 0)
        {
            return Instantiate(_prefab);
        }

        return _resources.Dequeue();
    }

    public void Put(Resource resource)
    {
        _resources.Enqueue(resource);
    }
}