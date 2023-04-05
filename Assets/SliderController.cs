using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{

    public Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        
        slider.minValue = 0;
        slider.maxValue = GameObject.Find("BASE_Low Poly Man.001").GetComponent<TimeBody>().pointsInTime.Count-1;
        Debug.Log(slider.maxValue);

        GameObject.Find("Low Poly SuperHero").GetComponent<PolyManager>().setSliderValue((int)slider.value);

    }
}
