using UnityEngine;

public class PlayerStatSheet : StatSheet
{
    [SerializeField] private Attack meleeAttack;
    [SerializeField] private float meleeBaseDamage;
    [SerializeField] private float airAttackGravityStopDuration;
    public Attack MeleeAttack { get => meleeAttack; }
    public float AirAttackGravityStopDuration { get => airAttackGravityStopDuration; }

    public override void InitializeStats()
    {
        base.InitializeStats();
        meleeAttack.DamageHandler.OverrideBaseAmount(meleeBaseDamage);
    }
}
