using UnityEngine;

public class CreatorCharacter : Creator<Character>
{
    [SerializeField] private Vector3 _characterRotation;

    public override Character Create(Vector3 position)
    {
        var character = Instantiate(Prefab, position, Quaternion.Euler(_characterRotation));
        character.gameObject.SetActive(true);

        return character;
    }
}