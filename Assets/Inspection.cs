using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inspection : MonoBehaviour
{
    // Start is called before the first frame update
    public bool[] kb1;
    public bool[] kb2;
    void Start()
    {
        kb1 = InputRedirector.kb1;
        kb2 = InputRedirector.kb2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
