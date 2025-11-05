using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]                                                                               //Ensure that a Rigidbody component is attached to the GameObject

public class TimeBody : MonoBehaviour
{

    private bool isRewinding = false;
    public float recordTime = 5f;                                                                                   //How many seconds back in time we can rewind

    List<PointInTime> pointsInTime;                                                                                 //store positions overtime for rewinding

    Rigidbody rb;

    void Start()
    {
        pointsInTime = new List<PointInTime>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T)) StartRewind();
    }

    private void FixedUpdate()
    {
        if (isRewinding) Rewind();
        else Record();                                                                                              //Add positions to the list
    }

    void Rewind()
    {
        if (pointsInTime.Count > 0)
        {
            PointInTime pointInTime = pointsInTime[0];                                                              //Get the first element in the list

            transform.position = pointInTime.position;
            transform.rotation = pointInTime.rotation;

            pointsInTime.RemoveAt(0);                                                                               //Remove the first element in the list
        }
        else
        {
            StopRewind();
        }
    }

    void Record()
    {
        //Check: Do we have more points in time than we would get in 5s? If yes, then start overwriting the oldest points
        if (pointsInTime.Count > Mathf.Round(recordTime / Time.fixedDeltaTime/*get the time between each fixedUpdate*/))
        {
            pointsInTime.RemoveAt(pointsInTime.Count - 1);                                                          //Remove the oldest point in time (elements at the BOTTOM of the list)
        }

        pointsInTime.Insert(0, new PointInTime(transform.position, transform.rotation));                            //Add values(current position) to the START/TOP of the list so
    }

    public void StartRewind()
    {
        isRewinding = true;
        Debug.Log("Rewinding started");

    }

    public void StopRewind()
    {
        isRewinding = false;
        Debug.Log("Rewinding stopped");

    }
}
