using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class LevelInteractionManager : MonoBehaviour
{
    public static LevelInteractionManager instance;

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
    public LayerMask enemyLayerMask;
    public GameObject enemyHealthBar;
    public GameObject minigameIndicator;
    private GameObject minigameIndicatorInstance;
    private EnemyHealthBar enemyHealthBarInstance;

    private BaseTowerClass prevHoveredTower;
    private BaseTowerClass currentHoveredTower;

    private BaseEnemyClass prevHoveredEnemy;
    private BaseEnemyClass currentHoveredEnemy;
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
        HealthBarPositioner();
        IndicatorPositioner();

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

                if(currentHoveredTower.degradeRank == currentHoveredTower.maxDegradeRank && minigameIndicatorInstance == null)
                {
                    minigameIndicatorInstance = Instantiate(minigameIndicator, GameObject.FindGameObjectWithTag("HoverCanvas").transform);
                    minigameIndicatorInstance.transform.position = Camera.main.WorldToScreenPoint(currentHoveredTower.gameObject.transform.position);
                }
            }
        }

        else
        {
            currentHoveredTower = null;

            if (minigameIndicatorInstance != null)
            {
                Destroy(minigameIndicatorInstance.gameObject);
                minigameIndicatorInstance = null;
            }
        }

        if(Physics.Raycast(ray, out RaycastHit hitInfo2, 100, enemyLayerMask, QueryTriggerInteraction.Collide))
        {
            if(hitInfo2.collider.gameObject.GetComponent<BaseEnemyClass>() != null)
            {
                currentHoveredEnemy = hitInfo2.collider.gameObject.GetComponent<BaseEnemyClass>();
                if(enemyHealthBarInstance == null)
                {
                    enemyHealthBarInstance = Instantiate(enemyHealthBar, GameObject.FindGameObjectWithTag("HoverCanvas").transform).GetComponent<EnemyHealthBar>();
                    enemyHealthBarInstance.gameObject.transform.position = Camera.main.WorldToScreenPoint(currentHoveredEnemy.healthBarPosition.transform.position);
                    enemyHealthBarInstance.SetTarget(currentHoveredEnemy);
                }
            }
        }

        else
        {
            currentHoveredEnemy = null;
            if  (enemyHealthBarInstance != null)
            {
                Destroy(enemyHealthBarInstance.gameObject);
                enemyHealthBarInstance = null;
            }
        }

        if (currentHoveredTower != prevHoveredTower)
        {
            if (prevHoveredTower != null)
            {
                prevHoveredTower.isHovered = false;

                if(currentHoveredTower == null) { return;  }
                if(currentHoveredTower.degradeRank == currentHoveredTower.maxDegradeRank && minigameIndicatorInstance != null)
                {
                    minigameIndicatorInstance.transform.position = Camera.main.WorldToScreenPoint(currentHoveredTower.transform.position);
                }

                else if (currentHoveredTower.degradeRank == currentHoveredTower.maxDegradeRank && minigameIndicatorInstance == null)
                {
                    minigameIndicatorInstance = Instantiate(minigameIndicator, GameObject.FindGameObjectWithTag("HoverCanvas").transform);
                    minigameIndicatorInstance.transform.position = Camera.main.WorldToScreenPoint(currentHoveredTower.gameObject.transform.position);
                }
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

    void HealthBarPositioner()
    {
        if (enemyHealthBarInstance != null && currentHoveredEnemy != null)
        {
            enemyHealthBarInstance.gameObject.transform.position = Camera.main.WorldToScreenPoint(currentHoveredEnemy.healthBarPosition.transform.position);
        }
    }

    void IndicatorPositioner()
    {
        if (minigameIndicatorInstance != null && currentHoveredTower != null)
        {
            minigameIndicatorInstance.gameObject.transform.position = Camera.main.WorldToScreenPoint(currentHoveredEnemy.healthBarPosition.transform.position);
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

    //public void EndMicrogame()
    //{
    //    EndMicrogame();
    //}

    public void EndMicrogame()
    {
        //test
        if (Input.GetKeyDown(KeyCode.I))
        {
            MicrogameManager.instance.MicrogameQuit();
        }
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
