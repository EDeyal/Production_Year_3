using Sirenix.OdinInspector;

[System.Serializable]
public class GroundSensorInfo : BaseSensorInfo
{
    [ReadOnly] public bool IsGrounded;
    protected override void Hit()
    {
        base.Hit();
        IsGrounded = true;
    }

    protected override void Miss()
    {
        base.Miss();
        IsGrounded = false;
    }

    protected override void PartialHit()
    {
        base.PartialHit();
        IsGrounded = true;
    }
}
