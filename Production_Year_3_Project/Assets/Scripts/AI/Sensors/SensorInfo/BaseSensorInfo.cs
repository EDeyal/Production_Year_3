using Sirenix.OdinInspector;
using UnityEngine;
public enum SensorInfoType
{
    Hit,
    PartialHit,
    Miss,
}
[System.Serializable]
public abstract class BaseSensorInfo
{
    [SerializeField] SensorTarget _groundCheckSensor;
    [ReadOnly] [SerializeField] SensorInfoType _sensorInfoType;
    [ReadOnly] public bool IsActive;
    public SensorInfoType SensorInfoType => _sensorInfoType;
    public void SubscribeToEvents(SensorHandler sensorHandler)
    {
        var raycastSensor = sensorHandler.GetSensor(_groundCheckSensor);
        raycastSensor.OnSensorHit += Hit;
        raycastSensor.OnSensorPartialHit += PartialHit;
        raycastSensor.OnSensorMiss += Miss;
    }
    public void UnsubscribeToEvents(SensorHandler sensorHandler)
    {
        var raycastSensor = sensorHandler.GetSensor(_groundCheckSensor);
        raycastSensor.OnSensorHit -= Hit;
        raycastSensor.OnSensorPartialHit -= PartialHit;
        raycastSensor.OnSensorMiss -= Miss;
    }
    protected virtual void Hit()
    {
        _sensorInfoType = SensorInfoType.Hit;
    }
    protected virtual void PartialHit()
    {
        _sensorInfoType = SensorInfoType.PartialHit;
    }
    protected virtual void Miss()
    {
        _sensorInfoType = SensorInfoType.Miss;
    }

}
