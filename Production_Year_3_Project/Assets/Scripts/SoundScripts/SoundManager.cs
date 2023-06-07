using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
     private Dictionary<string, SoundSO> audioClips;
    [SerializeField] List<SoundSO> soundClipsList;
    

    private void Awake()
    {
        audioClips = new Dictionary<string, SoundSO>();
        foreach (var item in soundClipsList)
        {
            audioClips.Add(item.Name, item);
        }
        
    }
    private void Start()
    {
        Debug.Log(audioClips.Count);
    }
    public void PlaySound(SoundSO playedsound)
    {
        //playedsound = audioClips.
    }
}
