using System.Collections.Generic;
using UnityEngine;

public class FreePlaceFinder : MonoBehaviour
{
    [SerializeField] private float _maxPositionX;
    [SerializeField] private float _minPositionX;
    [SerializeField] private float _maxPositionZ;
    [SerializeField] private float _minPositionZ;

    [SerializeField] private float _startPositionYRay;
    [SerializeField] private float _lengthRay;
    [SerializeField] private LayerMask _layerMask;

    [SerializeField] private float _spaceBetweenCentersOfObject;

    private List<Vector3> _freePlaces = new();

    public IEnumerable<Vector3> FreePlace => _freePlaces;

    public void FindFreePlace()
    {
        for (float x = _minPositionX; x <= _maxPositionX; x += _spaceBetweenCentersOfObject)
        {
            for (float z = _minPositionZ; z <= _maxPositionZ; z += _spaceBetweenCentersOfObject)
            {
                if (Physics.Raycast(new Vector3(x, _startPositionYRay, z), Vector3.down, out RaycastHit hit, _lengthRay, _layerMask))
                {
                    if (hit.collider.TryGetComponent(out Ground _))
                        _freePlaces.Add(hit.point);
                }
            }
        }
    }
}