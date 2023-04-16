using UnityEngine;

public class ThamulChaseState : BaseThamulState
{
    public override BaseState RunCurrentState()
    {
        Debug.Log("Thamul Chase State");
        //if the distance between thamul and the player is greater than X
        if (!_thamul.ThamulMeleeDistance.InitAction(
            new DistanceData(_thamul.MiddleOfBody.position, _thamulStateHandler.PlayerManager.MiddleOfBody.position)))
        {
            return _thamulStateHandler.CombatState;
        }
        else
        {
            _thamul.Chase();
            return this;
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
