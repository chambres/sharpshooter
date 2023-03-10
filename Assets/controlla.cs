using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controlla : MonoBehaviour
{
    public GameObject keyboardTracker;
    public bool firing= false;

    OSCTestSender oscTestSender;
    void Awake(){
        print("Application.dataPath = " + Application.dataPath);
        var p = new System.Diagnostics.Process();
        p.StartInfo.FileName = Application.dataPath + "/GlovePIE-0.46/PieFREE.exe";
        p.StartInfo.Arguments = Application.dataPath + "/GlovePIE-0.46/MAIN.PIE";
        p.Start();
        //p.StandardOutput.ReadToEnd().Dump();
    }
    // Start is called before the first frame update
    void Start()
    {
        oscTestSender = keyboardTracker.GetComponent<OSCTestSender>();
        StartCoroutine(waiter());

    }

    IEnumerator waiter()
     {
         int wait_time = Random.Range (0, 5);
         yield return new WaitForSeconds (wait_time);         
         firing= true;
         Debug.Log("FIRE");         
     }


    void checkFiring(){
        if(firing){   
            if(oscTestSender.getkb2("space") && firing){
                Debug.Log("kb2firedfirst");
                firing = false;
            };

            if(oscTestSender.getkb1("space") && firing){
                Debug.Log("kb1firedfirst");
                firing = false;
            };
        }
    }
    // Update is called once per frame
    void Update()
    {
        checkFiring();
            


    }
}
