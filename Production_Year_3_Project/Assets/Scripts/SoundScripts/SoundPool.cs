using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

public class SoundPool : MonoBehaviour
{    
    [SerializeField] GameObject AudioPrefab;
    [SerializeField] List<AudioSource> audioSources;
    [SerializeField] int poolSize;
    bool IsSetActive = false;
#if UNITY_EDITOR
    [Button("CreateNewAudioSourceTest")]
    void CreateNewAudioSourceTest()
    {
        CreateNewAudioSource();
    }
    [Button("GetAudioSourceTest")]
    void GetAudioSourceTest()
    {
        GetAudioSource();
    }


#endif
    private void Awake()
    {
        IsSetActive = false;
        audioSources = new List<AudioSource>();

        for (int i = 0; i < poolSize; i++)
        {
            CreateNewAudioSource();
        }
    }

    private AudioSource CreateNewAudioSource()
    {
        GameObject audioObj = Instantiate(AudioPrefab, transform);
        AudioSource audioSource = audioObj.GetComponent<AudioSource>();
        audioSource.gameObject.SetActive(false);
        audioSources.Add(audioSource);
        return audioSource;
    }

    public AudioSource GetAudioSource()
    {
        foreach (AudioSource audioSource in audioSources)
        {
            if (!audioSource.gameObject.activeInHierarchy)
            {
                audioSource.gameObject.SetActive(true);
                return audioSource;
            }                                        
        }
        CreateNewAudioSource().gameObject.SetActive(true);
        return audioSources[audioSources.Count - 1];
    }
}