using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    public event Action<IInterectable> ClickedOnInterectable;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                if (hitInfo.collider.TryGetComponent(out IInterectable interectable))
                {
                    ClickedOnInterectable?.Invoke(interectable);
                }
            }
        }
    }
}