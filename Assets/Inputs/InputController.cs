using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    private Vector2 mousePosition;
    public UnityEvent<Vector2> MousePosChange;

    public void GetMousePos(InputAction.CallbackContext context)
    {
        mousePosition = context.ReadValue<Vector2>();
        PlaceTurret.Instance.SetMousePosition(mousePosition);
    }

    public void GetPutTurret(InputAction.CallbackContext context)
    {
        PlaceTurret.Instance.SetPutTurret(context.performed);
    }
    public Vector2 MousePosition => mousePosition;
}
