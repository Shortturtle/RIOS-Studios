using UnityEngine;
using System.Collections;

public class KillYourself : MonoBehaviour
{
    public float timeToLive = 1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(KYS());
    }

    private IEnumerator KYS()
    {
        yield return new WaitForSeconds(timeToLive);
        Destroy(gameObject);
    }
}
