using Sirenix.OdinInspector;
[System.Serializable]
public class WallSensorInfo : BaseSensorInfo
{
    [ReadOnly] public bool IsNearWall;
    protected override void Hit()
    {
        base.Hit();
        IsNearWall = true;
    }

    protected override void Miss()
    {
        base.Miss();
        IsNearWall = false;
    }

    protected override void PartialHit()
    {
        base.PartialHit();
        IsNearWall = true;
    }
}
