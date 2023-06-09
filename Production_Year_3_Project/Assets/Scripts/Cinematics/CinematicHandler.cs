using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicHandler : MonoBehaviour,IRespawnable
{
    [SerializeField] CinematicTrigger _trigger;
    public void Respawn()
    {
        _trigger.ResetCinematic();
    }
}
