using System;
using UnityEngine;

[RequireComponent(typeof(CharacterMover))]
public class Character : MonoBehaviour, ICreatable
{
    [SerializeField] private CharacterCollector _collector;
    [SerializeField] private AnimationSwicher _animationSwicher;

    private CharacterMover _mover;

    private Resource _resource;
    private bool _isTakeResource;

    public event Action<Character> ReturnToStartPosition;

    public Transform StartPosition => _mover.TransfornStartPosition;

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
        if (_mover.IsWaitingInStartPosition && _isTakeResource)
            InvokeEventReturnToStartPosition();
    }

    public void SetNewStartPosition(Transform position)
    {
        _mover.SetStartPosition(position);
        _mover.MoveToStartPosition();
    }

    public void SetTarget(Resource resource)
    {
        _resource = resource;

        _collector.SetResourceTarget(resource);
        _mover.MoveToTarget(resource.StartPosition);

        _animationSwicher.Walk();
    }

    private void OnResourceCollected()
    {
        _isTakeResource = true;

        _mover.MoveToStartPosition();
    }

    private void InvokeEventReturnToStartPosition()
    {
        _isTakeResource = false;

        _animationSwicher.Idle();

        _resource.InvokeEventDestroyed();
        ReturnToStartPosition?.Invoke(this);
    }
}