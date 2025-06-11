using UnityEngine;

public class BasePreview : MonoBehaviour
{
    [SerializeField] private PreviewBaseObject _baseVizual;
    [SerializeField] private Renderer _baseRenderer;

    private Transform _transform;

    public void Initialize()=>
        _transform = transform;

    public void Activate() =>
        gameObject.SetActive(true);

    public void Deactivate() =>
        gameObject.SetActive(false);

    public void SetPosition(Vector3 position) =>
        _transform.position = position;

    public void ChangeColor(Color color)
    {
        color.a = 0.5f;

        _baseRenderer.material.color = color;
    }
}