using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatSheet : StatSheet
{
    [SerializeField] private Attack meleeAttack;
    [SerializeField] private float meleeBaseDamage;
    public Attack MeleeAttack { get => meleeAttack;}

    protected override void InitializeStats()
    {
        base.InitializeStats();
        meleeAttack.DamageHandler.OverrideBaseAmount(meleeBaseDamage);
    }
}
