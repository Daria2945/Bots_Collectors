using System.Collections;
using UnityEngine;

public class ShowerBasePreview : MonoBehaviour
{
    [SerializeField] private AreaRaycastScaner _areaRaycastScaner;
    [SerializeField] private BasePreview _basePreview;
    [SerializeField] private float _deleyShow = 0.1f;

    private Flag _flag;

    private Color _red = Color.red;
    private Color _green = Color.green;

    private Coroutine _coroutine;

    private void Awake()
    {
        _basePreview.Initialize();
    }

    public void SetFlag(Flag flag) =>
        _flag = flag;

    public void StartShow()
    {
        if (_coroutine != null)
            return;

        _basePreview.Activate();
        _coroutine = StartCoroutine(Showing());
    }

    public void StopShow()
    {
        if (_coroutine == null)
            return;

        StopCoroutine(_coroutine);
        _basePreview.Deactivate();

        _coroutine = null;
    }

    private IEnumerator Showing()
    {
        var wait = new WaitForSecondsRealtime(_deleyShow);

        while (enabled)
        {
            _basePreview.SetPosition(_flag.transform.position);
            _areaRaycastScaner.SetCenterBoxCast(_flag.transform.position);

            if (_areaRaycastScaner.TryTakePosition())
            {
                _basePreview.ChangeColor(_green);
            }
            else
            {
                _basePreview.ChangeColor(_red);
            }

            yield return wait;
        }
    }
}