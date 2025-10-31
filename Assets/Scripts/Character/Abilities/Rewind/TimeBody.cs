using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))] //Ensure that a Rigidbody component is attached to the GameObject

public class TimeBody : MonoBehaviour
{

    public bool isRewinding = false;

    List<PointInTime> pointsInTime;                           //store positions overtime for rewinding

    Rigidbody rb;

    void Start()
    {
        pointsInTime = new List<PointInTime>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T)) StartRewind();
        //if(Input.GetKeyUp(KeyCode.T)) StopRewind();
    }

    private void FixedUpdate()
    {
        if (isRewinding) Rewind();
        else Record();                                        //Add positions to the list
    }

    void Rewind()
    {
        if (pointsInTime.Count > 0)
        {
            PointInTime pointInTime = pointsInTime[0];        //Get the first element in the list

            transform.position = pointInTime.position;
            transform.rotation = pointInTime.rotation;

            pointsInTime.RemoveAt(0);                         //Remove the first element in the list
        }
        else
        {
            StopRewind();
        }
    }

    void Record()
    {
        pointsInTime.Insert(0, new PointInTime(transform.position, transform.rotation));           //Add values(current position) to the START/TOP of the list so
    }

    public void StartRewind()
    {
        isRewinding = true;
        Debug.Log("Rewinding started");

        rb.isKinematic = true;                                  //Disable physics while rewinding
    }

    public void StopRewind()
    {
        isRewinding = false;
        Debug.Log("Rewinding stopped");

        rb.isKinematic = false;                                 //Enable physics when not rewinding
    }
}
