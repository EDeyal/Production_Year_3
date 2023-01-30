using UnityEngine;
public class QovaxFatigueState : BaseQovaxState
{
    public override BaseState RunCurrentState()
    {
        //Debug.Log("Qovax Fatige State");
        if (_qovax.CheckForFatigueCooldown())
        {
            _qovax.IsFatigued = false;
            return _qovaxStateHandler.CombatState;
        }
        _qovax.CheckFatigued();
        return this;
    }
}
