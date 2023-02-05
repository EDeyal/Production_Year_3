using System.Collections.Generic;
using UnityEngine;

public class RoomHandler : MonoBehaviour, ICheckValidation
{
    int currentHallway = -1;
    [SerializeField] List<HallwayHandler> _hallways;
    [SerializeField] SpawningHandler _spawnHandler;
    public string RoomName => gameObject.name;

    private void Awake()
    {
        CheckValidation();
    }
    private void Start()
    {
        GameManager.Instance.RoomsManager.AddRoom(this);
    }
    public void ResetRoom()
    {
        _spawnHandler.DespawnEnemies();
        _spawnHandler.SpawnEnemies();
    }

    public void CheckValidation()
    {
        if (_spawnHandler == null)
        {
            throw new System.Exception($"RoomHandler with room name: {RoomName} has no Spawner");
        }
        if (_spawnHandler.Enemies.Count == 0)//not all rooms have enemies
        {
            Debug.LogWarning($"RoomHandler {RoomName} has no enemies");
        }
        if (_hallways.Count == 0)
        {
            throw new System.Exception($"RoomHandler with room name: {RoomName} has no Hallways");
        }

    }
}
