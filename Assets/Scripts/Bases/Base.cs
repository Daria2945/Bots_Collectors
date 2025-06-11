using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Base : MonoBehaviour, IInterectable, ICreatable
{
    [SerializeField] private CharactersCollection _charactersCollection;
    [SerializeField] private Flag _flag;
    [SerializeField] private ResourceBalance _balance;

    private InitializerBase _initialazer;
    private ResourceServer _resourceServer;

    private CreatorCharacter _creatorCharacter;
    private CreatorBase _creatorBase;

    private StateMachine _stateMachine = new();
    private StateCreateBase _stateCreateBase;
    private StateCreateCharacter _stateCreateCharacter;

    public CharactersCollection CharactersCollection => _charactersCollection;

    public Flag Flag => _flag;

    public CreatorBase CreatorBase => _creatorBase;

    public CreatorCharacter CreatorCharacter => _creatorCharacter;

    public InitializerBase Inizializer => _initialazer;

    private void OnDisable()
    {
        _resourceServer.AddedFreeResource -= SendToCollectResource;
    }

    private void Update()
    {
        _stateMachine.Update();
    }

    public void Initialize(InitializerBase initializer, ResourceServer resourceServer, CreatorCharacter creatorCharacter, CreatorBase creatorBase)
    {
        _initialazer = initializer;
        _resourceServer = resourceServer;
        _creatorCharacter = creatorCharacter;
        _creatorBase = creatorBase;

        InitializeStates();

        _resourceServer.AddedFreeResource += SendToCollectResource;
    }

    public bool TryGetFlag(out Flag flag)
    {
        int minCountCharacters = 2;
        flag = null;

        if (_charactersCollection.CharactersCount < minCountCharacters)
            return false;

        flag = _flag;

        return true;
    }

    private void SendToCollectResource()
    {
        if (_resourceServer.FreeResourcesCount > _charactersCollection.FreeCharacretsCount)
            DistributeRecources(_charactersCollection.FreeCharacretsCount);
        else
            DistributeRecources(_resourceServer.FreeResourcesCount);
    }

    private void DistributeRecources(int characterCount)
    {
        for (int i = 0; i < characterCount; i++)
        {
            if (_resourceServer.TryGetFreeResourse(out Resource resource) == false)
                return;

            if (_charactersCollection.TryGetFreeCharacter(out Character character) == false)
                return;

            character.SetTarget(resource);
            character.ReturnToStartPosition += OnReturnToStartPosition;
        }
    }

    private void OnReturnToStartPosition(Character character)
    {
        character.ReturnToStartPosition -= OnReturnToStartPosition;

        _charactersCollection.ReturnFreeCharacter(character);

        _stateMachine.CollectResource();

        _balance.Add();
    }

    private void InitializeStates()
    {
        int amountResourcesForCreateBase = 5;
        int amountResourcesForCreateCharacter = 3;

        _stateCreateBase = new(amountResourcesForCreateBase, _stateMachine, this);
        _stateCreateCharacter = new(amountResourcesForCreateCharacter, _stateMachine, this);

        _stateMachine.AddState(_stateCreateBase);
        _stateMachine.AddState(_stateCreateCharacter);

        _stateMachine.SetState<StateCreateCharacter>();
    }
}