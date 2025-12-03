using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class IceMelt : MonoBehaviour
{
    public float meltSpeed = 1.0f;
    private float meltCount;
    private CircleCollider2D circle;
    public GameObject sprite;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        circle = GetComponent<CircleCollider2D>();
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<MinigameFire>())
        {
            Melt();
        }
    }

    public void Melt()
    {
        sprite.transform.localScale = Vector3.one * (1 - (meltCount/100));

        meltCount += meltSpeed;

        if (sprite.transform.localScale.x <= 0.2)
        {
            FindFirstObjectByType<IceMGManager>().IceMelted();
            Destroy(gameObject);
        }
    }
}
