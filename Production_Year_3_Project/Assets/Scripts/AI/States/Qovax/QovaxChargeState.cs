using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QovaxChargeState : BaseQovaxState
{
    public override BaseState RunCurrentState()
    {
        Debug.Log("Qovax Charge State");
        var qovax = (QovaxEnemy)_qovaxStateHandler.RefEnemy;
        if (qovax.CheckForChargeCooldown())
        {
            return _qovaxStateHandler.FatigueState;
        }
        //stay in charge state until finished charge
        //from charge state he will have fatigue state
        //he will move only small movements in it
        return this;
    }
}
