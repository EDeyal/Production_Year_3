using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private PlayerVisuals visuals;
    [SerializeField] private PlayerData data;

    public PlayerVisuals Visuals { get => visuals;}
    public PlayerData Data { get => data;}
}
