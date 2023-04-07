using UnityEngine;
using UnityEngine.Events;
public class AttackAnimationHandler : MonoBehaviour
{
    private float lastAttacked;

    private bool attackDown;
    private bool attackFinished;
    private bool canAttack;
    private float attackBoost = 1;



    public UnityEvent OnAttackPerformedVisual;
    public UnityEvent<Attack> OnAttackPerformed;
    [SerializeField] private Attack meleeAttack;
    [SerializeField] private float attackCoolDown;
    [SerializeField] private string animTrigger;
    [SerializeField] private Animator anim;
    [SerializeField] private Transform vfxSpawnPoint;
    [SerializeField] private SwordSlashObjectPooler swordSlashOP;

    [SerializeField] private float attackRadius;
    [SerializeField] private Transform rightAttackPos;
    [SerializeField] private Transform leftAttackPos;
    [SerializeField] private LayerMask HitLayer;

    public Transform VfxSpawnPoint { get => vfxSpawnPoint; }
    public Attack MeleeAttack { get => meleeAttack; }
    public bool CanAttack { get => canAttack; set => canAttack = value; }

    private void Start()
    {
        GameManager.Instance.InputManager.OnBasicAttackDown.AddListener(AttackDownOn);
        GameManager.Instance.InputManager.OnBasicAttackUp.AddListener(AttackDownOff);
        OnAttackPerformedVisual.AddListener(SpawnSwordSlashVfx);
        OnAttackPerformed.AddListener(AttackDamageBoost);
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


    public void IncreaseAttackBoost(float amount)
    {
        attackBoost += amount;
    }

    private void AttackDamageBoost(Attack meleeAttack)
    {
        meleeAttack.DamageHandler.AddModifier(attackBoost);
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
        OnAttackPerformedVisual?.Invoke();
        OnAttackPerformed?.Invoke(MeleeAttack);
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
        Collider[] collidersFound = Physics.OverlapSphere(attackPos.position, attackRadius, HitLayer);
        foreach (var item in collidersFound)
        {
            BaseCharacter target = item.GetComponent<BaseCharacter>();
            if (ReferenceEquals(target, null))
            {
                return;
            }
            else if (!ReferenceEquals(target, null) && target is BaseEnemy && GameManager.Instance.PlayerManager.EnemyProximitySensor.IsTargetLegal(target))
            {
                target.Damageable.GetHit(meleeAttack, GameManager.Instance.PlayerManager.DamageDealer);
            }
            else if (target is not BaseEnemy && GameManager.Instance.PlayerManager.EnemyProximitySensor.IsTargetLegal(target))
            {
                target.Damageable.GetHit(meleeAttack, GameManager.Instance.PlayerManager.DamageDealer);
            }
        }
    }
}
