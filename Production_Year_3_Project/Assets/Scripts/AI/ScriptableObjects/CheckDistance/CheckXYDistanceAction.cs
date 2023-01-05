using UnityEngine;

[CreateAssetMenu(fileName = "New CheckXYDistanceAction", menuName = "ScriptableObjects/Actions/CheckXYDistance")]
public class CheckXYDistanceAction : BaseAction<DistanceData>
{
    public float Distance;
    public float Offset;
    public override bool InitAction(DistanceData distanceData)
    {
        if (distanceData.GetXYDistance() < Distance + Offset)
        {
            return true;
        }
        return false;
    }
}
