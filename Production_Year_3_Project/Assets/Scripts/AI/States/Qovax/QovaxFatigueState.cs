using UnityEngine;
public class QovaxFatigueState : BaseQovaxState
{
    public override BaseState RunCurrentState()
    {
        Debug.Log("Qovax Fatige State");
        var qovax = (QovaxEnemy)_qovaxStateHandler.RefEnemy;
        if (qovax.CheckForFatigueCooldown())
        {
            return _qovaxStateHandler.CombatState;
        }
        return this;
    }
}
