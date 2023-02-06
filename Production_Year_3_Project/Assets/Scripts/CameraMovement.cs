using Cinemachine;
using UnityEngine;
using System;
using System.Collections;


public class CameraMovement : MonoBehaviour
{
    private CinemachineFramingTransposer moveCamComp;
    private CinemachineBasicMultiChannelPerlin camShakeComp;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private float minHeight;
    [SerializeField] private float amplitude;
    [SerializeField] private float frequency;
    [SerializeField] private float shakeDuration;

    Coroutine activeShakeRoutine;

    private bool holdingDown;
    private void Start()
    {
        moveCamComp = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
        camShakeComp = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        GameManager.Instance.InputManager.OnLookDownDown.AddListener(HoldDown);
        GameManager.Instance.InputManager.OnLookDownUp.AddListener(ReleaseHoldingDown);
        GameManager.Instance.CacheCam(this);
        GameManager.Instance.PlayerManager.Damageable.OnTakeDmgGFX.AddListener(CamShake);
    }

    private void Update()
    {
        if (holdingDown)
        {
            MoveCameraYDownWards();
        }
    }
    
    [ContextMenu("Shake")]
    public void CamShake()
    {
        Debug.Log("shaking");
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
    }

    private void MoveCameraYDownWards()
    {
        moveCamComp.m_ScreenY -= Time.deltaTime;
        moveCamComp.m_ScreenY = Mathf.Clamp(moveCamComp.m_ScreenY, minHeight, 1.5f);
    }


    private void ReleaseHoldingDown()
    {
        holdingDown = false;
        moveCamComp.m_ScreenY = 0.5f;
    }

    private void HoldDown()
    {
        holdingDown = true;
    }
}
