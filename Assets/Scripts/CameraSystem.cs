using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Cinemachine;

public class CameraSystem : MonoBehaviour
{
    private CharacterController characterController;
    public CinemachineCamera cinemachineCam;
    public GameObject pauseScreen;

    public float moveSpeed;
    public float rotateSpeed;
    [SerializeField] private float fovMax = 90f;
    [SerializeField] private float fovMin = 10f;

    float moveForwardDirection = 0f;
    float moveSideDirection = 0f;
    float cameraRotateDirection = 0f;
    float cameraZoomDirection = 0f;
    private float targetFieldOfView = 70f;
    float edgeScrollSize = 20f;
    [SerializeField]private bool useEdgeScroll = false;
    private void Awake()
    {
        //refs
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        HandleMovement();
        if (useEdgeScroll) { HandleMovementEdgeScroll(); }
        HandleRotation();
        HandleZoom();
    }

    private void HandleMovement()
    {
        //Vector for input direction
        Vector3 inputDir = new Vector3(0, 0, 0);

        //to change value into forwards/backwards/no frontal movement
        if (moveForwardDirection > 0) { inputDir.z = +1f; }
        if (moveForwardDirection < 0) { inputDir.z = -1f; }
        if (moveForwardDirection == 0) { inputDir.z = 0f; }
        //to change value into left/right/no side movement
        if (moveSideDirection > 0) { inputDir.x = +1f; }
        if (moveSideDirection < 0) { inputDir.x = -1f; }
        if (moveSideDirection == 0) { inputDir.x = 0f; }

        //Vector for moving camera (mainly for if camera rotates, forwards is still forwards)
        Vector3 moveDir = transform.forward * inputDir.z + transform.right * inputDir.x;

        //Actually moving camera
        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }

    private void HandleMovementEdgeScroll()
    {
        //Vector for input direction
        Vector3 inputDir = new Vector3(0, 0, 0);

        //detection of mouse position for edge scrolling
        if (Input.mousePosition.x < edgeScrollSize) { inputDir.x = -1f; }
        if (Input.mousePosition.x > Screen.width - edgeScrollSize) { inputDir.x = +1f; }
        if (Input.mousePosition.y < edgeScrollSize) { inputDir.z = -1f; }
        if (Input.mousePosition.y > Screen.height - edgeScrollSize) { inputDir.z = +1f; }

        //Vector for moving camera (mainly for if camera rotates, forwards is still forwards)
        Vector3 moveDir = transform.forward * inputDir.z + transform.right * inputDir.x;

        //Actually moving camera
        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }

    private void HandleRotation()
    {
        //Value for rotation direction
        float rotateDir = 0f;

        //to change value into left/right/no side rotation
        if (cameraRotateDirection > 0) { rotateDir = -1f; }
        if (cameraRotateDirection < 0) { rotateDir = +1f; }
        if (cameraRotateDirection == 0) { rotateDir = 0f; }

        //Actually rotating camera
        transform.eulerAngles += new Vector3(0, rotateDir * rotateSpeed * Time.deltaTime, 0);
    }

    private void HandleZoom()
    {
        if (cameraZoomDirection > 0 )
        {
            targetFieldOfView += 5f;
        }
        if (cameraZoomDirection < 0)
        {
            targetFieldOfView -= 5f;
        }

        float zoomSpeed = 10f;

        targetFieldOfView = Mathf.Clamp(targetFieldOfView, fovMin, fovMax);

        

        cinemachineCam.Lens.FieldOfView = Mathf.Lerp(cinemachineCam.Lens.FieldOfView, targetFieldOfView, Time.deltaTime * zoomSpeed);
    }

    //Function for gathering player input and changing it into value for if player is moving forwards or backwards
    public void ForwardMove(InputAction.CallbackContext ctx) { ForwardMove(ctx.ReadValue<float>()); }
    public void ForwardMove(float dir) { moveForwardDirection = dir; }

    //Function for gathering player input and changing it into value for if player is moving left or right
    public void SideMove(InputAction.CallbackContext ctx) { SideMove(ctx.ReadValue<float>()); }
    public void SideMove(float dir) { moveSideDirection = dir; }

    //Function for gathering player input and changing it into value for if player rotates left or right
    public void RotateCamera(InputAction.CallbackContext ctx) { RotateCamera(ctx.ReadValue<float>()); }
    public void RotateCamera(float dir) { cameraRotateDirection = dir; }

    //Function for zooming camera
    public void ZoomCamera(InputAction.CallbackContext ctx) { ZoomCamera(ctx.ReadValue<float>()); }
    public void ZoomCamera(float dir) { cameraZoomDirection = dir; }
}
