using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AttackSpeedTracker : MonoBehaviour
{
    [SerializeField] private OffenseTowerBase tower;
    private TextMeshPro text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text.text = $"Time Between Attacks: {tower.timeBetweenAttacks.ToString()}s";
    }

    private void Awake()
    {
        text = GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = $"Time Between Attacks: " + tower.timeBetweenAttacks.ToString();
    }
}
