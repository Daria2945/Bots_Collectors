using System.Collections;
using UnityEngine;

public class FlagMover : MonoBehaviour
{
    [SerializeField] private float _speedShowFlagOnNewPosition = 0.1f;

    private Transform _transform;

    private Coroutine _coroutine;
    private bool _isWork;

    private void Awake()
    {
        _transform = transform;
    }

    public void StartWork()
    {
        if (_isWork)
            return;

        _coroutine = StartCoroutine(Moving());
        _isWork = true;
    }

    public void StopWork()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _isWork = false;
    }

    private IEnumerator Moving()
    {
        var wait = new WaitForSecondsRealtime(_speedShowFlagOnNewPosition);

        while (enabled)
        {
            Vector3 mousePosition = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                if (hitInfo.collider.TryGetComponent(out IInterectable interectable))
                {
                    _transform.position = hitInfo.point;
                }
            }

            yield return wait;
        }
    }
}