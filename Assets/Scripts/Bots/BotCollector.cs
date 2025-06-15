using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider), typeof(Rigidbody))]
public class BotCollector : MonoBehaviour
{
    private Transform _transform;
    private Resource _target;

    public event Action ResourceCollected;

    private void Awake()
    {
        _transform = transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_target == null)
        {
            return;
        }

        if (other.TryGetComponent(out Resource resource) && resource == _target)
        {
            ResourceCollected?.Invoke();
            TakeResource(resource);
        }
    }

    public void SetResourceTarget(Resource resource) =>
        _target = resource;

    private void TakeResource(Resource resource)
    {
        resource.Lift(_transform);
        _target = null;
    }
}