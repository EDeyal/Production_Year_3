using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomHandler : MonoBehaviour
{
    [SerializeField] string _roomName;
    int currentHallway;
    List<HallwayHandler> _hallways;
    SpawningHandler _spawnHandle;
    public string RoomName => _roomName;

    private void Start()
    {
        GameManager.Instance.RoomsManager.AddRoom(this);
    }
}
