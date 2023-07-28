using UnityEngine;
public class QovaxDeathState : BaseQovaxState
{
    public override BaseState RunCurrentState()
    {
        //Debug.Log("Qovax Death State");
        _qovax.OnDeath();
        return this;
    }
    public override void EnterState()
    {
        base.EnterState();
        _qovax.RB.drag = 0;
        _qovax.AnimatorHandler.Animator.SetTrigger(
        AnimatorHelper.GetParameter(AnimatorParameterType.IsDead));
        GameManager.Instance.SoundManager.PlaySound("QovaxDeathSoundTest");
    }
}