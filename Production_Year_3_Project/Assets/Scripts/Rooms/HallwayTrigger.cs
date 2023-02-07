using Sirenix.OdinInspector;
using System;
using UnityEngine;

public class HallwayTrigger: MonoBehaviour, ICheckValidation
{
    public event Action OnEnteredTrigger;
    public BoxCollider Sensor;
    public RoomHandler ClosestRoom;
    [ReadOnly] [SerializeField] int _targetlayerValue;
    public int TargetLayerValue
    {
        set
        {
            float rawLayerValue = value;
            rawLayerValue = Mathf.Log(rawLayerValue, 2);
            _targetlayerValue = (int)rawLayerValue;
            //Debug.Log("Looking for layer:  " +_targetlayerValue);
        }
    }
    private void Awake()
    {
        CheckValidation();
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log($"Something with layer {other.gameObject.layer} entered trigger");
        if (other.gameObject.layer == _targetlayerValue)
        {
            //Debug.Log("target entred a room trigger");
            OnEnteredTrigger.Invoke();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        //Debug.Log($"Something with layer {other.gameObject.layer} entered trigger");
        if (other.gameObject.layer == _targetlayerValue)
        {
            //Debug.Log("target exited a room trigger");
            GameManager.Instance.PlayerManager.CurrentRoom = ClosestRoom;
        }
    }

    public void CheckValidation()
    {
        if (ClosestRoom == null)
        {
            throw new Exception("HallwayTrigger has no room assigned");
        }
    }
}
