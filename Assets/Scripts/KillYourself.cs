using UnityEngine;
using System.Collections;

public class KillYourself : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(KYS());
    }

    private IEnumerator KYS()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
