using Unity.VisualScripting;
using UnityEngine;

public class BaseMicrogameClass : MonoBehaviour
{
    protected BaseTowerClass towerClass;
    public virtual void StartMicrogame() //starts microgame
    {
        Debug.Log("Start");
    }

    public virtual void EndMicrogame() //ends microgame
    {
        MicrogameManager.instance.MicrogameEnd(); 
        Debug.Log("End");
    }
}
