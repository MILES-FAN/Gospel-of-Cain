using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGameInput : MonoBehaviour
{
    PlayerControllerInput playerInputActions;

    Vector2 moveAxes => playerInputActions.Gameplay.Move.ReadValue<Vector2>();

    public bool jumpPressed => playerInputActions.Gameplay.Jump.WasPressedThisFrame();
    public bool jumpReleased => playerInputActions.Gameplay.Jump.WasReleasedThisFrame();
    public InputAction jumpAction => playerInputActions.Gameplay.Jump;
    public float horizontal => moveAxes.x;
    public bool isMoving => horizontal != 0f;

    public InputAction SnapAction => playerInputActions.Gameplay.Snap;

    public InputAction UIOKAction => playerInputActions.UI.OK;


    public InputAction AtkAction => playerInputActions.Gameplay.Throw;

    [HideInInspector]public bool isGrounded;

    [HideInInspector]public bool jumpOK;

    private void Awake()
    {
        playerInputActions = new PlayerControllerInput();
    }

    public void EnableGameplayInputs()
    {
        playerInputActions.Gameplay.Enable();
        playerInputActions.UI.Disable();
    }

    public void EnableUIInputs()
    {
        playerInputActions.UI.Enable();
        playerInputActions.Gameplay.Disable();
    }
}
