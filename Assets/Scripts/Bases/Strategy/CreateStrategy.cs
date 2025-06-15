public abstract class CreateStrategy
{
    public CreateStrategy(Base @base, int costCraete, ResourceBalance balance)
    {
        Base = @base;
        CostCreate = costCraete;
        Balance = balance;
    }

    protected Base Base { get; private set; }

    protected int CostCreate {  get; private set; }

    protected ResourceBalance Balance { get; private set; }

    public abstract void TryCreate();
}