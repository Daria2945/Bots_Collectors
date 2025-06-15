using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private InitializerBase _initializerBase;
    [SerializeField] private PlaceManager _placeManager;

    private void OnEnable()
    {
        _initializerBase.NewBaseInitialized += FindFreePlace;
    }

    private void OnDisable()
    {
        _initializerBase.NewBaseInitialized -= FindFreePlace;
    }

    private void FindFreePlace() =>
        _placeManager.FindFreePlace();
}