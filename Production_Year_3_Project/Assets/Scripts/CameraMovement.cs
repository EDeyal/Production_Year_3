using Cinemachine;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private CinemachineFramingTransposer magicComp;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private float minHeight;

    private bool holdingDown;
    private void Start()
    {
        magicComp = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
        GameManager.Instance.InputManager.OnLookDownDown.AddListener(HoldDown);
        GameManager.Instance.InputManager.OnLookDownUp.AddListener(ReleaseHoldingDown);
    }

    private void Update()
    {
        if (holdingDown)
        {
            MoveCameraYDownWards();
        }
    }

    private void MoveCameraYDownWards()
    {
        magicComp.m_ScreenY -= Time.deltaTime;
        magicComp.m_ScreenY = Mathf.Clamp(magicComp.m_ScreenY, minHeight, 1.5f);
    }


    private void ReleaseHoldingDown()
    {
        holdingDown = false;
        magicComp.m_ScreenY = 0.5f;
    }

    private void HoldDown()
    {
        holdingDown = true;
    }
}
