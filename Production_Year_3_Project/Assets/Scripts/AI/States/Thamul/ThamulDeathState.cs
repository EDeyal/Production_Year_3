using UnityEngine;

public class ThamulDeathState : BaseThamulState
{
    public override BaseState RunCurrentState()
    {
        Debug.Log("ThamulDeathState");
        _thamul.OnDeath();
        return this;
    }
    public override void EnterState()
    {
        base.EnterState();
        GameManager.Instance.SoundManager.PlaySound("ThamulDeathSoundTest");
        _thamul.AnimatorHandler.Animator.SetTrigger(
            AnimatorHelper.GetParameter(AnimatorParameterType.IsDead));
    }
}
