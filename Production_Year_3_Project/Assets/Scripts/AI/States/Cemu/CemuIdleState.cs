using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
public class CemuIdleState : BaseCemuState
{
    public override BaseState RunCurrentState()
    {
        Debug.Log("Cemu Idle State");
        if (_cemu.NoticePlayerDistance.InitAction(new DistanceData(
            _cemu.MiddleOfBody.position, _cemuStateHandler.PlayerManager.MiddleOfBody.position)))
        {
            if (_cemu.BoundsXDistanceAction.InitAction(new DistanceData(_cemu.MiddleOfBody.position, _cemu.BoundHandler.Bound.max))
|| _cemu.BoundsXDistanceAction.InitAction(new DistanceData(_cemu.MiddleOfBody.position, _cemu.BoundHandler.Bound.min)))
            {
                return _cemuStateHandler.PatrolState;
            }
            //check if player is in sight
            if (_cemu.HasDirectLineToPlayer(_cemu.NoticePlayerDistance.Distance))
                return _cemuStateHandler.CombatState;
        }
        return _cemuStateHandler.PatrolState;
    }

    public override void EnterState()
    {
        base.EnterState();
        _cemuStateHandler.RefEnemy.AnimatorHandler.Animator.SetFloat(
            AnimatorHelper.GetParameter(AnimatorParameterType.Speed),0);
    }
}
