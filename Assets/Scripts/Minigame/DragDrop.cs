using UnityEngine;

public class DragDrop : MonoBehaviour
{
    public GameObject objectToDrag;
    public GameObject objectDragToPos;

    public float dropDistance;

    public bool isLocked;

    Vector2 objectInitialPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        objectInitialPos = objectDragToPos.transform.position;
    }

    public void DragObject()
    {
        if(!isLocked)
        {
            objectToDrag.transform.position = Input.mousePosition;
        }
    }

    public void DropObject()
    {
        float Distance = Vector3.Distance(objectToDrag.transform.position, objectDragToPos.transform.position);
        if (Distance > dropDistance) 
        {
            isLocked = true;
            objectToDrag.transform.position = objectDragToPos.transform.position;
        }
        else
        {
            objectToDrag.transform.position = objectInitialPos;
        }
    }
}
