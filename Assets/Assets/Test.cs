using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(test());
    }
    int b = 0;

    IEnumerator test()
    {
        int a = 0;
        while (true)
        {
            a++;
            Debug.Log("a" + a);
            yield return null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        b++;
        Debug.Log("b"+b);
    }
}
