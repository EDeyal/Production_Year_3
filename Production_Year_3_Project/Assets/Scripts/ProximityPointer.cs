using UnityEngine;
using UnityEngine.Events;

public class ProximityPointer<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private Transform pointer;
    [SerializeField] private GameObject arrow;
    [SerializeField] private ProximitySensor<T> sensor;
    public UnityEvent<T> OnClosestTargetChanged;
    protected T target;
    private void Update()
    {
        PointToClosestLegalTarget();
    }
    protected virtual void PointToClosestLegalTarget()
    {
        T closestTarget = sensor.GetClosestLegalTarget();
        if (ReferenceEquals(closestTarget, null))
        {
            pointer.gameObject.SetActive(false);
            return;
        }
        if (!ReferenceEquals(target, closestTarget))
        {
            OnClosestTargetChanged.Invoke(closestTarget);
        }
        target = closestTarget;
        pointer.gameObject.SetActive(true);
        pointer.LookAt(target.transform.position);
    }

}
