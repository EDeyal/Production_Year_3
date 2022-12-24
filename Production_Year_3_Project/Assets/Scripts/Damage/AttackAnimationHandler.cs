using UnityEngine;
using UnityEngine.Events;
public class AttackAnimationHandler : MonoBehaviour
{
    [SerializeField] private float attackCoolDown;
    private float lastAttacked;
    
    [SerializeField] private string animTrigger;
    
    private bool attackDown;

    [SerializeField] private Animator anim;

    private void Start()
    {
        GameManager.Instance.InputManager.OnBasicAttackDown.AddListener(AttackDownOn);
        GameManager.Instance.InputManager.OnBasicAttackUp.AddListener(AttackDownOff);
        lastAttacked = attackCoolDown * -1;
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
        anim.SetBool(animTrigger, false);
    }

    protected virtual void Attack()
    {
        if (Time.time - lastAttacked < attackCoolDown)
        {
            return;
        }
        lastAttacked = Time.time;
        anim.SetBool(animTrigger, true);
    }
}
