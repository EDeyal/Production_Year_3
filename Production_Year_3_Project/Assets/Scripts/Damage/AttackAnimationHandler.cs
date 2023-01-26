using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
public class AttackAnimationHandler : MonoBehaviour
{
    private float lastAttacked;

    private bool attackDown;
    private bool attackFinished;

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

    private void Start()
    {
        GameManager.Instance.InputManager.OnBasicAttackDown.AddListener(AttackDownOn);
        GameManager.Instance.InputManager.OnBasicAttackUp.AddListener(AttackDownOff);
        OnAttackPerformed.AddListener(SpawnSwordSlashVfx);
        lastAttacked = attackCoolDown * -1;
        attackFinished = true;
    }

    private void Update()
    {
        if (attackDown)
        {
            Attack();
        }
    }

    private void AttackDownOn()
    {
        attackDown = true;
    }

    private void AttackDownOff()
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
        Debug.Log("OnAttackPreformed");
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
    {
        SwordSlash slash = swordSlashOP.GetPooledObject();
        slash.gameObject.SetActive(true);
        slash.transform.position = vfxSpawnPoint.position;
        slash.Effect.Play();
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
        List<BaseEnemy> enemiesFound = new List<BaseEnemy>();
        foreach (var item in collidersFound)
        {
            BaseEnemy enemy = item.GetComponent<BaseEnemy>();
            if (!ReferenceEquals(enemy,null))
            {
                enemiesFound.Add(enemy);
            }
        }
        foreach (var item in enemiesFound)
        {
            item.Damageable.GetHit(meleeAttack, GameManager.Instance.PlayerManager.DamageDealer);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(rightAttackPos.position, attackRadius);
        Gizmos.DrawWireSphere(leftAttackPos.position, attackRadius);
    }
}
