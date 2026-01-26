using UnityEngine;

public class DragDrop : MonoBehaviour
{
    //gameobjects for which object is being dragged and to which other object
    public GameObject objectToDrag;
    public GameObject objectSlot;

    //check for whether old screw is still on top and whether new screw is alr placed properly
    public bool isLocked;
    private bool canSlot;

    public AK.Wwise.Event screwIn;

    //new screw's initial position
    Vector2 objectInitialPos;
    //the position of the collided slot
    Vector2 objectFinalPos;

    void Start()
    {
        //set initial pos to object's current pos
        objectInitialPos = objectToDrag.transform.position;
        objectFinalPos = objectToDrag.transform.position;
    }

    //collide with available slots
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("ScrewSlot"))
        {
            objectSlot = other.gameObject;
            canSlot = true;
            objectFinalPos = objectSlot.transform.position;
        }
    }
    private void OnTriggerExit2D(Collider2D other) { canSlot = false; }


    public void DragObject()
    {
        //if not prop placed, object can be dragged by the mouse
        if (!isLocked) { objectToDrag.transform.position = Input.mousePosition; }
    }

    public void DropObject()
    {
        if (canSlot == false)
        {
            objectToDrag.transform.position = objectInitialPos;
            return;
        }
        else
        {
            //complete minigame part
            isLocked = true;
            AudioManager.instance.PlayAudioEvent(screwIn, gameObject);
            objectToDrag.transform.position = objectFinalPos;
            Collider2D slotCollider = objectSlot.GetComponent<CircleCollider2D>();
            slotCollider.enabled = false;
            FindAnyObjectByType<ScrewManager>().ProgressCheck();
        }
    }
}
