using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DamageDealingCollider : MonoBehaviour
{
    [SerializeField] private Attack attack;

    //if you want the attack to trigger events on the attacker too put something in the inspector
    [SerializeField] private DamageDealer damageDealer;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger");
        Damageable targetHit = other.GetComponent<Damageable>();
        if (!ReferenceEquals(targetHit, null) && attack.CheckTargetValidity(targetHit.TargetType))
        {
            if (!ReferenceEquals(damageDealer, null))
            {
                targetHit.GetHit(attack, damageDealer);
                Debug.Log(targetHit.gameObject.name + "was hit for by " + damageDealer.gameObject.name);
            }
            else
            {
                targetHit.GetHit(attack);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("trigger stay");
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collision");
    }
}
