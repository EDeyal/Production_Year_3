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
    private void Start()
    {
        Debug.Log(soundSos.Count);
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
        audioSource.loop = soundSO.isLooping;
        audioSource.volume = soundSO.volume;
        audioSource.pitch = soundSO.pitch;
    }

    IEnumerator WaitUntilDonePlaying(AudioSource audioSource)
    {
        yield return new WaitUntil(()=>!audioSource.isPlaying);
        audioSource.gameObject.SetActive(false);
    }
}
