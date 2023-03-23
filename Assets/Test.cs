using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(GetWindowHandle());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [System.Runtime.InteropServices.DllImport("user32.dll")]
    private static extern System.IntPtr GetActiveWindow();
    
    public static System.IntPtr GetWindowHandle() {
    return GetActiveWindow();
    }
}
