using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DashTowardsEnemy", menuName = "Ability/Thamu'l Drop")]
public class MeleeAttackProjectile : Ability
{
    private PlayerManager player => Owner as PlayerManager;
    public override void Cast()
    {
        player.Effectable.ApplyStatusEffect(new ProjectileOnMelee());
    }

}
