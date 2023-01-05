using UnityEngine;

[CreateAssetMenu(fileName = "GroundWalkAction",menuName = "ScriptableObjects/Actions/GroundWalk")]
public class GroundWalkAction : BaseAction<MoveData>
{
    public override bool InitAction(MoveData walkData)
    {
        var velocity = new Vector3(walkData.Direction.x * walkData.Speed, walkData.RB.velocity.y, 0);
        walkData.RB.velocity = velocity;
        return true;
    }
}