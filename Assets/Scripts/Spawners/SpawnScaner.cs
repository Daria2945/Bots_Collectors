using System.Collections.Generic;
using UnityEngine;

public class SpawnScaner : MonoBehaviour
{
    [Header("Scanning Area")]
    [SerializeField] private float _maxPositionX;
    [SerializeField] private float _minPositionX;
    [SerializeField] private float _maxPositionZ;
    [SerializeField] private float _minPositionZ;

    [Header("Setting Parameters Raycast")]
    [SerializeField] private float _raycastValueOriginY;
    [SerializeField] private float _rayLength;
    [SerializeField] private float _spaceBetweenObjects;
    [SerializeField] private LayerMask _layerMask;

    private List<Vector3> _freePositions = new();

    private void Awake()
    {
        ScanLavel();
    }

    public bool TryGetFreePosition(out Vector3 position)
    {
        position = default;

        if (_freePositions.Count > 0)
        {
            position = _freePositions[Random.Range(0, _freePositions.Count)];
            _freePositions.Remove(position);

            return true;
        }

        return false;
    }

    private void ScanLavel()
    {
        for (float x = _minPositionX; x < _maxPositionX; x += _spaceBetweenObjects)
        {
            for (float z = _minPositionZ; z < _maxPositionZ; z += _spaceBetweenObjects)
            {
                if (Physics.Raycast(new(x, _raycastValueOriginY, z), Vector3.down, out RaycastHit hit, _rayLength, _layerMask))
                {
                    if (hit.collider.TryGetComponent(out Ground _))
                    {
                        _freePositions.Add(hit.point);
                    }
                }
            }
        }
    }
}