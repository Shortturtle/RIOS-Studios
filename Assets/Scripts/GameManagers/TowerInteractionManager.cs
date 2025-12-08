using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class TowerInteractionManager : MonoBehaviour
{
    public static TowerInteractionManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError($"Tower Interaction Manager instance already exists! Remove one of the instances!");
            Destroy(instance);
            instance = this;
        }

        else
        {
            instance = this;
        }
    }

    public InputActionAsset inputMap;
    private bool isBuilding = false;
    private bool isPlayingMicrogame = false;
    public LayerMask towerLayerMask;

    private BaseTowerClass prevHoveredTower;
    private BaseTowerClass currentHoveredTower;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        SetBuildingBool();
        SetMinigameBool();
        InputTracker();

        if (!isBuilding && !isPlayingMicrogame)
        {
            Raycast();
        }

        else
        {
            currentHoveredTower = null;
        }
    }

    private void SetBuildingBool()
    {
        isBuilding = BuildingManager.instance.isPlacing;
    }

    private void SetMinigameBool()
    {
        isPlayingMicrogame = MicrogameManager.instance.currentlyPlayingMinigame;
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
                currentHoveredTower = hitInfo.collider.gameObject.GetComponent<BaseTowerClass>();
                currentHoveredTower.isHovered = true;
            }
        }

        else
        {
            currentHoveredTower = null;
        }

        if (currentHoveredTower != prevHoveredTower)
        {
            if (prevHoveredTower != null)
            {
                prevHoveredTower.isHovered = false;
            }

            prevHoveredTower = currentHoveredTower;
        }

    }

    void InputTracker()
    {
        if (isBuilding || isPlayingMicrogame)
        {
            inputMap.FindActionMap("DISABLE").Enable();
            inputMap.FindActionMap("TowerInteractionSystem").Disable();
        }

        else
        {
            inputMap.FindActionMap("TowerInteractionSystem").Enable();
            inputMap.FindActionMap("DISABLE").Disable();
        }
    }

    public void TowerStatMenuPopUp(InputAction.CallbackContext ctx)
    {
        if (currentHoveredTower != null)
        {
            Debug.Log("Open Stats Menu");
            return;
        }
    }

    public void ActivateMicrogame(InputAction.CallbackContext ctx)
    {
        ActivateMicrogame();
    }

    public void ActivateMicrogame()
    {
        if (currentHoveredTower != null && (currentHoveredTower.degradeRank == currentHoveredTower.maxDegradeRank) && !isPlayingMicrogame)
        {
            Debug.Log("StartMinigame");
            isPlayingMicrogame = true;
            MicrogameManager.instance.MicrogameStart(currentHoveredTower, currentHoveredTower.microgame);
        }
    }
}
