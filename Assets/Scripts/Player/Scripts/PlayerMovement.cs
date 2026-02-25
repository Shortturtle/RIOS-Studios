using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;
using static UnityEngine.EventSystems.EventTrigger;
using static UnityEngine.Rendering.DebugUI;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    //Dialogue stuff
    [SerializeField] public DialogueUI dialogueUI;
    public DialogueUI DialogueUI => dialogueUI;
    public IInteractable Interactable { get; set; }

    [SerializeField] private float maxSpeed = 10f;
    [SerializeField] private float rotationSpeed = 360f;

    [SerializeField] private float accelerationFactor = 5f;
    [SerializeField] private float decelerationFactor = 10f;

    [SerializeField] private float gravity = -9.8f;
    private Vector3 velocity;

    public LayerMask enemyLayer;

    private float currentSpeed;   //speed of player
    public bool hasControl = true;
    private bool towerSelectOpen;

    private Vector3 playerInput;   //player input vector number
    private bool pauseInput;

    //refs
    private CharacterController characterController;
    private QuestLogUI questUI;
    public GameObject pauseScreen;
    public Animator animator;

    [Header("Ability Settings")]
    public int stunAbilityCost;
    public int transmutateAbilityCost;
    public int rewindAbilityCost;
    public int hologramAbilityCost;
    public float effectRadius = 5f;
    public float stunDuration = 5f;

    public List<GameObject> fantasyEnemies;
    public int maxTransmutedEnemies = 3;

    [Header("VFX")]
    public VisualEffect vfxRenderer;
    public GameObject FreezeVFX;
    public GameObject RewindVFX;
    public GameObject TransmutateVFX;

    private void Awake()
    {

        //refs
        characterController = GetComponent<CharacterController>();
        questUI = FindFirstObjectByType<QuestLogUI>();
    }

    //For input system, enable & disable
    private void OnEnable()
    {
    }
    private void OnDisable()
    {
    }

    private void Update()
    {
        bool isGrounded = characterController.isGrounded;

        if(vfxRenderer != null ) { vfxRenderer.SetVector3("ColliderPos", transform.position); }
        

        if (isGrounded && velocity.y < 0) { velocity.y = -2; }
        if (!isGrounded) { velocity.y = gravity * Time.deltaTime; }

        if (hasControl == true)
        {

            Look();
            CalculateSpeed();
            AnimatorUpdate();
            Move();
        }

        //manual interaction to activate dialogue
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (!dialogueUI.IsOpen)
            {
                Interactable.Interact(this);
            }
        }

        //manual interaction to open quest menu
        if (Input.GetKeyDown(KeyCode.Q))
        {
            questUI.QuestLogTogglePressed();
        }
    }

    public void Freeze(InputAction.CallbackContext ctx)
    {
        if (ResourceManager.instance != null && ResourceManager.instance.currentAbilityPoint >= stunAbilityCost)
        {
            StartCoroutine(FreezeCo());
        }

        else
        {
            return;
        }
    }

    private IEnumerator FreezeCo()
    {
        GameObject freezeVFX = Instantiate(FreezeVFX, transform.position - new Vector3(0, 1f), FreezeVFX.transform.rotation);
        ResourceManager.instance.RemoveAbilityPoint(stunAbilityCost);
        //find colliders within the AoE that are on the enemy layer
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, effectRadius, enemyLayer);

        foreach (Collider hit in hitColliders)
        {
            BaseEnemyClass enemy = hit.GetComponent<BaseEnemyClass>();
            if (enemy != null)
            {
                enemy.freeze(stunDuration);
            }
        }

        yield return new WaitForSeconds(stunDuration + 1.5f);

        Destroy(freezeVFX);

    }

    public void RewindEnemies(InputAction.CallbackContext ctx)
    {
        if (ResourceManager.instance != null && ResourceManager.instance.currentAbilityPoint >= rewindAbilityCost)
        {
            Instantiate(RewindVFX, transform.position, Quaternion.identity);
            ResourceManager.instance.RemoveAbilityPoint(rewindAbilityCost);
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, effectRadius * 10, enemyLayer);  //*CHANGE ONCE GAME MANAGER SET UP*

            foreach (Collider hit in hitColliders)
            {
                BaseEnemyClass enemy = hit.GetComponent<BaseEnemyClass>();
                if (enemy != null)
                {

                    enemy.StartRewind();
                }
            }
        }

        else
        {
            return;
        }
    }

    public void TransmutateEnemies(InputAction.CallbackContext ctx)
    {
        if (ResourceManager.instance != null && ResourceManager.instance.currentAbilityPoint >= transmutateAbilityCost)
        {
            Instantiate(TransmutateVFX, transform.position, Quaternion.identity);
            ResourceManager.instance.RemoveAbilityPoint(transmutateAbilityCost);
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, effectRadius, enemyLayer);  //*CHANGE ONCE GAME MANAGER SET UP*

            List<BaseEnemyClass> enemiesDetected = new List<BaseEnemyClass>();


            //Add all detected enemies to a list
            foreach (Collider hit in hitColliders)
            {
                BaseEnemyClass enemy = hit.GetComponent<BaseEnemyClass>();
                if (enemy != null)
                {
                    enemiesDetected.Add(enemy);
                }
            }

            //Transmutate enemies from the list up to a limit of 3(set at the start)
            for (int maxToTransmute = maxTransmutedEnemies; maxToTransmute >= 0; maxToTransmute--)
            {
                BaseEnemyClass enemy = enemiesDetected[Random.Range(0, enemiesDetected.Count - 1)];  //Takes a random enemy from the list

                GameObject tempEnemy = Instantiate(fantasyEnemies[Random.Range(0, 2)], enemy.transform.position, Quaternion.identity);  //Spawns in a random fantasy enemy
                BaseEnemyClass tempClass = tempEnemy.GetComponent<BaseEnemyClass>();
                tempClass.InitializeEnemy_OnTrack(enemy.waypointManager, enemy.waypointIndex, enemy.distanceTravelled);  //Moves it to the same position as the original enemy

                //Kill the original enemy and remove from list
                enemiesDetected.Remove(enemy);
                Destroy(enemy.gameObject);
            }
        }
    }

    public void Hologram(InputAction.CallbackContext ctx)
    {
        if (ResourceManager.instance != null && ResourceManager.instance.currentAbilityPoint >= hologramAbilityCost)
        {
            ResourceManager.instance.RemoveAbilityPoint(hologramAbilityCost);
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, effectRadius, enemyLayer);  //*CHANGE ONCE GAME MANAGER SET UP*
            
            foreach (Collider hit in hitColliders)
            {
                BaseEnemyClass enemy = hit.GetComponent<BaseEnemyClass>();
                if (enemy != null)
                {

                    enemy.SpawnHologram();
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, effectRadius);
    }

    //player movement
    private void Move()
    {
        Vector3 moveDirection = transform.forward * currentSpeed * Time.deltaTime + velocity;
        characterController.Move(moveDirection);

    }

    //player rotation
    private void Look()
    {
        if (playerInput == Vector3.zero) return;

        Matrix4x4 isometricMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
        Vector3 multipliedMatrix = isometricMatrix.MultiplyPoint3x4(playerInput);

        Quaternion rotation = Quaternion.LookRotation(multipliedMatrix, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotationSpeed);
    }

    //for movement smoothing
    private void CalculateSpeed()
    {
        if(playerInput == Vector3.zero && currentSpeed > 0)
        {
            currentSpeed -= decelerationFactor * Time.deltaTime;
        }
        else if (playerInput != Vector3.zero && currentSpeed < maxSpeed)
        {
            currentSpeed += accelerationFactor * Time.deltaTime;
        }

        currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);
    }

    //gather player input
    private void GatherInput(Vector2 input)
    {
        playerInput = new Vector3(input.x, 0, input.y);
    }

    public void GatherInput(InputAction.CallbackContext ctx) { GatherInput(ctx.ReadValue<Vector2>()); }

    public void PauseMenu(InputAction.CallbackContext ctx)
    {
        if (MicrogameManager.instance != null && MicrogameManager.instance.currentlyPlayingMinigame == true)
        {
            MicrogameManager.instance.MicrogameQuit();
            return;
        }

        if (Time.timeScale == 0) { ResumeGame(); }
        else if (FindAnyObjectByType<GameManager>().gameEnd == false) { PauseGame(); }  
    }

    //for pause screen
    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseScreen.SetActive(true);
        hasControl = false;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1.0f;
        pauseScreen.SetActive(false);
        hasControl = true;
    }

    public void SelectMenu(InputAction.CallbackContext ctx)
    {
        FindAnyObjectByType<TowerSelectMenu>().TowerSelectMenuPopup();
    }
    public void QuestLog(InputAction.CallbackContext ctx)
    {
        FindAnyObjectByType<QuestLogUI>().QuestLogTogglePressed();
    }

    public void AnimatorUpdate()
    {
        animator.SetFloat("Speed", currentSpeed);
    }
}