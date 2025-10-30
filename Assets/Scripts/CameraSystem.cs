using UnityEngine;
using UnityEngine.InputSystem;

public class CameraSystem : MonoBehaviour
{
    //private CameraSystem myControls;   //input system
    //refs
    private CharacterController characterController;
    public GameObject pauseScreen;

    public float moveSpeed = 10f;
    public float speed = 5f;
    //public float turnRate = 360f;

    float moveForwardDirection = 0f;
    float moveSideDirection = 0f;

    private void Awake()
    {
        //For input system
        //myControls = new CameraSystem();

        //refs
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        //transform.position += transform.forward * moveDirection * speed * Time.deltaTime;
        //transform.Rotate(0, turnRate * Time.deltaTime * turnDirection, 0);

        Vector3 moveDir = new Vector3(0, 0, 0);

        if(moveForwardDirection > 0)
        {
            Debug.Log("mfd >0");
            moveDir.z = +1f;
        }
        if (moveForwardDirection < 0)
        {
            Debug.Log("mfd <0");
            moveDir.z = -1f;
        }
        if (moveForwardDirection == 0) { moveDir.z = 0f; }

        if (moveSideDirection > 0) { moveDir.x = +1f; }
        if (moveSideDirection < 0) { moveDir.x = -1f; }
        if (moveSideDirection == 0) { moveDir.x = 0f; }

        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }
    public void ForwardMove(InputAction.CallbackContext ctx)
    {
        ForwardMove(ctx.ReadValue<float>());
    }

    public void ForwardMove(float dir)
    {
        moveForwardDirection = dir;
    }

    public void SideMove(InputAction.CallbackContext ctx)
    {
        SideMove(ctx.ReadValue<float>());
    }

    public void SideMove(float dir)
    {
        moveSideDirection = dir;
    }

    //public void Move(InputAction.CallbackContext ctx)
    //{
    //    Move(ctx.ReadValue<float>());
    //}

    //public void Move(float dir)
    //{
    //    moveDirection = dir;
    //}

    //public void Turn(float dir)
    //{
    //    turnDirection = dir;
    //}
    //public void Turn(InputAction.CallbackContext ctx)
    //{
    //    Turn(ctx.ReadValue<float>());
    //}
}
