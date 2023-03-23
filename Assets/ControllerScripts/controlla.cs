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
        Debug.Log(heartbeat);
        heartbeat.Play();
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


    bool checkingfiring = true;


    void checkFiring(){

        if(firing){   
            if(InputRedirector.getkb2(space)){
                Debug.Log("kb2firedfirst");
                SNIPERSHOT.Play();
                firing = false;
                beginDegeneration = true;
                checkingfiring = false;
                return;
            }
            else if(InputRedirector.getkb1(space)){
                Debug.Log("kb1firedfirst");
                SNIPERSHOT.Play(); 
                firing = false;
                beginDegeneration = true;
                checkingfiring = false;
                return;
            };
            
        }
        else{
           if(InputRedirector.getkb2(space)){
                Debug.Log("kb1failedfirst");
                SNIPERSHOT.Play();
                firing = false;
                heartbeat.Stop();
                checkingfiring = false;
            }
            else if(InputRedirector.getkb1(space)){
                Debug.Log("kb2failedfirst");
                SNIPERSHOT.Play();
                firing = false;
                heartbeat.Stop();
                checkingfiring = false;
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

        if(checkingfiring){
            checkFiring();
        }
        

        if(beginDegeneration){
            StartCoroutine(subtractHealth());
            Debug.Log(sequence[sequenceIndex]);

            Debug.Log(InputRedirector.getkb1(sequence[sequenceIndex]));

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
}
