using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private int healthCount;
    private int maxHealth;
    private int energyCount;
    private int abilityPointCount;

    //Coin count UI
    public TextMeshProUGUI healthText;
    public Slider healthSlider;
    public TextMeshProUGUI energyText;
    public TextMeshProUGUI abilityPointText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        healthSlider.maxValue = ResourceManager.instance.maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        HealthHandler();
        EnergyHandler();
        APHandler();
    }

    private void HealthHandler()
    {
        healthCount = (int) ResourceManager.instance.currentBaseHealth;
        healthSlider.value = healthCount;
        maxHealth = (int) ResourceManager.instance.maxHealth;
        healthCount = Mathf.Clamp(healthCount, 0, maxHealth);

        //update ui for health
        healthText.text = $"{healthCount}/{maxHealth}";
    }
    private void EnergyHandler()
    {
        energyCount = ResourceManager.instance.currentEnergy;
        //update ui for money
        energyText.text = energyCount.ToString();
    }

    private void APHandler()
    {
        abilityPointCount = ResourceManager.instance.currentAbilityPoint;
        //update ui for ability points
        abilityPointText.text = abilityPointCount.ToString();
    }
}
