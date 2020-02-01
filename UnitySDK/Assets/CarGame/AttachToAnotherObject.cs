using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachToAnotherObject : MonoBehaviour
{

    public Transform otherObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = otherObject.position;
    }
}
