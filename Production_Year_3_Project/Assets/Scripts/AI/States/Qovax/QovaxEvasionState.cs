using UnityEngine;

public class QovaxEvasionState : BaseQovaxState
{
    public override BaseState RunCurrentState()
    {
        Debug.Log("Qovax Evasion State");
        ((FlyingEnemy)_qovaxStateHandler.RefEnemy).RandomMovement();
        return _qovaxStateHandler.CombatState;
    }
}
