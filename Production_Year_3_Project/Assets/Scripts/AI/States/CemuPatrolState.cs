using UnityEngine;

public class CemuPatrolState : BaseCemuState
{

    public override BaseState RunCurrentState()
    {
        Debug.Log("Cemu Patrol State");
        if (_cemuStateHandler.RefEnemy.NoticePlayerDistance.InitAction(new DistanceData(_cemuStateHandler.RefEnemy.transform.position, _cemuStateHandler.PlayerManager.transform.position)))
        {
            ((GroundEnemy)_cemuStateHandler.RefEnemy).StopMovement();
            return _cemuStateHandler.CombatState;
        }
        ((GroundEnemy)_cemuStateHandler.RefEnemy).Patrol();
        return this;
    }
    public override void EnterState()
    {
        base.EnterState();
        _cemuStateHandler.RefEnemy.AnimatorHandler.Animator.SetFloat(AnimatorHelper.GetParameter(AnimatorParameterType.Speed),ONE);
        //add logic for animat
    }
    public override void ExitState()
    {
        base.ExitState();
        _cemuStateHandler.RefEnemy.AnimatorHandler.Animator.SetFloat(AnimatorHelper.GetParameter(AnimatorParameterType.Speed), ZERO);

    }
}
