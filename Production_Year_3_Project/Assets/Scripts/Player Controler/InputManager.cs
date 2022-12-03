using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    private void Awake()
    {
        Instance = this;
        //for testing purposes
    }

    private PlayerInputs input;

    public UnityEvent OnJumpDown;
    public UnityEvent OnJump;
    public UnityEvent OnJumpUp;

    private void Start()
    {
        input = new PlayerInputs();
        input.Enable();

        input.BasicActions.Jump.started += InvokeOnJumpDown;
        input.BasicActions.Jump.performed += InvokeOnJump;
        input.BasicActions.Jump.canceled += InvokeOnJumpUp;
    }


    public void InvokeOnJumpDown(InputAction.CallbackContext obj)
    {

        OnJumpDown?.Invoke();
    }

    public void InvokeOnJump(InputAction.CallbackContext obj)
    {
        OnJump?.Invoke();
    }

    public void InvokeOnJumpUp(InputAction.CallbackContext obj)
    {
        OnJumpUp?.Invoke();
    }
    public Vector2 GetMoveVector()
    {
        return input.BasicActions.Move.ReadValue<Vector2>();
    }
}
