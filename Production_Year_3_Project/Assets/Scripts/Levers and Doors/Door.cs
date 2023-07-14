using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Door : MonoBehaviour, ICheckValidation
{
    [SerializeField] bool CanOpen = false;
    [SerializeField] Animator doorAnimator;
    [SerializeField] public direction directionEnum;

    private void OnTriggerEnter(Collider other)
    {
        if (CanOpen == true && other.gameObject.CompareTag("Player"))
        {
            AnimateDoor();
        }
    }
    public void AnimateDoor()
    {
        switch (directionEnum)
        {
            case direction.up:
                doorAnimator.SetTrigger("OpenUp");
                break;
            case direction.down:
                doorAnimator.SetTrigger("OpenDown");
                break;
            default:
                break;
        }
    }
    public void OpenDoor()
    {
        AnimateDoor();

    }
    public void CheckValidation()
    {
        if (doorAnimator == null)
            throw new System.Exception($"Door  {gameObject.name} Has no Door Animator");

    }
    public enum direction
    {
        up,
        down
    }
}
