public class CreateBotStrategy : CreateStrategy
{
    public CreateBotStrategy(Base @base, int requiedCountResource) : base(@base, requiedCountResource) { }

    public override void Create()
    {
        Base.CreateBot();
        ResetCurrentCountResource();
    }
}