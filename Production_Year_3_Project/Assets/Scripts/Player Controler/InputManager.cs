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
    }
    public void InvokeOnSpellCast(InputAction.CallbackContext obj)
    {
        Debug.Log("casting spell");
        OnSpellCast?.Invoke();
    }

    public void InvokeOnBasicAttackDown(InputAction.CallbackContext obj)
    {
        OnBasicAttackDown?.Invoke();
    }

    public void InvokeOnBasicAttackUp(InputAction.CallbackContext obj)
    {
        OnBasicAttackUp?.Invoke();
    }
    public void InvokeOnDashDown(InputAction.CallbackContext obj)
    {
        OnDashDown?.Invoke();
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
