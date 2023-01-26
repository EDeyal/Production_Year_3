using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(CCController))]
public class CCFlip : MonoBehaviour
{
    [SerializeField] Vector3 rightVector;
    [SerializeField] Vector3 leftVector;
    [SerializeField] CCController controller;
    [SerializeField] Transform objectToFlip;
    Coroutine active;

    private void Start()
    {
        controller.facingRight = true;
        active = StartCoroutine(WaitForMovingLeft());
    }

    IEnumerator WaitForMovingLeft()
    {
        yield return new WaitUntil(() => controller.Velocity.x < 0);
        objectToFlip.rotation = Quaternion.Euler(leftVector);
        controller.facingRight = false;
        active = StartCoroutine(WaitForMovingRight());
    }

    IEnumerator WaitForMovingRight()
    {
        yield return new WaitUntil(() => controller.Velocity.x > 0);
        objectToFlip.rotation = Quaternion.Euler(rightVector);
        controller.facingRight = true;
        active = StartCoroutine(WaitForMovingLeft());
    }

    public void FlipRight()
    {
        if (!ReferenceEquals(active, null))
        {
            StopCoroutine(active);
        }
        objectToFlip.rotation = Quaternion.Euler(rightVector);
        active = StartCoroutine(WaitForMovingLeft());
    }
    public void FlipLeft()
    {
        if (!ReferenceEquals(active, null))
        {
            StopCoroutine(active);
        }
        objectToFlip.rotation = Quaternion.Euler(leftVector);
        active = StartCoroutine(WaitForMovingRight());
    }

}
