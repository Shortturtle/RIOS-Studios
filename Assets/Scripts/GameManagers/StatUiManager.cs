using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class StatUiManager : MonoBehaviour
{
    public static StatUiManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError($"Stat Ui Manager instance already exists! Remove one of the instances!");
            Destroy(instance);
            instance = this;
        }

        else
        {
            instance = this;
        }
    }

    private bool isBuilding = false;
    public LayerMask towerLayerMask;

    private BaseTowerClass prevHoveredTower;
    private BaseTowerClass hoveredTower;
    private bool hoverUIActive = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        SetBuildingBool();

        if (!isBuilding)
        {
            Raycast();
        }
    }

    private void SetBuildingBool()
    {
        isBuilding = BuildingManager.instance.isPlacing;
    }

    void Raycast()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.yellow);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, 100, towerLayerMask, QueryTriggerInteraction.Ignore))
        {
            if (hitInfo.collider.gameObject.GetComponent<BaseTowerClass>() != null)
            {
                hoveredTower = hitInfo.collider.gameObject.GetComponent<BaseTowerClass>();

                if(hoveredTower != null && !hoverUIActive)
                {
                    ActivateHoverUI();
                }
            }
        }

        else if(hoveredTower != null && hoverUIActive)
        {
            DeactivateHoverUI();
            hoveredTower = null;
        }
    }

    void ActivateHoverUI()
    {
        hoveredTower.InitializeHoverUI();
        hoverUIActive = true;
    }

    void DeactivateHoverUI()
    {
        hoveredTower.DeleteHoverUI();
        hoverUIActive = false;
    }
}
