using UnityEngine;

[System.Serializable]
public class AttackWrapper
{
    [SerializeField] DamageDealingCollider _damageDealingCollider;
    [SerializeField] Attack _attack;
    public void Init(DamageDealer damageDealer)
    {
        _damageDealingCollider.CacheReferences(_attack, damageDealer);
    }

}
