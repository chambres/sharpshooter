using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockForce : MonoBehaviour
{
    public GameObject slider;
    public int count = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    

    // Update is called once per frame
    void Update()
    {
        //if(GetComponent<Rigidbody>().velocity.y > .1f)
        //{
        //    GetComponent<Rigidbody>().AddForce(GetComponent<Rigidbody>().velocity.y * 10 * 2.45f * GetComponent<Rigidbody>().mass * Vector3.down);
        //}
        //else
        //{
            GetComponent<Rigidbody>().AddForce(2.45f * GetComponent<Rigidbody>().mass * Vector3.down);
        //}
            
        

        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("pressed");
            if (GetComponent<Rigidbody>().velocity.y > 0)
            {
                GetComponent<Rigidbody>().drag = GetComponent<Rigidbody>().velocity.y;
            }
            else
            {

                GetComponent<Rigidbody>().drag = 0;

            }
            GetComponent<Rigidbody>().AddForce(2.45f / 4 * 1 / Time.deltaTime * GetComponent<Rigidbody>().mass * Vector3.up);
            count++;
            
        }

        slider.GetComponent<SliderController>().slider.value = (transform.position.y + 3) * 125;


    }
}
