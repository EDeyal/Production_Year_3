using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    ///public static InputManager Instance;


    private PlayerInputs input;
    public UnityEvent OnEscape;

    public UnityEvent OnJumpDown;
    public UnityEvent OnJump;
    public UnityEvent OnJumpUp;

    public UnityEvent OnDashDown;

    public UnityEvent OnBasicAttackDown;
    public UnityEvent OnBasicAttackUp;

    public UnityEvent OnSpellCast;

    public UnityEvent OnLookDownDown;
    public UnityEvent OnLookDownUp;
    public UnityEvent OnLookUpDown;
    public UnityEvent OnLookUpUp;
    public UnityEvent OnLookLeftDown;
    public UnityEvent OnLookLeftUp;
    public UnityEvent OnLookRightDown;
    public UnityEvent OnLookRightUp;
    public UnityEvent OnSavePoint;
    public UnityEvent OnToggleMap;


    //public bool LockInputs;
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
        input.BasicActions.Escape.started += InvokeEscape;
        input.Camera.MoveDown.started += InvokeOnLookDown;
        input.Camera.MoveDown.canceled += InvokeOnLookDownUp;
        input.Camera.MoveUp.started += InvokeOnLookUp;
        input.Camera.MoveUp.canceled += InvokeOnLookUpUp;
        input.Camera.MoveRight.started += InvokeOnLookRightDown;
        input.Camera.MoveRight.canceled += InvokeOnLookRightUp;
        input.Camera.MoveLeft.started += InvokeOnLookLeftDown;
        input.Camera.MoveLeft.canceled += InvokeOnLookLeftUp;
        input.BasicActions.SavePoint.started += InvokeSavePoint;
        input.BasicActions.ToggleMap.started += InvokeToggleMap;
    }

    public void InvokeToggleMap(InputAction.CallbackContext obj)
    {
        OnToggleMap?.Invoke();
    }
    public void SetCurserVisability(bool isVisible)
    {
        Cursor.visible = isVisible;
    }
    public void InvokeSavePoint(InputAction.CallbackContext obj)
    {
        OnSavePoint?.Invoke();
    }
    public void InvokeEscape(InputAction.CallbackContext obj)
    {
        Debug.Log("EscapedIsPressed");
        OnEscape?.Invoke();
    }
    public void InvokeOnLookLeftDown(InputAction.CallbackContext obj)
    {
        OnLookLeftDown?.Invoke();
    }

    public void InvokeOnLookLeftUp(InputAction.CallbackContext obj)
    {
        OnLookLeftUp?.Invoke();
    }

    public void InvokeOnLookRightUp(InputAction.CallbackContext obj)
    {
        OnLookRightUp?.Invoke();
    }

    public void InvokeOnLookRightDown(InputAction.CallbackContext obj)
    {
        OnLookRightDown?.Invoke();
    }

    public void InvokeOnLookUpUp(InputAction.CallbackContext obj)
    {
        OnLookUpUp?.Invoke();
    }
    public void InvokeOnLookUp(InputAction.CallbackContext obj)
    {

        OnLookUpDown?.Invoke();
    }
    public void InvokeOnLookDown(InputAction.CallbackContext obj)
    {

        OnLookDownDown?.Invoke();
    }
    public void InvokeOnLookDownUp(InputAction.CallbackContext obj)
    {

        OnLookDownUp?.Invoke();
    }
    public void InvokeOnSpellCast(InputAction.CallbackContext obj)
    {

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
