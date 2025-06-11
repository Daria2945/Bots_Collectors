using UnityEngine;

public class StateCreateBase : StateBase
{
    public StateCreateBase(int requiredAmountResources, StateMachine stateMachine, Base @base) : base(requiredAmountResources, stateMachine, @base) { }

    public override void Update()
    {
        if (HaveCollectedRequiredAmountResources == false)
            return;

        if (Base.Flag.IsGrounded == false)
            return;

        Create();

        Base.Flag.Deactivate();
        ResetAmountResources();

        StateMachine.SetState<StateCreateCharacter>();
    }

    protected override void Create()
    {
        Base @base = Base.CreatorBase.Create(Base.Flag.CurrentPosition);
        Base.Inizializer.InitializeBase(@base);

        SetCharacter(@base);
    }

    private void SetCharacter(Base @base)
    {
        Character character = Base.CharactersCollection.DeleteCharacter();

        @base.CharactersCollection.TryGetFreePosition(out Transform startPosition);
        @base.CharactersCollection.AddNewCharacter(character);

        character.SetNewStartPosition(startPosition);
    }
}