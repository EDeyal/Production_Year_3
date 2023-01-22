using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
public class AttackAnimationHandler : MonoBehaviour
{
    private float lastAttacked;
    
    
    private bool attackDown;

    private float AttackAnimationDuration;

    public UnityEvent OnAttackPerformed;
    [SerializeField] private float attackCoolDown;
    [SerializeField] private string animTrigger;
    [SerializeField] private AnimationClip attackAnimation;
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
        OnAttackPerformed?.Invoke();
    }
}
