using UnityEngine;
using System.Collections;

public class FruitSpawner : MonoBehaviour
{
    private Collider spawnArea;

    public Transform parentGameObject;
    public GameObject[] fruitPrefabs;
    //public GameObject spawner;
    //public float locationX;

    public float minSpawnDelay = 0.25f;
    public float maxSpawnDelay = 1f;

    private void OnEnable()
    {
        StartCoroutine(Spawn());
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(1f);
        while (enabled)
        {
            //locationX = Random.Range(-400, 400);
            //spawner.transform.position = new Vector3(locationX,0,0);
            GameObject prefab = fruitPrefabs[Random.Range(0, fruitPrefabs.Length)];            

            GameObject fruit = Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            fruit.transform.SetParent(GameObject.FindGameObjectWithTag("MinigameFN").transform, false);

            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
        }
    }

}
