using UnityEngine;

public class TowerButtonScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnTower(GameObject tower)
    {
        if(tower.GetComponent<BaseTowerClass>() != null)
        {
            BuildingManager.instance.TowerPlacement(tower);
        }
    }
}
