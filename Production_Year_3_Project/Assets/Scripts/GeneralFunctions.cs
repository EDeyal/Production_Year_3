using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEngine.RuleTile.TilingRuleOutput;

public static class GeneralFunctions
{
    const int ZERO = 0;
    const int MINUS_ONE = -1;
    const int ONE = 1;
    public static bool IsInRange(Vector3 a,Vector3 b,float distance)
    {
        var currentDistance = Vector3.Distance(a, b);
        //Debug.Log("Current Distance From Targer is: " + currentDistance);
        if (currentDistance <= distance)
            return true;
        return false;
    }
    public static float CalcRange(Vector3 a, Vector3 b)
    {
        return Vector3.Distance(a, b);
    }
    public static int GetXDirectionToTarget(Vector3 a, Vector3 target)
    { 
        if (a.x > target.x)
        {
            return MINUS_ONE;
        }
        else if (a.x < target.x)
        {
            return ONE;
        }
        return ZERO;
    }
    public static int GetYDirectionToTarget(Vector3 a, Vector3 target)
    {
        if (a.y > target.y)
        {
            return MINUS_ONE;
        }
        else if (a.y < target.y)
        {
            return ONE;
        }
        return ZERO;
    }
    public static int GetZDirectionToTarget(Vector3 a, Vector3 target)
    {
        if (a.z > target.z)
        {
            return MINUS_ONE;
        }
        else if (a.z < target.z)
        {
            return ONE;
        }
        return ZERO;
    }

}
