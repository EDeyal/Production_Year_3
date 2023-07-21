using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundHandler : MonoBehaviour
{
    private Dictionary<string, SoundSO> soundSos;
    [SerializeField] List<SoundSO> sosBank;


    private void Awake()
    {
        soundSos = new Dictionary<string, SoundSO>();
        foreach (var item in sosBank)
        {
            soundSos.Add(item.name, item);
        }

    }

    public void PlaySound(string name,AudioSource audioSource)
    {
        soundSos.TryGetValue(name, out SoundSO soundSo);
        if (soundSo != null)
        {
            SetAudioSource(audioSource, soundSo);
            audioSource.Play();
            StartCoroutine(WaitUntilDonePlaying(audioSource));
        }
        else
        {
            Debug.LogError("Sound Handler: soundSo equals Null");
        }
        
    }
    private void SetAudioSource(AudioSource audioSource,SoundSO soundSO)
    {
        audioSource.clip = soundSO.audioClip;
        audioSource.mute = soundSO.mute;
        audioSource.bypassEffects = soundSO.byPassEffect;
        audioSource.bypassListenerEffects = soundSO.byPassListener;
        audioSource.bypassReverbZones = soundSO.byPassReverbZone;
        audioSource.playOnAwake = soundSO.playOnAwake;
        audioSource.loop = soundSO.isLooping;
        audioSource.priority = (int)soundSO.priority;
        audioSource.volume = soundSO.volume;
        audioSource.pitch = soundSO.pitch;
        audioSource.reverbZoneMix = soundSO.reverbZoneMix;
        audioSource.spatialBlend = soundSO.spatialBlend;
        audioSource.dopplerLevel = soundSO.dopplerLevel;
        audioSource.rolloffMode = soundSO.rollOffMode;
        audioSource.minDistance = soundSO.minDistance;
        audioSource.maxDistance = soundSO.maxDistance;
    }

    IEnumerator WaitUntilDonePlaying(AudioSource audioSource)
    {
        yield return new WaitUntil(()=>!audioSource.isPlaying);
        audioSource.gameObject.SetActive(false);
    }
}
