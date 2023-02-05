using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class HallwayHandler: ICheckValidation
{
    [SerializeField] List<RoomHandler> _connectedRooms;
    [SerializeField] HallwayTrigger _trigger1;
    [SerializeField] HallwayTrigger _trigger2;
    [SerializeField] LayerMask _targetLayer;
    public int HallwayIndex;
    public void InitRoomTriggers()
    {
        _trigger1.TargetLayer = _targetLayer;
        _trigger2.TargetLayer = _targetLayer;    
    }      
    public void CheckValidation()
    {
        if (_connectedRooms.Count == 0)
        {
            throw new System.Exception($"Hallway number {HallwayIndex} has no rooms connected");
        }
    }
    //need to add triggers in order to know if the player entered the room or left the room
    //when exiting need to know to what room maybe a dictionary that connects the triggers to the room
}
