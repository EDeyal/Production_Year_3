using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.VersionControl;
using UnityEngine;
[CreateAssetMenu(fileName = "Sound", menuName = "NewSound")]
public class SoundSO : ScriptableObject
{
    public AudioClip Test;
    public string Name; 
}
