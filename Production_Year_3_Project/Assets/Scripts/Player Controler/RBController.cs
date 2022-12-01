using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RBController : MonoBehaviour
{
    [SerializeField] float movementSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] Vector3 velocity;
    [SerializeField] GroundCheck groundCheck;

    float gravity = 9.81f;
    [SerializeField] private float gravityScale;

    float horizontalInput;

    List<Vector3> externalForces = new List<Vector3>();
    [SerializeField] Rigidbody rb;

    private void Start()
    {
        if (ReferenceEquals(rb, null))
        {
            rb = GetComponent<Rigidbody>();
        }
        InputManager.Instance.OnJumpDown.AddListener(Jump);
    }

    private void LateUpdate()
    {
        SetVelocity();
    }

    private void Update()
    {
        CalcExternalForces();
        AddGravity();
        SetINputVelocity();
    }

    private void SetINputVelocity()
    {
        horizontalInput = InputManager.Instance.GetMoveVector().x;
    }

    private void Jump()
    {
        if (!groundCheck.IsGrounded())
            return;

        Vector3 force = Vector3.up * jumpForce;
        AddExternalForce(force);
    }

    private void CalcExternalForces()
    {
        foreach (var item in externalForces)
        {
            velocity += item;
        }
        externalForces.Clear();
    }

    private void AddGravity()
    {
        if (groundCheck.IsGrounded())
        {
            velocity.y = 0;
        }
        velocity -= new Vector3(0, gravity * gravityScale, 0); 
    }

    public void AddExternalForce(Vector3 force)
    {
        externalForces.Add(force);
    }

    private void SetVelocity()
    {
        rb.velocity = new Vector3(velocity.x + horizontalInput, velocity.y,0);
    }
}
