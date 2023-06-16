using Cinemachine;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Playables;

public class CinematicTrigger : MonoBehaviour, ICheckValidation
{
    [SerializeField] PlayableDirector _playableDirector;
    [SerializeField] Cinemachine.CinemachineVirtualCamera _cinematicCam;
    Cinemachine.CinemachineTrackedDolly _trackedDolly;
    [ReadOnly] bool _isPlaying = false;
    [SerializeField] float _speed = 1;
    static int _camPriority = 11;
    [ReadOnly] bool _hasPlayed;
    public void Awake()
    {
        _trackedDolly = _cinematicCam.GetCinemachineComponent<CinemachineTrackedDolly>();
        if(_trackedDolly == null)
                    Debug.LogError("Cinematic Missing Conponnent");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!_hasPlayed)
        {
            Debug.Log("CinematicTrigger Triggered");

            if (other.CompareTag("Player"))
            {
                Debug.Log("CinematicTrigger Activated");
                ActivateCinematicCamera();
            }
        }
    }
    public void ActivateCinematicCamera()
    {
        _playableDirector.Play();
        _hasPlayed = true;
    }
    private void Update()
    {
        //if (_isPlaying)
        //{
        //    _trackedDolly.m_PathPosition += Time.deltaTime* _speed;
        //}
        //if (_trackedDolly.m_PathPosition >= _trackedDolly.m_Path.MaxPos)
        //{
        //    //reached the end
        //    ResetCinematic();
        //}
    }
    public void ResetCinematic()
    {

    }

    public void CheckValidation()
    {
        if (_cinematicCam == null)
            throw new System.Exception("Cinematic Trigger has no Camera");
    }
}
