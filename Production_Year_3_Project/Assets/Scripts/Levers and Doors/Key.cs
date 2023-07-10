using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] bool isCollected;
    private void OnTriggerEnter(Collider other)
    {
        //identfy player using tag?
        //play collect key method
    }
    public void CollectKey()
    {
        //isCollected == true
        //turn of key
    }
}
