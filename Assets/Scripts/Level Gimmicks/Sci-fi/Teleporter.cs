using UnityEngine;
using System.Collections;

public class Teleporter : MonoBehaviour
{
    public Transform teleportDestination;
    private bool canTeleport = true;

    private void OnTriggerEnter(Collider other)
    {
        if (!canTeleport) return;
        if (!other.CompareTag("Player")) return;

        StartCoroutine(TeleportPlayer(other));
    }

    private IEnumerator TeleportPlayer(Collider player)
    {
        canTeleport = false;

        PlayerMovement movement = player.GetComponent<PlayerMovement>();
        if (movement) movement.enabled = false;

        player.transform.SetPositionAndRotation(
            teleportDestination.position,
            teleportDestination.rotation
        );

        // wait ONE frame
        yield return null;

        if (movement) movement.enabled = true;

        Debug.Log("Player teleported to: " + teleportDestination.position);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canTeleport = true;
        }
    }
}
