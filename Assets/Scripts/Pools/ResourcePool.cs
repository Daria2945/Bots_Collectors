using System.Collections.Generic;
using UnityEngine;

public class ResourcePool : MonoBehaviour
{
    [SerializeField] private Resource _prefab;

    private Queue<Resource> _resources = new();

    public Resource GetResource()
    {
        if (_resources.Count == 0)
        {
            var resource = Instantiate(_prefab);

            return resource;
        }

        return _resources.Dequeue();
    }

    public void PutResource(Resource resource)
    {
        _resources.Enqueue(resource);
    }
}