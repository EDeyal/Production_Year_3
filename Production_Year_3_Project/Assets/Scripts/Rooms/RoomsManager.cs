using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomsManager : MonoBehaviour
{
    [ReadOnly] Dictionary<string, RoomHandler> _roomsDictionary;
    [SerializeField] RoomHandler _currentRoom;
    public RoomHandler CurrentRoom
    {
        set
        {
            _currentRoom = value;
            GameManager.Instance.PlayerManager.CurrentRoom = _currentRoom;
        }
    }
    private void Awake()
    {
        _roomsDictionary = new Dictionary<string, RoomHandler>();
    }

    private void Start()
    {
        GameManager.Instance.PlayerManager.CurrentRoom = _currentRoom;
    }
    public void AddRoom(RoomHandler roomHandler)
    {
        if (!_roomsDictionary.TryAdd(roomHandler.RoomName, roomHandler))
        {
            Debug.LogError($"Room with name {roomHandler.RoomName} not could not be added," +
                $"check room name or if it is allready exists");
        }
        else
        {
            //Debug.Log($"Room with name: {roomHandler.RoomName} has been added to Rooms Manager");
        }
    }
    public void ResetRoom()
    {
        if (_roomsDictionary.TryGetValue(_currentRoom.RoomName, out RoomHandler roomHandler))
        {
            roomHandler.ResetRoom();
        }
        else
        {
            Debug.LogWarning($"RoomsManager tried to get Room:{_currentRoom.RoomName}  from room dictionary with no success can not reset room");
        }
    }
}
