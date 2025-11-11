using UnityEngine;
using System.Collections;

public class FNMGManager : MonoBehaviour
{
    //for managing open and closing of minigame window
    private bool mgOpen = false;
    private bool canOpenMG = true;

    public bool minigameComplete = false;
    public int completion = 0;
    public int numberToCompleteMinigame;

    public BaseTowerClass tower;
    //public FruitSpawner spawner;

    //progress check is a lie it actually helps count to the numberToCompleteMinigame
    public void ProgressCheck()
    {
        completion++;
        //if enough screws are replaced, minigame is cmplete and minigame close func is activated
        if (completion == numberToCompleteMinigame) { minigameComplete = true; }
        if (minigameComplete)
        {
            StartCoroutine(CloseMinigame());
        }
    }

    //use to activate open minigame func
    public void MinigamePopup()
    {
        if (mgOpen == false)
        {
            if (canOpenMG == true)
            {
                StartCoroutine(OpenMinigame());
            }
        }
    }

    //Opening and closing minigame
    private IEnumerator OpenMinigame()
    {
        transform.LeanMoveLocal(new Vector2(0, 0), 0.5f).setEaseOutCirc();
        mgOpen = true;
        canOpenMG = false;
        yield return new WaitForSeconds(0.6f);
        canOpenMG = true;
        GetComponent<FruitSpawner>().enabled = true;
    }
    private IEnumerator CloseMinigame()
    {
        GetComponent<FruitSpawner>().enabled = false;
        transform.LeanMoveLocal(new Vector2(0, -1075), 0.5f).setEaseOutCirc();
        mgOpen = false;
        canOpenMG = false;
        yield return new WaitForSeconds(0.6f);
        canOpenMG = true;
        tower.UndoDegrade();
    }

    public void InitalizeTower(BaseTowerClass towerToInitialize)
    {
        tower = towerToInitialize;
    }
}
