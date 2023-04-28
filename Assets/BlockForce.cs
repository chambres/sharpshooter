using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockForce : MonoBehaviour
{
    public GameObject slider;
    public int count = 1;

    public int keyboardNum = 0;

    public LinearIndicator healthBar;


    Rigidbody rb;
    DialogueManager dialogueManager;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
    }
    
    public bool allowMashing = false;
    public bool doneRecording = false;

    
    

    public float healthConversion()
    {
        return (transform.position.y + 3) * 8.33333f;
    }

    // Update is called once per frame
    void Update()
    {

        healthBar.SetValue((transform.position.y + 3) * 8.33333f);

        rb.AddForce(2.45f * rb.mass * Vector3.down);


        if(healthConversion() < 50 && !allowMashing && doneRecording)
        {
            Debug.Log("playing");
            dialogueManager.PlayBeginMashingDialogue();
            allowMashing = true;
        }
       

        if (InputRedirector.getKeyDown(InputRedirector.space, keyboardNum) && allowMashing)
        {
            Debug.Log("pressed");
            if (rb.velocity.y > 0)
            {
                rb.drag = rb.velocity.y;
            }
            else
            {
                rb.drag = 0;
            }
            
            rb.AddForce(2.45f / 3 * 1 / Time.deltaTime * rb.mass * Vector3.up);
            count++;

            if(healthConversion() > 100 && allowMashing)
            {
                Debug.Log("Revive");
                transform.position = new Vector3(transform.position.x, 12, transform.position.z);
                rb.isKinematic = true;
                dialogueManager.PlayReviveDialogue();
                allowMashing = false;
            }
            
        }

        slider.GetComponent<SliderController>().slider.value = (transform.position.y + 3) * 125;

        


    }
}
