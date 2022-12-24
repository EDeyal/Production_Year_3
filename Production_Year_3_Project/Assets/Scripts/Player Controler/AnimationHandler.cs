using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AnimationHandler : MonoBehaviour
{
    [SerializeField] private Animator anim;

    private int lastSpeed;
    
    public void SetSpeed(int givenSpeed)
    {
        if (lastSpeed == givenSpeed)
        {
            return;
        }
        else if (givenSpeed == 1)
        {
            StartCoroutine(LerpAnim());
        }
        else if (givenSpeed == 0)
        {
            StartCoroutine(LerpAnimBackwards());
        }
        lastSpeed = givenSpeed;
    }
    IEnumerator LerpAnim()
    {
        float counter = 0;
        while (counter <= 1)
        {
            Mathf.Lerp(0f, 1f, counter);
            counter += Time.deltaTime;
            anim.SetFloat("Movement Speed", counter);
            yield return new WaitForEndOfFrame();
        }
    }
    IEnumerator LerpAnimBackwards()
    {
        float counter = 1;
        while (counter >= 0)
        {
            Mathf.Lerp(0f, 1f, counter);
            counter -= Time.deltaTime;
            anim.SetFloat("Movement Speed", counter);
            yield return new WaitForEndOfFrame();
        }
    }
}
