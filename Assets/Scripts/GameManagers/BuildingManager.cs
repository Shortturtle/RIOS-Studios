using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager instance;

    public GameObject towerToPlace;
    public BaseTowerClass towerClass;
    public GameObject ghostTowerIndicator;
    private List<Renderer> renderers;
    public bool isPlacing;
    private bool canPlace;

    public Material placeableMaterial;
    public Material cannotPlaceMaterial;

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
        if (isPlacing && towerToPlace != null)
        {
            Raycast();
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
            
        }
    }

    void PlaceObject()
    {
        
    }

    public void TowerPlacementButton(GameObject tower)
    {
       if (tower.GetComponent<BaseTowerClass>() != null)
        {
            towerToPlace = tower;
            InitializeTowerIndicator();
            isPlacing = true;
            return;
        }
    }

    void InitializeTowerIndicator()
    {
        if (ghostTowerIndicator != null)
        {
            Destroy(ghostTowerIndicator);
            ghostTowerIndicator = null;
            towerClass = null;
            renderers.Clear();
        }

        ghostTowerIndicator = Instantiate(towerToPlace);
        towerClass = ghostTowerIndicator.GetComponent<BaseTowerClass>();
        towerClass.InitializeTower();
        towerClass.enabled = false;
        
        if(ghostTowerIndicator.GetComponent<Collider>() != null)
        {
            ghostTowerIndicator.GetComponent<Collider>().enabled = false;
        }

        if(ghostTowerIndicator.GetComponentsInChildren<Collider>() != null)
        {
            foreach (var collider in ghostTowerIndicator.GetComponentsInChildren<Collider>())
            {
                collider.enabled = false;
            }
        }

        if(ghostTowerIndicator.GetComponent<Renderer>() != null)
        {
            renderers.Add(ghostTowerIndicator.GetComponent<Renderer>());
        }

        if(ghostTowerIndicator.GetComponentsInChildren<Renderer>() != null)
        {
            foreach(var renderer in ghostTowerIndicator.GetComponentsInChildren<Renderer>())
            {
                renderers.Add(renderer);
            }
        }

        foreach(var renderer in renderers)
        {
            Material material = renderer.material;
            material = placeableMaterial;
        }
    }
}
