using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DamageDealingCollider : MonoBehaviour
{
     private Attack attack;
    //if you want the attack to trigger events on the attacker too put something in the inspector
     private DamageDealer damageDealer;

    public void CacheReferences(Attack givenAttack, DamageDealer dealer = null)
    {
        attack = givenAttack;
        damageDealer = dealer;
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
