using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpTrigger : MonoBehaviour, ICheckValidation
{
    [SerializeField] string _instructionText;
    [SerializeField] CheckDistanceAction _noticePlayerDistance;
    [SerializeField] CheckDistanceAction _closePlayerDistance;
    [SerializeField] bool _isOneTime;
    [SerializeField, ReadOnly] bool _hasBeenActivated;
    [SerializeField, ReadOnly] bool _isActive;
    public void CheckValidation()
    {
        if (_noticePlayerDistance == null)
            throw new System.Exception("PopUpTrigger has no noticePlayerDistance");
        if (_closePlayerDistance == null)
            throw new System.Exception("PopUpTrigger has no closePlayerDistance");
    }

    private void Update()
    {
        if (_hasBeenActivated)
        {
            return;
        }
        if (_noticePlayerDistance.InitAction(new DistanceData(transform.position, GameManager.Instance.PlayerManager.MiddleOfBody.position)))
        {
            //init popup
            if (!_isActive)
            {
                GameManager.Instance.UiManager.InstructionPopUp.UpdateInstructionText(_instructionText);
                _isActive = true;
            }

        }
        else if (!_closePlayerDistance.InitAction(new DistanceData(transform.position, GameManager.Instance.PlayerManager.MiddleOfBody.position)))
        {
            //close popup
            if (_isOneTime && _isActive)
            {
                _hasBeenActivated = true;
            }
            if (_isActive)
            {
                GameManager.Instance.UiManager.InstructionPopUp.CloseInstructionText();
                _isActive = false;
            }
        }
        else
        {
            //keep popUp
        }
    }
    private void OnDrawGizmosSelected()
    {
        _noticePlayerDistance.DrawGizmos(transform.position);
        _closePlayerDistance.DrawGizmos(transform.position);
    }
    private void OnDisable()
    {
        if (GameManager.Instance)
        {
            GameManager.Instance.UiManager.InstructionPopUp.CloseInstructionText();
            _isActive = false;
        }
    }
}
