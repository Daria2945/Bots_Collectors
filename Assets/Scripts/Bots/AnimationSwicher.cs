using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationSwicher : MonoBehaviour
{
    private readonly int IsIdle = Animator.StringToHash(nameof(IsIdle));
    private readonly int IsWalk = Animator.StringToHash(nameof(IsWalk));

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Idle() =>
        _animator.SetTrigger(IsIdle);

    public void Walk() =>
        _animator.SetTrigger(IsWalk);
}