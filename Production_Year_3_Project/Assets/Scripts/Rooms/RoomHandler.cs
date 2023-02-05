using System.Collections.Generic;
using UnityEngine;

public class RoomHandler : MonoBehaviour, ICheckValidation
{
    [SerializeField] string _roomName;
    int currentHallway = -1;
    List<HallwayHandler> _hallways;
    [SerializeField] SpawningHandler _spawnHandler;
    public string RoomName => _roomName;

    private void Awake()
    {
        CheckValidation();
    }
    private void Start()
    {
        GameManager.Instance.RoomsManager.AddRoom(this);
        SetHallwayIndex();
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
            throw new System.Exception($"RoomHandler with room name: {_roomName} has no Spawner");
        }
        _spawnHandler.CheckValidation();
        if (_hallways.Count == 0)
        {
            throw new System.Exception($"RoomHandler with room name: {_roomName} has no Hallways");
        }

    }
    private void SetHallwayIndex()
    {
        for (int i = 0; i < _hallways.Count; i++)
        {
            _hallways[i].HallwayIndex = i;
        }
    }
}
