using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class DamageDealingCollider : MonoBehaviour
{
    private Attack attack;
    private DamageDealer damageDealer;
    private Collider myCollider;
    [SerializeField] private float intervalBetweenDamage = 0.5f;
    private float lastDamaged;


    public UnityEvent OnColliderDealtDamage;
    public UnityEvent OnColliderHit;

    [SerializeField] private bool applyKnockBack;

    public Collider MyCollider { get => myCollider; }
    private void Start()
    {
        lastDamaged = intervalBetweenDamage * -1;
    }
    public void CacheReferences(Attack givenAttack, DamageDealer dealer = null)
    {
        attack = givenAttack;
        damageDealer = dealer;
        myCollider = GetComponent<Collider>();
    }

    /*  private void OnTriggerEnter(Collider other)
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
              if (applyKnockBack)
              {
                  targetHit.OnTotalDamageCalcRecieve.AddListener(OnTakeDamageKnockBack);
              }
              OnColliderDealtDamage?.Invoke();
          }
      }*/

    private void OnTriggerStay(Collider other)
    {
        if (Time.time - lastDamaged < intervalBetweenDamage)
        {
            return;
        }
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
            if (applyKnockBack)
            {
                targetHit.OnTotalDamageCalcRecieve.AddListener(OnTakeDamageKnockBack);
            }
            OnColliderDealtDamage?.Invoke();
            lastDamaged = Time.time;
        }
        OnColliderHit?.Invoke();
    }

    private void OnTakeDamageKnockBack(Attack givenAttack, Damageable target)
    {
        if (givenAttack.DamageHandler.GetFinalMult() > 0)
        {
            Vector3 normalizedDir = new Vector3(target.transform.position.x - transform.position.x, 0, 0).normalized;
            target.Owner.ApplyKnockBack(normalizedDir);
        }
        target.OnTotalDamageCalcRecieve.RemoveListener(OnTakeDamageKnockBack);
    }
}
