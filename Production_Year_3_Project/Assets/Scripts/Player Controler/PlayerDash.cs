using System.Collections;
using UnityEngine;
using UnityEngine.Events;


public class PlayerDash : MonoBehaviour
{
    [SerializeField] CCController controller;
    [SerializeField] float dashCoolDown;
    [SerializeField] float dashDuration;
    [SerializeField] float dashSpeed;

    float lastDashed;

    public UnityEvent OnDash;
    public UnityEvent OnDashEnd;

    private void Start()
    {
        lastDashed = dashCoolDown * -1;
        GameManager.Instance.InputManager.OnDashDown.AddListener(StartDash);
        OnDash.AddListener(controller.StartDashReset);
        OnDashEnd.AddListener(controller.EndDashReset);
    }

    private void StartDash()
    {
        if (Time.time - lastDashed >= dashCoolDown)
        {
            StartCoroutine(Dash());
            lastDashed = Time.time;
        }
    }

    IEnumerator Dash()
    {
        OnDash?.Invoke();
        controller.ResetVelocity(new Vector3(Mathf.Clamp(transform.rotation.y, -1, 1) * dashSpeed, 0, 0));
        yield return new WaitForSecondsRealtime(dashDuration);
        OnDashEnd?.Invoke();
    }


}
