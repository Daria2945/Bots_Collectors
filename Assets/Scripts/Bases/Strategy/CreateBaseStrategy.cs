public class CreateBaseStrategy : CreateStrategy
{
    private readonly Flag _flag;

    public CreateBaseStrategy(Base @base, int requiedCountResource, Flag flag) : base(@base, requiedCountResource)
    {
        _flag = flag;
    }

    public override void Create()
    {
        if (_flag.IsGrounded == false)
            return;

        Base.CreateBase();
        ResetCurrentCountResource();
    }
}