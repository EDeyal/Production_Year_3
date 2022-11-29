using System.Collections.Generic;
using UnityEngine;

public class CCController : MonoBehaviour
{
    [SerializeField] CharacterController controller;
    [SerializeField] float movementSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] Vector3 velocity;
    [SerializeField] GroundCheck groundCheck;


    private float gravityForce = 9.81f;
    [SerializeField] float gravityScale;

    List<Vector3> externalForces = new List<Vector3>();


    private void Start()
    {
        InputManager.Instance.OnJumpDown.AddListener(Jump);
    }

    private void LateUpdate()
    {
        SetGravity();
        SetVelocity();
    }

    private void SetVelocity()
    {
        velocity = new Vector3(InputManager.Instance.GetMoveVector().x * movementSpeed, 0, 0);

        controller.Move(velocity * Time.deltaTime);
    }


    private void SetGravity()
    {
        if (groundCheck.IsGrounded())
        {
            return;
        }

        float gravity = gravityForce * gravityScale;
        controller.Move(new Vector3(0, controller.velocity.y - gravity, 0) * Time.deltaTime);
    }
    private void Jump()
    {
        if (!groundCheck.IsGrounded())
        {
            return;
        }

        controller.Move(Vector3.up * jumpForce * Time.deltaTime);
    }
}
