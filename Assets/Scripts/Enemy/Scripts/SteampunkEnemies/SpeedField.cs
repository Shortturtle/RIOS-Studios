using UnityEngine;

public class SpeedField : MonoBehaviour
{
    public float speedIncrease;
    public float speedFieldRange;
    private void Start()
    {
        StunActivate();
    }

    private void StunActivate() //AoE damage
    {
        Collider[] collidersInRange = Physics.OverlapSphere(transform.position, speedFieldRange);

        foreach (Collider col in collidersInRange)
        {
            SteampunkFlyingEnemy flyingGuys = col.GetComponent<SteampunkFlyingEnemy>();

            if (flyingGuys != null)
            {
                //projectileEvent.Post(this.gameObject);
                flyingGuys.speed += speedIncrease;
            }
        }

        Destroy(gameObject);
    }
}
