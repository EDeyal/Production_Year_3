using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class SwordSlash : DamageDealingCollider
{
    [SerializeField] private VisualEffect effect;

    public VisualEffect Effect { get => effect;  }

    private void Start()
    {
        CacheReferences(GameManager.Instance.PlayerManager.PlayerMeleeAttack.MeleeAttack, GameManager.Instance.PlayerManager.DamageDealer);
    }
}
