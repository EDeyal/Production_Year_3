using Cinemachine;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Playables;

public class CinematicTrigger : MonoBehaviour, ICheckValidation, IRespawnable
{
    [SerializeField] PlayableDirector _playableDirector;
    [SerializeField] Cinemachine.CinemachineVirtualCamera _cinematicCam;
    Cinemachine.CinemachineTrackedDolly _trackedDolly;
    [ReadOnly] bool _hasPlayed = false;
    //[SerializeField] float _speed = 1;
    static int _camPriority = 11;
    [ReadOnly] bool _isPlaying = false;
    public void Awake()
    {
        _trackedDolly = _cinematicCam.GetCinemachineComponent<CinemachineTrackedDolly>();
        if (_trackedDolly == null)
            Debug.LogError("Cinematic Missing Conponnent");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_hasPlayed)
        {
            if (!_isPlaying)
            {
                Debug.Log("CinematicTrigger Triggered");

                if (other.CompareTag("Player"))
                {
                    Debug.Log("CinematicTrigger Activated");
                    ActivateCinematicCamera();
                }
            }
        }
    }
    public void ActivateCinematicCamera()
    {
        _playableDirector.Play();
        GameManager.Instance.InputManager.input.Disable();
        GameManager.Instance.UiManager.PlayerHud.gameObject.SetActive(false);
        _isPlaying = true;
    }
    private void Update()
    {
        if (_isPlaying)
        {
            if (_trackedDolly.m_PathPosition >= _trackedDolly.m_Path.MaxPos)
            {
                //reached the end
                _hasPlayed = true;
                ResetCinematic();
            }
        }
    }
    public void ResetCinematic()
    {
        _isPlaying = false;
        GameManager.Instance.InputManager.input.Enable();
        GameManager.Instance.UiManager.PlayerHud.gameObject.SetActive(true);
    }

    public void CheckValidation()
    {
        if (_cinematicCam == null)
            throw new System.Exception("Cinematic Trigger has no Camera");
    }

    public void Respawn()
    {
        _hasPlayed = false;
        _isPlaying = false;
        _trackedDolly.m_PathPosition = 0;
    }
}
