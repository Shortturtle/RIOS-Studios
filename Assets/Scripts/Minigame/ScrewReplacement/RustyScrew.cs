using UnityEngine;

public class RustyScrew : MonoBehaviour
{
    //gameobjects for which object is being dragged and to which other object
    public GameObject objectToDrag;

    //distance between object dragged and object place position before it can be placed into position
    public float dropDistance;
    public float grav;
    [SerializeField] private bool dropped = false;

    //initial position
    Vector2 objectInitialPos;


    public GameObject objectBelow;
    private Rigidbody2D rb;

    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;

        //set initial pos to object's current pos
        objectInitialPos = objectBelow.transform.position;
        Debug.Log(objectInitialPos);
    }

    public void DragObject()
    {
        if (!dropped) { objectToDrag.transform.position = Input.mousePosition; }
    }
    public void DropObject()
    {
        //check distance between obj and initial
        float Distance = Vector3.Distance(objectToDrag.transform.position, objectBelow.transform.position);
        if (Distance < dropDistance)
        {
            //put back home
            objectToDrag.transform.position = objectInitialPos;
        }
        else
        {
            dropped = true;
            rb.gravityScale = grav;
            objectBelow.GetComponent<DragDrop>().isBlocked = false;
            Destroy(gameObject, 4f);
        }
    }
}
