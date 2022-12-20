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

    private void Start()
    {
        StartCoroutine(WaitForMovingLeft());
    }

    IEnumerator WaitForMovingLeft()
    {
        yield return new WaitUntil(() => controller.Velocity.x < 0);
        objectToFlip.rotation = Quaternion.Euler(leftVector);
        controller.facingRight = false;
        StartCoroutine(WaitForMovingRight());
    }

    IEnumerator WaitForMovingRight()
    {
        yield return new WaitUntil(() => controller.Velocity.x > 0);
        objectToFlip.rotation = Quaternion.Euler(rightVector);
        controller.facingRight = true;
        StartCoroutine(WaitForMovingLeft());
    }

}
