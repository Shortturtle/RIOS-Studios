using UnityEngine;

public class BuildingSystem : MonoBehaviour
{
    public GameObject tower;
    public LayerMask groundLayerMask;
    private Vector3 currentPlacement;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Raycast();

        if (Input.GetMouseButtonDown(0))
        {
            PlaceObject();
        }
    }
    void Raycast()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        Ray ray = Camera.main.ScreenPointToRay(mousePos);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, 100, groundLayerMask))
        {
            currentPlacement = hitInfo.point;
        }

        else
        {
            currentPlacement = new Vector3(999,999,999);
        }
    }

    void PlaceObject()
    {
        Vector3 placementPosition = currentPlacement;

        Instantiate(tower, placementPosition, Quaternion.identity);
    }
}
