using UnityEngine;

public class SpeedField : MonoBehaviour
{
    //basically same as stun field except it affects specific enemy instead of tower
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
                flyingGuys.speed += speedIncrease;
            }
        }

        Destroy(gameObject);
    }
}
