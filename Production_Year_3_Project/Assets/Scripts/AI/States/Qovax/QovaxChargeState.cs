using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QovaxChargeState : BaseQovaxState
{
    public override BaseState RunCurrentState()
    {
        Debug.Log("Qovax Charge State");
        var qovax = (QovaxEnemy)_qovaxStateHandler.RefEnemy;
        //stay in charge state until finished charge
        if (qovax.Charge())
        {
            //from charge state he will have fatigue state
            //he will move only small movements in it
            return _qovaxStateHandler.FatigueState;
        }
        return this;
    }
}
