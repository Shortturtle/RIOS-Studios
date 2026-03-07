using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError($"Building Manager instance already exists! Remove one of the instances!");
            Destroy(instance);
            instance = this;
        }

        else
        {
            instance = this;
        }
    }
    private bool isMicrogamePlaying = false;

    public GameObject towerToPlace;
    public BaseTowerClass towerClass;
    public GameObject ghostTowerIndicator;
    public GameObject noEnergyText;
    public GameObject invalidPlaceLocationText;
    private List<Renderer> renderers = new List<Renderer>();
    public bool isPlacing;
    private bool canPlace;
    private EventSystem eventSystem;
    private bool overUI;

    public Material placeableMaterial;
    public Material cannotPlaceMaterial;

    public float topSafePercent = 12f;
    public LayerMask acceptedLayers;
    private Vector3 currentPlacement;

    public InputActionAsset inputMap;
    public AK.Wwise.Event towerBuilding;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        eventSystem = FindFirstObjectByType<EventSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        SetMinigameBool();
        InputTracker();

        if (isPlacing && towerToPlace != null)
        {
            Raycast();
        }
        
        overUI = eventSystem.IsPointerOverGameObject()? true: false;
    }

    private void SetMinigameBool()
    {
        isMicrogamePlaying = MicrogameManager.instance.currentlyPlayingMinigame;
    }
    void Raycast()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.yellow);

        if (Input.mousePosition.y > Screen.height * (1f - (topSafePercent / 100f)))
        {
            ghostTowerIndicator.SetActive(false);
        }

        else
        {
            ghostTowerIndicator.SetActive(true);
        }

        if ((Physics.Raycast(ray, out RaycastHit hitInfo, 100, acceptedLayers, QueryTriggerInteraction.Ignore)))
        {

            if (hitInfo.collider.gameObject.layer == LayerMask.NameToLayer("Path") 
                || hitInfo.collider.gameObject.layer == LayerMask.NameToLayer("Tower") 
                || towerClass.cost > ResourceManager.instance.currentEnergy)
            {
                currentPlacement = hitInfo.point;
                ghostTowerIndicator.transform.position = currentPlacement;
                canPlace = false;
                SetGhostObjectMaterial(cannotPlaceMaterial);
            }

            else if (hitInfo.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                currentPlacement = hitInfo.point;
                ghostTowerIndicator.transform.position = currentPlacement;
                canPlace = true;
                SetGhostObjectMaterial(placeableMaterial);
            }
        }

    }

    public void PlaceTower(InputAction.CallbackContext ctx)
    {
        if (overUI) { return; }

       if (isPlacing)
        {
            if (!canPlace)
            {
                if (towerClass.cost > ResourceManager.instance.currentEnergy)
                {
                    Debug.Log("Not Enough Money");
                    Instantiate(noEnergyText, GameObject.FindGameObjectWithTag("HoverCanvas").transform);
                }

                else
                {
                    Debug.Log("Invalid Position to place");
                    Instantiate(invalidPlaceLocationText, GameObject.FindGameObjectWithTag("HoverCanvas").transform);
                }
            }

            else
            {
                towerBuilding.Post(gameObject);
                ResourceManager.instance.RemoveEnergy(towerClass.cost);
                Instantiate(towerToPlace, currentPlacement, Quaternion.identity);
                StopPlacement();
            }
        }
    }

    public void MenuClose(InputAction.CallbackContext ctx)
    {
        if (isPlacing)
        {
            StopPlacement();
        }
    }
        void InputTracker()
    {
        if (isPlacing && !isMicrogamePlaying)
        {
            inputMap.FindActionMap("BuildingSystem").Enable();
            inputMap.FindActionMap("DISABLE").Disable();
        }

        else
        {
            inputMap.FindActionMap("DISABLE").Enable();
            inputMap.FindActionMap("BuildingSystem").Disable();
        }
    }

    public void TowerPlacement(GameObject tower)
    {
        if (!isMicrogamePlaying)
        {
            if (tower.GetComponent<BaseTowerClass>() != null)
            {
                if (towerToPlace != tower || towerToPlace == null)
                {
                    InitializeTowerIndicator(tower);
                }

                else if (towerToPlace == tower)
                {
                    StopPlacement();
                }
            }
        }
    }

    void InitializeTowerIndicator(GameObject tower)
    {
        if (ghostTowerIndicator != null)
        {
            Destroy(ghostTowerIndicator);
            towerToPlace = null;
            ghostTowerIndicator = null;
            towerClass = null;
            renderers.Clear();
        }

        isPlacing = true;
        towerToPlace = tower;
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
            foreach(var r in ghostTowerIndicator.GetComponentsInChildren<Renderer>())
            {
                if(!(r.gameObject.layer == LayerMask.GetMask("IgnoreRaycasts"))) { continue; }
                renderers.Add(r);
            }
        }

        foreach (var renderer in renderers)
        {
            Material[] materials = renderer.materials;
            for (int i = 0; i < materials.Length; i++)
            {
                materials[i] = placeableMaterial;
            }
            renderer.materials = materials; 
        }
    }

    void SetGhostObjectMaterial(Material mat)
    {
        foreach(var renderer in renderers)
        {
            Material[] materials = renderer.materials;
             for (int i = 0; i < materials.Length; i++)
            {
                materials[i] = mat;
            }
            renderer.materials = materials;
        }
    }

    void StopPlacement()
    {
        isPlacing = false;
        towerToPlace = null;
        if(ghostTowerIndicator != null)
        {
            Destroy(ghostTowerIndicator);
        }
        ghostTowerIndicator = null;
        towerClass = null;
        renderers.Clear();
    }
}
