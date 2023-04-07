using System.Collections;
using UnityEngine;

public class Spikes : DamageDealingCollider
{
    [SerializeField] private Attack refAttack;
    private Transform respawnPoint;
    bool _respawning = false;


    private void Start()
    {
        OnColliderDealtDamage.AddListener(StartRespawnPlayer);
        CacheReferences(refAttack);
    }


    private void StartRespawnPlayer()
    {
        //routine blackscreen fadeout...
        //+ reset room
        StartCoroutine(RespawnPlayer());
    }

    private IEnumerator RespawnPlayer()
    {
        respawnPoint = GameManager.Instance.RoomsManager.CurrentCheckpoint;
        if (_respawning)
        {
            yield break;
        }
        _respawning = true;
        var playerManager = GameManager.Instance.PlayerManager;
        playerManager.PlayerController.ResetVelocity();
        playerManager.LockPlayer();
        yield return StartCoroutine(GameManager.Instance.UiManager.PlayerHud.FadeToBlack());
        GameManager.Instance.RoomsManager.ResetRoom();
        Vector3 startPos = GameManager.Instance.PlayerManager.PlayerController.transform.position;
        float counter = 0f;
        while (counter < 1)
        {
            counter += Time.deltaTime * 10; //increase this/ mult by 10?
            GameManager.Instance.PlayerManager.PlayerController.transform.position = Vector3.Lerp(startPos, respawnPoint.position, counter);
            yield return new WaitForEndOfFrame();
        }
        _respawning = false;
        /* GameManager.Instance.PlayerManager.gameObject.SetActive(false);
         GameManager.Instance.PlayerManager.transform.position = respawnPoint.position;
         yield return new WaitForEndOfFrame();
         GameManager.Instance.PlayerManager.gameObject.SetActive(true);*/
        if (playerManager.Damageable.CurrentHp > 0)
        {
            playerManager.UnLockPlayer();
        }
        yield return StartCoroutine(GameManager.Instance.UiManager.PlayerHud.FadeFromBlack());
    }
}
