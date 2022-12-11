using UnityEngine;

public class WalkData
{
    public WalkData(Rigidbody rigidbody, Vector3 direction)
    {
        RB = rigidbody;
        Direction = direction;
    }
    public Rigidbody RB;
    public Vector3 Direction;
}
