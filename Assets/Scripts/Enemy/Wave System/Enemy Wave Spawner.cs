using UnityEngine;

public class EnemyWaveSpawner : MonoBehaviour
{
    public GameObject enemy;

    private float spawnTimer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnTimer = Random.Range(2f, 4f);
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer -= Time.deltaTime;

        if (spawnTimer < 0)
        {
            Instantiate(enemy, transform.position, Quaternion.identity);
            spawnTimer = Random.Range(2f, 4f);
        }
    }
}
