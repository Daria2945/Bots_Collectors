using System;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;

    public event Action<Base> ClickedOnBase;
    public event Action ClickedOnGround;

    private void OnEnable()
    {
        _inputReader.ClickedOnInterectable += HandleCollision;
    }

    private void OnDisable()
    {
        _inputReader.ClickedOnInterectable -= HandleCollision;
    }

    private void HandleCollision(IInterectable interectable)
    {
        if (interectable is Base @base)
            InvokeEventClickedOnBase(@base);

        if (interectable is Ground)
            InvokeEventClickedOnGround();
    }

    private void InvokeEventClickedOnBase(Base @base) =>
        ClickedOnBase?.Invoke(@base);

    private void InvokeEventClickedOnGround() =>
        ClickedOnGround?.Invoke();
}