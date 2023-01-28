using System.Collections;
using UnityEngine;


[CreateAssetMenu(fileName = "DashTowardsEnemy", menuName = "Ability/QuovaxDrop")]
public class DashTowardsEnemy : Ability
{
    [SerializeField] private float dashSpeed;
    [SerializeField] private Attack dashEndAbility;
    [SerializeField, Range(0f, 0.1f)] private float dashApex;
    private PlayerManager player => Owner as PlayerManager;
    public override void Cast()
    {
        player.StartCoroutine(dashTowardsTarget());
    }

    private IEnumerator dashTowardsTarget()
    {
        //BaseEnemy enemy = player.EnemyProximitySensor.GetClosestLegalTarget();
        Test enemy = player.TestProximitySensor.GetClosestLegalTarget();
        if (ReferenceEquals(enemy, null))
        {
            yield break;
        }
        Vector3 dest = new Vector3(enemy.transform.position.x, enemy.transform.position.y, 0);
        if (dest.x > player.transform.position.x)
        {
            player.PlayerFlipper.FlipRight();
        }
        else if (dest.x < player.transform.position.x)
        {
            player.PlayerFlipper.FlipLeft();
        }


        player.PlayerController.ResetGravity();
        player.PlayerController.ResetVelocity();
        player.PlayerController.CanMove = false;
        player.PlayerAbilityHandler.CanCast = false;
        Vector3 startPos = player.transform.position;
        float counter = 0;
        Invulnerability buff = new Invulnerability();
        player.Effectable.ApplyStatusEffect(buff);
        player.PlayerController.AnimBlender.SetBool("SuperDash", true);
        while (counter < 1)
        {
            Vector3 positionLerp = Vector3.Lerp(startPos, dest, counter);
            player.transform.position = positionLerp;
            counter += Time.deltaTime * dashSpeed;
            yield return new WaitForEndOfFrame();
        }

        player.PlayerController.AnimBlender.SetBool("SuperDash", false);
        player.PlayerController.ResetGravity();
        player.PlayerController.ResetVelocity();
        player.PlayerController.CanMove = true;
        player.PlayerAbilityHandler.CanCast = true;
        enemy.gameObject.SetActive(false);
        buff.Remove();
        //enemy.Damageable.GetHit(dashEndAbility, player.DamageDealer);
    }

}

