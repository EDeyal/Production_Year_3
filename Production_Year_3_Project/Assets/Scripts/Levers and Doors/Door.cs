using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Door : MonoBehaviour, ICheckValidation
{
    [SerializeField] public PopUpTrigger DoesntHaveKey;
    [SerializeField] public PopUpTrigger HaveKey;
    [SerializeField] public bool CanOpen = false;
    [SerializeField] Animator doorAnimator;
    [SerializeField] public direction directionEnum;

    private void OnTriggerStay(Collider other)
    {              
        if (CanOpen == true && Input.GetKeyDown(KeyCode.Z))
        {
            AnimateDoor();
            TurnOffPopUp(HaveKey);
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
    public void TurnOffPopUp(PopUpTrigger popUpTrigger)
    {
        popUpTrigger.gameObject.SetActive(false);
    }
    public void TurnOnPopUp(PopUpTrigger popUpTrigger)
    {
        popUpTrigger.gameObject.SetActive(true);
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
