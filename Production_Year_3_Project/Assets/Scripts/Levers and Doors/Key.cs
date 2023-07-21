using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] bool _isCollected;
    public bool IsCollected => _isCollected;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            CollectKey();
        }
    }
    public void CollectKey()
    {
        _isCollected = true;
        this.gameObject.SetActive(false);
        GameManager.Instance.SaveManager.RoomsManager.AddCollectedKey(this);
        Debug.Log(GameManager.Instance.SaveManager.RoomsManager.collectedKeys.Count);
    }
}
