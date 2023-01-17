using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DamageDealingCollider : MonoBehaviour
{
    private Attack attack;
    private DamageDealer damageDealer;
    private Collider myCollider;

    public Collider MyCollider { get => myCollider; }

    public void CacheReferences(Attack givenAttack, DamageDealer dealer = null)
    {
        attack = givenAttack;
        damageDealer = dealer;
        myCollider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Damageable targetHit = other.GetComponent<Damageable>();
        if (!ReferenceEquals(targetHit, null) && attack.CheckTargetValidity(targetHit.TargetType))
        {
            if (!ReferenceEquals(damageDealer, null))
            {
                targetHit.GetHit(attack, damageDealer);
            }
            else
            {
                targetHit.GetHit(attack);
            }
        }
    }
}
