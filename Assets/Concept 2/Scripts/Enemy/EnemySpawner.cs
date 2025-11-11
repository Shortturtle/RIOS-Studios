using UnityEngine;

namespace Concept2
{
    public class EnemySpawner : MonoBehaviour
    {
        public GameObject enemy;
        private float spawnTimer;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            spawnTimer = Random.Range(2f, 3.5f);
        }

        // Update is called once per frame
        void Update()
        {
            spawnTimer -= Time.deltaTime;

            if (spawnTimer < 0)
            {
                Instantiate(enemy, transform.position, Quaternion.identity);
                spawnTimer = Random.Range(2f, 3.5f);
            }
        }
    }
}
