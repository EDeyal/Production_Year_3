using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpTrigger : MonoBehaviour,ICheckValidation
{
    [SerializeField] string _instructionText;
    [SerializeField] CheckDistanceAction _noticePlayerDistance;
    [SerializeField] CheckDistanceAction _closePlayerDistance;
    [SerializeField] bool _isOneTime;
    [SerializeField,ReadOnly] bool _hasBeenActivated;
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
            GameManager.Instance.UiManager.InstructionPopUp.UpdateInstructionText(_instructionText);
            
        }
        else if (!_closePlayerDistance.InitAction(new DistanceData(transform.position, GameManager.Instance.PlayerManager.MiddleOfBody.position)))
        {
            //close popup
            GameManager.Instance.UiManager.InstructionPopUp.CloseInstructionText();
            if (_isOneTime)
            {
                _hasBeenActivated = true;
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
}
