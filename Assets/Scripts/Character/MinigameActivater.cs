using System.Collections.Generic;
using UnityEngine;

public class MinigameActivater : MonoBehaviour
{
    private List<BaseTowerClass> availableMinigames = new List<BaseTowerClass>();
    public bool isMinigamePlaying = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            if (availableMinigames != null && !isMinigamePlaying)
            {
                Debug.Log(availableMinigames[0]);
                availableMinigames[0].StartMicrogame();
                isMinigamePlaying = true;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.isTrigger == false)
        {
            BaseTowerClass tower = other.GetComponent<BaseTowerClass>();
            if ((tower != null) && (tower.degradeRank == tower.maxDegradeRank))
            {
                availableMinigames.Add(tower);
            }
        }
    }

    public void RemoveTowerFromList(BaseTowerClass tower)
    {
        availableMinigames.Remove(tower);
        isMinigamePlaying = false;
    }
}
