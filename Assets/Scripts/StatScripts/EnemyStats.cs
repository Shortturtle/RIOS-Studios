using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStats", menuName = "Stat Blocks/Enemy Stats")]
public class EnemyStats : ScriptableObject
{
    public float maxHealth;

    public float speed;

    public bool isCamo;

}
