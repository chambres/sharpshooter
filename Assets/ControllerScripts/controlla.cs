using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class controlla : MonoBehaviour
{


    public LinearIndicator healthBar;

    public bool firing= false;

    public AudioSource heartbeat;
    public AudioSource beat1;
    public AudioSource beat2;
    public AudioSource FIRE;
    public AudioSource SNIPERSHOT;
    bool beginDegeneration = false;

    public float beattime;
    
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
        healthBar.SetValue(health);
        StartCoroutine(waiter());
        StartCoroutine(beat());
        

    }


    IEnumerator beat(){
        while(true){
            yield return new WaitForSeconds (beattime);
            if(firing){
                FIRE.Play();
                StopCoroutine(beat());
                break;
            }
            beat1.Play();
            yield return new WaitForSeconds (beattime*3);
            beat2.Play();
            beattime -= .005f;
        }
    }

    IEnumerator waiter()
     {
         int wait_time = UnityEngine.Random.Range (6, 11);
         yield return new WaitForSeconds (wait_time);
         firing= true;
         Debug.Log("FIRE");         
         //FIRE.Play();
     }

     IEnumerator test(){
        int x = 100;
        while(true){
            healthBar.SetValue(health);
            yield return new WaitForSeconds (.1f);
            x--;
        }
     }


    bool checkingfiring = true;

    public GameObject playeronepolymodel;
    


    void checkFiring(){

        if(firing){   
            if(InputRedirector.getkb2(space)){
                Debug.Log("kb2firedfirst");
                SNIPERSHOT.Play();
                firing = false;
                beginDegeneration = true;
                checkingfiring = false;
                
                foreach (Transform child in playeronepolymodel.transform)
                {
                    child.gameObject.GetComponent<MeshRenderer>().enabled = true;
                    child.gameObject.GetComponent<Rigidbody>().useGravity = false;
                    
                }
                

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


    int polyExplosionFrameFromHealth(float health){
        return (int) (health * 35);
    }
    
    bool healthBarLower = true;
    IEnumerator subtractHealth(){
        
        if(healthBarLower){
            health = 50;
            StartCoroutine(test());
            healthBarLower = false;
        }
    
        yield return new WaitForSeconds (1f);
        health = health - .1f;
        Debug.Log("healtH" + polyExplosionFrameFromHealth(health));
        playeronepolymodel.GetComponent<PolyManager>().setSliderValue(polyExplosionFrameFromHealth(health));
        if(health <= 0){
            Debug.Log("DEAD");
            beginDegeneration = false;
            StopCoroutine(subtractHealth());
        }
        if(health >= 100){
            beginDegeneration = false;
            StopCoroutine(subtractHealth());
            Debug.Log("REVIVE");
        }
        
    }

    float timeOfLastPress;

    public bool IHaveTapped { get; private set; }

    void checkMashing(){
         StartCoroutine(subtractHealth());
            if (InputRedirector.getkb1(sequence[sequenceIndex])) {
                
                if (++sequenceIndex == sequence.Length){
                    sequenceIndex = 0;
                    Debug.Log("kb1");
                    health += .7f;
                }
            } else if (Input.anyKeyDown) sequenceIndex = 0;

            if (InputRedirector.getkb2(sequence[sequenceIndex])) {
                if (++sequenceIndex == sequence.Length){
                    sequenceIndex = 0;
                    Debug.Log("kb2");
                    health += .7f;
                }
            } else if (Input.anyKeyDown) sequenceIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {

        if(checkingfiring){
            checkFiring();
        }
        

        if(beginDegeneration){
           checkMashing();
        }
            


        


    }
}
