using UnityEngine;

public class QovaxEvasionState : BaseQovaxState
{
    public override BaseState RunCurrentState()
    {
        Debug.Log("Qovax Evasion State");
        _qovax.RandomMovement();
        return _qovaxStateHandler.CombatState;
    }
}
