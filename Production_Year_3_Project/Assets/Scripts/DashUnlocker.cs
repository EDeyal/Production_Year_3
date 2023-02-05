using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashUnlocker : MonoBehaviour
{
    [SerializeField] private LayerMask playerLayer;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetHashCode() == GameManager.Instance.PlayerManager.gameObject.GetHashCode())
        {
            GameManager.Instance.PlayerManager.PlayerDash.CanDash = true;
            gameObject.SetActive(false);
        }
    }
}
