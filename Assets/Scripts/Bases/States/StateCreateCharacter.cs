using UnityEngine;

public class StateCreateCharacter : StateBase
{
    private Transform _positionCreateCaracter;

    public StateCreateCharacter(int requiredAmountResources, StateMachine stateMachine, Base @base) : base(requiredAmountResources, stateMachine, @base) { }

    public override void Update()
    {
        if (Base.Flag.IsActive)
            StateMachine.SetState<StateCreateBase>();

        if (HaveCollectedRequiredAmountResources == false)
            return;

        if (Base.CharactersCollection.TryGetFreePosition(out _positionCreateCaracter) == false)
            return;

        Create();
    }

    protected override void Create()
    {
        var character = Base.CreatorCharacter.Create(_positionCreateCaracter.position);
        Base.CharactersCollection.AddNewCharacter(character);

        ResetAmountResources();
    }
}