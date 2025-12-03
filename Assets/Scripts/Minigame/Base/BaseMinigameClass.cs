using Unity.VisualScripting;
using UnityEngine;

public class BaseMinigameClass : MonoBehaviour
{
    protected BaseTowerClass towerClass;
    public virtual void StartMinigame()
    {
        //towerClass = tower;
        Debug.Log("Start");

    }

    public virtual void EndMinigame()
    {
        //towerClass.UndoDegrade();
        Debug.Log("End");
    }
}
