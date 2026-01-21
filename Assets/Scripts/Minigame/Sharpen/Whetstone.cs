using UnityEngine;

public class Whetstone : MonoBehaviour
{
    [SerializeField] private bool onAxe = false;
    [SerializeField] private bool movingUp = true;
    [SerializeField] private bool movingDown = false;

    [SerializeField]private bool sharpeningUp = false;
    [SerializeField] private bool sharpeningDown = false;

    private float stoneOriSize = 1f;
    private float stoneNewSize = 0.8f;
    private float timeTakenToDown = 1f;

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
            //if player stops being on the blade, show player they failed
            transform.localScale = new Vector3(stoneOriSize, stoneOriSize, stoneOriSize);
            sharpeningDown = false;
            sharpeningUp = false;
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
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

        if (other.CompareTag("AxeBlade"))
        {
            onAxe = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("AxeTop"))
        {
            if (movingDown)
            {
                sharpeningDown = true;
            }
        }
        if (other.CompareTag("AxeBottom"))
        {
            if (movingUp)
            {
                sharpeningUp = true;
            }
        }

        if(other.CompareTag("AxeBlade"))
        {
            onAxe = false;
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
