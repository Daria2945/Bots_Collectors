using TMPro;
using UnityEngine;

public class ShowerResourcesBalance : MonoBehaviour
{
    private const string StartText = "Resources: ";

    [SerializeField] private ResourceBalance _resourceBalance;
    [SerializeField] private TMP_Text _text;

    private void OnEnable()
    {
        _resourceBalance.ValueChanged += ShowBalance;
    }

    private void OnDisable()
    {
        _resourceBalance.ValueChanged -= ShowBalance;
    }

    private void ShowBalance(int resourcesCount)
    {
        _text.text = StartText + resourcesCount;
    }
}