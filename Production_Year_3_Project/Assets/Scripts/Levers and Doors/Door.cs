using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEditor;
using UnityEngine;

public class Door : MonoBehaviour, ICheckValidation
{

    [SerializeField] EmisionHandler _emisionHandler;
    [SerializeField] public BoxCollider PhyiscalCollider;
    [SerializeField] public bool CanOpen = false;
    [SerializeField] Animator doorAnimator;
    [SerializeField] public direction directionEnum;
    
    private void Awake()
    {
        _emisionHandler.Deactivate();
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
        _emisionHandler.Activate();
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
