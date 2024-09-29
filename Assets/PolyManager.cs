using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;
public class PolyManager : MonoBehaviour
{
    public GameObject blockForce;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void setSliderValue(int value){
        foreach (Transform child in this.transform)
        {   
            child.gameObject.GetComponent<TimeBody>().sliderValue = (int)Mathf.Lerp(child.gameObject.GetComponent<TimeBody>().sliderValue, value, .3f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
