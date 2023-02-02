using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomsManager : MonoBehaviour
{
    [ReadOnly] Dictionary<string,RoomHandler> _roomsDictionary;
    private void Awake()
    {
        _roomsDictionary = new Dictionary<string,RoomHandler>();
    }
    public void AddRoom(RoomHandler roomHandler)
    {
        if (_roomsDictionary.TryAdd(roomHandler.RoomName, roomHandler))
        {
            Debug.Log($"Room with name: {roomHandler.RoomName} has been added to Rooms Manager");
        }
        else
        {
            Debug.LogError($"Room with name {roomHandler.RoomName} not could not be added," +
                $"check room name or if it is allready exists");
        }
    }
}
