using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RaycastSensor
{
    public event Action OnSensorHit;
    public event Action OnSensorMiss;
    public event Action OnSensorPartialHit;
    //all sensors will check for the same thing,
    //no logic differences can be implemented here
    [SerializeField] List<SensorData> _sensors;
    [SerializeField] SensorTarget _sensorTarget;
    public bool IsActive = true;
    public SensorTarget SensorTarget => _sensorTarget;
    public void CheckRaycasts(Transform transform)
    {
        if(IsActive == false)
            return;

        bool isHitAll = true;
        bool isHitPartialy = false;
        foreach (var item in _sensors)
        {
            float rawLayerValue = _sensorTarget.LayerMask.value;
            rawLayerValue = Mathf.Log(rawLayerValue, 2);
            Vector3 relativePos = new Vector3(transform.position.x + item.Offset.x, transform.position.y + item.Offset.y);
            bool isHit = Physics.Raycast(relativePos, item.Direcion, item.Range, _sensorTarget.LayerMask);

            //is hit partialy is true if is hit was true at some point
            isHitPartialy = isHitPartialy || isHit;
            //isHitAll is false if isHit was false at some point
            isHitAll = isHitAll && isHit;
        }
        if (isHitAll)
        {
            //all hit
            OnSensorHit?.Invoke();
        }
        else if (isHitPartialy)
        {
            //some hit
            OnSensorPartialHit?.Invoke();
        }
        else
        {
            // none hit
            OnSensorMiss?.Invoke();
        }
    }

#if UNITY_EDITOR
    public void OnDrawSensor(Transform transform)
    {
        if (IsActive == false)
            return;
        bool isHitAll = true;
        bool isHitPartialy = false;
        foreach (var item in _sensors)
        {
            Vector3 relativePos = new Vector3(transform.position.x + item.Offset.x, transform.position.y + item.Offset.y);
            bool isHit = Physics.Raycast(relativePos, item.Direcion, item.Range, _sensorTarget.LayerMask);

            //is hit partialy is true if is hit was true at some point
            isHitPartialy = isHitPartialy || isHit;
            //isHitAll is false if isHit was false at some point
            isHitAll = isHitAll && isHit;
        }

        if (isHitAll)
        {
            Gizmos.color = _sensorTarget.HitColor;
        }
        else if (isHitPartialy)
        {
            Gizmos.color = _sensorTarget.PartialHitColor;
        }
        else
        {
            Gizmos.color = _sensorTarget.MissColor;
        }

        foreach (var item in _sensors)
        {

            Vector3 relativePos = new Vector3(transform.position.x + item.Offset.x, transform.position.y + item.Offset.y);

            Vector3 to = item.Direcion * item.Range;
            to = new Vector3(relativePos.x + to.x, relativePos.y + to.y);
            Gizmos.DrawLine(relativePos, to);
        }
    }
#endif
}
