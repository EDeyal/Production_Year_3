using System.Collections;
using UnityEngine;


[CreateAssetMenu(fileName = "DashTowardsEnemy", menuName = "Ability/QuovaxDrop")]
public class DashTowardsEnemy : Ability
{
    [SerializeField] private float dashSpeed;
    [SerializeField] private Attack dashEndAbility;
    [SerializeField, Range(0f, 10f)] private float dashApex;
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

        player.PlayerController.ResetGravity();
        player.PlayerController.ResetVelocity();
        player.PlayerController.CanMove = false;
        player.PlayerAbilityHandler.CanCast = false;
       
        Vector3 dest = new Vector3(enemy.transform.position.x, enemy.transform.position.y, 0);
        if (dest.x > player.transform.position.x)
        {
            player.PlayerFlipper.FlipRight();
        }
        else if (dest.x < player.transform.position.x)
        {
            player.PlayerFlipper.FlipLeft();
        }

        Vector3 playerRot = player.Gfx.transform.eulerAngles;
        player.Gfx.LookAt(dest);
        player.Gfx.eulerAngles = new Vector3(player.Gfx.eulerAngles.x, playerRot.y, playerRot.z);

        Vector3 startPos = player.transform.position;
        float counter = 0;
        Invulnerability buff = new Invulnerability();
        player.Effectable.ApplyStatusEffect(buff);
        player.PlayerController.AnimBlender.SetBool("SuperDash", true);
        player.SwordVFX.PlayQuovaxDashParticle();
        while (counter < 1)
        {
            Vector3 positionLerp = Vector3.Lerp(startPos, dest, counter);
            player.transform.position = positionLerp;
            counter += Time.deltaTime * dashSpeed;
            yield return new WaitForEndOfFrame();
        }
        player.SwordVFX.StopQuovaxDashParticle();
        player.Gfx.eulerAngles = playerRot;
        player.PlayerController.AnimBlender.SetBool("SuperDash", false);
        yield return new WaitForSecondsRealtime(dashApex);
        player.PlayerController.ResetGravity();
        player.PlayerController.ResetVelocity();
        player.PlayerController.CanMove = true;
        player.PlayerAbilityHandler.CanCast = true;
        enemy.gameObject.SetActive(false);
        buff.Remove();
        GameManager.Instance.Cam.CamShake();
        enemy.Damageable.GetHit(dashEndAbility, player.DamageDealer);
    }

}

