using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private ResourcePool _resourcePool;
    [SerializeField] private PlaceManager _placeManager;

    [SerializeField] private float _delaySpawn = 2f;

    private void Awake()
    {
        StartCoroutine(Spawning());
    }

    private void Spawn()
    {
        if (_placeManager.TryGetFreePosition(out Vector3 spawnPosition) == false)
            return;

        var resource = _resourcePool.Get();

        InitializeResourse(resource, spawnPosition);
    }

    private void InitializeResourse(Resource resource, Vector3 position)
    {
        resource.IncreaseIndex();
        resource.SetStartPosition(position);
        resource.gameObject.SetActive(true);

        resource.Destroyed += PutInPool;
    }

    private void PutInPool(Resource resource)
    {
        resource.Destroyed -= PutInPool;

        _placeManager.TryVacatePosition(resource.StartPosition);
        resource.gameObject.SetActive(false);
        resource.Reset();
        _resourcePool.Put(resource);
    }

    private IEnumerator Spawning()
    {
        var wait = new WaitForSecondsRealtime(_delaySpawn);

        while (enabled)
        {
            yield return wait;

            Spawn();
        }
    }
}