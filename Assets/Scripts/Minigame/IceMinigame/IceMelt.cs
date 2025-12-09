using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CircleCollider2D))]
public class IceMelt : MonoBehaviour
{
    public float meltSpeed = 1.0f;
    private float meltCount;
    private CircleCollider2D circle;
    public GameObject sprite;
    private Image iceImage;
    public Sprite ice_Normal;
    public Sprite ice_Half;
    public Sprite ice_Melt;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        circle = GetComponent<CircleCollider2D>();
        iceImage = sprite.GetComponent<Image>();
    }

    private void Update()
    {
        SetTexture();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<MinigameFire>())
        {
            Melt();
        }
    }

    private void SetTexture()
    {
        if (sprite.transform.localScale.x > 0.8)
        {
            iceImage.sprite = ice_Normal;
        }

        else if (sprite.transform.localScale.x > 0.6)
        {
            iceImage.sprite = ice_Half;
        }

        else
        {
            iceImage.sprite = ice_Melt;
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
