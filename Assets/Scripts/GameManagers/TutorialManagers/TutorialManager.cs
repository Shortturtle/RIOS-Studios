using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    // This is one of the only comments im not writing retroactively but I wanna KILL MYSELF FUCK THIS
    [Header("Tutorial GameObjects")]
    public List<GameObject> tutorialGameObjects = new List<GameObject>();

    [Header("WaveManager")]
    public TutorialWaveManager waveManager;

    [Header("TutorialFlags")]
    private TutorialState currentTutorialState = TutorialState.PlaceFirstTower;
    private bool placedFirstTower;
    private bool firstWaveClear;

    private enum TutorialState
    {
        PlaceFirstTower,
        FirstWave,
        RepairTower,
        SecondWave,
        Ability,
        End
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
        switch (currentTutorialState)
        {
            case TutorialState.PlaceFirstTower:
                PlaceFirstTowerCheck(); break;
        }
    }

    private void PlaceFirstTowerCheck()
    {
        BaseTowerClass placedTower = FindFirstObjectByType<BaseTowerClass>();
        if (placedTower != null && placedTower.enabled == true)
        {
            Debug.Log("Placed First Tower");
            placedFirstTower = true;
            currentTutorialState = TutorialState.FirstWave;
            waveManager.StartWave();
        }
    }

    private void FirstWaveClearCheck()
    {
        if (waveManager.currentWave != 1 && !waveManager.currentWavePlaying)
        {
            firstWaveClear = true;

        }
    }
}
