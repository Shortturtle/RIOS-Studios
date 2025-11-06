using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerSelectMenu : MonoBehaviour
{
    private bool menuOpen = false;
    private bool canOpenMenu = true;

    //checks for conditions so menu open/close isnt wacko
    public void TowerSelectMenuPopup()
    {
        if (menuOpen == false)
        {
            if(canOpenMenu == true)
            {
                StartCoroutine(OpenTowerSelectMenu());
            }
        }
        else
        {
            if (canOpenMenu == true)
            {
                StartCoroutine(CloseTowerSelectMenu());
            }
        }
    }

    //to open/close tower select menu
    private IEnumerator OpenTowerSelectMenu()
    {
        transform.LeanMoveLocal(new Vector2(290, 0), 0.5f).setEaseOutCirc();
        menuOpen = true;
        canOpenMenu = false;
        yield return new WaitForSeconds(0.6f);
        canOpenMenu = true;
    }
    private IEnumerator CloseTowerSelectMenu()
    {
        transform.LeanMoveLocal(new Vector2(1925, 0), 0.5f).setEaseOutCirc();
        menuOpen = false;
        canOpenMenu = false;
        yield return new WaitForSeconds(0.6f);
        canOpenMenu = true;
    }
}
