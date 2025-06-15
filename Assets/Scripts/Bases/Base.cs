using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Base : MonoBehaviour, IInterectable, ICreatable
{
    [SerializeField] private Flag _flag;
    [SerializeField] private BotCollection _bots;
    [SerializeField] private ResourceBalance _balance;

    private InitializerBase _initializerBase;
    private ResourceServer _resourceServer;

    private CreatorBase _creatorBase;
    private CreatorBot _creatorBot;

    private CreateBaseStrategy _createBaseStrategy;
    private CreateBotStrategy _createBotStrategy;
    private CreateStrategy _currentStrategy;

    private void OnDisable()
    {
        _resourceServer.AddedFreeResource -= SendToCollectResource;
        _flag.Deactivated -= OnFlagDeactivate;
    }

    public void Initialize(InitializerBase initializer, ResourceServer resourceServer, CreatorBot creatorBot, CreatorBase creatorBase)
    {
        _initializerBase = initializer;
        _resourceServer = resourceServer;
        _creatorBot = creatorBot;
        _creatorBase = creatorBase;

        InitializeStrategy();

        _resourceServer.AddedFreeResource += SendToCollectResource;
        _flag.Deactivated += OnFlagDeactivate;
    }

    public void AddNewBot(Bot bot)
    {
        _bots.TryGetFreePosition(out Vector3 freePosition);

        _bots.AddNewBot(bot);

        bot.SetNewStartPosition(freePosition);
        bot.MoveToStartPosition();
    }

    public bool TryGetFlag(out Flag flag)
    {
        int minCountCharacters = 2;
        flag = null;

        if (_bots.BotsCount < minCountCharacters)
            return false;

        flag = _flag;

        if (_currentStrategy != _createBaseStrategy)
            ChangeStrategy();

        return true;
    }

    private void OnFlagDeactivate()
    {
        if (_currentStrategy != _createBotStrategy)
            ChangeStrategy();
    }

    private void ChangeStrategy()
    {
        if (_currentStrategy == _createBotStrategy)
            _currentStrategy = _createBaseStrategy;
        else
            _currentStrategy = _createBotStrategy;
    }

    private void SendToCollectResource()
    {
        if (_resourceServer.FreeResourcesCount > _bots.FreeBotsCount)
            DistributeRecources(_bots.FreeBotsCount);
        else
            DistributeRecources(_resourceServer.FreeResourcesCount);
    }

    private void DistributeRecources(int characterCount)
    {
        for (int i = 0; i < characterCount; i++)
        {
            if (_resourceServer.TryGetFreeResourse(out Resource resource) == false)
                return;

            if (_bots.TryGetFreeBot(out Bot bot) == false)
                return;

            bot.MoveToResource(resource);
            bot.ReturnToStartPosition += TryTakeResource;
        }
    }

    private void TryTakeResource(Bot bot)
    {
        bot.ReturnToStartPosition -= TryTakeResource;

        if (_bots.TryReturnFreeBot(bot) == false)
            return;

        if (bot.HaveResource == false)
        {
            bot.Wait();
        }
        else
        {
            _balance.Add();
            _currentStrategy.AddResource();
            bot.PassOnResource();

            TryCreate();
        }
    }

    private void TryCreate()
    {
        if (_currentStrategy.CanCreate)
            _currentStrategy.Create();
    }

    public void CreateBase()
    {
        Base newBase = _creatorBase.Create(_flag.CurrentPosition);
        _initializerBase.InitializeBase(newBase);
        _flag.Deactivate();

        Bot bot = _bots.DeleteBot();

        newBase.AddNewBot(bot);
    }

    public void CreateBot()
    {
        if (_bots.TryGetFreePosition(out Vector3 createPosition) == false)
            return;

        Bot bot = _creatorBot.Create(createPosition);
        bot.SetNewStartPosition(createPosition);
        _bots.AddNewBot(bot);
    }

    private void InitializeStrategy()
    {
        int requiredCountResourceForCreateBase = 5;
        int requiredCountResourceForCreateBot = 3;

        _createBaseStrategy = new(this, requiredCountResourceForCreateBase, _flag);
        _createBotStrategy = new(this, requiredCountResourceForCreateBot);

        _currentStrategy = _createBotStrategy;
    }
}