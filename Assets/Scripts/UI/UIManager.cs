using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    //UI numbers
    //public int minHealth;
    public int healthCount;
    //private int minMoney = 0;
    public int moneyCount;

    //Coin count UI
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI energyText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HealthHandler();
        EnergyHandler();
    }

    private void HealthHandler()
    {
        if(healthCount < 0)
        {
            healthCount = 0;
        }
        //update ui for health
        healthText.text = "Health: " + healthCount;
    }
    private void EnergyHandler()
    {
        //update ui for money
        energyText.text = "Energy: " + moneyCount;
    }
}
