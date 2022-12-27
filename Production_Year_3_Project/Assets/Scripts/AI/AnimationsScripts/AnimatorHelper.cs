using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnimatorParameterType
{
    Speed,
}
public static class AnimatorHelper
{
    public static string GetParameter(AnimatorParameterType type)
    {
        switch (type)
        {
            case AnimatorParameterType.Speed:
                return "Speed";
            default:
                throw new System.Exception("Animator Helper Recived Wrong AnimatorParameterType");
        }
    }
}
