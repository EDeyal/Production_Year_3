using System.Collections.Generic;
using UnityEngine;

public class ProximitySensor<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] protected float checkRadius;
    [SerializeField] private Transform rayFirePoint;
    [SerializeField] protected LayerMask targetLayer;
    [SerializeField] protected LayerMask blockedLayer;
    [SerializeField] private float meleeZone;
    [SerializeField] private string tag;

    private void Start()
    {
        if (ReferenceEquals(rayFirePoint, null))
        {
            rayFirePoint = transform;
        }
    }
    public T[] GetTargetsInProximity()//without line of sight
    {
        Collider[] foundColliders = Physics.OverlapSphere(transform.position, checkRadius, targetLayer);
        List<T> foundObjects = new List<T>();
        foreach (var item in foundColliders)
        {
            T obj = item.GetComponent<T>();
            if (!ReferenceEquals(obj, null))
            {
                foundObjects.Add(obj);
            }
        }
        return foundObjects.ToArray();
    }
    public T[] GetLegalTargets()//with line of sight
    {
        T[] targets = GetTargetsInProximity();
        if (targets.Length == 0)
        {
            return null;
        }
        List<T> legalTargets = new List<T>();

        foreach (var item in targets)
        {
            Vector2 dir = (item.transform.position - transform.position);
            if (dir.magnitude <= meleeZone && Condition(item))
            {
                legalTargets.Add(item);
                continue;
            }
            RaycastHit hit;
            if (Physics.Raycast(rayFirePoint.position, dir, out hit, blockedLayer) && Condition(item))
            {
                if (hit.collider.gameObject.CompareTag(tag))
                {
                    legalTargets.Add(item);
                }
            }
        }
        return legalTargets.ToArray();
    }

    public T GetClosestLegalTarget()//closest with line of sight
    {
        T[] legalTargets = GetLegalTargets();
        if (ReferenceEquals(legalTargets, null) || legalTargets.Length == 0)
        {
            return null;
        }
        T closestPoint = legalTargets[0];
        for (int i = 0; i < legalTargets.Length; i++)
        {
            float dist = GeneralFunctions.CalcRange(closestPoint.transform.position, transform.position);
            if (GeneralFunctions.CalcRange(legalTargets[i].transform.position, transform.position) < dist)
            {
                closestPoint = legalTargets[i];
            }
        }
        return closestPoint;
    }


    public bool IsTargetLegal(T instance)
    {
        T[] legals = GetLegalTargets();
        foreach (var item in legals)
        {
            if (ReferenceEquals(item, instance))
            {
                return true;
            }
        }
        Debug.Log(instance.name + " isnt legal");
        return false;
    }

    public virtual bool Condition(T Instance)
    {
        return true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, checkRadius);
        /* Gizmos.color = Color.magenta;
         T[] legals = GetLegalTargets();
         if (!ReferenceEquals(legals, null) && legals.Length > 0)
         {
             foreach (var item in legals)
             {
                 Gizmos.DrawLine(rayFirePoint.position, item.transform.position);
             }
         }*/
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, meleeZone);
        if (!ReferenceEquals(GetClosestLegalTarget(), null))
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(rayFirePoint.position, GetClosestLegalTarget().transform.position);
        }
    }

}
