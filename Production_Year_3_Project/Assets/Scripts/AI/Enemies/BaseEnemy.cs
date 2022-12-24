using Sirenix.OdinInspector;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public abstract class BaseEnemy : MonoBehaviour, ICheckValidation
{
    [SerializeField] Bounds _bound;
    [SerializeField] Rigidbody _rb;
    [SerializeField] SensorHandler _sensorHandler;
    [SerializeField] BaseAction<DistanceData> _noticePlayerDistance;
    [SerializeField] BaseAction<DistanceData> _chasePlayerDistance;

    public Bounds Bound => _bound;
    public Rigidbody RB => _rb;
    public SensorHandler SensorHandler => _sensorHandler;
    public BaseAction<DistanceData> NoticePlayerDistance => _noticePlayerDistance;
    public BaseAction<DistanceData> chasePlayerDistance => _chasePlayerDistance;

    public virtual void CheckValidation()
    {
        if (!_sensorHandler)
            throw new System.Exception("BaseEnemy has no SensorHandler");
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(_bound.center, _bound.size);
    }
}
