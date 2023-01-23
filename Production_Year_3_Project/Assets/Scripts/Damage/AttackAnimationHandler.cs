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
    [SerializeField] private float attackCoolDown;
    [SerializeField] private string animTrigger;
    [SerializeField] private string animTriggerRun;
    [SerializeField] private AnimationClip attackAnimation;
    [SerializeField] private Animator anim;
    private void Start()
    {
        GameManager.Instance.InputManager.OnBasicAttackDown.AddListener(AttackDownOn);
        GameManager.Instance.InputManager.OnBasicAttackUp.AddListener(AttackDownOff);
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
}
