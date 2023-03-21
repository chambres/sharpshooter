using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controlla : MonoBehaviour
{
    public GameObject keyboardTracker;
    public bool firing= false;

    public AudioSource heartbeat;
    public AudioSource FIRE;
    public AudioSource SNIPERSHOT;
    bool beginDegeneration = false;

    public float health = 100f;

    private char[] sequence = new char[]{
        'q',
        'w'
    };

    private int sequenceIndex;
    
    OSCTestSender oscTestSender;
    void Awake(){
        print("Application.dataPath = " + Application.dataPath);
        // var p = new System.Diagnostics.Process();
        // p.StartInfo.FileName = Application.dataPath + "/GlovePIE-0.46/PieFREE.exe";
        // p.StartInfo.Arguments = Application.dataPath + "/GlovePIE-0.46/MAIN.PIE";
        // p.Start();
        //p.StandardOutput.ReadToEnd().Dump();
    }
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(heartbeat);
        heartbeat.Play();
        oscTestSender = keyboardTracker.GetComponent<OSCTestSender>();
        StartCoroutine(waiter());

    }

    IEnumerator waiter()
     {
        
         int wait_time = Random.Range (5, 10);
         yield return new WaitForSeconds (wait_time);         
         firing= true;
         FIRE.Play();
         heartbeat.Stop();
         Debug.Log("FIRE");         
         //FIRE.Play();
     }


    void checkFiring(){
        if(firing){   
            if(oscTestSender.getkb2("space") && firing){
                Debug.Log("kb2firedfirst");
                SNIPERSHOT.Play();
                firing = false;
                beginDegeneration = true;
            };

            if(oscTestSender.getkb1("space") && firing){
                Debug.Log("kb1firedfirst");
                SNIPERSHOT.Play(); 
                firing = false;
                beginDegeneration = true;
            };
            
        }
        else{
           if(oscTestSender.getkb2("space")){
                Debug.Log("kb1failedfirst");
                SNIPERSHOT.Play();
                firing = false;
                heartbeat.Stop();
            };

            if(oscTestSender.getkb1("space")){
                Debug.Log("kb2failedfirst");
                SNIPERSHOT.Play();
                firing = false;
                heartbeat.Stop();
            }; 
            
        }

    }
    
    
    IEnumerator subtractHealth(){
        while(true){
            yield return new WaitForSeconds (1f);
            health = health - .1f;
            if(health <= 0){
                Debug.Log("DEAD");
                beginDegeneration = false;
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

        checkFiring();

        if(beginDegeneration){
            StartCoroutine(subtractHealth());
            if (oscTestSender.getkb1(sequence[sequenceIndex])) {
                if (++sequenceIndex == sequence.Length){
                    sequenceIndex = 0;
                    Debug.Log("kb1");
                    health += 5;
                }
            } else if (Input.anyKeyDown) sequenceIndex = 0;

            if (oscTestSender.getkb2(sequence[sequenceIndex])) {
                if (++sequenceIndex == sequence.Length){
                    sequenceIndex = 0;
                    Debug.Log("kb2");
                    health += 5;
                }
            } else if (Input.anyKeyDown) sequenceIndex = 0;
        }
            


        


    }
}
