public enum AnimatorParameterType
{
    Speed,
    IsDead,
    IsCharging,
    IsGrounded,
    IsHit
}
public static class AnimatorHelper
{
    public static string GetParameter(AnimatorParameterType type)
    {
        switch (type)
        {
            case AnimatorParameterType.Speed:
                return "Speed";
            case AnimatorParameterType.IsDead:
                return "IsDead";
            case AnimatorParameterType.IsCharging:
                return "IsCharging";
            case AnimatorParameterType.IsGrounded:
                return "IsGrounded";
            case AnimatorParameterType.IsHit:
                return "IsHit";
            default:
                throw new System.Exception("Animator Helper Recived Wrong AnimatorParameterType");
        }
    }
}
