using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private ResourcePool _pool;
    [SerializeField] private SpawnScaner _scaner;
    [SerializeField] private float _delay;

    private void Start()
    {
        StartCoroutine(Spawning());
    }

    private void Spawn()
    {
        if (_scaner.TryGetFreePosition(out Vector3 spawnPosition))
        {
            var resource = _pool.GetResource();

            resource.transform.position = spawnPosition;
            resource.gameObject.SetActive(true);

            resource.Destroyed += ReturnInPool;
        }
    }

    private void ReturnInPool(Resource resource)
    {
        resource.Destroyed -= ReturnInPool;

        resource.gameObject.SetActive(false);
        resource.Reset();

        _pool.PutResource(resource);
    }

    private IEnumerator Spawning()
    {
        var wait = new WaitForSeconds(_delay);

        while (enabled)
        {
            yield return wait;

            Spawn();
        }
    }
}