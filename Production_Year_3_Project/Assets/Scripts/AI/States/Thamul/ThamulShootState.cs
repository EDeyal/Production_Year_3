using UnityEngine;

public class ThamulShootState : BaseThamulState
{
    public override BaseState RunCurrentState()
    {
        Debug.Log("Thamul Shoot State");
        if (_thamul.Shoot())
        {
            return _thamulStateHandler.CombatState;
        }
        return this;
    }
    public override void EnterState()
    {
        base.EnterState();
        //animations
    }
    public override void ExitState()
    {
        base.ExitState();
        //animations
    }
}
