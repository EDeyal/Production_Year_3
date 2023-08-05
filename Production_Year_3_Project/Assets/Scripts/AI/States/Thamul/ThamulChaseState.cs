using UnityEngine;

public class ThamulChaseState : BaseThamulState
{
    public override BaseState RunCurrentState()
    {
        //Debug.Log("Thamul Chase State");
        //if the enemy is within melee range
        if (_thamul.ThamulMeleeDistance.InitAction(
            new DistanceData(_thamul.MiddleOfBody.position, _thamulStateHandler.PlayerManager.MiddleOfBody.position)))
        {
            return _thamulStateHandler.MeleeState;
        }
        //else if player is within the chase range, chase it
        else if (_thamul.ThamulRunAfterPlayerDistance.InitAction(
            new DistanceData(_thamul.MiddleOfBody.position, _thamulStateHandler.PlayerManager.MiddleOfBody.position)))
        {
            _thamul.Chase();
            return this;
        }
        //check for range attacks
        if (_thamul.ThamulRangeChaseDistance.InitAction(
            //check that thamul is in range
            new DistanceData(_thamul.MiddleOfBody.position, _thamulStateHandler.PlayerManager.MiddleOfBody.position)))
        {
            if (Mathf.Abs(_thamul.transform.position.y - _thamulStateHandler.PlayerManager.transform.position.y) < _thamul.HightDifferenceOffset)
            {
                return _thamulStateHandler.ShootState;
            }
            else
            {
                _thamul.Chase();
            }
        }
        //as long that the player is within thamul chase range he needs to chase it
        else if (_thamul.ChasePlayerDistance.InitAction(
            new DistanceData(_thamul.MiddleOfBody.position, _thamulStateHandler.PlayerManager.MiddleOfBody.position)))
        {
            _thamul.Chase();
            return this;
        }
        //go back to see if you need to take a different action
        return _thamulStateHandler.CombatState;
    }
    public override void EnterState()
    {
        base.EnterState();
        _thamul.AnimatorHandler.Animator.SetFloat(
            AnimatorHelper.GetParameter(AnimatorParameterType.Speed),
            _thamul.EnemyStatSheet.Speed);
    }
}
