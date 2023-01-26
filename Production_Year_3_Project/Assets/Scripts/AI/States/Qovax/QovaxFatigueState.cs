using UnityEngine;
public class QovaxFatigueState : BaseQovaxState
{
    public override BaseState RunCurrentState()
    {
        Debug.Log("Qovax Fatige State");
        var qovax = (QovaxEnemy)_qovaxStateHandler.RefEnemy;
        if (qovax.CheckForFatigueCooldown())
        {
            qovax.IsFatigued = false;
            return _qovaxStateHandler.CombatState;
        }
        qovax.CheckFatigued();
        return this;
    }
}
