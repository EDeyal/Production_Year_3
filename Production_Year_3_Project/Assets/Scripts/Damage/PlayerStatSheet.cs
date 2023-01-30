using UnityEngine;

public class PlayerStatSheet : StatSheet
{
    [SerializeField] private Attack meleeAttack;
    [SerializeField] private float meleeBaseDamage;
    [SerializeField] private float takeDamageKnockBackForce;
    [SerializeField, Range(1, 10)] private float knockBackDurationMultiplyer;
    public Attack MeleeAttack { get => meleeAttack; }
    public float TakeDamageKnockBackForce { get => takeDamageKnockBackForce; }
    public float KnockBackDuration { get => knockBackDurationMultiplyer; }

    public override void InitializeStats()
    {
        base.InitializeStats();
        meleeAttack.DamageHandler.OverrideBaseAmount(meleeBaseDamage);
    }
}
