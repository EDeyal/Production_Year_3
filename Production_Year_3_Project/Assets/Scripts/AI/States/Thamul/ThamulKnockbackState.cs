using UnityEngine;

public class ThamulKnockbackState : BaseThamulState
{
    public override BaseState RunCurrentState()
    {
        Debug.Log("ThamulKnockBack");
        if (_thamul.CheckKnockbackEnemy())
        {
            return _thamulStateHandler.CombatState;
        }
        return this;
    }
    public override void EnterState()
    {
        base.EnterState();
        //knockback animation
    }
}
