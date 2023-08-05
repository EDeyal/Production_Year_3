using UnityEngine;
using UnityEngine.Events;

public class ProximityPointer<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private Transform pointer;
    [SerializeField] private GameObject arrow;
    [SerializeField] private ProximitySensor<T> sensor;
    public UnityEvent<T> OnClosestTargetChanged;
    public bool disabled;
    protected T target;
    private void Start()
    {
        SetActive(false);
    }
    private void Update()
    {
        PointToClosestLegalTarget();
    }
    protected virtual void PointToClosestLegalTarget()
    {
        T closestTarget = sensor.GetClosestLegalTarget();
        if (ReferenceEquals(closestTarget, null) || disabled)
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

    public void SetActive(bool state)
    {
        pointer.gameObject.SetActive(state);
    }

}
