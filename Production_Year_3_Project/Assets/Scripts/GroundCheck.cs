using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;
using UnityEngine.Events;
public class GroundCheck : MonoBehaviour
{
    [SerializeField] List<GroundCheckSensor> sensors = new List<GroundCheckSensor>();
    [SerializeField] LayerMask hitLayer;

    public UnityEvent OnGrounded;
    public UnityEvent OnNotGrounded;
    private void Start()
    {
        StartCoroutine(WaitForGrounded());
    }
    public bool IsAllGrounded()
    {
        foreach (var item in sensors)
        {
            Vector3 relativePos = new Vector3(transform.position.x + item.Offset.x, transform.position.y + item.Offset.y);
            if (!Physics.Raycast(relativePos, item.Direcion, item.Range, hitLayer))
            {
                return false;
            }
        }
        return true;
    }

    public bool IsGrounded()
    {
        foreach (var item in sensors)
        {
            Vector3 relativePos = new Vector3(transform.position.x + item.Offset.x, transform.position.y + item.Offset.y);
            if (Physics.Raycast(relativePos, item.Direcion, item.Range, hitLayer))
            {
                return true;
            }
        }
        return false;
    }

    private bool IsSensorGrounded(GroundCheckSensor givenSensor)
    {
        Vector3 relativePos = new Vector3(transform.position.x + givenSensor.Offset.x, transform.position.y + givenSensor.Offset.y);
        if (Physics.Raycast(relativePos, givenSensor.Direcion, givenSensor.Range, hitLayer))
        {
            return true;
        }
        return false;
    }

    IEnumerator WaitForGrounded()
    {
        yield return new WaitUntil(() => IsGrounded());
        OnGrounded?.Invoke();
        StartCoroutine(WaitForNotGrounded());
    }
    IEnumerator WaitForNotGrounded()
    {
        yield return new WaitUntil(() => !IsGrounded());
        OnNotGrounded?.Invoke();
        StartCoroutine(WaitForGrounded());
    }

    private void OnDrawGizmos()
    {
        foreach (var item in sensors)
        {
            Vector3 relativePos = new Vector3(transform.position.x + item.Offset.x, transform.position.y + item.Offset.y);
            Vector3 to = item.Direcion * item.Range;
            to = new Vector3(relativePos.x + to.x, relativePos.y + to.y);
            Gizmos.color = Color.red;
            if (IsSensorGrounded(item))
            {
                Gizmos.color = Color.green;
            }
            Gizmos.DrawLine(relativePos, to);
        }
    }

}


[System.Serializable]
public class GroundCheckSensor
{
    public Vector2 Direcion;
    public Vector2 Offset;
    public float Range;
}
