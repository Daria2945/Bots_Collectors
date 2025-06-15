using System;
using UnityEngine;

[RequireComponent(typeof(BotMover))]
public class Bot : MonoBehaviour, ICreatable
{
    private const float MinDistanceToStartPosition = 0.3f;

    [SerializeField] private BotCollector _collector;
    [SerializeField] private AnimationSwicher _animationSwicher;

    private Resource _resource;
    private BotMover _mover;
    private Transform _transform;

    private bool _isPlayWalkAnimation;
    private bool _isPlayIdleAnimation;

    private bool _hasLeftStartPosition;

    public event Action<Bot> ReturnToStartPosition;

    private float _distanseToStartPosition => Vector3.Distance(_transform.position, StartPosition);

    public Vector3 StartPosition { get; private set; }

    public bool HaveResource { get; private set; }

    private void Start()
    {
        _mover = GetComponent<BotMover>();
        _transform = transform;
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
        if (_distanseToStartPosition > MinDistanceToStartPosition && _hasLeftStartPosition == false)
        {
            _hasLeftStartPosition = true;
        }

        if (_distanseToStartPosition < MinDistanceToStartPosition && _hasLeftStartPosition)
        {
            _hasLeftStartPosition = false;
            ReturnToStartPosition?.Invoke(this);
        }
    }

    public void Wait()
    {
        if (_isPlayIdleAnimation)
            return;

        _isPlayIdleAnimation = true;
        _isPlayWalkAnimation = false;

        _animationSwicher.Idle();
    }

    public void MoveToResource(Resource target)
    {
        _resource = target;
        _collector.SetResourceTarget(target);

        Move(target.StartPosition);
    }

    public void MoveToStartPosition() =>
        Move(StartPosition);

    public void PassOnResource()
    {
        HaveResource = false;

        Wait();

        _resource.InvokeEventDestroyed();
    }

    public void SetNewStartPosition(Vector3 startPosition) =>
        StartPosition = startPosition;

    private void Move(Vector3 targetPosition)
    {
        _mover.SetTargetPosition(targetPosition);
        _mover.Move();

        if (_isPlayWalkAnimation)
            return;

        _isPlayIdleAnimation = false;
        _isPlayWalkAnimation = true;

        _animationSwicher.Walk();
    }

    private void OnResourceCollected()
    {
        HaveResource = true;

        MoveToStartPosition();
    }
}