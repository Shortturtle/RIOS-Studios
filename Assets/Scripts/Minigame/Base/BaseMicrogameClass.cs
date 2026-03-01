using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BaseMicrogameClass : MonoBehaviour
{
    protected BaseTowerClass towerClass;
    public bool minigameEnd;
    //public GameObject tickImg;
    public virtual void StartMicrogame() //starts microgame
    {
        Debug.Log("Start");
    }

    public virtual void EndMicrogame(GameObject tickImg) //ends microgame
    {
        if (minigameEnd) { return; }
        StartCoroutine(TickAfterMinigame(tickImg));
        minigameEnd = true;

        //MicrogameManager.instance.MicrogameEnd();
        //Debug.Log("End");
    }

    private IEnumerator TickAfterMinigame(GameObject tickImg)
    {
        GameObject img = Instantiate(tickImg, GameObject.FindGameObjectWithTag("MicrogameCanvas").transform);
        img.transform.SetParent(gameObject.transform);
        yield return new WaitForSeconds(0.8f);
        Destroy(img);
        MicrogameManager.instance.MicrogameEnd();
    }
}
