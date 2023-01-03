using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TestAbility", menuName = "Ability/Test")]

public class TestAbilityDash : Ability
{
    public override void Cast()
    {
        base.Cast();
        if (ReferenceEquals(Owner, null))
        {
            return;
        }
        Owner.Effectable.ApplyStatusEffect(new CemuSpeedBoost(), Owner.Effector);
    }




}
