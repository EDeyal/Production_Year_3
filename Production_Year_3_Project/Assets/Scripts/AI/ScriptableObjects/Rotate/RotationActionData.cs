using System.Collections;
using UnityEngine;
public enum RotationDirectionType
{
    None,
    Right,
    Left,
    Front,
    Back
}
public class RotationActionData
{
    RotationDirectionType _direction;
    GameObject _rotationObject;
    public RotationDirectionType Direction => _direction;
    public GameObject RotationObject => _rotationObject;
    public RotationActionData(GameObject rotationObject)
    {
        _rotationObject = rotationObject;
    }
    public void UpdateRotationData(RotationDirectionType direction)
    {
        _direction = direction;
    }
}
