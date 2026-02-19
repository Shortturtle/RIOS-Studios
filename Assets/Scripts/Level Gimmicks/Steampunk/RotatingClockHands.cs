
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingClockHands : MonoBehaviour
{
    [Header("Spin Settings")]                   //This is literally just for aesthetic purposes
    public int spinRounds = 3;                  //Full rotations before pointing
    public float spinSpeed;                     //Degrees per second

    [Header("Target Settings")]
    public List<EnemySpawnBase> spawnBases = new List<EnemySpawnBase>();
    public float pointSpeed;

    public Vector3 modelRotationOffset;         //Adjust the model's rotation bc the pivot is offset by -90

    private Coroutine rotateRoutine;
    private EnemySpawnBase selectedBase;

    public void Update()
    {
        //For testing purposes
        if (Input.GetKeyDown(KeyCode.P))
        {
            SpinPickAndPoint();
            Debug.Log("Clock Hand Pointing to: " + selectedBase.name);
        }
    }

    //Called by the Wave Manager at wave end
    public EnemySpawnBase SpinPickAndPoint()
    {
        if (spawnBases.Count == 0)
        {
            Debug.LogError("No spawn bases assigned to Clock Hands!");
            return null;
        }

        //Pick a random base
        selectedBase = spawnBases[Random.Range(0, spawnBases.Count)];

        //Start the spin animation
        if (rotateRoutine != null)
        { StopCoroutine(rotateRoutine); }

        rotateRoutine = StartCoroutine(SpinThenPoint(selectedBase.transform));

        //Return the selected base so the WaveManager can use it
        return selectedBase;
    }

    IEnumerator SpinThenPoint(Transform target)
    {
        //Spin
        float totalSpin = 360f * spinRounds;
        float spun = 0f; //How much we've spun so far

        while (spun < totalSpin)
        {
            float delta = spinSpeed * Time.deltaTime;
            transform.Rotate(Vector3.up, delta, Space.Self);
            spun += delta;
            yield return null;
        }

        //ThenPoint (this will make it turn the shortest way, which can be counter-clockwise)
        Vector3 direction = target.position - transform.position;   //Get the direction to target(enemy spawn base)
        direction.y = 0f;                                           //lock the y axis so it doesn't tilt

        Debug.DrawRay(transform.position, direction, Color.red, 10f);

        //Point towards the target
        Quaternion targetRotation = Quaternion.LookRotation(direction.normalized, Vector3.up) * Quaternion.Euler(modelRotationOffset);

        //Rotate to smoothly point towards the target
        while (Quaternion.Angle(transform.rotation, targetRotation) > 0.1f)
        {
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                pointSpeed * Time.deltaTime
            );

            yield return null;
        }

        /*/ThenPoint (this forces clockwise rotation only)
        Vector3 direction = target.position - transform.position;   //Get the direction to target(enemy spawn base)
        direction.y = 0f;                                           //lock the y axis so it doesn't tilt

        Debug.DrawRay(transform.position, direction, Color.red, 10f);

        //Point towards the target
        Quaternion targetRotation = Quaternion.LookRotation(direction.normalized, Vector3.up) * Quaternion.Euler(modelRotationOffset);

        //Get current and target y rotation values
        float currentY = transform.eulerAngles.y;
        float targetY = targetRotation.eulerAngles.y;

        //Rotate until aligned to target
        while (Mathf.Abs(Mathf.DeltaAngle(currentY, targetY)) > 0.1f)
        {
            float step = spinSpeed * Time.deltaTime;    //calculate clockwise step
            currentY -= step;                           //force clockwise rotation

            transform.rotation = Quaternion.Euler(0f, currentY, 0f);

            yield return null;
        }*/

        //Ensure exact alignment at the end
        transform.rotation = targetRotation;
    }
}