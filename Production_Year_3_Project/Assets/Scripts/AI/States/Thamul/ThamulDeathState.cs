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
        if (GameManager.Instance.SoundManager.isFunnySounds)
        {
            GameManager.Instance.SoundManager.PlaySound("ThamulDeathSoundTest");
        }
        else
        {
            GameManager.Instance.SoundManager.PlaySound("ThamulDeath");
        }
        _thamul.AnimatorHandler.Animator.SetTrigger(
            AnimatorHelper.GetParameter(AnimatorParameterType.IsDead));
    }
}
