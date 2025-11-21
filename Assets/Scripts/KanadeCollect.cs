using UnityEngine;

public class KanadeCollect : MonoBehaviour
{
    //test for the quest functionality
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CollectKanade();
        }
    }

    private void CollectKanade()
    {
        //ref to the action in miscEvents script through GameEventManager (i assume)
        GameEventManager.instance.miscEvents.KanadeCollected();

        Destroy(gameObject);
    }
}
