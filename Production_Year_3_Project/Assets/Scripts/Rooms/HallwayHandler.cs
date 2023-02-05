using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class HallwayHandler:MonoBehaviour, ICheckValidation
{
    [SerializeField] List<RoomHandler> _connectedRooms;
    [SerializeField] List<HallwayTrigger> _triggers;
    [SerializeField] LayerMask _targetLayer;
    public int HallwayIndex;
    private void Start()
    {
        InitRoomTriggers();
    }
    public void InitRoomTriggers()
    {
        foreach (var trigger in _triggers)
        {
            trigger.TargetLayerValue = _targetLayer.value;
        }
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
