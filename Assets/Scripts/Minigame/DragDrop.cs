using UnityEngine;

public class DragDrop : MonoBehaviour
{
    //gameobjects for which object is being dragged and to which other object
    public GameObject objectToDrag;
    public GameObject objectDragToPos;
    
    //distance between object dragged and object place position before it can be placed into position (i just made it more confusing didnt i)
    public float dropDistance;

    //check for whether old screw is still on top and whether new screw is alr placed properly
    public bool isBlocked = true;
    public bool isLocked;

    //new screw's initial position
    Vector2 objectInitialPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //set initial pos to object's current pos
        objectInitialPos = objectToDrag.transform.position;
    }

    public void DragObject()
    {
        //if not prop placed, object can be dragged by the mouse
        if(!isLocked)
        {
            objectToDrag.transform.position = Input.mousePosition;
        }
    }

    public void DropObject()
    {
        //check distance between obj and placement
        float Distance = Vector3.Distance(objectToDrag.transform.position, objectDragToPos.transform.position);
        //if placement pos blocked by old screw, no placy new one
        if(isBlocked == true) { return; }
        if (Distance < dropDistance) 
        {
            //complete minigame part
            isLocked = true;
            objectToDrag.transform.position = objectDragToPos.transform.position;
            FindAnyObjectByType<SPMGManager>().ProgressCheck();
        }
        else
        {
            //put back home
            objectToDrag.transform.position = objectInitialPos;
        }
    }
}
