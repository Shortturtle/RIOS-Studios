using UnityEngine;

[CreateAssetMenu(fileName = "OffenseTowerStats", menuName = "Stat Blocks/Offense Tower Stats")]
public class OffenseTowerStats : ScriptableObject 
{
    [Header("Cost")]
    public int Cost;
    [Space(10)]

    [Header("Damage Stats")]
    public float Damage;
    public float TimeBetweenAttacks;
    public float Range;
    [Space(10)]

    [Header("Projectile")]
    public GameObject Projectile;
    [Space(10)]

    [Header("Degrade/Overdrive")]
    public float DegradeTimerDuration;
    public float OverdriveTimerDuration;
    public float BufferTimerDuration;
    public int MaxDegradeRank;
    [Space(10)]

    [Header("Microgame")]
    public GameObject Microgame;

    private void OnValidate()
    {
        if(Microgame != null)
        {
            if(Microgame.GetComponent<BaseMicrogameClass>() == null)
            {
                Microgame = null;
            }
        }
    }
}
