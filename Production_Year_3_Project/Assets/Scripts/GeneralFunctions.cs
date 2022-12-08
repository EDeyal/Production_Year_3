using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEngine.RuleTile.TilingRuleOutput;

public static class GeneralFunctions
{
    public static bool IsInRange(Vector3 a,Vector3 b,float distance)
    {
        var currentDistance = Vector3.Distance(a, b);
        //Debug.Log("Current Distance From Targer is: " + currentDistance);
        if (currentDistance < distance)
            return true;
        return false;
    }
    public static float CalcRange(Vector3 a, Vector3 b)
    {
        return Vector3.Distance(a, b);
    }

}
