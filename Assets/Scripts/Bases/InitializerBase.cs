using System;
using UnityEngine;

public class InitializerBase : MonoBehaviour
{
    [SerializeField] private ResourceServer _resourceServer;
    [SerializeField] private CreatorBase _creatorBase;
    [SerializeField] private CreatorBot _creatorCharacter;
    [SerializeField] private Transform _positionFirstBase;
    [SerializeField] private int _startCountCharacters = 3;

    public event Action NewBaseInitialized;

    private void Start()
    {
        Base firstBase = _creatorBase.Create(_positionFirstBase.position);
        InitializeBase(firstBase);

        CreateCharacters(_startCountCharacters, firstBase);
    }

    public void InitializeBase(Base @base)
    {
        @base.Initialize(this, _resourceServer, _creatorCharacter, _creatorBase);
        NewBaseInitialized?.Invoke();
    }

    private void CreateCharacters(int count, Base @base)
    {
        for (int i = 0; i < count; i++)
            @base.CreateBot();
    }
}