using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class TimeBody : MonoBehaviour {

	bool isRewinding = false;

	public float recordTime;

	public List<PointInTime> pointsInTime;

	Rigidbody rb;

	// Use this for initialization
	void Start () { 
		pointsInTime = new List<PointInTime>();
		rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
	}

	//add slider
	[Range(0, 3499)]
	public int sliderValue = 0;

	void FixedUpdate ()
	{
		if(timerRunning){
         tenSec -= Time.smoothDeltaTime;
             if(tenSec >= 0){
				Record();
             }else{
				Debug.Log("Count: "  +pointsInTime.Count);
                 Debug.Log("Done");
				 this.GetComponent<Rigidbody>().isKinematic = true;
                 timerRunning = false;
             }
         }

		try
		{
			PointInTime pointInTime = pointsInTime[sliderValue];
			transform.position = pointInTime.position;
			transform.rotation = pointInTime.rotation;
		}
        catch (ArgumentOutOfRangeException e)
        {
			;
        }
            
			
	}

	 public float tenSec;
     public bool timerRunning = true;
	 int i;
     int ij;
	
	void Rewind ()
	{
		if (ij < pointsInTime.Count)
		{
			PointInTime pointInTime = pointsInTime[ij];
            transform.position = pointInTime.position;
			transform.rotation = pointInTime.rotation;
			ij++;
			//pointsInTime.RemoveAt(0);
		} else
		{
			StopRewind();
		}
		
	}

	void Record ()
	{


		pointsInTime.Insert(0, new PointInTime(transform.position, transform.rotation));
	}

	public void StartRewind ()
	{
		isRewinding = true;
		rb.isKinematic = true;
	}

	public void StopRewind ()
	{
		isRewinding = false;
		rb.isKinematic = false;
	}
}
