using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CemuBoost", menuName = "Ability/Enemies/Cemu")]
public class CemuBoost : Ability
{
    [SerializeField] private float decayingHealthAmount;

    public override void Cast()
    {
        Owner.Effectable.ApplyStatusEffect(new CemuSpeedBoost());
        Owner.StatSheet.DecayingHealth.AddDecayingHealth(decayingHealthAmount);
    }


}
