using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesCheckpoint : MonoBehaviour
{
    [SerializeField] private List<Spikes> spikesLinked = new List<Spikes>();

    // Start is called before the first frame update
    void Start()
    {
        Spikes[] spikesInChildren = GetComponentsInChildren<Spikes>();

        foreach (Spikes item in spikesInChildren)
        {
            spikesLinked.Add(item);
            item.AssignCheckpoint(this.transform);
        }
    }


}
