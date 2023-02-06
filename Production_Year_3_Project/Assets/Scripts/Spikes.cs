using System;
using System.Collections;
using UnityEngine;

public class Spikes : DamageDealingCollider
{
    [SerializeField] private Attack refAttack;
    [SerializeField] private Transform respawnPoint;


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
        GameManager.Instance.PlayerManager.PlayerController.ResetVelocity();
        GameManager.Instance.PlayerManager.LockPlayer();
        yield return StartCoroutine(GameManager.Instance.UiManager.PlayerHud.FadeToBlack());
        Vector3 startPos = GameManager.Instance.PlayerManager.PlayerController.transform.position;
        //float counter = 0f;
        //while (counter < 1)
        //{
        //    counter += Time.deltaTime;
        //    GameManager.Instance.PlayerManager.PlayerController.transform.position = Vector3.Lerp(startPos, respawnPoint.position, counter);
        //    yield return new WaitForEndOfFrame();
        //}

        // TODO: Move player should be a function in player, that should also reset all forces applied to the player CC
        yield return new WaitForEndOfFrame();
        GameManager.Instance.PlayerManager.PlayerController.enabled = false;
        GameManager.Instance.PlayerManager.PlayerController.transform.position = new Vector3(respawnPoint.position.x, respawnPoint.position.y, respawnPoint.position.z);
        GameManager.Instance.PlayerManager.PlayerController.enabled = true;
        GameManager.Instance.InputManager.LockInputs = true;
        GameManager.Instance.PlayerManager.UnLockPlayer();
        yield return StartCoroutine(GameManager.Instance.UiManager.PlayerHud.FadeFromBlack());
        GameManager.Instance.InputManager.LockInputs = false;



    }
}
