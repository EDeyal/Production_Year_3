using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicHandler : MonoBehaviour,IRespawnable
{

    [SerializeField] CinematicTrigger _trigger;

    public void Start()
    {
        GameManager.Instance.SaveManager.SavePointHandler.RespawnAssets.Add(this);
    }
    public void OnDestroy()
    {
        GameManager.Instance.SaveManager.SavePointHandler.RespawnAssets.Remove(this);
    }
    public void Respawn()
    {
        _trigger.ResetCinematic();
        Debug.LogError("Reset Cinematic Handler");
    }
}
