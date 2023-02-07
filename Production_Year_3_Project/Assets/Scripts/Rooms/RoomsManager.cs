using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomsManager : MonoBehaviour
{
    [ReadOnly] Dictionary<string,RoomHandler> _roomsDictionary;
    [SerializeField] RoomHandler _startingRoom;
    string _currentRoom = "Environment";
    private void Awake()
    {
        _roomsDictionary = new Dictionary<string,RoomHandler>();
    }

    private void Start()
    {
        GameManager.Instance.PlayerManager.CurrentRoom  = _startingRoom;        
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
        _roomsDictionary.TryGetValue(_currentRoom,out RoomHandler roomHandler);
        roomHandler.ResetRoom();
    }
}
