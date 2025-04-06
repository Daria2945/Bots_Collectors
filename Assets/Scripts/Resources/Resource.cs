using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshObstacle))]
public class Resource : MonoBehaviour
{
    private NavMeshObstacle _meshObstacle;
    private Transform _transform;

    public bool IsAboveGround { get; private set; }

    public event Action<Resource> Destroyed;

    private void Awake()
    {
        _meshObstacle = GetComponent<NavMeshObstacle>();
        _transform = transform;
    }

    public void Reset()
    {
        IsAboveGround = false;
        _transform.parent = null;
    }

    public void InvokeEventDestroyed() =>
        Destroyed?.Invoke(this);

    public void Lift(Transform transformParent)
    {
        IsAboveGround = true;

        _transform.SetParent(transformParent);
        _transform.position = transformParent.position;

        DeactivateNavMeshObstacle();
    }

    public void ActiviteNavMeshObstacle() =>
        _meshObstacle.enabled = true;

    private void DeactivateNavMeshObstacle() =>
        _meshObstacle.enabled = false;
}