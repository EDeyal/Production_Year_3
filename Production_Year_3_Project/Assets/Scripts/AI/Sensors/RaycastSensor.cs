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
        if (IsActive == false)
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
    public bool SendRayToTarget(Transform transform, float maxDistance)
    {
        //can have multiple starting positions
        //will not call events, this is only to check for hit or miss
        bool isHitAll = true;
        bool isHitPartialy = false;
        var playerPos = GameManager.Instance.PlayerManager.transform.position;
        var directionToPlayer = playerPos - transform.position;
        directionToPlayer.Normalize();
        foreach (var item in _sensors)
        {
            bool isHit = false;
            float rawLayerValue = _sensorTarget.LayerMask.value;
            rawLayerValue = Mathf.Log(rawLayerValue, 2);
            Vector3 relativePos = new Vector3(transform.position.x + item.Offset.x, transform.position.y + item.Offset.y);
            RaycastHit hit = new RaycastHit();
            Ray ray = new Ray(relativePos, directionToPlayer);
            Physics.Raycast(ray, out hit, maxDistance);
            if (hit.transform != null)
            {
                if (hit.transform.gameObject.layer == rawLayerValue)
                {
                    isHit = true;
                }
            }
            //is hit partialy is true if is hit was true at some point
            isHitPartialy = isHitPartialy || isHit;
            //isHitAll is false if isHit was false at some point
            isHitAll = isHitAll && isHit;
        }

        if (isHitAll)
        {
            //all hit
            return true;
        }
        else if (isHitPartialy)
        {
            return true;
            //some hit
        }
        else
        {
            return false;
            // none hit
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
    public void DrawLineToTarget(Transform transform, float maxDistance)
    {

        if (IsActive == false)
            return;
        bool isHitAll = true;
        bool isHitPartialy = false;
        if (!GameManager.Instance)
            return;
        var playerPos = GameManager.Instance.PlayerManager.transform.position;
        var directionToPlayer = playerPos - transform.position;
        directionToPlayer.Normalize();
        foreach (var item in _sensors)
        {
            bool isHit = false;
            float rawLayerValue = _sensorTarget.LayerMask.value;
            rawLayerValue = Mathf.Log(rawLayerValue, 2);
            Vector3 relativePos = new Vector3(transform.position.x + item.Offset.x, transform.position.y + item.Offset.y);
            RaycastHit hit = new RaycastHit();
            Ray ray = new Ray(relativePos, directionToPlayer);
            Physics.Raycast(ray, out hit, maxDistance);
            if (hit.transform != null)
            {
                if (hit.transform.gameObject.layer == rawLayerValue)
                {
                    isHit = true;
                }
            }
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

            Vector3 to = directionToPlayer * maxDistance;
            to = new Vector3(relativePos.x + to.x, relativePos.y + to.y);
            Gizmos.DrawLine(relativePos, to);
        }
    }
#endif
}
