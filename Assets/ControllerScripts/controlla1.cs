using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controlla1 : MonoBehaviour
{
    public GameObject keyboardTracker;
    public bool firing= false;

    public AudioSource heartbeat;
    public AudioSource FIRE;
    public AudioSource SNIPERSHOT;
    bool beginDegeneration = false;
    
    int space = 32;

    public float health = 100f;

    private char[] sequence = new char[]{
        'q',
        'w'
    };

    private int sequenceIndex;


    // Start is called before the first frame update
    void Start()
    {

    }


    bool checkingfiring = true;



    // Update is called once per frame
    void Update()
    {


        

            Debug.Log((InputRedirector.getkb1('q')));

            if (InputRedirector.getkb1(sequence[sequenceIndex])) {
                if (++sequenceIndex == sequence.Length){
                    sequenceIndex = 0;
                    Debug.Log("kb1");
                    health += 5;
                }
            } else if (Input.anyKeyDown) sequenceIndex = 0;

            if (InputRedirector.getkb2(sequence[sequenceIndex])) {
                if (++sequenceIndex == sequence.Length){
                    sequenceIndex = 0;
                    Debug.Log("kb2");
                    health += 5;
                }
            } else if (Input.anyKeyDown) sequenceIndex = 0;
        
            


        


    }
}
