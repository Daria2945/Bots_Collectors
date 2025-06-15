using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class BotMover : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Vector3 _targetPosition;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    public void SetTargetPosition(Vector3 targetPosition) =>
        _targetPosition = targetPosition;

    public void Move()
    {
        if (_targetPosition == null)
            throw new System.NullReferenceException("Target Position Not Set");

        _agent.destination = _targetPosition;
    }
}