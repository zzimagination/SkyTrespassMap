using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testtt : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            GetComponent<Renderer>().sortingOrder = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            GetComponent<Renderer>().sortingOrder = -2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            GetComponent<Renderer>().sortingOrder = 3;
        }
    }
}
