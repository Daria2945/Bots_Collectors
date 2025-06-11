using UnityEngine;

public class InitializerBase : MonoBehaviour
{
    [SerializeField] private ResourceServer _resourceServer;
    [SerializeField] private CreatorBase _creatorBase;
    [SerializeField] private CreatorCharacter _creatorCharacter;
    [SerializeField] private Transform _positionFirstBase;
    [SerializeField] private int _startCountCharacters = 3;

    private void Awake()
    {
        Base firstBase = _creatorBase.Create(_positionFirstBase.position);
        InitializeBase(firstBase);

        CreateCharacters(_startCountCharacters, firstBase);
    }

    public void InitializeBase(Base @base) =>
        @base.Initialize(this, _resourceServer, _creatorCharacter, _creatorBase);

    private void CreateCharacters(int count, Base @base)
    {
        for (int i = 0; i < count; i++)
        {
            if (@base.CharactersCollection.TryGetFreePosition(out Transform freePosition) == false)
                return;

            Character character = _creatorCharacter.Create(freePosition.position);
            @base.CharactersCollection.AddNewCharacter(character);
        }
    }
}