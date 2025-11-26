using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    static ResourceManager instance;

    public float maxHealth;
    public float startingEnergy;

    protected float currentBaseHealth;
    protected float currentEnergy;
    protected int currentAbilityPoint;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError($"Resource Manager instance already exists! Remove one of the instances!");
            Destroy( instance );
            instance = new ResourceManager();
        }

        else
        {
            instance = new ResourceManager();
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
            return;
        }
    }

    public void AddHealth(float amount)
    {
        currentBaseHealth += amount;

        currentBaseHealth = Mathf.Clamp( currentBaseHealth, 0, maxHealth);
    }

    public void AddEnergy(float amount)
    {
        currentEnergy += amount;
    }

    public void RemoveEnergy(float amount)
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
