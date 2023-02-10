using UnityEngine;
[CreateAssetMenu(fileName = "New RotationAction SO", menuName = "ScriptableObjects/Rotation/RotationAction")]
public class RotationAction : BaseAction<RotationActionData>
{
    [SerializeField] float _angleOffset = 5;
    [SerializeField] float _rightRotationAngle = 90;
    [SerializeField] float _leftRotationAngle = -90;
    [SerializeField] float _frontRotationAngle = 180;
    [SerializeField] float _backRotationAngle = 0;
    [SerializeField] float _minRotationPerFrame = -180;
    [SerializeField] float _maxRotationPerFrame = 180;
    [SerializeField] float _minAmountOfRotationPerFrame = 5;
    [SerializeField] bool _isRotatingTowardsCam = true;

    public override bool InitAction(RotationActionData rotationData)
    {
        float rotationAngle = 0;
        if (rotationData.Direction == RotationDirectionType.Right)
        {
            rotationAngle = _rightRotationAngle;
        }
        else if (rotationData.Direction == RotationDirectionType.Left)
        {
            rotationAngle = _leftRotationAngle;
        }
        else if (rotationData.Direction == RotationDirectionType.Front)
        {
            rotationAngle = _frontRotationAngle;
        }
        else if (rotationData.Direction == RotationDirectionType.Back)
        {
            rotationAngle = _backRotationAngle;
        }
        else
        {
            throw new System.Exception("Rotation Direction has no direction");
        }
        Rotate(rotationData.RotationObject, rotationAngle);
        return true;
    }
    private void Rotate(GameObject rotationObject,float wantedAngle)
    {
        //make sure that the delta will be up to 180 degreese
        if (wantedAngle < 0 && rotationObject.transform.eulerAngles.y >= 0)
        {
            wantedAngle += 360;
        }
        else if (wantedAngle >0 && rotationObject.transform.eulerAngles.y < 0)
        {
            wantedAngle -= 360;
        }
        //calculate delta between current y angle and the wanted angle
        var absDeltaOfRotation = rotationObject.transform.eulerAngles.y - wantedAngle;
        //if angle is bigger or smaller than 360 there is no need to make the transition that big, we shorten it
        if (absDeltaOfRotation > 360 || absDeltaOfRotation < -360)
        {
            absDeltaOfRotation %= 360;
        }

        //absolute angle in order to check offset so we can stop in time
        absDeltaOfRotation = Mathf.Abs(absDeltaOfRotation);
        if (absDeltaOfRotation < _angleOffset)
        {
            return;
        }
        //multiply by delta time to make a smooth rotation
        var additionThisFrame = absDeltaOfRotation * Time.deltaTime;

        //Rotate towards camera or against it by Game design choice
        if (_isRotatingTowardsCam)
        {
            if (wantedAngle <180)
                additionThisFrame = -additionThisFrame;
        }
        else
        {
            if (wantedAngle > 180)
                additionThisFrame = -additionThisFrame;
        }

        //limit amount of rotation this frame
        additionThisFrame = Mathf.Clamp(additionThisFrame, _minRotationPerFrame, _maxRotationPerFrame);
        if (additionThisFrame < _minAmountOfRotationPerFrame)
        {
            if (additionThisFrame < 0)
            {
                additionThisFrame = -_minAmountOfRotationPerFrame;
            }
            else
            {
                additionThisFrame = _minAmountOfRotationPerFrame;
            }
        }
        //Rotate with unity method, and the magic happens
        rotationObject.transform.Rotate(new Vector3(0, additionThisFrame, 0));
    }
}

