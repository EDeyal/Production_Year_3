using System.Collections;
using UnityEngine;
using UnityEngine.Events;


public class PlayerDash : MonoBehaviour
{
    [SerializeField] CCController controller;
    [SerializeField] float dashCoolDown;
    [SerializeField] float dashDuration;
    [SerializeField] float dashSpeed;
    [SerializeField] float dashApexTime;
    [SerializeField] Animator anim;
    [SerializeField] GroundCheck rightCheck;
    [SerializeField] GroundCheck leftCheck;

    float lastDashed;

    public UnityEvent OnDash;
    public UnityEvent OnDashEnd;

    bool dashDurationUp;
    bool canDash;

    int dashDir;

    public bool CanDash { get => canDash; set => canDash = value; }

    private void Start()
    {
        lastDashed = dashCoolDown * -1;
        GameManager.Instance.InputManager.OnDashDown.AddListener(StartDash);
        OnDash.AddListener(controller.StartDashReset);
        OnDash.AddListener(TurnOnWallChecks);
        OnDash.AddListener(DashAnimOn);
        OnDash.AddListener(controller.ReleaseJumpHeld);
        OnDashEnd.AddListener(controller.EndDashReset);
        OnDashEnd.AddListener(TurnOffWallChecks);
        OnDashEnd.AddListener(DashAnimOff);
        canDash = true;
    }

    private void StartDash()
    {
        if (Time.time - lastDashed >= dashCoolDown && canDash && controller.CanMove)
        {
            StartCoroutine(Dash());
        }
    }

    IEnumerator Dash()
    {
        OnDash?.Invoke();
        StartCoroutine(DashCounter());
        GameManager.Instance.PlayerManager.PlayerMeleeAttack.CanAttack = false;
        if (!controller.facingRight)
        {
            dashDir = -1;
        }
        else
        {
            dashDir = 1;
        }
        controller.ZeroGravity();
        controller.ResetVelocity(new Vector3(dashSpeed * dashDir, 0, 0));
        yield return new WaitUntil(() => dashDurationUp || rightCheck.IsGrounded() || leftCheck.IsGrounded());
        lastDashed = Time.time;
        controller.ResetGravity();
        GameManager.Instance.PlayerManager.PlayerMeleeAttack.CanAttack = true;
        yield return StartCoroutine(OnDashEndApex());
        OnDashEnd?.Invoke();
    }

    IEnumerator DashCounter()
    {
        dashDurationUp = false;
        yield return new WaitForSecondsRealtime(dashDuration);
        dashDurationUp = true;

    }

    IEnumerator OnDashEndApex()
    {
        controller.ResetVelocity();
        yield return new WaitForSecondsRealtime(dashApexTime);
    }
    private void TurnOnWallChecks()
    {
        rightCheck.gameObject.SetActive(true);
        leftCheck.gameObject.SetActive(true);
    }
    private void TurnOffWallChecks()
    {
        rightCheck.gameObject.SetActive(false);
        leftCheck.gameObject.SetActive(false);
    }

    private void DashAnimOn()
    {
        anim.SetBool("Dash", true);
    }
    private void DashAnimOff()
    {
        anim.SetBool("Dash", false);
    }

    public void ResetDashCoolDoown(Ability givenAbility)
    {
        lastDashed = dashCoolDown * -1;
    }


}
