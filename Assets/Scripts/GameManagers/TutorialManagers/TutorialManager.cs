using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    // This is one of the only comments im not writing retroactively but I wanna KILL MYSELF FUCK THIS  -Danish
    [Header("Tutorial GameObjects")]
    public List<GameObject> tutorialGameObjects = new List<GameObject>();

    [Header("WaveManager")]
    public TutorialWaveManager waveManager;

    [Header("TutorialFlags")]
    public TowerSelectMenu towerSelectMenu;
    private TutorialState currentTutorialState = TutorialState.OpenTowerMenu;
    private bool openedTowerMenu;
    private bool placedFirstTower;
    private bool towerDegraded;
    private bool microgameOpen;

    private int frameCount;

    private enum TutorialState
    {
        OpenTowerMenu,
        PlaceFirstTower,
        TowerDegraded,
        MicrogameExposition,
        MicrogamePendingCompletion,
        Ability,
        End,
        Win
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (GameObject gameObject in tutorialGameObjects) { gameObject.SetActive(false); }

        waveManager.TutorialStart();
        StartCoroutine(OpenTutorialImage(0, 1));
    }

    // Update is called once per frame
    void Update()
    {
        TutorialProgressCheck();

        if (Input.GetMouseButtonDown(0))
        {
            CloseTutorialImage();
        }
    }

    private void CloseTutorialImage()
    {
        if (Time.timeScale == 0)
        {
            foreach (GameObject gameObject in tutorialGameObjects)
            {
                if (gameObject.activeInHierarchy == true)
                {
                    gameObject.SetActive(false);
                    Time.timeScale = 1f;

                    if(currentTutorialState == TutorialState.End)
                    {
                        currentTutorialState = TutorialState.Win;
                    }
                    return;
                }
            }
        }
    }

    private IEnumerator OpenTutorialImage(int numToOpen, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        tutorialGameObjects[numToOpen].SetActive(true);
        Time.timeScale = 0;
    }

    private void TutorialProgressCheck()
    {
        frameCount++;

        if (!(frameCount % 3 == 0)) { return; }

        switch (currentTutorialState)
        {
            case TutorialState.OpenTowerMenu:
                OpenTowerMenuCheck(); break;
            case TutorialState.PlaceFirstTower:
                PlaceFirstTowerCheck(); break;
            case TutorialState.TowerDegraded:
                TowerDegradeCheck(); break;
            case TutorialState.MicrogameExposition:
                MicrogameOpenCheck(); break;
            case TutorialState.MicrogamePendingCompletion:
                MicrogameCompletionCheck(); break;
            case TutorialState.Ability:
                AbilityUseCheck(); break;
            case TutorialState.End:
                GameWinCheck(); break;
            case TutorialState.Win:
                WinScreenCheck();  break;
        }

        frameCount = 0;
    }

    private void OpenTowerMenuCheck()
    {
        if (towerSelectMenu.menuOpen)
        {
            Debug.Log("MenuOpen");
            StartCoroutine(OpenTutorialImage(1, 0.1f));
            currentTutorialState = TutorialState.PlaceFirstTower;
            openedTowerMenu = true;
        }
    }

    private void PlaceFirstTowerCheck()
    {
        BaseTowerClass placedTower = FindFirstObjectByType<BaseTowerClass>();
        if (placedTower != null && placedTower.enabled == true)
        {
            Debug.Log("Placed First Tower");
            placedFirstTower = true;
            currentTutorialState = TutorialState.TowerDegraded;
            waveManager.StartWave();
        }
    }

    private void TowerDegradeCheck()
    {
        BaseTowerClass[] allPlacedTowers = FindObjectsByType<BaseTowerClass>(FindObjectsSortMode.None);
        foreach (BaseTowerClass tower in allPlacedTowers)
        {
            if (tower.isMaxDegraded)
            {
                Debug.Log("TowerDegraded");
                StartCoroutine(OpenTutorialImage(2, 0.2f));
                currentTutorialState = TutorialState.MicrogameExposition;
                towerDegraded = true;
            }
        }
    }

    private void MicrogameOpenCheck()
    {
        if (MicrogameManager.instance.currentlyPlayingMinigame)
        {
            Debug.Log("MinigameOpen");
            StartCoroutine(OpenTutorialImage(3, 0.2f));
            currentTutorialState = TutorialState.MicrogamePendingCompletion;

        }
    }

    private void MicrogameCompletionCheck()
    {
        if (MicrogameManager.instance.repaired)
        {
            Debug.Log("MinigameComplete");
            StartCoroutine(OpenTutorialImage(4, 0.4f));
            currentTutorialState = TutorialState.Ability;
        }
    }

    private void AbilityUseCheck()
    {
        if(ResourceManager.instance.currentAbilityPoint == 3)
        {
            Debug.Log("EnoughToCastAbility");
            StartCoroutine(OpenTutorialImage(5, 0.2f));
            currentTutorialState = TutorialState.End;
        }
    }

    private void GameWinCheck()
    {
        if (waveManager.winYes)
        {
            Debug.Log("Win");
            StartCoroutine(OpenTutorialImage(6, 0.2f));
        }
    }

    protected void WinScreenCheck()
    {
        if (GameManager.instance.gameEnd)
        {
            GameManager.instance.WinGame();
        }
    }
}
