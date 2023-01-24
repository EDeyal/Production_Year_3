using System.Collections;
using UnityEngine;


[CreateAssetMenu(fileName = "DashTowardsEnemy", menuName = "Ability/QuovaxDrop")]
public class DashTowardsEnemy : Ability
{
    [SerializeField] private float dashSpeed;
    [SerializeField] private Attack dashEndAbility;
    private PlayerManager player => Owner as PlayerManager;
    public override void Cast()
    {
        player.StartCoroutine(dashTowardsTarget());
    }

    private IEnumerator dashTowardsTarget()
    {
        BaseEnemy enemy = player.EnemyProximitySensor.GetClosestLegalTarget();
        if (ReferenceEquals(enemy, null))
        {
            yield break;
        }
        Vector3 dest = new Vector3(enemy.transform.position.x, enemy.transform.position.y + 1f, 0);
        player.PlayerController.ResetGravity();
        player.PlayerController.ResetVelocity();
        player.PlayerController.CanMove = false;
        player.PlayerAbilityHandler.CanCast = false;
        Vector3 startPos = player.transform.position;
        float counter = 0;
        player.PlayerController.AnimBlender.SetBool("Dash", true);
        while (counter < 1)
        {
            Vector3 positionLerp = Vector3.Lerp(startPos, dest, counter);
            player.transform.position = positionLerp;
            counter += Time.deltaTime * dashSpeed;
            yield return new WaitForEndOfFrame();
        }
        player.PlayerController.AnimBlender.SetBool("Dash", false);
        player.PlayerController.ResetGravity();
        player.PlayerController.ResetVelocity();
        player.PlayerController.CanMove = true;
        player.PlayerAbilityHandler.CanCast = true;
        enemy.Damageable.GetHit(dashEndAbility, player.DamageDealer);
    }
}

