using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioSourcePooler audioSourcePooler;
    [SerializeField] SoundHandler soundHandler;
#if UNITY_EDITOR
    [Button("TestPlaySound")]
    void PlaySoundTest()
    {
        PlaySound("TestSound");
    }
#endif
    public void PlaySound(string name)
    {
        AudioObject newAudio = audioSourcePooler.GetPooledObject();
        newAudio.gameObject.SetActive(true);
        soundHandler.PlaySound(name, newAudio.AudioSource);
    }
}
