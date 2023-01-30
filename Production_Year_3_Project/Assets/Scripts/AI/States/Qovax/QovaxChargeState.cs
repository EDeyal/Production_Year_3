using UnityEngine;

public class QovaxChargeState : BaseQovaxState
{
    public override BaseState RunCurrentState()
    {
        Debug.Log("Qovax Charge State");
        //stay in charge state until finished charge
        if (_qovax.Charge())
        {
            //from charge state he will have fatigue state
            //he will move only small movements in it
            return _qovaxStateHandler.FatigueState;
        }
        return this;
    }
}
