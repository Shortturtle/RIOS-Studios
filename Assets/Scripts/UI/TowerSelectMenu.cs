using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerSelectMenu : MonoBehaviour
{
    public GameObject towerCollection;
    public Animator towerMenuAnimator;
    public bool menuOpen = false;
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
        canOpenMenu = false;
        yield return new WaitForSeconds(0.42f);
        towerMenuAnimator.SetBool("Stay", true);
        menuOpen = true;
        canOpenMenu = true;
    }
    private IEnumerator CloseTowerSelectMenu()
    {
        towerMenuAnimator.SetBool("Stay", false);
        canOpenMenu = false;
        yield return new WaitForSeconds(0.42f);
        menuOpen = false;
        canOpenMenu = true;
        towerCollection.SetActive(false);
        towerMenuAnimator.ResetTrigger("Open");
    }
}
