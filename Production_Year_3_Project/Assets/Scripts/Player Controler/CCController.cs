using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CCController : MonoBehaviour
{
    [SerializeField] CharacterController controller;
    [SerializeField, ReadOnly] private float movementSpeed;
    private float startingMovementSpeed;


    [SerializeField] float jumpHeight;

    [SerializeField] Vector3 velocity;
    //[SerializeField] Vector3 gravity;

    [SerializeField] GroundCheck groundCheck;
    [SerializeField] GroundCheck ceilingDetector;


    bool canMove;
    bool useGravity;

    bool jumpPressed;
    bool jumpIsHeld;

    bool canHoldJump;
    bool canJump;

    bool jumped;

    bool coyoteAvailable;
    float jumpHeldTimer;
    [SerializeField] float jumpHeldTime;
    [SerializeField] int numberOfJumps;
    [SerializeField] float coyoteThreshold;
    [SerializeField, Range(0, 1)] float extraJumpsHeightModifier;
    int jumpsLeft;

    [SerializeField] float apexGravityScale;

    private float gravityForce = 9.81f;
    private float startingGravityScale;
    [SerializeField] float gravityScale;
    [SerializeField] float maxGravity;
    [SerializeField, Range(0, 1)] float midAirAttackStopDuration;
    private bool midAirAttackUsed;

    [SerializeField] AnimationHandler animBlender;
    bool isFalling => DistanceFromPreviousPos().y < 0 && !groundCheck.IsGrounded();

    public Vector3 Velocity { get => velocity; }
    public float MovementSpeed { get => movementSpeed; set => movementSpeed = value; }
    public bool CanMove { get => canMove; set => canMove = value; }

    Vector3 oldPos;

    List<Vector3> externalForces = new List<Vector3>();
    public UnityEvent<Vector3> OnRecieveForce;
    public UnityEvent OnJump;

    public bool facingRight;
    private void Start()
    {
        //inputs
        GameManager.Instance.InputManager.OnJumpDown.AddListener(Jump);
        GameManager.Instance.InputManager.OnJump.AddListener(HoldJump);
        GameManager.Instance.InputManager.OnJumpUp.AddListener(ReleaseJumpHeld);

        startingMovementSpeed = movementSpeed;


        groundCheck.OnGrounded.AddListener(ResetVelocity);
        groundCheck.OnGrounded.AddListener(ResetCanJump);
        groundCheck.OnGrounded.AddListener(ResetJumpsLeft);
        groundCheck.OnGrounded.AddListener(ResetJumped);
        groundCheck.OnGrounded.AddListener(ResetJumpHeldTimer);
        groundCheck.OnGrounded.AddListener(LandAnim);
        groundCheck.OnGrounded.AddListener(ResetMidAirAttackUsed);

        OnJump.AddListener(ResetCanHoldJump);
        OnJump.AddListener(JumpAnim);

        groundCheck.OnNotGrounded.AddListener(StartCoyoteTime);

        ceilingDetector.OnGrounded.AddListener(CeilingReset);

        OnRecieveForce.AddListener(ApplyExtrenalForces);

        startingGravityScale = gravityScale;
        useGravity = true;
        canMove = true;
    }

    private void Update()
    {
        if (!canMove)
        {
            return;
        }
        SetInputVelocity();
        ApplyGravity();

    }
    private void LateUpdate()
    {
        MoveController();
    }
    private void SetInputVelocity()
    {
        velocity.x = GameManager.Instance.InputManager.GetMoveVector().x * movementSpeed;
        animBlender.SetSpeed((int)Mathf.Abs(GameManager.Instance.InputManager.GetMoveVector().x));
        if (jumpPressed)
        {
            Debug.Log("jumped");
            if (!IsFirstJump())
            {
                DisableGravity();
            }
            velocity.y = Mathf.Sqrt(jumpHeight * 2f * gravityForce);
            jumpPressed = false;
            jumped = true;
            OnJump?.Invoke();
        }
        else if (jumpIsHeld && jumpHeldTimer > 0)
        {
            Debug.Log("jump was held");

            jumpHeldTimer -= Time.deltaTime;
            velocity.y = Mathf.Sqrt(jumpHeight * 2f * gravityForce);
            ResetGravity();
        }
    }
    private void ApplyGravity()
    {
        if (!useGravity)
        {
            return;
        }
        if (groundCheck.IsGrounded())
        {
            //gravity = Vector3.zero;
        }
        else
        {
            velocity.y -= gravityForce * gravityScale * Time.deltaTime;
        }
        velocity.y = Mathf.Clamp(velocity.y, maxGravity * -1, 100);

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
        if (canJump && (groundCheck.IsGrounded() || coyoteAvailable || (jumpsLeft > 0 && jumped)))
        {
            jumpsLeft--;
            if (jumpsLeft >= 0)
            {
                jumpPressed = true;
            }
            else
            {
                canJump = false;
            }
        }
    }

    private void HoldJump()
    {
        if (!groundCheck.IsGrounded() && canHoldJump && !ceilingDetector.IsGrounded() && jumped)
        {
            ResetGravity();
            canHoldJump = false;
            jumpIsHeld = true;
        }
    }

    private void ReleaseJumpHeld()
    {
        if (jumpIsHeld)
        {
            jumpIsHeld = false;
        }

        if (jumpsLeft > 0)
        {
            //cheking if can jump again
            canHoldJump = true;
            canJump = true;
            ResetJumpHeldTimer();
            jumpHeldTimer *= extraJumpsHeightModifier;
        }
        else
        {
            canHoldJump = false;
        }
    }

    private void ResetJumpHeldTimer()
    {
        jumpHeldTimer = jumpHeldTime;
    }

    public void ResetGravity()
    {
        velocity.y = Mathf.Clamp(velocity.y, 0, 100);
        gravityScale = startingGravityScale;
    }

    public void StartDashReset()
    {
        canMove = false;
        ResetGravity();
        ResetVelocity();
    }

    public void EndDashReset()
    {
        canMove = true;
        ResetVelocity();
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


    private void CeilingReset()
    {
        ResetVelocityY();
        ReleaseJumpHeld();
    }
    private void ResetJumpsLeft()
    {
        jumpsLeft = numberOfJumps;
    }

    private void ResetCanJump()
    {
        canJump = true;
    }

    public void DisableGravity()
    {
        StartCoroutine(JumpGravityDisable());
    }
    IEnumerator JumpGravityDisable()
    {
        useGravity = false;
        yield return new WaitForEndOfFrame();
        useGravity = true;
        ResetGravity();
    }

    private void ResetCanHoldJump()
    {
        //canHoldJump = true;
        StartCoroutine(WaitUntilJumpIsntHeld());
    }

    private void ResetJumped()
    {
        Debug.Log("jumped reset");
        jumped = false;
        jumpIsHeld = false;
    }

    private bool IsFirstJump()
    {
        if (jumpsLeft < numberOfJumps - 1)
        {
            return false;
        }
        return true;
    }

    private void StartCoyoteTime()
    {
        if (!jumped) //if the character isnt jumping
        {
            StartCoroutine(CoyoteCounter());
        }
    }

    IEnumerator WaitUntilJumpIsntHeld()
    {
        canHoldJump = false;
        yield return new WaitUntil(() => !jumpIsHeld);
        canHoldJump = true;
    }

    IEnumerator CoyoteCounter()
    {
        coyoteAvailable = true;
        yield return new WaitForSecondsRealtime(coyoteThreshold);
        coyoteAvailable = false;
    }

    IEnumerator JumpApexWait()
    {
        yield return new WaitUntil(() => isFalling);
        gravityScale = 0;
        yield return new WaitForEndOfFrame();
        gravityScale = startingGravityScale;
    }

    public void AddForce(Vector3 force) //direction * force
    {
        externalForces.Add(force);
        OnRecieveForce?.Invoke(force);
    }

    private Vector3 DistanceFromPreviousPos()
    {
        if (ReferenceEquals(oldPos, null))
        {
            oldPos = transform.position;
            return Vector3.zero;
        }
        Vector3 olderpos = oldPos;
        oldPos = transform.position;
        return transform.position - olderpos;
    }

    private void JumpAnim()
    {
        animBlender.SetTrigger("Jumped");
    }

    private void LandAnim()
    {
        animBlender.SetTrigger("Landed");
    }
    private void ResetMidAirAttackUsed()
    {
        midAirAttackUsed = false;
    }

    public void MidAirGraivtyAttackStop()
    {
        if (groundCheck.IsGrounded() || midAirAttackUsed)
        {
            return;
        }
        StartCoroutine(MidAirAttackGravity());
    }

    public void SetSpeed(float givenSpeed)
    {
        movementSpeed = givenSpeed;
    }

    private IEnumerator MidAirAttackGravity()
    {
        midAirAttackUsed = true;
        useGravity = false;
        canMove = false;
        ResetVelocity();
        yield return new WaitForSecondsRealtime(midAirAttackStopDuration);
        useGravity = true;
        canMove = true;
        ResetGravity();
    }
}
