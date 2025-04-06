using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider), typeof(Rigidbody))]
public class BaseScaner : MonoBehaviour
{
    public event Action<Resource> FindedResource;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Resource resource) && resource.IsAboveGround == false)
        {
            FindedResource?.Invoke(resource);
        }
    }
}