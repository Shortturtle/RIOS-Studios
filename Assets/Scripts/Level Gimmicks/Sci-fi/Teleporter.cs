using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class Teleporter : MonoBehaviour
{
    public Transform teleportDestination;
    private bool canTeleport = true;
    public bool tpCamera;

    public GameObject tpOther;
    public float timeToTp;
    public float timeAfterTp;
    public float tpCd;

    public Animator tpAnim;

    private void OnTriggerEnter(Collider other)
    {
        if (!canTeleport) return;
        if (!other.CompareTag("Player")) return;

        StartCoroutine(TeleportPlayer(other));
    }

    private IEnumerator TeleportPlayer(Collider player)
    {
        canTeleport = false;
        tpOther.SetActive(false);

        StartCoroutine(Teleport(player));
        
        // wait ONE frame
        yield return null;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine("TpWait");
        }
    }

    private IEnumerator TpWait()
    {
        yield return new WaitForSeconds(tpCd);
        canTeleport = true;
        tpOther.SetActive(true);
    }
    private IEnumerator Teleport(Collider player)
    {
        if(tpCamera == true)
        {
            tpAnim.SetTrigger("teleport");
        }
        yield return new WaitForSeconds(timeToTp);
        PlayerMovement movement = player.GetComponent<PlayerMovement>();
        if (movement) movement.enabled = false;

        player.transform.SetPositionAndRotation(
            teleportDestination.position,
            teleportDestination.rotation
        );
        yield return new WaitForSeconds(timeAfterTp);
        if (movement) movement.enabled = true;
    }
}
