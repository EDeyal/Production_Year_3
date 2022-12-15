using System.Collections.Generic;
using UnityEngine;
public class SensorHandler : MonoBehaviour
{
    [SerializeField] List<RaycastSensor> _raycastSensors;
    private void Update()
    {
        foreach (var sensor in _raycastSensors)
        {
            sensor.CheckRaycasts(transform);
        }
    }
    public RaycastSensor GetSensor(SensorTarget sensorTarget)
    {
        foreach (var sensor in _raycastSensors)
        {
            if (sensor.SensorTarget == sensorTarget)
            {
                return sensor;
            }
        }
        return null;
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        foreach (var sensor in _raycastSensors)
        {
            sensor.OnDrawSensor(transform);
        }
    }
#endif
}
