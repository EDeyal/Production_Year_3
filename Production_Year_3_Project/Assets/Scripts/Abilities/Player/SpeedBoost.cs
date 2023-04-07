using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SpeedBoost", menuName = "Ability/CemuDrop")]
public class SpeedBoost : Ability
{
    [SerializeField] private float decayingHealthAmount;
    public override void Cast()
    {
        Owner.Effectable.ApplyStatusEffect(new MovementSpeedBoost());
        //Owner.StatSheet.DecayingHealth.AddDecayingHealth(decayingHealthAmount);
    }
}
