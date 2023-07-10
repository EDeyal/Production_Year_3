using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] bool isOpen = false;
    [SerializeField] Animation DoorAnimation;

    public void AnimateDoor()
    {
        //if a leaver was activated
        //play DoorAnimation
    }
    public void OpenDoor()
    {
        //if a door was interacted with and key is collected
        //play animate door method

    }
}
