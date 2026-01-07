using UnityEngine;

public class AoEPotion : BaseProjectileClass
{
    public GameObject potion;

    protected override void ProjectileEffect()
    {
        BaseEnemyClass frickThisGuy = target.GetComponent<BaseEnemyClass>();

        if (frickThisGuy != null)
        {
            frickThisGuy.Damage(damage);
        }
    }
}
