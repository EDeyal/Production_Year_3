public enum AnimatorParameterType
{
    Speed,
    IsDead,
    IsCharging,
    IsGrounded,
    IsHit,
    HitUp,
    HitDown,
    Melee,
    Ranged,
    HasAttacked
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
            case AnimatorParameterType.HitUp:
                return "HitUp";
            case AnimatorParameterType.HitDown:
                return "HitDown";
            case AnimatorParameterType.Melee:
                return "Melee";
            case AnimatorParameterType.Ranged:
                return "Ranged";
            case AnimatorParameterType.HasAttacked:
                return "HasAttacked";

            default:
                throw new System.Exception("Animator Helper Recived Wrong AnimatorParameterType");
        }
    }
}
