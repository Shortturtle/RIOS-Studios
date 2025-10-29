using UnityEngine;
using UnityEngine.InputSystem;

public class CameraSystem : MonoBehaviour
{
    //private CameraSystem myControls;   //input system
    //refs
    private CharacterController characterController;
    public GameObject pauseScreen;

    public float moveSpeed;
    public float speed = 5f;
    public float turnRate = 360f;

    float moveDirection = 0f, turnDirection = 0f;

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



        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }
    //public void HorizontalMove(InputAction.CallbackContext ctx)
    //{
    //    Move(ctx.ReadValue<float>());
    //}
    //public void Move()
    //{

    //}

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
