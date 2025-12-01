using Unity.Cinemachine;
using UnityEngine;

public class MinigameFire : MonoBehaviour
{
    public GameObject minigameFire;


    // Update is called once per frame
    void Update()
    {
        minigameFire.transform.position = Input.mousePosition;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        FindFirstObjectByType<IceMGManager>().IceMelted();
        Destroy(other.gameObject);
    }
}
