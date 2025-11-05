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
    public TextMeshProUGUI moneyText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HealthHandler();
        MoneyHandler();
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
    private void MoneyHandler()
    {
        //update ui for money
        moneyText.text = "Money: " + moneyCount;
    }
}
