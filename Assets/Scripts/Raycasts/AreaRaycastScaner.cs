using UnityEngine;

public class AreaRaycastScaner : MonoBehaviour
{
    [SerializeField] private Base _base;
    [SerializeField] private LayerMask _layerMask;

    private Vector3 _centerBoxCast;
    private Vector3 _halfSizeBase;

    private void Awake()
    {
        _halfSizeBase = _base.transform.localScale / 2;
    }

    public void SetCenterBoxCast(Vector3 center) =>
        _centerBoxCast = center;


    public bool TryTakePosition()
    {
        Collider[] colliders;

        colliders = Physics.OverlapBox(_centerBoxCast, _halfSizeBase, Quaternion.identity, _layerMask);

        if (colliders.Length > 1)
        {
            return false;
        }
        else
        {
            if (colliders[0].TryGetComponent(out Ground _))
                return true;
            else
                return false;
        }
    }
}