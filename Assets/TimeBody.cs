using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBody : MonoBehaviour {

	bool isRewinding = false;

	public float recordTime = 5f;

	public List<PointInTime> pointsInTime;

	Rigidbody rb;

	// Use this for initialization
	void Start () {
		pointsInTime = new List<PointInTime>();
		rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Return))
			StartRewind();
		if (Input.GetKeyUp(KeyCode.Return))
			StopRewind();
	}

	//add slider
	[Range(0, 149)]
	public int sliderValue = 0;

	void FixedUpdate ()
	{
		if (isRewinding)
			Rewind();
		
		
		
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

		PointInTime pointInTime = pointsInTime[sliderValue];
		transform.position = pointInTime.position;
		transform.rotation = pointInTime.rotation;
			
	}

	 public float tenSec = 5;
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
		if (pointsInTime.Count > Mathf.Round(recordTime / Time.fixedDeltaTime))
		{
			pointsInTime.RemoveAt(pointsInTime.Count - 1);
		}

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
