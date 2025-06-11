using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class CharacterMover : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Transform _transform;

    private Vector3 _startPosition;

    public Transform TransfornStartPosition { get; private set; }

    public bool IsWaitingInStartPosition => _transform.position.x == _startPosition.x && _transform.position.z == _startPosition.z;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _transform = transform;
        _startPosition = _transform.position;
        TransfornStartPosition = transform;
    }

    public void SetStartPosition(Transform startPosition)
    {
        _startPosition = startPosition.position;
        TransfornStartPosition = startPosition;
    }

    public void MoveToTarget(Vector3 targetPosition) =>
        Move(targetPosition);

    public void MoveToStartPosition() =>
        Move(_startPosition);

    private void Move(Vector3 targetPosition) =>
        _agent.destination = targetPosition;
}