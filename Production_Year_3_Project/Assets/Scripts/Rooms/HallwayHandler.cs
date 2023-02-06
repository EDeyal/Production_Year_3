using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class HallwayHandler:MonoBehaviour, ICheckValidation
{
    [SerializeField] List<HallwayTrigger> _triggers;
    [SerializeField] LayerMask _targetLayer;
    public int HallwayIndex;
    private void Start()
    {
        InitRoomTriggers();
        foreach (var trigger in _triggers)
        {
            trigger.OnEnteredTrigger += ActivateRoom;
        }
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
        if (_triggers.Count == 0)
        {
            throw new System.Exception($"Hallway number {HallwayIndex} has no Hallway triggers");
        }
    }
    public void ActivateRoom()
    {
        var playerRoom = GameManager.Instance.PlayerManager.CurrentRoom;
        foreach (var trigger in _triggers)
        {
            if (trigger.ClosestRoom != playerRoom)
            {
                trigger.ClosestRoom.ActivateRoom();
            }
        }
    }
    //need to add triggers in order to know if the player entered the room or left the room
    //when exiting need to know to what room maybe a dictionary that connects the triggers to the room
}
