using Concept2;
using TMPro;
using UnityEngine;

public class OffenseTowerHoverUI : MonoBehaviour
{
    public TextMeshProUGUI damageValue;
    public TextMeshProUGUI timeBetweenAttackValue;
    public TextMeshProUGUI rangeValue;
    public TextMeshProUGUI targettingMode;

    public void SetValues(OffenseTowerBase tower)
    {
        damageValue.text = $"Damage: {tower.damageValue}";
        rangeValue.text = $"Range: {tower.rangeValue}";
        timeBetweenAttackValue.text = $"TBA: {tower.timeBetweenAttackValue}";
        targettingMode.text = $"Targetting: {tower.targettingMode.ToString()}";
    }
}
