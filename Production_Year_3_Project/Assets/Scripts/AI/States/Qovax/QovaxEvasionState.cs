using UnityEngine;

public class QovaxEvasionState : BaseQovaxState
{
    public override BaseState RunCurrentState()
    {
        Debug.Log("Qovax Evasion State");
        return _qovaxStateHandler.CombatState;
    }
}
