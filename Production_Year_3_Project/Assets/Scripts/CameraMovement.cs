using Cinemachine;
using UnityEngine;
using System;
using System.Collections;


public class CameraMovement : MonoBehaviour
{
    private CinemachineFramingTransposer moveCamComp;
    private CinemachineBasicMultiChannelPerlin camShakeComp;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private CinemachineBrain brain;
    [SerializeField] private float minVerticalDistacne;
    [SerializeField] private float maxVerticalDistance;
    [SerializeField] private float minHorizontalDistance;
    [SerializeField] private float maxHorizontalDistance;
    [SerializeField] private float amplitude;
    [SerializeField] private float frequency;
    [SerializeField] private float shakeDuration;

    Coroutine activeShakeRoutine;

    private bool holdingDown;
    private bool holdingUp;
    private bool holdingLeft;
    private bool holdingRight;
    private void Start()
    {
        moveCamComp = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
        camShakeComp = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        GameManager.Instance.InputManager.OnLookDownDown.AddListener(HoldDown);
        GameManager.Instance.InputManager.OnLookUpDown.AddListener(HoldUp);
        GameManager.Instance.InputManager.OnLookRightDown.AddListener(HoldLeft);
        GameManager.Instance.InputManager.OnLookLeftDown.AddListener(HoldRight);

        GameManager.Instance.InputManager.OnLookDownUp.AddListener(ReleaseHoldingCam);
        GameManager.Instance.InputManager.OnLookUpUp.AddListener(ReleaseHoldingCam);
        GameManager.Instance.InputManager.OnLookLeftUp.AddListener(ReleaseHoldingCam);
        GameManager.Instance.InputManager.OnLookRightUp.AddListener(ReleaseHoldingCam);

        GameManager.Instance.PlayerManager.PlayerController.GroundCheck.OnNotGrounded.AddListener(ReleaseHoldingCam);
        GameManager.Instance.CacheCam(this);
        GameManager.Instance.PlayerManager.Damageable.OnTakeDmgGFX.AddListener(CamShake);
    }

    private void Update()
    {
        if (GameManager.Instance.PlayerManager.PlayerController.GroundCheck.IsGrounded())
        {
            if (holdingDown)
            {
                MoveCameraYDownWards();
            }
            else if (holdingUp)
            {
                MoveCameraYUpwards();
            }
            else if (holdingRight)
            {
                MoveCameraXForwards();
            }
            else if (holdingLeft)
            {
                MoveCameraXBackwards();
            }
        }
      
    }
    
    [ContextMenu("Shake")]
    public void CamShake()
    {
        camShakeComp.m_AmplitudeGain = amplitude;
        camShakeComp.m_FrequencyGain = frequency;
        if (!ReferenceEquals(activeShakeRoutine, null))
        {
            StopCoroutine(activeShakeRoutine);
        }
        activeShakeRoutine = StartCoroutine(StopShakeCountDown());
    }

    private IEnumerator StopShakeCountDown()
    {
        yield return new WaitForSecondsRealtime(shakeDuration);
        camShakeComp.m_AmplitudeGain = 0f;
        camShakeComp.m_FrequencyGain = 0f;
        brain.transform.rotation = Quaternion.Euler(Vector3.zero);
        //set brain rotation to v3.z
    }

    private void MoveCameraYDownWards()
    {
        moveCamComp.m_ScreenY -= Time.deltaTime;
        moveCamComp.m_ScreenY = Mathf.Clamp(moveCamComp.m_ScreenY, minVerticalDistacne, maxVerticalDistance);
    }
    private void MoveCameraYUpwards()
    {
        moveCamComp.m_ScreenY += Time.deltaTime;
        moveCamComp.m_ScreenY = Mathf.Clamp(moveCamComp.m_ScreenY, minVerticalDistacne, maxVerticalDistance);
    }
    private void MoveCameraXBackwards()
    {
        moveCamComp.m_ScreenX -= Time.deltaTime;
        moveCamComp.m_ScreenX = Mathf.Clamp(moveCamComp.m_ScreenX, minHorizontalDistance, maxHorizontalDistance);
    }
    private void MoveCameraXForwards()
    {
        moveCamComp.m_ScreenX += Time.deltaTime;
        moveCamComp.m_ScreenX = Mathf.Clamp(moveCamComp.m_ScreenX, minHorizontalDistance, maxHorizontalDistance);
    }
    private void ReleaseHoldingCam()
    {
        holdingDown = false;
        holdingUp = false;
        holdingLeft = false;
        holdingRight = false;
        moveCamComp.m_ScreenY = 0.5f;
        moveCamComp.m_ScreenX = 0.5f;
    }

    private void HoldDown()
    {
        holdingDown = true;
    }
    private void HoldUp()
    {
        holdingUp = true;
    }
    private void HoldRight()
    {
        holdingRight = true;
    }
    private void HoldLeft()
    {
        holdingLeft = true;
    }
}
