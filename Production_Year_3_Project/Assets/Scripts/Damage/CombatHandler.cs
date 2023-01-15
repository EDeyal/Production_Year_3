using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CombatHandler
{
    [SerializeField] List<AttackWrapper> _attacksWrapper;
    [SerializeField] DamageDealer _damageDealer;
    [SerializeField] Damageable _damagable;
    public void Init()
    {
        foreach (var wrapper in _attacksWrapper)
        {
            wrapper.Init(_damageDealer);
        }
    }
}
