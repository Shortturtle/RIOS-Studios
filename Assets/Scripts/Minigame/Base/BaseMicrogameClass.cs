using Unity.VisualScripting;
using UnityEngine;

public class BaseMicrogameClass : MonoBehaviour
{
    protected BaseTowerClass towerClass;
    public virtual void StartMicrogame()
    {
        Debug.Log("Start");
    }

    public virtual void EndMicrogame()
    {
        MicrogameManager.instance.MicrogameEnd();
        Debug.Log("End");
    }
}
