using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RBController : MonoBehaviour
{
    [SerializeField] float movementSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] Vector3 velocity;
    [SerializeField]GroundCheck groundCheck;

    List<Vector3> externalForces = new List<Vector3>();
    Rigidbody rb;

    //non mono -> 
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        InputManager.Instance.OnJumpDown.AddListener(Jump);
    }

    private void LateUpdate()
    {
        SetVelocity();
    }
    public void AddExternalForce(Vector3 givenForce) //send direction multiplied by force
    {
        externalForces.Add(givenForce);
    }


    private void SetVelocity()
    {
        velocity = new Vector2(InputManager.Instance.GetMoveVector().x * movementSpeed, rb.velocity.y);
        foreach (var item in externalForces)
        {
            velocity += item;
        }
        externalForces.Clear();
        rb.velocity = velocity;
    }

    private void Jump()
    {
        if (!groundCheck.IsGrounded())
            return;

        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    

}
