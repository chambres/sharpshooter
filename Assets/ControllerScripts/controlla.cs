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
            if(InputRedirector.getKeyDown(space, 2)){
                Debug.Log("kb2firedfirst");
                SNIPERSHOT.Play();
                firing = false;
                beginDegeneration = true;
                checkingfiring = false;
                GameObject.Find("BlockForce").GetComponent<Rigidbody>().isKinematic = false;                
                return;
            }
            else if(InputRedirector.getKeyDown(space, 1)){
                Debug.Log("kb1firedfirst");
                SNIPERSHOT.Play(); 
                firing = false;
                beginDegeneration = true;
                checkingfiring = false;
                return;
            };
            
        }
        else{
           if(InputRedirector.getKeyDown(space, 2)){
                Debug.Log("kb1failedfirst");
                SNIPERSHOT.Play();
                firing = false;
                heartbeat.Stop();
                checkingfiring = false;
            }
            else if(InputRedirector.getKeyDown(space, 1)){
                Debug.Log("kb2failedfirst");
                SNIPERSHOT.Play();
                firing = false;
                heartbeat.Stop();
                checkingfiring = false;
            }; 
        }
    }


    float polyExplosionFrameFromHealth(float health){
        return GameObject.Find("BlockForce").GetComponent<BlockForce>().transform.position.y * 11.1111f;
    }
    
    bool healthBarLower = true;
    IEnumerator subtractHealth(){
        
        if(healthBarLower){
            health = 100;
            StartCoroutine(test());
            healthBarLower = false;
        }
    
        yield return new WaitForSeconds (1f);
        //health = health - .1f;
        Debug.Log("healtH" + polyExplosionFrameFromHealth(health));
        
        //playeronepolymodel.GetComponent<PolyManager>().setSliderValue(polyExplosionFrameFromHealth(health));
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

    

    // Update is called once per frame
    void Update()
    {

        if(checkingfiring){
            checkFiring();
        }

            


        


    }
}
