using UnityEngine;

public class Whetstone : MonoBehaviour
{
    private bool onAxe = false;
    private bool movingUp = true;
    private bool movingDown = false;

    private bool sharpeningUp = false;
    private bool sharpeningDown = false;

    [SerializeField] private float stoneOriSize;
    [SerializeField] private float stoneNewSize;
    [SerializeField] private float timeTakenToDown;

    private SharpenManager sharpenManager;
    private void Start()
    {
        sharpenManager = GetComponentInParent<SharpenManager>();
    }

    private void Update()
    {
        transform.position = Input.mousePosition;

        if (!onAxe)
        {
            //if player stops being on the blade, tell player they failed
            transform.LeanScale(new Vector3(stoneOriSize, stoneOriSize), timeTakenToDown).setEaseInOutCirc();
            sharpeningDown = false;
            sharpeningUp = false;
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("AxeBottom"))
        {
            transform.LeanScale(new Vector3(stoneNewSize, stoneNewSize), timeTakenToDown).setEaseInOutCirc();
            if (sharpeningDown)
            {
                movingDown = false;
                movingUp = true;
                Debug.Log("axebottom");
                sharpenManager.ProgressCheck();
            }
        }

        if (other.CompareTag("AxeTop"))
        {
            transform.LeanScale(new Vector3(stoneNewSize, stoneNewSize), timeTakenToDown).setEaseInOutCirc();
            if (sharpeningUp)
            {
                movingUp = false;
                movingDown = true;
                Debug.Log("axeup");
                sharpenManager.ProgressCheck();
            }
        }
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("AxeTop"))
        {
            if (movingDown)
            {
                Debug.Log("sharpeningdown");
                sharpeningDown = true;
            }
        }
        if (other.CompareTag("AxeBottom"))
        {
            if (movingUp)
            {
                Debug.Log("sharpeningup");
                sharpeningUp = true;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("AxeBlade"))
        {
            onAxe = true;
        }
    }

}
