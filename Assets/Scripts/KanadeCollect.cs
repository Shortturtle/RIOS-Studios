using System.Collections;
using UnityEngine;

public class KanadeCollect : MonoBehaviour
{
    public GameObject objectRenderer;
    private bool collectable = true;
    //test for the quest functionality
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && collectable == true)
        {
            collectable = false;
            CollectKanade();
        }
    }

    private void CollectKanade()
    {
        //ref to the action in miscEvents script through GameEventManager (i assume)
        GameEventManager.instance.miscEvents.KanadeCollected();

        StartCoroutine(AfterCollected());
    }

    private IEnumerator AfterCollected()
    {
        objectRenderer.SetActive(false);

        yield return new WaitForSeconds(5);
        collectable = true;
        objectRenderer.SetActive(true);
    }
}
