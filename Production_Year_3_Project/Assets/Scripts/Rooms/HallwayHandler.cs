using System.Collections.Generic;
using UnityEngine;

public class HallwayHandler : MonoBehaviour
{
    List<RoomHandler> _rooms;
    [SerializeField] List<BoxCollider> _roomColliders;
    [SerializeField] LayerMask _playerLayer;
    //need to add triggers in order to know if the player entered the room or left the room
    //Trigger need to be a class with 2 colliders, one for entering the hallway and one for exiting the hallway
    //when exiting need to know to what room maybe a dictionary that connects the triggers to the room
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == _playerLayer)
        {
            Debug.Log("Player Entered Hallway Trigger");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == _playerLayer)
        {
            Debug.Log("Player Exited Hallway Trigger");
        }
    }
}
