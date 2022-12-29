using UnityEngine;

public class MoveData
{
    private Rigidbody _rb;
    private Vector3 _direction;
    private float _speed;
    public Vector3 Direction => _direction;
    public Rigidbody RB => _rb;
    public float Speed => _speed;
    public MoveData(Rigidbody rigidbody, Vector3 direction,float speed)
    {
        _rb = rigidbody;
        _direction = direction;
        _speed = speed;
    }
    public void UpdateData(float speed)
    {
        _speed = speed;
    }
    public void UpdateData(Vector3 newDirection)
    {
        _direction = newDirection;
    }
    public void UpdateData(Vector3 newDirection, float speed)//should use most of the times
    {
        _direction = newDirection;
        _speed = speed;
    }

}
