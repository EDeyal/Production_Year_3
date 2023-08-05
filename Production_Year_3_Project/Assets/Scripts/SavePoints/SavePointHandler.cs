using System.Collections;
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
    public int SetPlayerSavePoint(SavePoint savePoint,bool withFeedback)
    {
        if (_savePoints.Count == 0)
        {
            throw new System.Exception("Save point handler has 0 save points assigned to it");
        }
        else
        {
            Debug.Log("Save point handler is setting player point");
            Debug.Log("Amount of save points is: " + _savePoints.Count);
            if (savePoint.ID != _currentSavePointID)
            {
                foreach (var savePoints in _savePoints)
                {
                    savePoints.DeactivateSavePoint();
                }
            }
            _currentSavePointID = savePoint.ID;
            if (withFeedback)
            {
                savePoint.PlayParticles();
                savePoint.ActivateSavePoint(true);
            }
            else
            { 
                savePoint.ActivateSavePoint(false);
            }

            var damageable = GameManager.Instance.PlayerManager.Damageable;
            damageable.Heal(new DamageHandler() { BaseAmount = damageable.MaxHp });
            Debug.Log($"Setting Player to savepoint num:{_currentSavePointID}");
            return CurrentSavePointID;
        }
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
