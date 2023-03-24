using System.Collections.Generic;
using UnityEngine;

public class SavePointHandler : MonoBehaviour
{
    [SerializeField] int _currentSavePoint;
    public int CurrentSavePoint=>_currentSavePoint;

    List<IRespawnable> _respawnAssets;
    public List<IRespawnable> RespawnAssets => _respawnAssets; 

    List<SavePoint> _savePoints;
    public List<SavePoint> SavePoints => _savePoints;

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
    public int SetPlayerSavePoint(SavePoint savePoint)
    {
        _currentSavePoint = savePoint.ID;
        return CurrentSavePoint;
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
        var transform = _savePoints[_currentSavePoint].SpawnPointTransform;
        return transform;
    }
    private void OnDestroy()
    {
        _savePoints.Clear();
        _respawnAssets.Clear();
    }
}
