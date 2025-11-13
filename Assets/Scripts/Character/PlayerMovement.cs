using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    //Dialogue stuff
    [SerializeField] private DialogueUI dialogueUI;
    public DialogueUI DialogueUI => dialogueUI;
    public IInteractable Interactable { get; set; }

    [SerializeField] private float maxSpeed = 10f;
    [SerializeField] private float rotationSpeed = 360f;

    [SerializeField] private float accelerationFactor = 5f;
    [SerializeField] private float decelerationFactor = 10f;

    [SerializeField] private float gravity = -9.8f;
    private Vector3 velocity;

    //stun ability stuff
    public float stunRadius = 5f;
    public LayerMask enemyLayer;

    private float currentSpeed;   //speed of player
    public bool hasControl = true;
    private bool towerSelectOpen;

    private InputSystem_Actions myControls;   //input system
    private Vector3 playerInput;   //player input vector number
    private bool pauseInput;

    //refs
    private CharacterController characterController;
    public GameObject pauseScreen;

    private void Awake()
    {
        //For input system
        myControls = new InputSystem_Actions();

        //refs
        characterController = GetComponent<CharacterController>();
    }

    //For input system, enable & disable
    private void OnEnable()
    {
        myControls.Player.Pause.performed += PauseMenu;
        myControls.Player.Select.performed += SelectMenu;
        myControls.Player.Enable();

        
    }
    private void OnDisable()
    {
        myControls.Player.Pause.performed -= PauseMenu;
        myControls.Player.Select.performed -= SelectMenu;
        myControls.Player.Disable();

    }

    private void Update()
    {
        bool isGrounded = characterController.isGrounded;

        if (isGrounded && velocity.y < 0) { velocity.y = -2; }
        if (!isGrounded) { velocity.y = gravity * Time.deltaTime; }

        if (hasControl == true)
        {
            GatherInput();

            Look();
            CalculateSpeed();

            Move();
        }

        //PauseMenu();

        //manual interaction to activate dialogue
        if (Input.GetKeyDown(KeyCode.T))
        {
            Interactable?.Interact(this);        
        }

        //manual stun bubble activation
        if (Input.GetKeyDown(KeyCode.E))
        {
            StunBubble();
        }
    }

    void StunBubble()
    {
        //find colliders within the AoE that are on the enemy layer
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, stunRadius, enemyLayer);
        
        foreach (Collider hit in hitColliders)
        {
            BaseEnemyClass enemy = hit.GetComponent<BaseEnemyClass>();
            if (enemy != null)
            {
                enemy.Stun();
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, stunRadius);
    }

    //player movement
    private void Move()
    {
        Vector3 moveDirection = transform.forward * currentSpeed * Time.deltaTime/* * playerInput.magnitude*/ + velocity;
        characterController.Move(moveDirection);
    }

    //player rotation
    private void Look()
    {
        if (playerInput == Vector3.zero) return;

        Matrix4x4 isometricMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
        Vector3 multipliedMatrix = isometricMatrix.MultiplyPoint3x4(playerInput);

        Quaternion rotation = Quaternion.LookRotation(multipliedMatrix, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotationSpeed/* * Time.deltaTime*/);
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
    private void GatherInput()
    {
        Vector2 input = myControls.Player.Move.ReadValue<Vector2>();
        playerInput = new Vector3(input.x, 0, input.y);
    }

    private void PauseMenu(InputAction.CallbackContext ctx)
    {
        if (Time.timeScale == 0) { ResumeGame(); }
        else { PauseGame(); }        
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
}