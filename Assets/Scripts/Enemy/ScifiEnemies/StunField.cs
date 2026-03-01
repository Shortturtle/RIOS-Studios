using Unity.VisualScripting;
using UnityEngine;

public class StunField : MonoBehaviour
{
    //stun stats
    public float stunDuration;
    public float stunRange;

    //activate the stun field
    private void Start() { StunActivate(); }
    private void StunActivate() //AoE damage
    {
        //get towers in range, detects if offencetowerbase is in, then calls stun func in the tower
        Collider[] collidersInRange = Physics.OverlapSphere(transform.position, stunRange);

        foreach (Collider col in collidersInRange)
        {
            OffenseTowerBase getStunnedNerd = col.GetComponent<OffenseTowerBase>();

            if (getStunnedNerd != null)
            {
                getStunnedNerd.Stun(stunDuration);
            }
        }
        Destroy(gameObject); //destroy itself
    }
}
