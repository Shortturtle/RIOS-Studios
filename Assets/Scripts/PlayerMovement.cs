using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    
    
    private InputSystem_Actions myControls;
    private Vector3 playerInput;


    private void Awake()
    {
        myControls = new InputSystem_Actions();
    }


    private void OnEnable()
    {
        //myControls.Player.Move.performed += StartMove;
        //myControls.Player.Move.canceled += StopMove;

        myControls.Player.Enable();
    }
    private void OnDisable()
    {
        
        
        myControls.Player.Disable();
    }

    private void Update()
    {
        //GatherInput();
    }

    //private void GatherInput(/*InputAction.CallbackContext ctx*/)
    //{
    //    Vector2 input = myControls.Player.Move.ReadValue<Vector2>();
    //    playerInput = new Vector3(input.x, 0, input.y);

    //    Debug.Log(playerInput);
    //}
}
