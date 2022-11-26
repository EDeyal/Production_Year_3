using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RBController : MonoBehaviour
{
    [SerializeField] float movementSpeed;
    [SerializeField] float jumpForce;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        InputManager.Instance.OnJumpDown.AddListener(Jump);
    }

    private void Update()
    {
        SetVelocity();
    }


    private void SetVelocity()
    {
        rb.velocity = new Vector2(InputManager.Instance.GetMoveVector().x * movementSpeed, rb.velocity.y);
    }

    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    

}
