using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshObstacle))]
public class Resource : MonoBehaviour
{
    private static int s_index = 0;

    private Transform _transform;
    private NavMeshObstacle _meshObstacle;

    public event Action<Resource> Destroyed;

    public Vector3 StartPosition { get; private set; }

    public int Index => s_index;

    public bool IsGrounded { get; private set; } = true;

    private void Awake()
    {
        _meshObstacle = GetComponent<NavMeshObstacle>();
        _transform = transform;
    }

    public void InvokeEventDestroyed() =>
        Destroyed?.Invoke(this);

    public void IncreaseIndex() =>
        s_index++;

    public void Reset()
    {
        transform.parent = null;
        IsGrounded = true;
    }

    public void SetStartPosition(Vector3 startPosition)
    {
        transform.position = startPosition;
        StartPosition = startPosition;

        ActiviteNavMeshObstacle();
    }

    public void Lift(Transform transformParent)
    {
        IsGrounded = false;

        _transform.SetParent(transformParent);
        _transform.position = transformParent.position;

        DeactivateNavMeshObstacle();
    }

    private void ActiviteNavMeshObstacle() =>
        _meshObstacle.enabled = true;

    private void DeactivateNavMeshObstacle() =>
        _meshObstacle.enabled = false;
}