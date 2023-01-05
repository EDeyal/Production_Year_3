using UnityEngine;

[CreateAssetMenu(fileName = "New AirWalkAction", menuName = "ScriptableObjects/Actions/AirWalk")]

public class AirWalkAction : BaseAction<MoveData>
{
    public override bool InitAction(MoveData walkData)
    {
        var velocity = new Vector3(walkData.Direction.x * walkData.Speed, walkData.Direction.y * walkData.Speed, 0);
        walkData.RB.velocity = velocity;
        return true;
    }
}
