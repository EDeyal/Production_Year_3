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

    private void Start()
    {
        input = new PlayerInputs();
        input.Enable();

        input.BasicActions.Jump.started += InvokeOnJumpDown;
    }


    public void InvokeOnJumpDown(InputAction.CallbackContext obj)
    {
        OnJumpDown?.Invoke();
    }

    public Vector2 GetMoveVector()
    {
        return input.BasicActions.Move.ReadValue<Vector2>();
    }
}
