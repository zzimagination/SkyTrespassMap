using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCube : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float _x = Input.GetAxis("Horizontal");
        float _y = Input.GetAxis("Vertical");

        Vector3 now= transform.localPosition;
        Vector3 target = transform.localPosition + new Vector3(_x, 0, _y);
        transform.Translate(new Vector3(_x, 0, _y)*Time.deltaTime*5);

    }
}
