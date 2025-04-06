using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private Character _prefabCharacter;
    [SerializeField] private float _startRotationYForCharacter;
    [SerializeField] private Transform[] _startPositionsForCharacters;

    [Space(15)]
    [SerializeField] private BaseScaner _scaner;
    [SerializeField] private float _delayCollection;

    [Space(15)]
    [SerializeField] private ResourcesCounter _resourcesCounter;

    private Queue<Resource> _resources = new();
    private Queue<Character> _characters = new();

    private void Awake()
    {
        TakeStartPositions();
        StartCoroutine(Collecting());
    }

    private void OnEnable()
    {
        _scaner.FindedResource += OnFindedResource;
    }

    private void OnDisable()
    {
        _scaner.FindedResource -= OnFindedResource;
    }

    private void OnFindedResource(Resource resource) =>
        _resources.Enqueue(resource);

    private void TakeStartPositions()
    {
        foreach (var position in _startPositionsForCharacters)
        {
            var character = Instantiate(_prefabCharacter, position.position, Quaternion.Euler(0, _startRotationYForCharacter, 0));

            character.gameObject.SetActive(true);
            _characters.Enqueue(character);
        }
    }

    private void CollectResource()
    {
        if (_resources.Count == 0)
            return;

        if (_characters.Count == 0)
            return;

        var character = _characters.Dequeue();
        character.SetTarget(_resources.Dequeue());

        character.ReturnToStartPosition += OnReturnToStartPosition;
    }

    private void OnReturnToStartPosition(Character character)
    {
        character.ReturnToStartPosition -= OnReturnToStartPosition;

        _characters.Enqueue(character);
        _resourcesCounter.AddResource();
    }

    private IEnumerator Collecting()
    {
        var wait = new WaitForSeconds(_delayCollection);

        while (enabled)
        {
            CollectResource();

            yield return wait;
        }
    }
}