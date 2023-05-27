using UnityEngine;

public class ThamulShootState : BaseThamulState
{
    public override BaseState RunCurrentState()
    {
        Debug.Log("Thamul Shoot State");
        if (Mathf.Abs(_thamul.transform.position.y - _thamulStateHandler.PlayerManager.transform.position.y) < _thamul.HightDifferenceOffset)
        {
            if (_thamul.Shoot())
            {
                return _thamulStateHandler.CombatState;
            }
            else
            {
                return this;
            }
        }
        else
        {
            //_thamul.ResetProjectileCooldown();
            return _thamulStateHandler.ChaseState;
        }
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
