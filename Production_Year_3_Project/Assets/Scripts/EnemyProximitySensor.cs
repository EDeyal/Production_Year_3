using UnityEngine;


public class EnemyProximitySensor : ProximitySensor<BaseCharacter>
{
    public override bool Condition(BaseCharacter instance)
    {
        if (instance.Damageable.CurrentHp <=0)
        {
            return false;
        }
        return true;
    }
}
