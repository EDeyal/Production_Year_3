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

    private void Start()
    {
        StartCoroutine(WaitForMovingLeft());
    }

    IEnumerator WaitForMovingLeft()
    {
        yield return new WaitUntil(() => controller.Velocity.x < 0);
        transform.localScale = leftVector;
        StartCoroutine(WaitForMovingRight());
    }

    IEnumerator WaitForMovingRight()
    {
        yield return new WaitUntil(() => controller.Velocity.x > 0);
        transform.localScale = rightVector;
        StartCoroutine(WaitForMovingLeft());
    }

}
