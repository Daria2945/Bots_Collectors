using UnityEngine;

public abstract class Creator<T> : MonoBehaviour where T : MonoBehaviour, ICreatable
{
    [SerializeField] private T _prefab;

    protected T Prefab => _prefab;

    public abstract T Create(Vector3 position);
}