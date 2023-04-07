using System.Collections.Generic;
using UnityEngine;

public class SavePointHandler : MonoBehaviour
{
    [SerializeField] int _currentSavePointID;
    [SerializeField] SavePoint _startingSavePoint;
    public int CurrentSavePointID=>_currentSavePointID;

    List<IRespawnable> _respawnAssets;
    public List<IRespawnable> RespawnAssets => _respawnAssets; 

    List<SavePoint> _savePoints;
    public List<SavePoint> SavePoints => _savePoints;
    public SavePoint StartingSavePoint => _startingSavePoint;

    private void Awake()
    {
        _savePoints = new List<SavePoint>();
        _respawnAssets = new List<IRespawnable>();
    }
    public void RespawnObjects()
    {
        if (_respawnAssets != null)
        {
            foreach (var respawnable in _respawnAssets)
            {
                respawnable.Respawn();
            }
        }
        else
        {
            Debug.LogWarning("Save Point Handler Respawn Assets are Null");
        }
    }
    public int SetPlayerSavePoint(SavePoint savePoint,bool withVisuals)
    {
        _currentSavePointID = savePoint.ID;
        if (withVisuals)
        {
            savePoint.PlayParticles();
        }
        var damageable = GameManager.Instance.PlayerManager.Damageable;
        damageable.Heal(new DamageHandler() { BaseAmount = damageable.MaxHp});
        Debug.Log($"Setting Player to savepoint num:{_currentSavePointID}");
        return CurrentSavePointID;
    }
    public void RegisterToSavePointHandler(SavePoint savePoint)
    {
        savePoint.ID = _savePoints.Count;
        _savePoints.Add(savePoint);
    }
    /// <summary>
    /// Respawn all respawnables and get spawnPoint tranform for player
    /// </summary>
    /// <returns></returns>
    public Transform RespawnToSpawnPoint()
    {
        RespawnObjects();
        GameManager.Instance.SaveManager.RoomsManager.CurrentRoom = _savePoints[_currentSavePointID].SavePointRoom;
        GameManager.Instance.SaveManager.RoomsManager.CurrentRoom.ResetRoom();
        var transform = _savePoints[_currentSavePointID].SpawnPointTransform;
        return transform;
    }
    private void OnDestroy()
    {
        _savePoints.Clear();
        _respawnAssets.Clear();
    }
}