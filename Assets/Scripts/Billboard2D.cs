using UnityEngine;

public class Billboard2D : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        LookAtCamera();
    }

    void LookAtCamera()
    {
        transform.LookAt(Camera.main.transform);
    }
}
