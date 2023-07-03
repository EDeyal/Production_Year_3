using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed;
    [SerializeField] private float lifeTime;
    [SerializeField] private DamageDealingCollider damageDealingCollider;
    [SerializeField] private Attack projectileAttack;
    [SerializeField, Range(0,1)] private float projectileAttackDamageMod;


    public DamageDealingCollider DamageDealingCollider { get => damageDealingCollider; }

    private void Awake()
    {
        damageDealingCollider.OnColliderDealtDamage.AddListener(TurnOff);
        damageDealingCollider.OnColliderHit.AddListener(TurnOff);
    }

    private void OnEnable()
    {
        StartCoroutine(LifeTimeCountDown());
    }
    private void OnDisable()
    {
        transform.eulerAngles = Vector3.zero;
    }

    private IEnumerator LifeTimeCountDown()
    {
        yield return new WaitForSeconds(lifeTime);
        TurnOff();
    }
    public void CacheStats(Attack givenAttack, DamageDealer dealer)
    {
        projectileAttack.DamageHandler = new DamageHandler();
        projectileAttack.DamageHandler.CopyDamageHandler(givenAttack.DamageHandler);
        projectileAttack.DamageHandler.AddModifier(projectileAttackDamageMod);
        DamageDealingCollider.CacheReferences(projectileAttack, dealer);
    }

    public void Blast(Vector3 direction, bool flip = false)
    {
        if (flip)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        rb.velocity = direction * speed;
    }

    private void TurnOff()
    {
        gameObject.SetActive(false);
    }
}
