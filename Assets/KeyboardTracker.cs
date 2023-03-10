using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class KeyboardTracker : MonoBehaviour
{
    List<Keyboard> KeyboardPool = new List<Keyboard>();
    
    public void PoolKeyboards()
        {
            var t = InputSystem.devices;
            Debug.Log(t[1]);
            foreach (var r in t)
            {
                Debug.Log(r);
                if (r.name.Contains("Keyboard"))
                {
                        Debug.Log("Found Keyboard: " + r.name);
                        KeyboardPool.Add(r as Keyboard);
                        //KeyboardPool[y] = r as Keyboard;
                }
            }
            
        }
    
    //Check states on Update
    void Start()
    {
        PoolKeyboards();
    }
    void Update()
        {
            for (int i = 0; i < InputSystem.devices.Count; i++)
            {
                if (InputSystem.devices[i].name.Contains("Keyboard"))
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        Debug.Log("Input from keyboard: " + InputSystem.devices[i].name + i);
                    }
                }
            }
        }
}
