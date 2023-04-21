public class ThamulMeleeState : BaseThamulState
{
    public override BaseState RunCurrentState()
    {
        if (_thamul.MeleeAttack())//will wait until finishing the attack
        {
            return _thamulStateHandler.CombatState;
        }
        return this;
    }
}