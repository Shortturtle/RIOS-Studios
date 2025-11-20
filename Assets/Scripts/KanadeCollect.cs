using UnityEngine;

public class KanadeCollect : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CollectKanade();
        }
    }

    private void CollectKanade()
    {
        GameEventManager.instance.miscEvents.KanadeCollected();
        Destroy(gameObject);
    }
}
