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
    
    public bool allowMashing = false;

    // Update is called once per frame
    void Update()
    {


        GetComponent<Rigidbody>().AddForce(2.45f * GetComponent<Rigidbody>().mass * Vector3.down);


        if(transform.position.y < -2.6f && !allowMashing)
        {
            allowMashing = true;
        }
       

        if (InputRedirector.getKeyDown(InputRedirector.space,1) && allowMashing)
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
            GetComponent<Rigidbody>().AddForce(2.45f / 3 * 1 / Time.deltaTime * GetComponent<Rigidbody>().mass * Vector3.up);
            count++;
            
        }

        slider.GetComponent<SliderController>().slider.value = (transform.position.y + 3) * 125;


    }
}
