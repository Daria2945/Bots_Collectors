public abstract class CreateStrategy
{
    private readonly int _requiedCountResource;
    private int _currentCountResource;

    public CreateStrategy(Base @base, int requiedCountResource)
    {
        Base = @base;
        _requiedCountResource = requiedCountResource;
    }

    public bool CanCreate => _currentCountResource >= _requiedCountResource;

    protected Base Base { get; private set; }

    public abstract void Create();

    public void AddResource() =>
        _currentCountResource++;

    protected void ResetCurrentCountResource() =>
        _currentCountResource = 0;
}