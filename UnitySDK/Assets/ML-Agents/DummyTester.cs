using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyTester : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // transform.
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, 5*Time.deltaTime);
    }
}
