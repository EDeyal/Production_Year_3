using UnityEngine;

public class DistanceData
{
    public DistanceData(Vector3 a, Vector3 b,float offset)
    {
        _a = a;
        _b = b;
        Offset = offset;
    }
    Vector3 _a;
    Vector3 _b;
    public float Offset;

    public float GetXDistance()
    {
        var xDistance = _a.x - _b.x;
        xDistance =  Mathf.Abs(xDistance);
        return xDistance;
    }
    public float GetYDistance()
    {
        var yDistance = _a.y - _b.y;
        yDistance = Mathf.Abs(yDistance);
        return yDistance;
    }
    public float GetZDistance()
    {
        var zDistance = _a.z - _b.z;
        zDistance = Mathf.Abs(zDistance);
        return zDistance;
    }
    public float GetTotalDistance()
    {
        var totalDistance = Vector3.Distance(_a, _b);
        return totalDistance;
    }
}
