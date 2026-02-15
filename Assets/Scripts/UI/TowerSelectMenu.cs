using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerSelectMenu : MonoBehaviour
{
    public GameObject towerCollection;
    public Animator towerMenuAnimator;
    private bool menuOpen = false;
    private bool canOpenMenu = true;

    private void Start()
    {
        towerCollection.SetActive(false);
    }
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
        towerCollection.SetActive (true);
        towerMenuAnimator.SetTrigger("Open");
        menuOpen = true;
        canOpenMenu = false;
        yield return new WaitForSeconds(0.42f);
        towerMenuAnimator.SetBool("Stay", true);
        canOpenMenu = true;
    }
    private IEnumerator CloseTowerSelectMenu()
    {
        towerMenuAnimator.SetBool("Stay", false);
        menuOpen = false;
        canOpenMenu = false;
        yield return new WaitForSeconds(0.42f);
        canOpenMenu = true;
        towerCollection.SetActive(false);
        towerMenuAnimator.ResetTrigger("Open");
    }
}
