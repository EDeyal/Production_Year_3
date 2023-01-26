using UnityEngine;
using UnityEngine.Events;
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
        Debug.Log("Attack finished");
        attackFinished = true;
    }

    private void SpawnSwordSlashVfx()
    {
        SwordSlash slash = swordSlashOP.GetPooledObject();
        slash.gameObject.SetActive(true);
        slash.transform.position = vfxSpawnPoint.position;
        slash.Effect.Play();
    }
}
