using UnityEngine;

[CreateAssetMenu(fileName = "GroundWalkAction",menuName = "ScriptableObjects/Actions/GroundWalk")]
public class GroundWalkAction : BaseAction<WalkData>
{
    public float Speed;

    public override bool InitAction(WalkData walkData)
    {
        var velocity = new Vector3(walkData.Direction.x * Speed, walkData.RB.velocity.y, 0);
        walkData.RB.velocity = velocity;
        return true;
    }
}
