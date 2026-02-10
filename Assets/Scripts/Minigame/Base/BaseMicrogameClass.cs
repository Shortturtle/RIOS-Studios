using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BaseMicrogameClass : MonoBehaviour
{
    protected BaseTowerClass towerClass;
    //public GameObject tickImg;
    public virtual void StartMicrogame() //starts microgame
    {
        Debug.Log("Start");
    }

    public virtual void EndMicrogame(GameObject tickImg) //ends microgame
    {
        StartCoroutine(TickAfterMinigame(tickImg));

        //MicrogameManager.instance.MicrogameEnd();
        //Debug.Log("End");
    }

    private IEnumerator TickAfterMinigame(GameObject tickImg)
    {
        GameObject img = Instantiate(tickImg, GameObject.FindGameObjectWithTag("MicrogameCanvas").transform);
        yield return new WaitForSeconds(1.5f);
        Destroy(img);
        MicrogameManager.instance.MicrogameEnd();
    }
}
