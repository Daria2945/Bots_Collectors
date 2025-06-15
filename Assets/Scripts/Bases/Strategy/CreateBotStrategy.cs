public class CreateBotStrategy : CreateStrategy
{
    public CreateBotStrategy(Base @base, int costCraete, ResourceBalance balance) : base(@base, costCraete, balance) { }

    public override void TryCreate()
    {
        if (Balance.TryRemove(CostCreate))
            Base.CreateBot();
    }
}