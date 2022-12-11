using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

[CreateAssetMenu(fileName = "CheckXDistanceAction", menuName = "ScriptableObjects/Actions/CheckXDistance")]

public class CheckXDistanceAction<T> : BaseAction<DistanceData>
{
    public float Distance;
    public override bool InitAction(DistanceData distanceData)
    {
        var totalDistance = Mathf.Abs(distanceData.X1 - distanceData.X2);

        if (totalDistance < distanceData.Offset)
        {
            return true;
        }
        return false;
    }
}
