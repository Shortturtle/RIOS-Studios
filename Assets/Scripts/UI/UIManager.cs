using System;
using System.Xml.Serialization;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private int healthCount;
    private int maxHealth;
    [Range(0f, 1f)]
    private float healthPercentage;
    private int energyCount;
    private int abilityPointCount;
    private int waveCount;
    private int maxWaveCount;

   
    public TextMeshProUGUI healthText;
    public Material healthBarMaterial;
    public TextMeshProUGUI energyText;
    public TextMeshProUGUI abilityPointText;
    public TextMeshProUGUI waveText;
    public TextMeshProUGUI loseScreenWaveText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        maxHealth = (int)ResourceManager.instance.maxHealth;
        maxWaveCount = EnemyWaveManager.instance.maxWaves;
    }

    // Update is called once per frame
    void Update()
    {
        HealthHandler();
        EnergyHandler();
        APHandler();
        WaveHandler();
        if (healthCount <= 0)
            {
                UpdateLoseScreen();
        }
    }

    private void HealthHandler()
    {
        healthCount = (int) ResourceManager.instance.currentBaseHealth;
        healthCount = Mathf.Clamp(healthCount, 0, maxHealth);
        healthPercentage = (float)Math.Round(((float)healthCount/(float)maxHealth), 2);
        healthBarMaterial.SetFloat("_Percentage", healthPercentage);

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

    private void WaveHandler()
    {
        waveCount = EnemyWaveManager.instance.currentWave;

        //update ui for wave count
        waveText.text = $"Wave: {waveCount}/{maxWaveCount}";
    }

    private void UpdateLoseScreen()
    {
        loseScreenWaveText.text = $"You made it to<color=#800EBF><size=120%><font=dum1 SDF Outline>{waveCount}/{maxWaveCount}</color></size></font>!";
    }
}
