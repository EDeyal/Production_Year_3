using System.Collections;
using UnityEngine;
using UnityEngine.Events;


public class PlayerDash : MonoBehaviour
{
    [SerializeField] CCController controller;
    [SerializeField] float dashCoolDown;
    [SerializeField] float dashDuration;
    [SerializeField] float dashSpeed;
    [SerializeField] Animator anim;
    [SerializeField] GroundCheck rightCheck;
    [SerializeField] GroundCheck leftCheck;

    float lastDashed;

    public UnityEvent OnDash;
    public UnityEvent OnDashEnd;

    bool dashDurationUp;

    private void Start()
    {
        lastDashed = dashCoolDown * -1;
        GameManager.Instance.InputManager.OnDashDown.AddListener(StartDash);
        OnDash.AddListener(controller.StartDashReset);
        OnDash.AddListener(RollAnim);
        OnDash.AddListener(TurnOnWallChecks);
        OnDashEnd.AddListener(controller.EndDashReset);
        OnDashEnd.AddListener(TurnOffWallChecks);
    }

    private void StartDash()
    {
        if (Time.time - lastDashed >= dashCoolDown)
        {
            StartCoroutine(Dash());
        }
    }

    private void RollAnim()
    {
        anim.SetTrigger("Roll");
    }

    IEnumerator Dash()
    {
        OnDash?.Invoke();
        StartCoroutine(DashCounter());
        controller.ResetVelocity(new Vector3(Mathf.Clamp(transform.rotation.y, -1, 1) * dashSpeed, 0, 0));
        yield return new WaitUntil(() => dashDurationUp || rightCheck.IsGrounded() || leftCheck.IsGrounded());
        lastDashed = Time.time;
        controller.ResetGravity();
        OnDashEnd?.Invoke();
    }

    IEnumerator DashCounter()
    {
        dashDurationUp = false;
        yield return new WaitForSecondsRealtime(dashDuration);
        dashDurationUp = true;

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

    

}
