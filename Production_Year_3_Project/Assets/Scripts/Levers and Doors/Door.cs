using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour ,ICheckValidation
{
    [SerializeField] bool isOpen = false;
    [SerializeField] Animator doorAnimator;

    public void AnimateDoor()
    {
        doorAnimator.SetTrigger("OpenUp");
        //if a leaver was activated
        //play DoorAnimation
    }

    public void CheckValidation()
    {
        //CheckAnimator
    }

    public void OpenDoor()
    {
        //if a door was interacted with and key is collected
        //play animate door method

    }
    //enum direction(up,down) 
}
