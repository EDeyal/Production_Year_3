using UnityEngine;

public class QovaxChargeState : BaseQovaxState
{
    public override BaseState RunCurrentState()
    {
        //Debug.Log("Qovax Charge State");
        //stay in charge state until finished charge
        if (_qovax.Charge())
        {
            //from charge state he will have fatigue state
            //he will move only small movements in it
            return _qovaxStateHandler.FatigueState;
        }
        return this;
    }
    public override void EnterState()
    {
        base.EnterState();
        _qovax.AnimatorHandler.Animator.SetBool(AnimatorHelper.GetParameter(AnimatorParameterType.IsCharging), true);
    }
    public override void ExitState()
    {
        base.ExitState();
        _qovax.AnimatorHandler.Animator.SetBool(AnimatorHelper.GetParameter(AnimatorParameterType.IsCharging), false);
        _qovax.ResetCharge();
    }
}
