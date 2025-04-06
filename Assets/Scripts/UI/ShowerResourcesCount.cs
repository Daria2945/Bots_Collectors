using TMPro;
using UnityEngine;

public class ShowerResourcesCount : MonoBehaviour
{
    private const string StartText = "Resources: ";

    [SerializeField] private ResourcesCounter _resourcesCounter;
    [SerializeField] private TMP_Text _text;

    private void OnEnable()
    {
        _resourcesCounter.ResourcesChanged += ShowResources;
    }

    private void OnDisable()
    {
        _resourcesCounter.ResourcesChanged -= ShowResources;
    }

    private void ShowResources(int resourcesCount)
    {
        _text.text = StartText + resourcesCount;
    }
}