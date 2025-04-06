using System;
using UnityEngine;

[RequireComponent(typeof(CharacterMover))]
public class Character : MonoBehaviour
{
    [SerializeField] private CharacterCollector _collector;
    [SerializeField] private AnimationSwicher _animationSwicher;

    private CharacterMover _mover;

    private Resource _resource;
    private bool _hasResourceCollected;

    public event Action<Character> ReturnToStartPosition;

    private void Awake()
    {
        _mover = GetComponent<CharacterMover>();
    }

    private void OnEnable()
    {
        _collector.ResourceCollected += OnResourceCollected;
    }

    private void OnDisable()
    {
        _collector.ResourceCollected -= OnResourceCollected;
    }

    private void Update()
    {
        if (_mover.IsWaitingInStartPosition && _hasResourceCollected)
            InvokeEventReturnToStartPosition();
    }

    public void SetTarget(Resource resource)
    {
        _resource = resource;

        _collector.SetResourceTarget(resource);
        _mover.MoveToTarget(resource.transform.position);

        _animationSwicher.Walk();
    }

    private void OnResourceCollected()
    {
        _hasResourceCollected = true;

        _collector.DeleteResourceTarget();
        _mover.MoveToStartPosition();
    }

    private void InvokeEventReturnToStartPosition()
    {
        _hasResourceCollected = false;

        _animationSwicher.Idle();

        _resource.InvokeEventDestroyed();
        ReturnToStartPosition?.Invoke(this);
    }
}