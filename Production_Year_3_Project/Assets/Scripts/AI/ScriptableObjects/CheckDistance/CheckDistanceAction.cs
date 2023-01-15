using UnityEngine;

[CreateAssetMenu(fileName = "CheckDistanceAction", menuName = "ScriptableObjects/Actions/CheckDistance")]

public class CheckDistanceAction : BaseAction<DistanceData>
{
    public Color GizmoColor;
    public float Distance;
    public float Offset;
    public override bool InitAction(DistanceData distanceData)
    {
        if (distanceData.GetTotalDistance() < Distance + Offset)
        {
            return true;
        }
        return false;
    }
    public override void DrawGizmos(Vector3 pos)
    {
        Gizmos.color = GizmoColor;
        Gizmos.DrawWireSphere(pos, Distance);
    }
}
