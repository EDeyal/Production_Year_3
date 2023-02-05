using Sirenix.OdinInspector;
using System;
using UnityEngine;

public class HallwayTrigger: MonoBehaviour
{
    public Action OnEnteredTrigger;
    public BoxCollider Sensor;
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

    private void OnTriggerExit(Collider other)
    {
        //Debug.Log($"Something with layer {other.gameObject.layer} entered trigger");
        if (other.gameObject.layer == _targetlayerValue)
        {
            Debug.Log("target triggered a room trigger");
        }
    }
}
