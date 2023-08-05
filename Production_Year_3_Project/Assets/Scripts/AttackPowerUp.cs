using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPowerUp : PowerUp
{
    [SerializeField] private float attackPowerIncrease;
    protected override void OnPickedUp()
    {
        base.OnPickedUp();
        GameManager.Instance.PlayerManager.PlayerMeleeAttack.IncreaseAttackBoost(attackPowerIncrease);
        //GameManager.Instance.PlayerManager.PlayProgressionParticle(_progressionColor);
        GameManager.Instance.UiManager.ProgressionPopUp.CollectNewProgression(ProgressionType.Damage);
    }
}