using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider), typeof(Rigidbody))]
public class LevelScaner : MonoBehaviour
{
    public event Action<Resource> FindedResource;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Resource resource) && resource.IsGrounded)
        {
            FindedResource?.Invoke(resource);
        }
    }
}