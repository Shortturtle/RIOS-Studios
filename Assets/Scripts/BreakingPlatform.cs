using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]

public class BreakingPlatform : MonoBehaviour
{
    [Header("Tower Detection Stuff")]
    public LayerMask towerLayer;
    public int towerLimit = 3;
    public int towersDetected;

    [Header("Shake Settings")]
    public float shakeIntensity = 0.1f;
    public float shakeSpeed = 30f;

    private Vector3 originalPosition;
    private Coroutine shakeRoutine;

    private void Start()
    {
        //Store the platforms OG postion to call later
        originalPosition = transform.localPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Check: is the object entering on the tower layer?
        if (((1 << other.gameObject.layer) & towerLayer) != 0)
        {
            towersDetected++;

            if (towersDetected == towerLimit - 1)
            {
                if (shakeRoutine == null) shakeRoutine = StartCoroutine(ShakePlatform());
            }

            //Check: tower limit reached?
            if (towersDetected >= towerLimit)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Check: is the object leaving on the tower layer? If yes, reduce the count
        if (((1 << other.gameObject.layer) & towerLayer) != 0)
        {
            towersDetected = Mathf.Max(0, towersDetected - 1); //Ensure it doesn't go below 0

            //Stop shaking if below the shake threshold
            if (towersDetected < towerLimit - 1 && shakeRoutine != null)
            {
                StopCoroutine(shakeRoutine);
                shakeRoutine = null;
                transform.localPosition = originalPosition;
            }
        }
    }

    private IEnumerator ShakePlatform()
    {
        while (true) //keep shaking while the coroutine is running
        {
            //Calculate shake offset using sine and cosine for smoother motion
            float x = Mathf.Sin(Time.time * shakeSpeed) * shakeIntensity;
            float z = Mathf.Cos(Time.time * shakeSpeed) * shakeIntensity;

            Vector3 shakeOffset = new Vector3(x, 0f, z);

            //Apply the shake offset to create the shake effect
            transform.localPosition = originalPosition + shakeOffset;
            yield return null;
        }
    }
}