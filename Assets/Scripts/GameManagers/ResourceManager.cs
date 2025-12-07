using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager instance;

    public float maxHealth;
    public int startingEnergy;

    [HideInInspector] public float currentBaseHealth;
    [HideInInspector] public int currentEnergy;
    [HideInInspector] public int currentAbilityPoint;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError($"Resource Manager instance already exists! Remove one of the instances!");
            Destroy( instance );
            instance = this;
        }

        else
        {
            instance = this;
        }

        currentBaseHealth = maxHealth;
        currentEnergy = startingEnergy;
        currentAbilityPoint = 0;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReduceHealth(float amount)
    {
        currentBaseHealth -= amount;

        if (currentBaseHealth <= 0)
        {
            GameManager.instance.LoseGame();
            return;
        }
    }

    public void AddHealth(float amount)
    {
        currentBaseHealth += amount;

        currentBaseHealth = Mathf.Clamp( currentBaseHealth, 0, maxHealth);
    }

    public void AddEnergy(int amount)
    {
        currentEnergy += amount;
    }

    public void RemoveEnergy(int amount)
    {
        currentEnergy -= amount;
    }

    public void AddAbilityPoint(int amount)
    {
        currentAbilityPoint += amount;
    }

    public void RemoveAbilityPoint(int amount)
    {
        currentAbilityPoint -= amount;
    }
}
