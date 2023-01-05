using UnityEngine;

public class PlayerStatSheet : StatSheet
{
    [SerializeField] private Attack meleeAttack;
    [SerializeField] private float meleeBaseDamage;
    public Attack MeleeAttack { get => meleeAttack; }

    public override void InitializeStats()
    {
        base.InitializeStats();
        meleeAttack.DamageHandler.OverrideBaseAmount(meleeBaseDamage);
    }
}
