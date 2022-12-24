using UnityEngine;

[CreateAssetMenu(fileName = "CheckDistanceAction", menuName = "ScriptableObjects/Actions/CheckDistance")]

public class CheckDistanceAction : BaseAction<DistanceData>
{
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
}
