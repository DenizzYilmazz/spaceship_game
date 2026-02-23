using UnityEngine;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance {  get; private set; }

    private InputActions InputActions;

    private void Awake()
    {
        Instance = this;

        InputActions = new InputActions();
        InputActions.Enable();
    }

    private void OnDestroy()
    {
        InputActions.Disable();
    }

    public bool IsUpActionPressed()
    {
        return InputActions.Player.LanderUp.IsPressed();
    }

    public bool IsRightActionPressed()
    {
        return InputActions.Player.LanderRight.IsPressed();
    }

    public bool IsLeftActionPressed()
    {
        return InputActions.Player.LanderLeft.IsPressed();
    }

    public Vector2 GetMovementInputVector2()
    {
        return InputActions.Player.Movement.ReadValue<Vector2>();
    }
}
