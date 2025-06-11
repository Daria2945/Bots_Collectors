using UnityEngine;

[RequireComponent(typeof(FlagMover))]
public class Flag : MonoBehaviour
{
    private Transform _transform;

    public FlagMover Mover { get; private set; }

    public Vector3 CurrentPosition { get; private set; }

    public bool IsActive => gameObject.activeSelf;

    public bool IsGrounded { get; private set; }

    private void Awake()
    {
        _transform = transform;
        Mover = GetComponent<FlagMover>();
    }

    public void Activate() =>
        gameObject.SetActive(true);

    public void Deactivate() =>
        gameObject.SetActive(false);

    public void Lift() =>
        IsGrounded = false;

    public void PutOnGround()
    {
        IsGrounded = true;
        CurrentPosition = _transform.position;
    }
}