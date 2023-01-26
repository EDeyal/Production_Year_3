using UnityEngine;

[CreateAssetMenu(fileName = "CemuBoost", menuName = "Ability/Enemies/Cemu")]
public class CemuDecayingHealthBoost : Ability
{
    [SerializeField] private float decayingHealthAmount;

    public override void Cast(BaseCharacter givenCharacter)
    {
        givenCharacter.Effectable.ApplyStatusEffect(new MovementSpeedBoost());
        givenCharacter.StatSheet.DecayingHealth.AddDecayingHealth(decayingHealthAmount);
    }


}
