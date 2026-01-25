using AK.Wwise;
using UnityEngine;

public class Crank : MonoBehaviour
{
    public GameObject crankHandle;
    public GameObject crankCenter;

    private Vector2 mousePosition;
    private float radius;
    private float rotationPercentage;

    private float currentAngle;
    private float totalAngle;

    public bool isDragStarted;
    public Canvas canvas;
    public JackBoxCrankManager jackBoxCrankManager;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        radius = Mathf.Abs(Vector2.Distance(crankCenter.transform.position, crankHandle.transform.position));
        Debug.Log(radius);
    }

    // Update is called once per frame
    void Update()
    {
        if (isDragStarted)
        {
            CrankingItSoGood();
        }

    }  

    public void CrankingItSoGood()
    {
        mousePosition = Input.mousePosition;

        Vector2 vectorFromCentertoHandle = (Vector2) crankHandle.transform.position - (Vector2) crankCenter.transform.position;
        Vector2 vectorFromCentertoMouse = mousePosition - (Vector2)crankCenter.transform.position;

        if(vectorFromCentertoMouse.magnitude > radius)
        {
            Vector2.ClampMagnitude(vectorFromCentertoMouse, radius);
        }

        currentAngle = Vector2.SignedAngle(vectorFromCentertoHandle, vectorFromCentertoMouse);

        if (currentAngle > 0) { return;  }

        else
        {
            transform.Rotate(new Vector3 (0, 0, currentAngle));
        }

        totalAngle += currentAngle;
        rotationPercentage = totalAngle / 360;
        jackBoxCrankManager.currentRotations = rotationPercentage;
    }
}
