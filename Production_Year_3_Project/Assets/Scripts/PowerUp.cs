using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] protected Color _progressionColor;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            OnPickedUp();
        }
    }
    protected virtual void OnPickedUp()
    {
        gameObject.SetActive(false);
    }

}
