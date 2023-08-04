using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Lock : MonoBehaviour, ICheckValidation
{
    [SerializeField] Door myDoor;
    [SerializeField] Animator lockAnimator;
    [SerializeField] EmisionHandler _emisionHandler;
    [SerializeField] public PopUpTrigger DoesntHaveKey;
    [SerializeField] public PopUpTrigger HaveKey;
    [SerializeField] float _timer;


    private void Awake()
    {
        CheckValidation();
        _emisionHandler.Deactivate();
    }
    public void ActivateLock()
    {
        _emisionHandler.Activate();
        //trigger animator
    }
    public IEnumerator Timer()
    {
        yield return new WaitForSeconds(_timer);
        myDoor.OpenDoor();
    }
    private void OnTriggerStay(Collider other)
    {
        if (myDoor.CanOpen == true && Input.GetKeyDown(KeyCode.C))
        {
            TurnOffPopUp(HaveKey);
            ActivateLock();
            StartCoroutine(Timer());
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
    public void CheckValidation()
    {
        if (_emisionHandler == null)
            throw new System.Exception($"Lock {gameObject.name} has no emision handler");
        if (lockAnimator == null)
            throw new System.Exception($"Lock {gameObject.name} has no animator");
        if (myDoor == null)
            throw new System.Exception($"Lock {gameObject.name} has no Door");
    }
}
