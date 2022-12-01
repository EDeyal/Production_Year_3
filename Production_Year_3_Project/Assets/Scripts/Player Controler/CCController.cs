using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CCController : MonoBehaviour
{
    [SerializeField] CharacterController controller;
    [SerializeField] float movementSpeed;
    [SerializeField] float jumpHeight;

    [SerializeField] Vector3 velocity;
    [SerializeField] Vector3 gravity;

    [SerializeField] GroundCheck groundCheck;
    [SerializeField] GroundCheck ceilingDetector;

    bool jumpPressed;
    bool jumped;
    bool coyoteAvailable;
    [SerializeField] float coyoteThreshold;

    [SerializeField] float apexWaitTime;
    [SerializeField] float apexGravityScale;

    private float gravityForce = 9.81f;
    [SerializeField] float gravityScale;
    private float startingGravityScale;

    List<Vector3> externalForces = new List<Vector3>();
    public UnityEvent <Vector3>OnRecieveForce;

    private void Start()
    {
        InputManager.Instance.OnJumpDown.AddListener(Jump);
        groundCheck.OnGrounded.AddListener(ResetVelocity);
        groundCheck.OnNotGrounded.AddListener(StartCoyoteTime);
        ceilingDetector.OnGrounded.AddListener(ResetVelocityY);
        OnRecieveForce.AddListener(ApplyExtrenalForces);
        startingGravityScale = gravityScale;
    }

    private void Update()
    {
        SetInputVelocity();
        ApplyGravity();

    }
    private void LateUpdate()
    {
        MoveController();
    }
    private void SetInputVelocity()
    {
        velocity.x = InputManager.Instance.GetMoveVector().x * movementSpeed;

        if (jumpPressed)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * 2f * gravityForce);
            jumped = true;
            groundCheck.OnGrounded.AddListener(ResetJumped);
            jumpPressed = false;
            StartCoroutine(JumpApexWait());
        }
    }

    private void ApplyGravity()
    {
        if (groundCheck.IsGrounded())
        {
            gravity = Vector3.zero;
        }
        else
        {
            gravity.y -= gravityForce * gravityScale * Time.deltaTime;
        }
        controller.Move(gravity * Time.deltaTime);
    }
    private void MoveController()
    {
        controller.Move(velocity * Time.deltaTime);
    }

    private void ApplyExtrenalForces(Vector3 force)
    {
        Vector3 totalExternalForce = Vector3.zero;
        foreach (var item in externalForces)
        {
            totalExternalForce += item;
        }
        externalForces.Clear();
        controller.Move(totalExternalForce * Time.deltaTime);
    }

    private void Jump()
    {
        if (groundCheck.IsGrounded() || coyoteAvailable)
        {
            jumpPressed = true;
        }
    }

    public void ResetVelocity()
    {
        velocity = Vector3.zero;
    }

    public void ResetVelocityX()
    {
        velocity = new Vector2(0, velocity.y);

    }
    public void ResetVelocityY()
    {
        velocity = new Vector2(velocity.x, 0);
    }
    public void ResetVelocity(Vector3 givenVelocity)
    {
        velocity = givenVelocity;
    }

    private void ResetJumped()
    {
        jumped = false;
        groundCheck.OnGrounded.RemoveListener(ResetJumped);
    }

    private void StartCoyoteTime()
    {
        if (!jumped) //if the character isnt jumping
        {
            StartCoroutine(CoyoteCounter());
        }
    }

    IEnumerator CoyoteCounter()
    {
        coyoteAvailable = true;
        yield return new WaitForSecondsRealtime(coyoteThreshold);
        coyoteAvailable = false;
    }

    IEnumerator JumpApexWait()
    {
        yield return new WaitUntil(() => controller.velocity.y < 0);
        gravityScale = apexGravityScale;
        yield return new WaitForSecondsRealtime(apexWaitTime);
        gravityScale = startingGravityScale;
    }

    public void AddForce(Vector3 force)
    {
        externalForces.Add(force);
        OnRecieveForce?.Invoke(force);
    }

    [ContextMenu("force")]
    public void TestForce()
    {
        AddForce(Vector2.one * 5);
    }
}
