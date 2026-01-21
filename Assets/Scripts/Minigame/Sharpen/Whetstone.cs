using UnityEngine;

public class Whetstone : MonoBehaviour
{
    //for tracking the axe movement
    private bool onAxe = false;
    private bool movingUp = true;
    private bool movingDown = false;

    private bool sharpeningUp = false;
    private bool sharpeningDown = false;

    //to edit scale
    private float stoneOriSize = 1f;
    private float stoneNewSize = 0.8f;

    //ref manager
    private SharpenManager sharpenManager;
    private void Start() { sharpenManager = GetComponentInParent<SharpenManager>(); }

    private void Update()
    {
        //set its posisiton to mouse
        transform.position = Input.mousePosition;

        if (!onAxe)
        {
            //if player stops being on the blade, show player they failed
            transform.localScale = new Vector3(stoneOriSize, stoneOriSize, stoneOriSize);
            sharpeningDown = false;
            sharpeningUp = false;
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        //when player enters either axe top/bottom, and sharpening up/down is kept, set movingUp/Down to the other one and call progresscheck
        if (other.CompareTag("AxeBottom"))
        {
            transform.localScale = new Vector3(stoneNewSize, stoneNewSize, stoneNewSize);
            
            if (sharpeningDown)
            {
                sharpeningDown = false;
                movingDown = false;
                movingUp = true;
                sharpenManager.ProgressCheck();
            }
        }

        if (other.CompareTag("AxeTop"))
        {
            transform.localScale = new Vector3(stoneNewSize, stoneNewSize, stoneNewSize);
            if (sharpeningUp)
            {
                sharpeningUp = false;
                movingUp = false;
                movingDown = true;
                sharpenManager.ProgressCheck();
            }
        }

        //onAxe true when mouse in axeblade
        if (other.CompareTag("AxeBlade")) { onAxe = true; }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        //checks what movement player shoudl be doing [sharpen up or down], then starts the sharpening
        if (other.CompareTag("AxeTop"))
        {
            if (movingDown) { sharpeningDown = true; }
        }
        if (other.CompareTag("AxeBottom"))
        {
            if (movingUp) { sharpeningUp = true; }
        }

        if (other.CompareTag("AxeBlade")) { onAxe = false; } //if exit the axe blade, onAxe false
    }

    private void OnTriggerStay2D(Collider2D other) { if (other.CompareTag("AxeBlade")) { onAxe = true; } }  //if in axe blade range, onAxe is true
}
