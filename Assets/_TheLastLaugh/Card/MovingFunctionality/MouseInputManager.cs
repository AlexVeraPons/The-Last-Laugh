using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class MouseInputManager : MonoBehaviour
{
    public UnityEvent<Vector2> MouseMoved;
    public UnityEvent<Vector2> MouseLeftClicked;
    public UnityEvent<Vector2> MouseRightClicked;
    public UnityEvent<Vector2> MouseLeftReleased;
    public UnityEvent<Vector2> MouseRightReleased;

    private void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        MouseMoved?.Invoke(mousePosition);

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            MouseLeftClicked?.Invoke(mousePosition);
        }
        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            MouseRightClicked?.Invoke(mousePosition);
        }
        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            MouseLeftReleased?.Invoke(mousePosition);
        }
        if (Mouse.current.rightButton.wasReleasedThisFrame)
        {
            MouseRightReleased?.Invoke(mousePosition);
        }
    }
}
