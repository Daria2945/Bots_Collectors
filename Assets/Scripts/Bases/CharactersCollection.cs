using System.Collections.Generic;
using UnityEngine;

public class CharactersCollection : MonoBehaviour
{
    [SerializeField] private List<Transform> _freePositionsForCharacters;

    private List<Character> _allCharacters = new();
    private Queue<Character> _freeCharacters = new();

    public int CharactersCount => _allCharacters.Count;
    
    public int FreeCharacretsCount => _freeCharacters.Count;

    public void ReturnFreeCharacter(Character character)=>
        _freeCharacters.Enqueue(character);

    public bool TryGetFreeCharacter(out Character character)
    {
        character = null;

        if (_freeCharacters.Count == 0)
            return false;

        character = _freeCharacters.Dequeue();
        return true;
    }

    public bool TryGetFreePosition(out Transform freePosition)
    {
        freePosition = default;

        if (_freePositionsForCharacters.Count == 0)
            return false;

        freePosition = _freePositionsForCharacters[0];
        _freePositionsForCharacters.Remove(freePosition);

        return true;
    }

    public void AddNewCharacter(Character character)
    {
        if (_allCharacters.Contains(character))
            return;

        _allCharacters.Add(character);
        _freeCharacters.Enqueue(character);
    }

    public Character DeleteCharacter()
    {
        Character character;

        if (_freeCharacters.Count == 0)
        {
            character = _allCharacters[0];
            _allCharacters.Remove(character);
        }
        else
        {
            character = _freeCharacters.Dequeue();
            _allCharacters.Remove(character);
        }

        _freePositionsForCharacters.Add(character.StartPosition);

        return character;
    }
}