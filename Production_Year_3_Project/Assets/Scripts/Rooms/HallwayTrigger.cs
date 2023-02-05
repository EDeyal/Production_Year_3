using System;
using UnityEngine;

public class HallwayTrigger: MonoBehaviour
{
    public Action OnEnteredTrigger;
    public BoxCollider Sensor;
    LayerMask _targetLayer;
    public LayerMask TargetLayer { set => _targetLayer = value; }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == _targetLayer)
        {
            Debug.Log("target triggered a room trigger");
        }
    }
}
