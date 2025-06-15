using System.Collections.Generic;
using UnityEngine;

public class BotCollection : MonoBehaviour
{
    [SerializeField] private List<Transform> _transformFreePositions;

    private List<Vector3> _freePositions = new();

    private List<Bot> _allBots = new();
    private Queue<Bot> _freeBots = new();

    public int BotsCount => _allBots.Count;

    public int FreeBotsCount => _freeBots.Count;

    private void Awake()
    {
        for (int i = 0; i < _transformFreePositions.Count; i++)
            _freePositions.Add(_transformFreePositions[i].position);
    }

    public bool TryReturnFreeBot(Bot bot)
    {
        if (_allBots.Contains(bot) == false)
            return false;

        _freeBots.Enqueue(bot);

        return true;
    }

    public bool TryGetFreeBot(out Bot bot)
    {
        bot = null;

        if (_freeBots.Count == 0)
            return false;

        bot = _freeBots.Dequeue();

        return true;
    }

    public bool TryGetFreePosition(out Vector3 freePosition)
    {
        freePosition = default;

        if (_freePositions.Count == 0)
            return false;

        freePosition = _freePositions[0];
        _freePositions.Remove(freePosition);

        return true;
    }

    public void AddNewBot(Bot bot)
    {
        if (_allBots.Contains(bot))
            return;

        _allBots.Add(bot);
        _freeBots.Enqueue(bot);
    }

    public Bot DeleteBot()
    {
        Bot bot;

        if (_freeBots.Count == 0)
        {
            bot = _allBots[0];
            _allBots.Remove(bot);
        }
        else
        {
            bot = _freeBots.Dequeue();
            _allBots.Remove(bot);
        }

        _freePositions.Add(bot.StartPosition);

        return bot;
    }
}