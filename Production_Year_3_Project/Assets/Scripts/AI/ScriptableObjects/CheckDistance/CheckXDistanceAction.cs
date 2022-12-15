using UnityEngine;

[CreateAssetMenu(fileName = "CheckXDistanceAction", menuName = "ScriptableObjects/Actions/CheckXDistance")]

public class CheckXDistanceAction : BaseAction<DistanceData>
{
    public float Distance;
    public override bool InitAction(DistanceData distanceData)
    {
        var totalXDistance = distanceData.GetXDistance();

        if (totalXDistance < distanceData.Offset)
        {
            return true;
        }
        return false;
    }
}
