using UnityEngine;

public class FlagController : MonoBehaviour
{
    [SerializeField] private InputHandler _inputHandler;
    [SerializeField] private AreaRaycastScaner _areaRaycastScaner;
    [SerializeField] private ShowerBasePreview _showerBasePreview;

    private Flag _currentFlag = null;

    private void OnEnable()
    {
        _inputHandler.ClickedOnBase += OnClickedOnBase;
        _inputHandler.ClickedOnGround += OnClickedOnGround;
    }

    private void OnDisable()
    {
        _inputHandler.ClickedOnBase -= OnClickedOnBase;
        _inputHandler.ClickedOnGround -= OnClickedOnGround;
    }

    private void OnClickedOnBase(Base @base)
    {
        if (_currentFlag != null)
            return;

        if (@base.TryGetFlag(out _currentFlag) == false)
            return;

        _currentFlag.Activate();
        _currentFlag.Lift();

        _currentFlag.Mover.StartWork();

        _showerBasePreview.SetFlag(_currentFlag);
        _showerBasePreview.StartShow();
    }

    private void OnClickedOnGround()
    {
        if (_currentFlag == null)
            return;

        _areaRaycastScaner.SetCenterBoxCast(_currentFlag.transform.position);

        if (_areaRaycastScaner.TryTakePosition() == false)
            return;


        _currentFlag.Mover.StopWork();
        _currentFlag.PutOnGround();

        _currentFlag = null;
        _showerBasePreview.StopShow();
    }
}