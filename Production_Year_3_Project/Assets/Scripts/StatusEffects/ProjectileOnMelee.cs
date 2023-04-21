using System.Collections;
using UnityEngine;

public class ProjectileOnMelee : StatusEffect
{
    float counter = 0f;
    private PlayerManager player => host as PlayerManager;
    public override void Reset()
    {
        counter = 0f;
    }

    protected override void Subscribe()
    {
        player.PlayerMeleeAttack.OnAttackPerformed.AddListener(ShootProjectile);
        player.StartCoroutine(Duration());
    }

    protected override void UnSubscribe()
    {
        player.PlayerMeleeAttack.OnAttackPerformed.RemoveListener(ShootProjectile);
    }

    private void ShootProjectile(Attack givenAttack)
    {
        Projectile bullet = GameManager.Instance.ObjectPoolsHandler.PlayerProjectileObjectPool.GetPooledObject();
        bullet.transform.position = player.transform.position;
        bullet.CacheStats(givenAttack, player.DamageDealer);
        if (player.PlayerController.facingRight)
        {
            bullet.Blast(new Vector3(1, 0, 0));
        }
        else
        {
            bullet.Blast(new Vector3(-1, 0, 0));
        }
        bullet.gameObject.SetActive(true);
    }

    private IEnumerator Duration()
    {
        while (counter < 5)
        {
            counter += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        host.Effectable.RemoveStatusEffect(this);
    }
}
