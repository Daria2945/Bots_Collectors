public abstract class StateBase
{
    private readonly int _requiredAmountResources;
    private int _amountResourcesCollected;

    public StateBase(int requiredAmountResources, StateMachine stateMachine, Base @base)
    {
        _requiredAmountResources = requiredAmountResources;
        StateMachine = stateMachine;
        Base = @base;
    }

    protected StateMachine StateMachine { get; private set; }

    protected Base Base { get; private set; }

    protected bool HaveCollectedRequiredAmountResources => _amountResourcesCollected >= _requiredAmountResources;

    public abstract void Update();

    public void CollectResource() =>
        _amountResourcesCollected++;

    protected abstract void Create();

    protected void ResetAmountResources() =>
        _amountResourcesCollected = 0;
}