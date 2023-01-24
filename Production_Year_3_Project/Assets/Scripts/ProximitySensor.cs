using System.Collections.Generic;
using UnityEngine;

public class ProximitySensor<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] protected float checkRadius;
    [SerializeField] protected LayerMask targetLayer;

    public T[] GetTargetsInProximity()
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
    public T[] GetLegalTargets()
    {
        T[] targets = GetTargetsInProximity();
        if (targets.Length == 0)
        {
            return null;
        }
        List<T> legalTargets = new List<T>();

        foreach (var item in targets)
        {
            Vector2 dir = item.transform.position - transform.position;
            RaycastHit hit;
            if (Physics.Raycast(transform.position, dir, out hit) && item.transform.position == hit.collider.transform.position)
            {
                legalTargets.Add(item);
            }
        }
        return legalTargets.ToArray();
    }

    public T GetClosestLegalTarget()
    {
        T[] legalTargets = GetLegalTargets();
        if (ReferenceEquals(legalTargets, null) || legalTargets.Length == 0)
        {
            return null;
        }
        T closestPoint = legalTargets[0];
        for (int i = 0; i < legalTargets.Length - 1; i++)
        {
            if (GeneralFunctions.CalcRange(legalTargets[i].transform.position, transform.position) <= GeneralFunctions.CalcRange(legalTargets[i + 1].transform.position, transform.position))
            {
                closestPoint = legalTargets[i + 1];
            }
        }
        Debug.Log(closestPoint.transform.parent.name);
        return closestPoint;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, checkRadius);
    }

}
