using System.Collections;
using UnityEngine;


[CreateAssetMenu(fileName = "DashTowardsEnemy", menuName = "Ability/QuovaxDrop")]
public class DashTowardsEnemy : Ability
{
    [SerializeField] private float dashSpeed;
    [SerializeField] private Attack dashEndAbility;
    [SerializeField, Range(0f, 10f)] private float dashApex;


    private PlayerManager player => Owner as PlayerManager;

    public override bool TryCast()
    {
        BaseCharacter target = player.EnemyProximitySensor.GetClosestLegalTarget();
        if (ReferenceEquals(target, null))
        {
            return false;
        }
        return true;
    }
    public override void Cast()
    {
        player.StartCoroutine(dashTowardsTarget());
    }

    private IEnumerator dashTowardsTarget()
    {
        BaseCharacter target = player.EnemyProximitySensor.GetClosestLegalTarget();
        if (ReferenceEquals(target, null))
        {
            yield break;
        }
        ResetPlayer(false);
        Vector3 dest = new Vector3(target.MiddleOfBody.position.x, target.MiddleOfBody.position.y, 0);
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
        player.Effectable.ApplyStatusEffect(new Invulnerability());
        player.PlayerController.AnimBlender.SetBool("SuperDash", true);
        player.SwordVFX.PlayQuovaxDashParticle();
        while (counter < 1)
        {
            Vector3 positionLerp = Vector3.Lerp(startPos, dest, counter);
            player.transform.position = positionLerp;
            counter += Time.deltaTime * dashSpeed;
            yield return new WaitForEndOfFrame();
        }
        target.Damageable.GetHit(dashEndAbility, player.DamageDealer);
        GameManager.Instance.Cam.CamShake();
        player.Effectable.RemoveStatusEffect(new Invulnerability());
        player.SwordVFX.StopQuovaxDashParticle();
        player.Gfx.eulerAngles = playerRot;
        player.PlayerController.AnimBlender.SetBool("SuperDash", false);
        ResetPlayer(true);
        player.PlayerController.ZeroGravity();
        yield return new WaitForSecondsRealtime(dashApex);
        player.PlayerController.ResetGravity();
    }

    private void ResetPlayer(bool state)
    {
        player.PlayerController.ResetGravity();
        player.PlayerController.ResetVelocity();
        player.PlayerController.CanMove = state;
        player.PlayerMeleeAttack.CanAttack = state;
        player.PlayerAbilityHandler.CanCast = state;
    }
   
}

