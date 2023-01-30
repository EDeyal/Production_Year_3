public class QovaxDeathState : BaseQovaxState
{
    public override BaseState RunCurrentState()
    {
        _qovax.OnDeath();
        return this;
    }
}
