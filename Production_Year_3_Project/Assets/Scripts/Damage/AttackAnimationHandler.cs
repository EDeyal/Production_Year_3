using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class AttackAnimationHandler : MonoBehaviour
{
    private float lastAttacked;

    private bool attackDown;
    private bool attackFinished;
    private bool canAttack;

    private float AttackAnimationDuration;


    public UnityEvent OnAttackPerformed;
    [SerializeField] private Attack meleeAttack;
    [SerializeField] private float attackCoolDown;
    [SerializeField] private string animTrigger;
    [SerializeField] private Animator anim;
    [SerializeField] private Transform vfxSpawnPoint;
    [SerializeField] private SwordSlashObjectPooler swordSlashOP;

    [SerializeField] private float attackRadius;
    [SerializeField] private Transform rightAttackPos;
    [SerializeField] private Transform leftAttackPos;
    [SerializeField] private LayerMask enemyHitLayer;

    public Transform VfxSpawnPoint { get => vfxSpawnPoint; }
    public Attack MeleeAttack { get => meleeAttack; }
    public bool CanAttack { get => canAttack; set => canAttack = value; }

    private void Start()
    {
        GameManager.Instance.InputManager.OnBasicAttackDown.AddListener(AttackDownOn);
        GameManager.Instance.InputManager.OnBasicAttackUp.AddListener(AttackDownOff);
        OnAttackPerformed.AddListener(SpawnSwordSlashVfx);
        lastAttacked = attackCoolDown * -1;
        attackFinished = true;
        CanAttack = true;
    }

    private void Update()
    {
        if (canAttack && attackDown)
        {
            Attack();
        }
    }

    private void AttackDownOn()
    {
        if (!CanAttack)
        {
            return;
        }
        attackDown = true;
    }

    public void AttackDownOff()
    {
        attackDown = false;
    }

    protected virtual void Attack()
    {
        //only call this if attack animation is finished + attackdown + cd finished
        if (Time.time - lastAttacked < attackCoolDown || !attackFinished)
        {
            return;
        }
        attackFinished = false;
        OnAttackPerformed?.Invoke();
    }

    public void SetLastAttacked(float givenTime)
    {
        lastAttacked = givenTime;
    }
    public void AttackFinishedTrue()
    {
        attackFinished = true;
    }

    private void SpawnSwordSlashVfx()
    {/*
        SwordSlash slash = swordSlashOP.GetPooledObject();
        slash.gameObject.SetActive(true);
        slash.transform.position = vfxSpawnPoint.position;
        slash.Effect.Play();*/
    }

    //call this from a specific frame in the animation, if the anim doesnt reach this frame the attack wont trigger
    public void MeleeAttackEvent()
    {
        Transform attackPos;
        if (GameManager.Instance.PlayerManager.PlayerController.facingRight)
        {
            attackPos = rightAttackPos;
        }
        else
        {
            attackPos = leftAttackPos;
        }
        Collider[] collidersFound = Physics.OverlapSphere(attackPos.position, attackRadius, enemyHitLayer);
        foreach (var item in collidersFound)
        {
            BaseCharacter enemy = item.GetComponent<BaseCharacter>();
            if (!ReferenceEquals(enemy, null) && GameManager.Instance.PlayerManager.EnemyProximitySensor.IsTargetLegal(enemy))
            {
                enemy.Damageable.GetHit(meleeAttack, GameManager.Instance.PlayerManager.DamageDealer);
            }
        }
    }
   /* private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(rightAttackPos.position, attackRadius);
        Gizmos.DrawWireSphere(leftAttackPos.position, attackRadius);
    }*/
}
