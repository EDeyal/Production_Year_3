using System.Collections.Generic;
using UnityEngine;

public class RoomHandler : MonoBehaviour, ICheckValidation
{
    int currentHallway = -1;
    List<HallwayHandler> _hallways;
    [SerializeField] SpawningHandler _spawnHandler;
    [SerializeField] List<RoomGoal> roomGoals;
    public string RoomName => gameObject.name;
    public List<HallwayHandler> Hallways => _hallways;

    private void Awake()
    {
        _hallways = new List<HallwayHandler>();
        CheckValidation();
        InitRoomGoal();
    }
    private void Start()
    {
        GameManager.Instance.SaveManager.RoomsManager.AddRoom(this);
    }
    public void ResetRoom()
    {
        _spawnHandler.DespawnEnemies();
        _spawnHandler.SpawnEnemies();
    }
    public void ActivateRoom()
    {
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
    }
    void InitRoomGoal()
    {
        if (roomGoals == null)
        {
            return;
        }
        var enemies = _spawnHandler.Enemies;
        foreach (var goal in roomGoals)
        {
            goal.SetRoomEnemies(enemies);  
        }
    }
}
