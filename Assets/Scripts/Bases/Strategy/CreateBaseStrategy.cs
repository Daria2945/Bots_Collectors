public class CreateBaseStrategy : CreateStrategy
{
    private readonly Flag _flag;

    public CreateBaseStrategy(Base @base, int costCreate, ResourceBalance balance, Flag flag) : base(@base, costCreate, balance)
    {
        _flag = flag;
    }

    public override void TryCreate()
    {
        if (_flag.IsGrounded == false)
            return;

        if (Balance.TryRemove(CostCreate))
            Base.CreateBase();
    }
}