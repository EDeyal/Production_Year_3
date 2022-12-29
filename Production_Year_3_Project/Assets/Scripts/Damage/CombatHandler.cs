using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CombatHandler
{
    [SerializeField] DamageDealingCollider _damageDealingCollider;
    [SerializeField] DamageDealer _damageDealer;
    [SerializeField] Damageable _damagable;
    List<Attack> _attacks;

    public void AddAttacks(List<Attack> attacks)
    {
        _attacks = attacks;
    }
    public void Init()
    {
        foreach (var item in _attacks)
        {
            _damageDealingCollider.CacheReferences(item, _damageDealer);
        }
    }
    public Attack GetAttackType(Attack attack)
    {
        foreach (var item in _attacks)
        {
            if (item == attack)
                return item;
        }
        return null;
    }
}
