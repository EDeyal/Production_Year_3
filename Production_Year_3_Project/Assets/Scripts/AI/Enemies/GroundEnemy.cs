using UnityEngine;

public abstract class GroundEnemy : BaseEnemy
{
    protected GameObject _nextPatrollPoint;
    public float DistanceOffset;//plaster should come from Character Data
    public override bool IsInRange(int distance)
    {
        var currentDistance = Vector3.Distance(transform.position, _player.transform.position);
        //Debug.Log("Current Distance From Targer is: " + currentDistance);
        if (currentDistance < distance)
            return true;
        return false;
    }

    public virtual void Patroll(GameObject a, GameObject b)
    {
        if (_nextPatrollPoint == null) //randomly choose 1 point to go to
        {
        //ground enemy moves between 2 points
            if (Random.Range(0, 2) == 0)
                _nextPatrollPoint = a;
            else
                _nextPatrollPoint = b;
        }

        if (Vector3.Distance(transform.position,_nextPatrollPoint.transform.position)<DistanceOffset)
            _nextPatrollPoint = _nextPatrollPoint == a ? b : a;
        //Change Next PatrollPoint if Reached to a point

    }
}
