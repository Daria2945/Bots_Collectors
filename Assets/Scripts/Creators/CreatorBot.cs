using UnityEngine;

public class CreatorBot : Creator<Bot>
{
    [SerializeField] private Vector3 _characterRotation;

    public override Bot Create(Vector3 position)
    {
        var bot = Instantiate(Prefab, position, Quaternion.Euler(_characterRotation));
        bot.gameObject.SetActive(true);

        return bot;
    }
}