using UnityEngine;

[CreateAssetMenu(fileName = "CheckXDistanceAction", menuName = "ScriptableObjects/Actions/CheckXDistance")]

public class CheckXDistanceAction : BaseAction<DistanceData>
{
    public float Distance;
    public float Offset;
    public override bool InitAction(DistanceData distanceData)
    {
        if (distanceData.GetXDistance() < Distance + Offset)
        {
            return true;
        }
        return false;
    }
}
