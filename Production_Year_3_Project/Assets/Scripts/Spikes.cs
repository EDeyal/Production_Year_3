using System;
using System.Collections;
using UnityEngine;

public class Spikes : DamageDealingCollider
{
    [SerializeField] private Attack refAttack;
    [SerializeField] private Transform respawnPoint;
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

    internal void AssignCheckpoint(Transform assignedRespawnCheckpoint)
    {
        respawnPoint = assignedRespawnCheckpoint;
    }

    private IEnumerator RespawnPlayer()
    {
        if (_respawning)
        {
            yield break;
        }
        _respawning = true;
        var playerManager = GameManager.Instance.PlayerManager;
        playerManager.PlayerController.ResetVelocity();
        GameManager.Instance.InputManager.LockInputs = true;
        playerManager.LockPlayer();
        yield return StartCoroutine(GameManager.Instance.UiManager.PlayerHud.FadeToBlack());
        GameManager.Instance.RoomsManager.ResetRoom();
        Vector3 startPos = GameManager.Instance.PlayerManager.PlayerController.transform.position;
        float counter = 0f;
        while (counter < 1)
        {
            counter += Time.deltaTime;
            GameManager.Instance.PlayerManager.PlayerController.transform.position = Vector3.Lerp(startPos, respawnPoint.position, counter);
            yield return new WaitForEndOfFrame();
        }

        // TODO: Move player should be a function in player, that should also reset all forces applied to the player CC
        /*   yield return new WaitForEndOfFrame();
           playerManager.PlayerController.enabled = false;
           playerManager.PlayerController.transform.position = new Vector3(respawnPoint.position.x, respawnPoint.position.y, respawnPoint.position.z);
           playerManager.PlayerController.enabled = true;*/
        playerManager.UnLockPlayer();
        yield return StartCoroutine(GameManager.Instance.UiManager.PlayerHud.FadeFromBlack());
        GameManager.Instance.InputManager.LockInputs = false;
        _respawning = false;
    }
}
