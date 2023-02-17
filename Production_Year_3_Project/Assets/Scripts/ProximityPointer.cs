using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityPointer<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private Transform pointer;
    [SerializeField] private GameObject arrow;
    [SerializeField] private ProximitySensor<T> sensor;

    private void Update()
    {
        PointToClosestLegalTarget();
    }
    private void PointToClosestLegalTarget()
    {
        T target = sensor.GetClosestLegalTarget();
        if (ReferenceEquals(target, null))
        {
            pointer.gameObject.SetActive(false);
            return;
        }
        pointer.gameObject.SetActive(true);
        pointer.LookAt(target.transform.position);
    }
    
}
