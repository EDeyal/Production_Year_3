using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private PlayerHud playerHud;

    public void CachePlayerHud(PlayerHud givenHud)
    {
        playerHud = givenHud;
    }

    public PlayerHud PlayerHud { get => playerHud; }
}
