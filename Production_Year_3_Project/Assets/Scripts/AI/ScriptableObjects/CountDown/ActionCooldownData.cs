public class ActionCooldownData
{
    ActionCooldown _cooldownTask;
    public ActionCooldown CooldownTask => _cooldownTask;
    public ActionCooldownData(ref ActionCooldown cooldownTask)//need reference to the current enemy
    {
        _cooldownTask = cooldownTask;
    }
}
