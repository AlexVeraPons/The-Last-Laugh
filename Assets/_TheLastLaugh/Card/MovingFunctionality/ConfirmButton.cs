using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ConfirmButton : MonoBehaviour
{
    public Action OnConfirmButtonClicked;
    public void OnMouseClick(Vector2 mousePosition)
    {
        if (IsMouseOver(mousePosition))
        {
            OnConfirmButtonClicked?.Invoke();
        }
    }

    private bool IsMouseOver(Vector2 mousePosition)
    {
        return GetComponent<Collider2D>().OverlapPoint(mousePosition);
    }
}
