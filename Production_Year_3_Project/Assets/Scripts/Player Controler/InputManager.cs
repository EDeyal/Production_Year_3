using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    ///public static InputManager Instance;


    private PlayerInputs input;

    public UnityEvent OnJumpDown;
    public UnityEvent OnJump;
    public UnityEvent OnJumpUp;

    public UnityEvent OnDashDown;

    public UnityEvent OnBasicAttackDown;
    public UnityEvent OnBasicAttackUp;

    public UnityEvent OnSpellCast;

    public UnityEvent OnLookDownDown;
    public UnityEvent OnLookDownUp;

    public bool LockInputs;
    private void Start()
    {
        input = new PlayerInputs();
        input.Enable();

        input.BasicActions.Jump.started += InvokeOnJumpDown;
        input.BasicActions.Jump.performed += InvokeOnJump;
        input.BasicActions.Jump.canceled += InvokeOnJumpUp;
        input.BasicActions.Dash.started += InvokeOnDashDown;
        input.BasicActions.BasicAttack.started += InvokeOnBasicAttackDown;
        input.BasicActions.BasicAttack.canceled += InvokeOnBasicAttackUp;
        input.BasicActions.SpellAttack.started += InvokeOnSpellCast;
        input.Camera.MoveDown.started += InvokeOnLookDown;
        input.Camera.MoveDown.canceled += InvokeOnLookDownUp;
    }

    public void InvokeOnLookDown(InputAction.CallbackContext obj)
    {
        if (LockInputs)
        {
            return;
        }
        OnLookDownDown?.Invoke();
    }
    public void InvokeOnLookDownUp(InputAction.CallbackContext obj)
    {
        if (LockInputs)
        {
            return;
        }
        OnLookDownUp?.Invoke();
    }
    public void InvokeOnSpellCast(InputAction.CallbackContext obj)
    {
        if (LockInputs)
        {
            return;
        }
        OnSpellCast?.Invoke();
    }

    public void InvokeOnBasicAttackDown(InputAction.CallbackContext obj)
    {
        if (LockInputs)
        {
            return;
        }
        OnBasicAttackDown?.Invoke();
    }

    public void InvokeOnBasicAttackUp(InputAction.CallbackContext obj)
    {
        if (LockInputs)
        {
            return;
        }
        OnBasicAttackUp?.Invoke();
    }
    public void InvokeOnDashDown(InputAction.CallbackContext obj)
    {
        if (LockInputs)
        {
            return;
        }
        OnDashDown?.Invoke();
    }

    public void InvokeOnJumpDown(InputAction.CallbackContext obj)
    {
        if (LockInputs)
        {
            return;
        }
        OnJumpDown?.Invoke();
    }

    public void InvokeOnJump(InputAction.CallbackContext obj)
    {
        if (LockInputs)
        {
            return;
        }
        OnJump?.Invoke();
    }

    public void InvokeOnJumpUp(InputAction.CallbackContext obj)
    {
        if (LockInputs)
        {
            return;
        }
        OnJumpUp?.Invoke();
    }
    public Vector2 GetMoveVector()
    {
        return input.BasicActions.Move.ReadValue<Vector2>();
    }
}
