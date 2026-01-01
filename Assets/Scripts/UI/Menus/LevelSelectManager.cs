using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelSelectManager : MonoBehaviour
{
    public SwitchScene switchScenes;

    //era is medieval/steampunk/scifi, era level is lobbies and game levels 
    public GameObject eraSelect;
    public GameObject eraLevelSelect;

    private bool canOpenMenu;
    private bool menuOpen;
    private bool eraSelOpen = false;
    private bool eraLvlSelOpen = false;

    //attach to whatever thing to open the menu
    public void LevelSelectMenuPopup()
    {
        if (menuOpen == false)
        {
            if (canOpenMenu == true)
            {
                OpenLevelSelectMenu();
            }
        }
        else
        {
            if (canOpenMenu == true)
            {
                CloseLevelSelectMenu();
            }
        }
    }

    //attach to button that switches the btwn eras and lvls
    public void SwitchSelectionMenu()
    {
        if(canOpenMenu == true)
        {
            if (eraSelOpen)
            {
                StartCoroutine(CloseEraSelect());
                StartCoroutine(OpenEraLevelSelect());
            }
            else if (eraLvlSelOpen)
            {
                StartCoroutine(CloseEraLevelSelect());
                StartCoroutine(OpenEraSelect());
            }
        }
        
    }

    private void OpenLevelSelectMenu()
    {
        StartCoroutine(OpenEraSelect());
    }
    private void CloseLevelSelectMenu()
    {
        if (eraSelOpen)
        {
            StartCoroutine(CloseEraSelect());
        }
        else if (eraLvlSelOpen)
        {
            StartCoroutine(CloseEraLevelSelect());
        }
    }

    //Opening and closing minigame
    private IEnumerator OpenEraSelect()
    {
        eraSelect.transform.LeanMoveLocal(new Vector2(0, 0), 0.5f).setEaseOutCirc();
        menuOpen = true;
        canOpenMenu = false;
        eraSelOpen = true;
        yield return new WaitForSeconds(0.6f);
        canOpenMenu = true;
    }
    //Opening and closing minigame
    private IEnumerator OpenEraLevelSelect()
    {
        eraLevelSelect.transform.LeanMoveLocal(new Vector2(0, 0), 0.5f).setEaseOutCirc();
        menuOpen = true;
        canOpenMenu = false;
        eraLvlSelOpen = true;
        yield return new WaitForSeconds(0.6f);
        canOpenMenu = true;
    }

    
    private IEnumerator CloseEraSelect()
    {
        eraSelect.transform.LeanMoveLocal(new Vector2(0, -1075), 0.5f).setEaseOutCirc();
        menuOpen = false;
        canOpenMenu = false;
        eraSelOpen = false;
        yield return new WaitForSeconds(0.6f);
        canOpenMenu = true;

    }
    private IEnumerator CloseEraLevelSelect()
    {
        eraLevelSelect.transform.LeanMoveLocal(new Vector2(0, -1075), 0.5f).setEaseOutCirc();
        menuOpen = false;
        canOpenMenu = false;
        eraLvlSelOpen = false;
        yield return new WaitForSeconds(0.6f);
        canOpenMenu = true;

    }

    public void ToLevelOne()
    {
        ////change this once level is out
        //switchScenes.FadeOutAndLoad("MedievalLevelOne");
    }
}
