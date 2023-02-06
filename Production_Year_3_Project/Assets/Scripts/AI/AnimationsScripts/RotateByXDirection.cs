using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class RotateByXDirection : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private CCController _controller;
    [SerializeField] Vector3 _rightVector;
    [SerializeField] Vector3 _leftVector;
    [SerializeField] Transform _objectToFlip;
    public bool _isUsingRigidbody;

    private void OnValidate()
    {
        if (!_objectToFlip)
        {
            _objectToFlip = transform;
        }
        if (_isUsingRigidbody)
        {
            if (!_rb)
            {
                _rb = GetComponent<Rigidbody>();
                if (!_rb)
                {
                    _rb = GetComponentInParent<Rigidbody>();
                }
            }
        }
        else
        {
            if (!_controller)
            {
                _controller = GetComponent<CCController>();
                if (!_controller)
                {
                    _controller = GetComponentInParent<CCController>();
                }
            }
        }

    }
    private void OnEnable()
    {
        StartCoroutine(WaitForMovingLeft());
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }
    IEnumerator WaitForMovingLeft()
    {
        if (_isUsingRigidbody)
        {
            yield return new WaitUntil(() => _rb.velocity.x < 0);
        }
        else
        {
            yield return new WaitUntil(() => _controller.Velocity.x < 0);
        }

        _objectToFlip.eulerAngles = new Vector3(_objectToFlip.eulerAngles.x,_leftVector.y, _objectToFlip.eulerAngles.z);

        if (!_isUsingRigidbody)
            _controller.facingRight = false;

        StartCoroutine(WaitForMovingRight());
    }

    IEnumerator WaitForMovingRight()
    {
        if (_isUsingRigidbody)
        {
            yield return new WaitUntil(() => _rb.velocity.x > 0);
        }
        else
        {
            yield return new WaitUntil(() => _controller.Velocity.x > 0);
        }
        _objectToFlip.eulerAngles = new Vector3(_objectToFlip.eulerAngles.x, _rightVector.y, _objectToFlip.eulerAngles.z);

        if (!_isUsingRigidbody)
            _controller.facingRight = true;

        StartCoroutine(WaitForMovingLeft());
    }
}
