using System.Collections;
using UnityEngine;

public class KanadeCollect : MonoBehaviour
{
    public GameObject objectRenderer;
    public BoxCollider hitBox;
    //private bool collectable = true;
    //test for the quest functionality
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") /*&& collectable == true*/)
        {
            hitBox.enabled = false;
            CollectKanade();
        }
    }

    private void CollectKanade()
    {
        //ref to the action in miscEvents script through GameEventManager (i assume)
        GameEventManager.instance.miscEvents.KanadeCollected();

        StartCoroutine(AfterCollected());
    }

    public IEnumerator AfterCollected()
    {
        objectRenderer.SetActive(false);
        
        yield return new WaitForSeconds(5);
        hitBox.enabled = true;
        objectRenderer.SetActive(true);
    }
}
