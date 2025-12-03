using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager instance;

    public GameObject towerToPlace;
    public BaseTowerClass towerClass;
    public GameObject ghostTowerIndicator;
    public bool isPlacing;

    public float topSafePercent = 12f;
    public LayerMask placementLayerMask, obstacleLayerMask;
    private Vector3 currentPlacement;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlacing)
        {

        }
    }
    void Raycast()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        Ray ray = Camera.main.ScreenPointToRay(mousePos);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, 100))
        {
            currentPlacement = hitInfo.point;
        }

        else
        {
            currentPlacement = new Vector3(999,999,999);
        }
    }

    void PlaceObject()
    {
        
    }

    public void StartTowerPlacement(GameObject tower)
    {
       if (tower.GetComponent<BaseTowerClass>() != null)
        {
            towerToPlace = tower;
            ghostTowerIndicator = Instantiate(towerToPlace);
            towerClass = ghostTowerIndicator.GetComponent<BaseTowerClass>();
            towerClass.InitializeTower();
            towerClass.enabled = false;
        }
    }

}
