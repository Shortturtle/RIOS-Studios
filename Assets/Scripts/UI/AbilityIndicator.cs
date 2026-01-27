using UnityEngine;
using UnityEngine.UI;

public class AbilityIndicator : MonoBehaviour
{
    public Color ableToCast;
    public Color unableToCast;
    public int costRequirement;
    public bool isAbleToCast;
    public AK.Wwise.Event ableToCastSFX;

    private Image abilityImage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        abilityImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        AbilityCheck();
    }

    private void AbilityCheck()
    {
        if (!isAbleToCast)
        {
            if (ResourceManager.instance.currentAbilityPoint >= costRequirement)
            {
                abilityImage.color = ableToCast;
                isAbleToCast = true;
                AudioManager.instance.PlayAudioEvent(ableToCastSFX, gameObject);
            }

            else
            {
                abilityImage.color = unableToCast;
            }
        }

        else
        {
            if (ResourceManager.instance.currentAbilityPoint < costRequirement)
            {
                abilityImage.color = unableToCast;
                isAbleToCast = false;
            }
        }
    }
}
