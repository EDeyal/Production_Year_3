using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GroundEnemy : BaseEnemy
{
    public override bool IsInRange(int distance)
    {
        var currentDistance = Vector3.Distance(transform.position, _player.transform.position);
        Debug.Log(currentDistance);
        if (currentDistance < distance)
            return true;
        return false;
    }
}
