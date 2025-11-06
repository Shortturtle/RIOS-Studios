using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SPMGManager : MonoBehaviour
{
    private bool mgOpen = false;
    private bool canOpenMG = true;
    public bool minigameComplete = false;


    public void MinigamePopup()
    {
        if (mgOpen == false)
        {
            if (canOpenMG == true)
            {
                StartCoroutine(OpenMinigame());
            }
        }
        else
        {
            if (canOpenMG == true)
            {
                if (minigameComplete)
                {
                    StartCoroutine(CloseMinigame());
                }
                
            }
        }
    }

    private IEnumerator OpenMinigame()
    {
        transform.LeanMoveLocal(new Vector2(0, 0), 0.5f).setEaseOutCirc();
        mgOpen = true;
        canOpenMG = false;
        yield return new WaitForSeconds(0.6f);
        canOpenMG = true;
    }
    private IEnumerator CloseMinigame()
    {
        transform.LeanMoveLocal(new Vector2(0, -1075), 0.5f).setEaseOutCirc();
        mgOpen = false;
        canOpenMG = false;
        yield return new WaitForSeconds(0.6f);
        canOpenMG = true;
    }
}
