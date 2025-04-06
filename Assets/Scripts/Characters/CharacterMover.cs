using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class CharacterMover : MonoBehaviour
{
    private NavMeshAgent _agent;

    private Vector3 _startPosition;
    private Transform _transform;

    public bool IsWaitingInStartPosition => _transform.position.x == _startPosition.x && _transform.position.z == _startPosition.z;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _startPosition = transform.position;
        _transform = transform;
    }

    public void MoveToTarget(Vector3 targetPosition) =>
        Move(targetPosition);

    public void MoveToStartPosition() =>
        Move(_startPosition);

    private void Move(Vector3 targetPosition) =>
        _agent.destination = targetPosition;
}