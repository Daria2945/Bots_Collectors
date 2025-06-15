using System.Collections.Generic;
using UnityEngine;

public class PlaceManager : MonoBehaviour
{
    [SerializeField] private FreePlaceFinder _placeFinder;

    private List<Vector3> _freePosition = new();
    private List<Vector3> _occupiedPosition = new();

    public void FindFreePlace()
    {
        _placeFinder.FindFreePlace();
        _freePosition = new List<Vector3>(_placeFinder.FreePlace);
    }

    public bool TryGetFreePosition(out Vector3 position)
    {
        position = default;

        if (_freePosition.Count == 0)
            return false;

        position = _freePosition[Random.Range(0, _freePosition.Count)];

        _freePosition.Remove(position);
        _occupiedPosition.Add(position);

        return true;
    }

    public void TryVacatePosition(Vector3 position)
    {
        if (_occupiedPosition.Count == 0)
            return;

        if (_occupiedPosition.Contains(position) == false)
            return;

        _occupiedPosition.Remove(position);
        _freePosition.Add(position);
    }
}