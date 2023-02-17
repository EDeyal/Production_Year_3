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


    protected override bool CheckIfTargetIsInMeleeZone(BaseCharacter target)
    {
        float dist =  GeneralFunctions.CalcRange(target.MiddleOfBody.position, transform.position);
        if (dist <= meleeZone && Condition(target))
        {
            return true;
        }
        return false;
    }

}
