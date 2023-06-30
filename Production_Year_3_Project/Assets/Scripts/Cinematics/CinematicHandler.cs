using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicHandler : MonoBehaviour,IRespawnable
{

    [SerializeField] CinematicTrigger _trigger;
    [SerializeField] bool _isRespawnable = true;

    public void Start()
    {
        if (_isRespawnable)
        {
            GameManager.Instance.SaveManager.SavePointHandler.RespawnAssets.Add(this);
        }
    }
    public void OnDestroy()
    {
        if (_isRespawnable)
        {
            var respawnAssets = GameManager.Instance.SaveManager.SavePointHandler.RespawnAssets;
            if (respawnAssets.Count > 0)
                respawnAssets.Remove(this);
        }
    }
    public void Respawn()
    {
        _trigger.Respawn();
        //Debug.LogError("Reset Cinematic Handler");
    }
}
