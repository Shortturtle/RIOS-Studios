using Unity.VisualScripting;
using UnityEngine;

public class StunField : MonoBehaviour
{
    public float stunDuration;
    public float stunRange;
    private void Start()
    {
        StunActivate();
    }

    private void StunActivate() //AoE damage
    {
        Collider[] collidersInRange = Physics.OverlapSphere(transform.position, stunRange);

        foreach (Collider col in collidersInRange)
        {
            OffenseTowerBase getStunnedNerd = col.GetComponent<OffenseTowerBase>();

            if (getStunnedNerd != null)
            {
                //projectileEvent.Post(this.gameObject);
                getStunnedNerd.Stun(stunDuration);
            }
        }

        Destroy(gameObject);
    }
}
