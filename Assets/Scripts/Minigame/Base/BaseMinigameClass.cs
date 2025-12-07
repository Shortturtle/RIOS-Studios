using Unity.VisualScripting;
using UnityEngine;

public class BaseMinigameClass : MonoBehaviour
{
    protected BaseTowerClass towerClass;
    public virtual void StartMinigame(BaseTowerClass tower)
    {
        towerClass = tower;
        Debug.Log("Start");

    }

    public virtual void EndMinigame()
    {
        towerClass.RepairTower();
        Debug.Log("End");
    }
}
