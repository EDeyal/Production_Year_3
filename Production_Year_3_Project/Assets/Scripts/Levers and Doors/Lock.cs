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
    [SerializeField] private GameObject key;

    private void Awake()
    {
        CheckValidation();
        _emisionHandler.Deactivate();
    }
    public void ActivateLock()
    {
        _emisionHandler.Activate();
        lockAnimator.SetTrigger("Unlock");
        StartCoroutine(Timer());
    }
    public IEnumerator Timer()
    {
        yield return new WaitForSeconds(_timer);
        myDoor.OpenDoor();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetHashCode() == GameManager.Instance.PlayerManager.gameObject.GetHashCode())
        {
            GameManager.Instance.InputManager.OnOpenDoor.AddListener(StartOpeningDoor);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetHashCode() == GameManager.Instance.PlayerManager.gameObject.GetHashCode())
        {
            GameManager.Instance.InputManager.OnOpenDoor.RemoveListener(StartOpeningDoor);
        }
    }
    private void StartOpeningDoor()
    {
        if (myDoor.CanOpen)
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

    public void KeyOn()
    {
        key.SetActive(true);
    }
}
