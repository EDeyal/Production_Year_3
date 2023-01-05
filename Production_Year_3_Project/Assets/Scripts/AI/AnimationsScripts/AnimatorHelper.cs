using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnimatorParameterType
{
    Speed,
    IsDead,
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
            default:
                throw new System.Exception("Animator Helper Recived Wrong AnimatorParameterType");
        }
    }
}
