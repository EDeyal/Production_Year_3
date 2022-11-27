using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] public SceneManager SceneManager;
    [SerializeField] public PlayerManager PlayerManager;


}
